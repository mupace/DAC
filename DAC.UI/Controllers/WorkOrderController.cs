using DAC.Models.DTOs;
using DAC.Models.ResponseModels;
using DAC.UI.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DAC.UI.Controllers;

[Authorize]
public class WorkOrderController : Controller
{
    private const string _tokenCacheKey = "_apiToken";

    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<WorkOrderApiSettings> _workOrderApiSettings;

    public WorkOrderController(IOptions<WorkOrderApiSettings> workOrderApiSettings, IMemoryCache memoryCache)
    {
        _workOrderApiSettings = workOrderApiSettings;
        _memoryCache = memoryCache;
    }

    public async Task<IActionResult> Index()
    {
        await AuthWorkOrderApi();

        var workOrders = await GetPagedWorkOrders();

        return View(workOrders);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        await AuthWorkOrderApi();

        var order = await GetWorkOrder(id);

        if (order == null) return View("Error");


        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(WorkOrderDTO model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await AuthWorkOrderApi();

        var order = await UpdateWorkOrder(model);

        if (order == null) return View("Error");

        return View(order);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        return RedirectToAction("Index");
    }

    private async Task AuthWorkOrderApi()
    {
        if (_memoryCache.TryGetValue(_tokenCacheKey, out string existingToken))
        {
            return;
        }

        using (var client = new RestClient(_workOrderApiSettings.Value.ApiUrl))
        {
            var request = new RestRequest(_workOrderApiSettings.Value.Endpoints.Login);
            request.AddJsonBody(JsonConvert.SerializeObject(_workOrderApiSettings.Value.ApiKey));

            var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<JObject>(response.Content).GetValue("token").ToString();
                _memoryCache.Set(_tokenCacheKey, token, TimeSpan.FromSeconds(60));
            }
        }
    }

    private async Task<PagedWorkOrdersResponseModel> GetPagedWorkOrders(int pageSize = 50, int page = 0)
    {
        if (_memoryCache.TryGetValue(_tokenCacheKey, out string token))
        {
            using (var client = new RestClient(_workOrderApiSettings.Value.ApiUrl))
            {
                var request = new RestRequest(_workOrderApiSettings.Value.Endpoints.WorkOrderGetPaged);
                request.AddHeader("Authorization", $"Bearer {token}");

                request.AddParameter("pageSize", pageSize);
                request.AddParameter("page", page);

                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<PagedWorkOrdersResponseModel>(response.Content);
                }
            }
        }

        return new PagedWorkOrdersResponseModel();
    }

    private async Task<WorkOrderDTO> GetWorkOrder(Guid id)
    {
        if (_memoryCache.TryGetValue(_tokenCacheKey, out string token))
        {
            using (var client = new RestClient(_workOrderApiSettings.Value.ApiUrl))
            {
                var request = new RestRequest($"{_workOrderApiSettings.Value.Endpoints.WorkOrderGet}/{id}");
                request.AddHeader("Authorization", $"Bearer {token}");

                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<WorkOrderDTO>(response.Content);
                }
            }
        }

        return null;
    }

    private async Task<WorkOrderDTO> UpdateWorkOrder(WorkOrderDTO model)
    {
        if (_memoryCache.TryGetValue(_tokenCacheKey, out string token))
        {
            using (var client = new RestClient(_workOrderApiSettings.Value.ApiUrl))
            {
                var request = new RestRequest(string.Format(_workOrderApiSettings.Value.Endpoints.WorkOrderUpdate, model.Id));
                request.AddHeader("Authorization", $"Bearer {token}");

                request.AddJsonBody(model);

                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<WorkOrderDTO>(response.Content);
                }
            }
        }

        return null;
    }
}
namespace DAC.UI.Models.Settings;

public class WorkOrderApiSettings
{
    public string ApiUrl { get; set; }

    public string ApiKey { get; set; }

    public Endpoints Endpoints { get; set; }
}

public class Endpoints
{
    public string Login { get; set; }

    public string WorkOrderGet { get; set; }

    public string WorkOrderGetPaged { get; set; }

    public string WorkOrderUpdate { get; set; }
}
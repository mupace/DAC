using DAC.Constants.enums;

namespace DAC.Models;

public class OperationResultModel<T> where T : class
{
    public OperationResultModel(OperationResult result, T data)
    {
        OperationResult = result;
        Data = data;
    }

    public OperationResult OperationResult { get; set; }

    public T Data { get; set; }
}
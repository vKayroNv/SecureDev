namespace CardStorageService.API.Interfaces
{
    public interface IOperationResult
    {
        int ErrorCode { get; }

        string? ErrorMessage { get; }
    }
}

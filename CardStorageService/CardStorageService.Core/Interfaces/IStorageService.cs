namespace CardStorageService.Core.Interfaces
{
    public interface IStorageService<TIn, TOut, TId>
    {
        Task<IReadOnlyList<TOut>> GetAll(CancellationToken cts);

        Task<TOut> GetById(TId id, CancellationToken cts);

        Task<TId> Create(TIn data, CancellationToken cts);

        Task<int> Update(TIn data, CancellationToken cts);

        Task<int> Delete(TId id, CancellationToken cts);
    }
}

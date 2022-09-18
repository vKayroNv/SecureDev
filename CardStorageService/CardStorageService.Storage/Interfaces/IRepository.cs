namespace CardStorageService.Storage.Interfaces
{
    public interface IRepository<T, TId>
    {
        Task<IReadOnlyList<T>> GetAll(CancellationToken cts);

        Task<T> GetById(TId id, CancellationToken cts);

        Task<TId> Create(T data, CancellationToken cts);

        Task<int> Update(T data, CancellationToken cts);

        Task<int> Delete(TId id, CancellationToken cts);
    }
}

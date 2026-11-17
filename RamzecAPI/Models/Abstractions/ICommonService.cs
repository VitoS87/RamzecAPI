namespace RamzecAPI.Models.Abstractions
{
    public interface ICommonService
    {
        public interface ICommonService<T, K>
        {
            bool Create(T model);
            bool Update(T model);
            bool Delete(K id);
            Task<T> Get(K id);
            Task<IEnumerable<T>> GetAll();
        }
    }
}

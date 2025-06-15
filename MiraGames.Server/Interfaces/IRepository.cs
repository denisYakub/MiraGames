namespace MiraGames.Server.Interfaces
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Get(Guid id);
        void Update(T entity);
        void Save();
        void Add(T entity);
        IEnumerable<T> GetAll();
    }
}

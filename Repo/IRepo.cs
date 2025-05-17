namespace GIF_S.Repo
{
    public interface IRepo<T> where T : class
    {
        public void Add(T item);
        public List<T> GetAll();
        public T GetByFilter(Func<T , bool>Get);
    }
}

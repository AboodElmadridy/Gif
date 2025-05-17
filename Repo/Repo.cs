using GIF_S.Model;

namespace GIF_S.Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly GIFContext context;

        public Repo(GIFContext context) {
            this.context = context;
        }

        public void Add(T item)
        {
            context.Add(item);
        }

        public List<T> GetAll()
        {
            return (context.Set<T>().ToList());
        }

        public T GetByFilter(Func<T, bool> Get)
        {
            return (context.Set<T>().Where(Get).FirstOrDefault());
        }
    }
}

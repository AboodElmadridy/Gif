using GIF_S.Model;

namespace GIF_S.Repo
{
    public class SFileRepo : Repo<SFile>, ISFile
    {
        private readonly GIFContext context;

        public SFileRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(SFile SFile)
        {
            var old = context.SFiles.FirstOrDefault(sf => sf.Id == SFile.Id);
            if (old != null)
            {
                old.Name = SFile.Name;
                old.IsCompleted= SFile.IsCompleted;
                old.SecId = SFile.SecId;
                old.Blocked = SFile.Blocked;

            }
        }
    }
}
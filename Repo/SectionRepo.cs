using GIF_S.Model;

namespace GIF_S.Repo
{
    public class SectionRepo : Repo<Section>, ISection
    {
        private readonly GIFContext context;

        public SectionRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Section Section)
        {
            var old = context.Sections.FirstOrDefault(s => s.Id == Section.Id);
            if (old != null)
            {
                old.Title = Section.Title;
                old.Descreption = Section.Descreption;
                old.CrsId = Section.CrsId;
                old.Blocked = Section.Blocked;
            }
        }
    }
}
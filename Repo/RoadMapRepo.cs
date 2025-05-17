using GIF_S.Model;

namespace GIF_S.Repo
{
    public class RoadMapRepo : Repo<RoadMap>, IRoadMap
    {
        private readonly GIFContext context;

        public RoadMapRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(RoadMap RoadMap)
        {
            var old = context.RoadMaps.FirstOrDefault(rm => rm.Id == RoadMap.Id);
            if (old != null)
            {
                old.Title = RoadMap.Title;
                old.Image = RoadMap.Image;
                old.Description = RoadMap.Description;
                old.Blocked = RoadMap.Blocked;

            }
        }
    }
}
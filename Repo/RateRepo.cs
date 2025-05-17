using GIF_S.Model;

namespace GIF_S.Repo
{
    public class RateRepo:Repo<Rate>,IRate
    {
        private readonly GIFContext context;

        public RateRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Rate Rate)
        {
            var old = context.Rates.FirstOrDefault(r => r.Id == Rate.Id);
            if (old != null)
            {
                old.No_Of_Stars = Rate.No_Of_Stars;
                old.Review =  Rate.Review;
                old.Blocked =  Rate.Blocked;
                
            }
        }
    }
}

using GIF_S.Model;

namespace GIF_S.Repo
{
    public class WishListRepo : Repo<WishList>, IWishList
    {
        private readonly GIFContext context;

        public WishListRepo(GIFContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(WishList WishList)
        {
            var old = context.WishLists.FirstOrDefault(w => w.Id == WishList.Id);
            if (old != null)
            {
                old.UserId= WishList.UserId;
                old.Blocked = WishList.Blocked;
            }
        }
    }
}
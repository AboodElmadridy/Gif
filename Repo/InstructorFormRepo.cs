using GIF_S.Model;

namespace GIF_S.Repo
{
    public class InstructorFormRepo : Repo<InstructorForm>
    {
        public InstructorFormRepo(GIFContext context) : base(context)
        {
        }
    }
}

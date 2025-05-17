namespace GIF_S.Model
{
    public class Progress
    {
        public int Id { get; set; }
        public double Ratio {  get; set; }
        public bool Blocked { get; set; } = false;
        public virtual UserCourseEnroll UserCourse { get; set; }
    }
}

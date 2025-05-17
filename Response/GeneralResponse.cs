namespace GIF_S.Response
{
    public class GeneralResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public dynamic? Data { get; set; }
    }
}

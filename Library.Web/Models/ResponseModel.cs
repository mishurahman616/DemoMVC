namespace Library.Web.Models
{
    public enum ResponseType
    {
        Success,
        Danger
    }
    public class ResponseModel
    {
        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
    }
}

namespace NZWalks.API.Entities.ResponseEntity
{
    public class CommonResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }
}

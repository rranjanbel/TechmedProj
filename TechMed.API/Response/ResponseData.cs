namespace TechMed.API.Response
{
    public class ResponseData
    {
        public int ResponseCode { get; set; }
        public bool Status { get; set; }
        public object Message { get; set; }
        public object Data { get; set; }
    }
}

namespace Rideshare.Uber.Sdk.Models
{
    public class UberResponse<T>
    {
        public T Data { get; set; }
        public UberError Error { get; set; }
    }
}
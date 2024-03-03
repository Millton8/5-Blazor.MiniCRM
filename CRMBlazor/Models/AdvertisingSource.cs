using CRMBlazor.Interface;

namespace CRMBlazor.Models
{
    public class AdvertisingSource:IName,ITestik
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
    }
}

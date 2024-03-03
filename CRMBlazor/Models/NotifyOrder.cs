using System.ComponentModel.DataAnnotations.Schema;

namespace CRMBlazor.Models
{
    public class NotifyOrder
    {
        public string? Name { get; set; }
        public string Phone { get; set; }
        public string Advertise { get; set; }
        public int Area { get; set; }
        public string JobType { get; set; }
        public string? Description { get; set; }

        public NotifyOrder(string? name, string phone, string advertise, int area, string jobType, string? description)
        {
            Name = name;
            Phone = phone;
            Advertise = advertise;
            Area = area;
            JobType = jobType;
            Description = description;
        }


    }
}

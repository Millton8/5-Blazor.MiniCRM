using System.ComponentModel.DataAnnotations.Schema;

namespace CRMBlazor.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Phone { get; set; }
        public DateTime RegistredDate { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [ForeignKey("Advertise")]
        public int AdvId { get; set; }
        public virtual AdvertisingSource Advertise { get; set; }
        public int Area { get; set; }
        public int JobTypeId { get; set; }
        public JobType JobType { get; set; }
        public int PricePerMeter { get; set; }
        public int Price { get; set; }
        public int StageId { get; set; }
        public Stage Stage { get; set; }
        public List<UserTask>? UserTasks { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public bool Notify { get; set; } 

        public Order()
        {
            RegistredDate = DateTime.Today;
            Notify = false;
        }


    }
}

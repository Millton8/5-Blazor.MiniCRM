namespace CRMBlazor.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? NotifyDate { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public int? UserId { get; set; }
        public int? OrderId { get; set; }

        public UserTask()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            StartDate = new DateOnly(now.Year,now.Month,now.Day);
            
        }


    }
}

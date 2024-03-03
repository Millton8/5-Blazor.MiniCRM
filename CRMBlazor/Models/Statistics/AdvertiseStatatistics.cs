namespace CRMBlazor.Models.Statistics
{
    public class AdvertiseStatatistics
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int advSourceId { get; set; }
        public AdvertisingSource advSource { get; set; }
        public int jobTypeId { get; set; }
        public JobType jobType { get; set; }
        public int Expense { get; set; }
        public int ShowTimes { get; set; }
        public int Views { get; set; }
        public int Contacts { get; set; }
        public int ContactCost { get; set; }
        public int Turnover { get; set; }
        public int SalesCount { get; set; }
        public int Sales { get; set; }
        

    }
}

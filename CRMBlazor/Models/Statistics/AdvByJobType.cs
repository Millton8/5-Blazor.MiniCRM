namespace CRMBlazor.Models.Statistics
{
    public class AdvByJobType
    {
        public int Id { get; set; }
        public int jobTypeId { get; set; }
        public JobType jobType { get; set; } = new();
        public int Expense { get; set; }
        public int ShowTimes { get; set; }
        public int Views { get; set; }
        public int Contacts { get; set; }
        public int Turnover { get; set; }
        public int SalesCount { get; set; }
        public int Sales { get; set; }
        public int AdvertiseStatatisticsByJobTypesId { get; set; }

        public static AdvByJobType operator +(AdvByJobType item1, AdvByJobType item2)
        {
            return new AdvByJobType() { jobType=item2.jobType,Expense = item1.Expense + item2.Expense };
        }
    }
}

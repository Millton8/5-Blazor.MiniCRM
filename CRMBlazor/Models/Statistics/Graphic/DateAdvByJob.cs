namespace CRMBlazor.Models.Statistics.Graphic
{
    public class DateAdvByJob
    {
        public DateOnly Date {  get; set; }
        public List<AdvByJobType> Job { get; set; }
        public int Count { get; set; }
    }
}

namespace CRMBlazor.Models.Statistics
{
    public class AdvertiseStatatisticsByJobTypes
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int advSourceId { get; set; }
        public AdvertisingSource advSource { get; set; }
        public List<AdvByJobType> AdvByJobTypes { get; set; } = new();

        public AdvertiseStatatisticsByJobTypes()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            Date = new DateOnly(now.Year, now.Month, now.Day);
        }
    }
}

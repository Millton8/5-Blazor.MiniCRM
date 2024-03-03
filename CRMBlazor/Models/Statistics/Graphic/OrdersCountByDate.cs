namespace CRMBlazor.Models.Statistics.Graphic
{
    public class OrdersCountByDate
    {
        public DateTime? Date { get; set; }
        public int Count { get; set; }
        public List<ItemCount> Counts { get; set; }
    }
}

namespace DAL2Service.Models
{
    public class SignalDbModel
    {
        public int Id { get; set; }
        public DateTime TimeStemp { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }
        public bool Error { get; set; }

    }
}

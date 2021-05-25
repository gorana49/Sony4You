namespace back.Models
{
    public class Sony
    {
        public string Notes { get; set; }
        public string SerialNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public Sony(string Serial, string Ty, decimal Pr, string Notes)
        {
            this.Notes = Notes;
            SerialNumber = Serial;
            Type = Ty;
            Price = Pr;
        }
        public Sony() { }
    }
}

namespace Blackrock_Test.Modals
{
    public class Portfolio
    {
        public int Port_ID { get; set; }
        public string Port_Name { get; set; }
        public string Port_Country { get; set; }
        public string Port_CCY { get; set; }
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}

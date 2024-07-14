namespace Blackrock_Test.Modals
{
    public class Loan
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int OriginalLoanAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal CollateralValue { get; set; }
        public string Rating { get; set; }
    public Portfolio Portfolio { get; set; }
    }
}

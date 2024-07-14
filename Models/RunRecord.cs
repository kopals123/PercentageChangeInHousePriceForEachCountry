namespace Blackrock_Test.Modals
{
        public class RunRecord
        {
            public int Id { get; set; }
            public DateTime RunDate { get; set; }
            public string Country { get; set; }
            public decimal PercentageChange { get; set; }
            public decimal TotalOutstandingLoanAmount { get; set; }
            public decimal TotalCollateralValue { get; set; }
            public decimal ScenarioCollateralValue { get; set; }
            public decimal ExpectedLoss { get; set; }
        }
}

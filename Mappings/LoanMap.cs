using Blackrock_Test.Modals;
using CsvHelper.Configuration;

namespace Blackrock_Test.Mappings
{
    public class LoanMap : ClassMap<Loan>
    {
        public LoanMap()
        {
            Map(m => m.Id).Name("Loan_ID");
            Map(m => m.PortfolioId).Name("Port_ID");
            Map(m => m.OriginalLoanAmount).Name("OriginalLoanAmount");
            Map(m => m.OutstandingAmount).Name("OutstandingAmount");
            Map(m => m.CollateralValue).Name("CollateralValue");
            Map(m => m.Rating).Name("CreditRating");
        }
    }
}

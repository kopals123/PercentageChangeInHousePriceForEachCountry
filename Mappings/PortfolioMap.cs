namespace Blackrock_Test.Mappings
{
        using Blackrock_Test.Modals;
        using CsvHelper.Configuration;

        public class PortfolioMap : ClassMap<Portfolio>
        {
            public PortfolioMap()
            {
                Map(m => m.Port_ID).Name("Port_ID");
                Map(m => m.Port_Name).Name("Port_Name");
                Map(m => m.Port_Country).Name("Port_Country");
                Map(m => m.Port_CCY).Name("Port_CCY");
            }
        }
}

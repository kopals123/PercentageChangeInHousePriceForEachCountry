namespace Blackrock_Test.Mappings
{
    using Blackrock_Test.Modals;
    using CsvHelper.Configuration;

    public class RatingMap : ClassMap<CreditRating>
    {
        public RatingMap()
        {
            Map(m => m.Rating).Name("Rating");
            Map(m => m.ProbabilityOfDefault).Name("ProbablilityOfDefault");
        }
    }
}

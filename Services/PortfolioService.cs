using Blackrock_Test.Data;
using Blackrock_Test.Mappings;
using Blackrock_Test.Modals;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class PortfolioService
{
    private readonly AppDbContext _context;

    public PortfolioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RunRecord>> CalculateAsync(Dictionary<string, decimal> percentageChanges)
    {
        var portfolios = LoadPortfoliosFromCsv("C:\\Users\\daksh\\source\\repos\\Portfolios.csv");
        var loans = LoadLoansFromCsv("C:\\Users\\daksh\\source\\repos\\Loans.csv");
        var ratings = LoadRatingsFromCsv("C:\\Users\\daksh\\source\\repos\\Ratings.csv");

        foreach (var loan in loans)
        {
            var portfolio = portfolios.FirstOrDefault(p => p.Port_ID == loan.PortfolioId);
            if (portfolio != null)
            {
                portfolio.Loans.Add(loan);
            }
        }

        var runDate = DateTime.Now;
        var results = new List<RunRecord>();

        foreach (var portfolio in portfolios)
        {
            if (percentageChanges.TryGetValue(portfolio.Port_Country, out var countryChange))
            {
                var totalOutstandingLoanAmount = portfolio.Loans.Sum(l => l.OutstandingAmount);
                var totalCollateralValue = portfolio.Loans.Sum(l => l.CollateralValue);

                decimal scenarioCollateralValue = 0;
                decimal expectedLoss = 0;

                foreach (var loan in portfolio.Loans)
                {
                    var collateralValueChange = loan.CollateralValue * (1 + countryChange / 100);
                    var recoveryRate = collateralValueChange / loan.OutstandingAmount;
                    var lossGivenDefault = 1 - recoveryRate;
                    var probabilityOfDefault = ratings.FirstOrDefault(r => r.Rating == loan.Rating)?.ProbabilityOfDefault ?? 0;
                    var loanExpectedLoss = probabilityOfDefault * lossGivenDefault * loan.OutstandingAmount;

                    scenarioCollateralValue += collateralValueChange;
                    expectedLoss += loanExpectedLoss;
                }

                results.Add(new RunRecord
                {
                    RunDate = runDate,
                    Country = portfolio.Port_Country,
                    PercentageChange = countryChange,
                    TotalOutstandingLoanAmount = totalOutstandingLoanAmount,
                    TotalCollateralValue = totalCollateralValue,
                    ScenarioCollateralValue = scenarioCollateralValue,
                    ExpectedLoss = expectedLoss
                });
            }
        }

        await AddRunRecordsAsync(results);

        return results;
    }

    private async Task AddRunRecordsAsync(List<RunRecord> records)
    {
        foreach (var record in records)
        {
            var existingRecord = await _context.RunRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RunDate == record.RunDate && r.Country == record.Country);

            if (existingRecord == null)
            {
                _context.RunRecords.Add(record);
            }
            else
            {
                existingRecord.PercentageChange = record.PercentageChange;
                existingRecord.TotalOutstandingLoanAmount = record.TotalOutstandingLoanAmount;
                existingRecord.TotalCollateralValue = record.TotalCollateralValue;
                existingRecord.ScenarioCollateralValue = record.ScenarioCollateralValue;
                existingRecord.ExpectedLoss = record.ExpectedLoss;
                _context.RunRecords.Update(existingRecord);
            }
        }

        await _context.SaveChangesAsync();
    }

    public List<Portfolio> LoadPortfoliosFromCsv(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.Context.RegisterClassMap<PortfolioMap>();
            return csv.GetRecords<Portfolio>().ToList();
        }
    }

    public List<Loan> LoadLoansFromCsv(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.Context.RegisterClassMap<LoanMap>();
            return csv.GetRecords<Loan>().ToList();
        }
    }

    public List<CreditRating> LoadRatingsFromCsv(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.Context.RegisterClassMap<RatingMap>();
            return csv.GetRecords<CreditRating>().ToList();
        }
    }

    public async Task<List<RunRecord>> GetRunsAsync()
    {
        return await _context.RunRecords.ToListAsync();
    }
}
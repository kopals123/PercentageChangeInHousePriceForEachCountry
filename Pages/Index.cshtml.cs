using Blackrock_Test.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly PortfolioService _portfolioService;

    public IndexModel(PortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
        Countries = new[] { "GB", "US", "FR", "DE", "SG", "GR" };
    }

    [BindProperty]
    public Dictionary<string, decimal> CountryChanges { get; set; } = new();

    public List<RunRecord> RunRecords { get; set; }

    public string[] Countries { get; }

    public async Task OnGetAsync()
    {
        RunRecords = await _portfolioService.GetRunsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        RunRecords = await _portfolioService.CalculateAsync(CountryChanges);
        return Page();
    }
}
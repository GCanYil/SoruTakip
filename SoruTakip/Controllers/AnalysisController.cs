using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoruTakip.Data;
using SoruTakip.Models;

namespace SoruTakip.Controllers;

[Authorize]
public class AnalysisController : Controller
{
    private readonly AppDbContext _context;
    public AnalysisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(int QuestionId, string Note)
    {
        if (!string.IsNullOrWhiteSpace(Note))
        {
            var analysis = new Analysis
            {
                QuestionId = QuestionId,
                Note = Note,
                CreatedAt = DateTime.UtcNow
            };
            _context.Analyses.Add(analysis);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Detail", "Question", new { id = QuestionId });
    }
}
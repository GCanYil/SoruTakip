using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoruTakip.Data;
using SoruTakip.Models;

namespace SoruTakip.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var now = DateTime.UtcNow;
        var weekAgo = now.AddDays(-7);
        var monthAgo = now.AddDays(-30);
        var questions = await _context.Questions
            .Include(x => x.Subject)
            .Include(x => x.Topic)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        ViewBag.TotalQuestions = questions.Count;
        ViewBag.SolvedQuestions = questions.Count(x => x.Status == QuestionStatus.Solved);
        ViewBag.StuckQuestions = questions.Count(x => x.Status == QuestionStatus.Stuck);
        ViewBag.TotalFolders = await _context.Folders.CountAsync(f => f.UserId == userId);

        ViewBag.ThisWeekTotal = questions.Count(q => q.CreatedAt >= weekAgo);
        ViewBag.ThisMonthTotal = questions.Count(q => q.CreatedAt >= monthAgo);
        
        ViewBag.TopicStats = questions
            .GroupBy(q => new { SubjectName = q.Subject.Name, TopicName = q.Topic.Name })
            .Select(g => new
            {
                Subject = g.Key.SubjectName,
                Topic = g.Key.TopicName,
                Total = g.Count(),
                Solved = g.Count(q => q.Status == QuestionStatus.Solved),
                Stuck = g.Count(q => q.Status == QuestionStatus.Stuck),
                LastWeek = g.Count(q => q.CreatedAt >= weekAgo),
                LastMonth = g.Count(q => q.CreatedAt >= monthAgo)
            })
            .OrderByDescending(x => x.Total)
            .ToList();
        
        ViewBag.SubjectStats = questions
            .GroupBy(q => q.Subject.Name)
            .Select(g => new
            {
                Subject = g.Key,
                Total = g.Count(),
                Solved = g.Count(q => q.Status == QuestionStatus.Solved),
                Stuck = g.Count(q => q.Status == QuestionStatus.Stuck)
            })
            .OrderByDescending(x => x.Total)
            .ToList();
        
        ViewBag.FolderStats = await _context.Folders
            .Where(f => f.UserId == userId)
            .Select(f => new
            {
                Name = f.Name,
                Total = f.QuestionFolders.Count,
                Solved = f.QuestionFolders.Count(qf => qf.Question.Status == QuestionStatus.Solved),
                Stuck = f.QuestionFolders.Count(qf => qf.Question.Status == QuestionStatus.Stuck)
            })
            .ToListAsync();
        
        ViewBag.RecentQuestions = questions
            .OrderByDescending(q => q.CreatedAt)
            .Take(5)
            .ToList();
        return View();
    }
}
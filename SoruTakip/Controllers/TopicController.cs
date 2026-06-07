using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoruTakip.Data;
using SoruTakip.Models;
using SoruTakip.ViewModels;

namespace SoruTakip.Controllers;

[Authorize]
public class TopicController : Controller
{
    private readonly AppDbContext _context;

    public TopicController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var topics = await _context.Topics
            .Include(t => t.Subject)
            .ToListAsync();
        return View(topics);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var subjects = await _context.Subjects.ToListAsync();
        ViewBag.Subjects = subjects;
        return View(new TopicViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,SubjectId")] TopicViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            return View(model);
        }

        var topic = new Topic { Name = model.Name, SubjectId = model.SubjectId };
        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var topic = await _context.Topics
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (topic == null) return NotFound();
        ViewBag.Subjects = await _context.Subjects.ToListAsync();
        return View(new TopicViewModel
        {
            Id = topic.Id,
            Name = topic.Name,
            SubjectId = topic.SubjectId,
            SubjectName = topic.Subject.Name
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SubjectId")] TopicViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            return View(model);
        }

        var topic = await _context.Topics.FindAsync(id);
        if (topic == null) return NotFound();
        topic.Name = model.Name;
        topic.SubjectId = model.SubjectId;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var topic = await _context.Topics.FindAsync(id);
        if (topic == null) return NotFound();

        var hasQuestions = await _context.Questions
            .AnyAsync(q => q.TopicId == id);

        if (hasQuestions)
        {
            TempData["Error"] = "Cannot delete this topic because it has questions assigned to it.";
            return RedirectToAction(nameof(Index));
        }

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
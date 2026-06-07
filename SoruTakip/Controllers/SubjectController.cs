using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoruTakip.Data;
using SoruTakip.Models;
using SoruTakip.ViewModels;

namespace SoruTakip.Controllers;

[Authorize]
public class SubjectController :Controller
{
    private readonly AppDbContext _context;

    public SubjectController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var subjects = await _context.Subjects.Include(s => s.Topics)
            .ToListAsync();
        return View(subjects);
    }

    [HttpGet]
    public IActionResult Create() => View(new SubjectViewModel());

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name")] SubjectViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var subject = new Subject { Name = model.Name };
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null) return NotFound();
        return View(new SubjectViewModel { Id = subject.Id, Name = subject.Name });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SubjectViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null) return NotFound();
        subject.Name = model.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null) return NotFound();
        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
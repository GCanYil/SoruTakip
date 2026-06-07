using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoruTakip.Data;
using SoruTakip.Models;
using SoruTakip.ViewModels;

namespace SoruTakip.Controllers;

[Authorize]
public class FolderController:Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FolderController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var folders = await _context.Folders
            .Where(x => x.UserId == userId)
            .Select(x => new FolderViewModel
            {
                Id = x.Id,
                Name = x.Name,
                QuestionCount = x.QuestionFolders.Count
            })
            .ToListAsync();
        return View(folders);
    }
    
    [HttpGet]
    public IActionResult Create() => View(new FolderViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name")] FolderViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userId = _userManager.GetUserId(User)!;
        var folder = new Folder { Name = model.Name, UserId = userId };
        _context.Folders.Add(folder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var folder = await _context.Folders
            .Include(x => x.QuestionFolders)
            .ThenInclude(x => x.Question)
            .ThenInclude(x => x.Subject)
            .Include(x => x.QuestionFolders)
            .ThenInclude(x => x.Question)
            .ThenInclude(x => x.Topic)
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (folder == null)
        {
            return NotFound();
        }
        return View(folder);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var folder = await _context.Folders
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (folder == null)
        {
            return NotFound();
        }
        return View(new FolderViewModel { Id = folder.Id, Name = folder.Name });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FolderViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userId = _userManager.GetUserId(User)!;
        var folder = await _context.Folders
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (folder == null) return NotFound();
        folder.Name = model.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var folder = await _context.Folders
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (folder == null) return NotFound();
        _context.Folders.Remove(folder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddQuestion(int folderId, int questionId)
    {
        var userId = _userManager.GetUserId(User)!;
        var folder = await _context.Folders
            .FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);
        if (folder == null) return NotFound();
        var exists = await _context.QuestionFolders
            .AnyAsync(x => x.FolderId == folderId && x.QuestionId == questionId);
        if (!exists)
        {
            _context.QuestionFolders.Add(new QuestionFolder
            {
                FolderId = folderId,
                QuestionId = questionId
            });
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Detail", new { id = folderId });
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveQuestion(int folderId, int questionId)
    {
        var qf = await _context.QuestionFolders
            .FirstOrDefaultAsync(qf => qf.FolderId == folderId && qf.QuestionId == questionId);

        if (qf != null)
        {
            _context.QuestionFolders.Remove(qf);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Detail", new { id = folderId });
    }
}
using SoruTakip.Data;
using SoruTakip.Models;
using SoruTakip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoruTakip.Controllers;

[Authorize]
public class QuestionController:Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly UserManager<ApplicationUser> _userManager;

    public QuestionController(AppDbContext context, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _env = env;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int? subjectId, int? topicId, QuestionStatus? status)
    {
        var userId = _userManager.GetUserId(User)!;
        var query = _context.Questions.Include(x => x.Subject)
            .Include(x => x.Topic)
            .Where(x => x.UserId == userId).AsQueryable();

        if (subjectId.HasValue)
        {
            query = query.Where(x => x.SubjectId == subjectId);
        }

        if (topicId.HasValue)
        {
            query = query.Where(x => x.TopicId == topicId);
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status);
        }

        ViewBag.Subjects = await _context.Subjects.ToListAsync();
        ViewBag.Topics = await _context.Topics.ToListAsync();
        ViewBag.SelectedSubjectId = subjectId;
        ViewBag.SelectedTopicId = topicId;
        ViewBag.SelectedStatus = status;
        return View(await query.OrderByDescending(x => x.CreatedAt).ToListAsync());
        
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Subjects = await _context.Subjects.ToListAsync();
        ViewBag.Topics = await _context.Topics.ToListAsync();
        return View(new QuestionViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([Bind("SubjectId,TopicId,Status,ImageFile")] QuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            ViewBag.Topics = await _context.Topics.ToListAsync();
            return View(model);
        }

        var userId = _userManager.GetUserId(User)!;
        var question = new Question
        {
            SubjectId = model.SubjectId,
            TopicId = model.TopicId,
            Status = model.Status,
            UserId = userId
        };

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            var savePath = Path.Combine(_env.WebRootPath, "images", fileName);
            using var stream = new FileStream(savePath, FileMode.Create);
            await model.ImageFile.CopyToAsync(stream);
            question.ImagePath = "/images/" + fileName;
        }
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var question = await _context.Questions
            .Include(x => x.Subject)
            .Include(x => x.Topic)
            .Include(x => x.Analyses)
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (question == null) return NotFound();
        return View(question);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var question = await _context.Questions
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (question == null)
        {
            return NotFound();
        }

        ViewBag.Subjects = await _context.Subjects.ToListAsync();
        ViewBag.Topics = await _context.Topics.ToListAsync();
        return View(new QuestionViewModel
        {
            Id = question.Id,
            SubjectId = question.SubjectId,
            TopicId = question.TopicId,
            Status = question.Status,
            ImagePath = question.ImagePath
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,TopicId,Status,ImageFile")] QuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Subjects = await _context.Subjects.ToListAsync();
            ViewBag.Topics = await _context.Topics.ToListAsync();
            return View(model);
        }

        var userId = _userManager.GetUserId(User)!;
        var question = await _context.Questions
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (question == null)
        {
            return NotFound();
        }

        question.SubjectId = model.SubjectId;
        question.TopicId = model.TopicId;
        question.Status = model.Status;
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            if (!string.IsNullOrEmpty(question.ImagePath))
            {
                var oldPath = Path.Combine(_env.WebRootPath, question.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            var savePath = Path.Combine(_env.WebRootPath, "images", fileName);
            using var stream = new FileStream(savePath, FileMode.Create);
            await model.ImageFile.CopyToAsync(stream);
            question.ImagePath = "/images/" + fileName;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = _userManager.GetUserId(User)!;
        var question = await _context.Questions
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (question == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(question.ImagePath))
        {
            var oldPath = Path.Combine(_env.WebRootPath, question.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
        }
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
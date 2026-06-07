using SoruTakip.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SoruTakip.ViewModels;

public class QuestionViewModel
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public QuestionStatus Status { get; set; } = QuestionStatus.Stuck;
    
    [Required]
    public int SubjectId { get; set; }
    
    [Required]
    public int TopicId { get; set; }

    public string SubjectName { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public IFormFile? ImageFile { get; set; }
}
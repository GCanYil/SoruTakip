using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SoruTakip.Models;

public class Question
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public QuestionStatus Status { get; set; } = QuestionStatus.Stuck;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    public ICollection<Analysis> Analyses { get; set; } = new List<Analysis>();
    public ICollection<QuestionFolder> QuestionFolders { get; set; } = new List<QuestionFolder>();

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
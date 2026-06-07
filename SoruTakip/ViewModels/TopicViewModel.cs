using System.ComponentModel.DataAnnotations;

namespace SoruTakip.ViewModels;

public class TopicViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = string.Empty;
}
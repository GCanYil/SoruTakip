namespace SoruTakip.Models;

public class Analysis
{
    public int Id { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    
}
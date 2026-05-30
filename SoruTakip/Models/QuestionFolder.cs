namespace SoruTakip.Models;

public class QuestionFolder
{
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null;
    
    public int FolderId { get; set; }
    public Folder Folder { get; set; } = null;
}
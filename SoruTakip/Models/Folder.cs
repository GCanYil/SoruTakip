namespace SoruTakip.Models;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public ICollection<QuestionFolder> QuestionFolders { get; set; } = new List<QuestionFolder>();
    
}
namespace MusicStore.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = default!; //también podría ser: = string.Empty
    public bool Status { get; set; } = true;
}


namespace WebApplication1;
using System.Collections.Generic;

using System.Linq;

public class Todos
{
    // Constructor
    public int Id { get; set; }
    public string? Description { get; set; }

    public bool Completed { get; set; } = false;

    public string? Date { get; set; }

    public string? Priority { get; set; }
}
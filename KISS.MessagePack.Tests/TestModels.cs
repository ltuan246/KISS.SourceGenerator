namespace KISS.MessagePack.Tests;

/// <summary>
/// Simple test model to demonstrate basic MessagePack generation
/// </summary>
[MessagePackObjectGenerator]
public partial class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    public List<string> Skills { get; set; } = [];
}

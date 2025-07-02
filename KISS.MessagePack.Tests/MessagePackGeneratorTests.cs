using Xunit;

namespace KISS.MessagePack.Tests;

/// <summary>
/// Integration tests demonstrating KISS.MessagePack compatibility with xUnit testing framework
/// These tests verify that the source generator works correctly in test environments
/// </summary>
public class KissMessagePackIntegrationTests
{
    [Fact]
    public void Person_Serialize_SerializesCorrectly()
    {
        // Arrange
        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            BirthDate = new DateTime(1993, 5, 15),
            Skills = ["C#", "SQL", "Azure", "MessagePack"]
        };

        // Act
        byte[] serialized = person.Serialize();
        var deserializedPerson = MessagePackExtensions.Deserialize<Person>(serialized);

        // Assert
        Assert.NotNull(serialized);
        Assert.True(serialized.Length > 0);
        Assert.NotNull(deserializedPerson);
        Assert.Equal(person.FirstName, deserializedPerson.FirstName);
        Assert.Equal(person.LastName, deserializedPerson.LastName);
        Assert.Equal(person.Age, deserializedPerson.Age);
        Assert.Equal(person.BirthDate, deserializedPerson.BirthDate);
        Assert.Equal(person.Skills.Count, deserializedPerson.Skills.Count);
        Assert.Equal(person.Skills, deserializedPerson.Skills);
    }
}

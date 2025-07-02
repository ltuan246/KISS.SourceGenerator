# KISS MessagePack Source Generator

[![NuGet](https://img.shields.io/nuget/v/KISS.MessagePack.svg)](https://www.nuget.org/packages/KISS.MessagePack/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight C# source generator that automatically generates MessagePack serialization attributes for your classes. Following the KISS principle (Keep It Simple, Stupid), this generator eliminates the need to manually add MessagePack attributes to your data classes.

## Features

- 🚀 **Zero Runtime Dependencies**: Pure source generator with no runtime overhead
- 🎯 **Simple Usage**: Just add one attribute to your class
- 🔧 **Automatic Key Assignment**: Generates sequential MessagePack keys automatically
- 📦 **Roslyn-Based**: Uses the latest C# source generator technology
- 🌐 **.NET Standard 2.0**: Compatible with .NET Framework, .NET Core, and .NET 5+

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package KISS.MessagePack
```

Or via Package Manager Console:

```powershell
Install-Package KISS.MessagePack
```

## Usage

### Basic Example

1. Mark your class with the `[MessagePackObjectGenerator]` attribute:

```csharp
using KISS.MessagePack;

[MessagePackObjectGenerator]
public partial class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
}
```

2. The source generator will automatically create the MessagePack attributes:

```csharp
// Generated code (Person.MessagePack.g.cs)
using MessagePack;

[MessagePackObject]
public partial class Person
{
    [Key(0)]
    public string FirstName { get; set; }

    [Key(1)]
    public string LastName { get; set; }

    [Key(2)]
    public int Age { get; set; }

    [Key(3)]
    public DateTime BirthDate { get; set; }
}
```

## Requirements

- .NET Standard 2.0 or higher
- C# 9.0 or later (for source generators)
- MessagePack for C# package (for the actual serialization)

## xUnit Testing Integration

KISS.MessagePack is fully compatible with xUnit testing framework and .NET 8.0. The source generator works seamlessly in test projects.

### Setting up xUnit Tests

1. **Create a test project targeting .NET 8.0:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
    <PackageReference Include="MessagePack" Version="2.5.198" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\KISS.MessagePack\KISS.MessagePack.csproj"
                      OutputItemType="Analyzer"
                      ReferenceOutputAssembly="true" />
  </ItemGroup>
</Project>
```

2. **Create test models:**
```csharp
[MessagePackObjectGenerator]
public partial class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
}
```

3. **Write xUnit tests:**
```csharp
[Fact]
public void Person_Serialize_SerializesCorrectly()
{
    // Arrange
    var person = new Person
    {
        FirstName = "John",
        LastName = "Doe",
        Age = 30,
        BirthDate = new DateTime(1993, 5, 15)
    };

    // Act
    byte[] serialized = person.Serialize();
    var deserializedPerson = MessagePackExtensions.Deserialize<Person>(serialized);

    // Assert
    Assert.NotNull(serialized);
    Assert.NotNull(deserializedPerson);
    Assert.Equal(person.FirstName, deserializedPerson.FirstName);
    Assert.Equal(person.LastName, deserializedPerson.LastName);
    Assert.Equal(person.Age, deserializedPerson.Age);
    Assert.Equal(person.BirthDate, deserializedPerson.BirthDate);
}
```

## How It Works

1. The source generator scans your code for classes marked with `[MessagePackObjectGenerator]`
2. For each marked class, it generates a partial class with MessagePack attributes
3. Properties and fields are automatically assigned sequential `[Key(n)]` attributes
4. The generated code is compiled alongside your project

## Limitations and Known Issues

### Current Limitations

1. **Complex Nested Collections**: The current version has limitations with deeply nested collection scenarios. Simple collections like `List<string>` work fine, but complex nested structures may require manual MessagePack attribute configuration.

2. **Generated Class Approach**: The generator creates separate `*MessagePackGenerated` classes rather than adding attributes directly to your original classes. This means:
   - You work with generated proxy classes for serialization
   - Implicit conversion operators handle the conversion between original and generated classes
   - Extension methods provide convenient `Serialize()` and `Deserialize<T>()` methods

### Supported Scenarios

✅ **Fully Supported:**
```csharp
[MessagePackObjectGenerator]
public partial class Person
{
    public string Name { get; set; }           // ✅ Simple properties
    public int Age { get; set; }               // ✅ Value types
    public DateTime BirthDate { get; set; }    // ✅ DateTime
    public List<string> Skills { get; set; }   // ✅ Simple collections
    public string? Description { get; set; }   // ✅ Nullable reference types
    public int? CategoryId { get; set; }       // ✅ Nullable value types
}
```

⚠️ **Limited Support:**
```csharp
[MessagePackObjectGenerator]
public partial class ComplexModel
{
    // May require manual MessagePack configuration for complex scenarios
    public Dictionary<string, List<CustomObject>> ComplexData { get; set; }
    public List<Dictionary<int, string[]>> NestedCollections { get; set; }
}
```

### Workarounds

For complex scenarios not fully supported by the generator, you can:

1. **Use manual MessagePack attributes** on specific properties
2. **Break down complex structures** into simpler, generator-friendly classes
3. **Use the generated classes as a starting point** and customize as needed

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Changelog

### v1.0.0
- Initial release
- Basic MessagePack attribute generation
- Support for classes, properties, and fields
- Automatic key assignment
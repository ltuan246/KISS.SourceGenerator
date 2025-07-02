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

### Advanced Usage

The generator works with:
- Properties with getters and setters
- Fields (public, private, protected)
- Nested classes
- Generic classes
- Inheritance hierarchies

```csharp
[MessagePackObjectGenerator]
public partial class Employee : Person
{
    public string EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public List<string> Skills { get; set; }
}
```

## Requirements

- .NET Standard 2.0 or higher
- C# 9.0 or later (for source generators)
- MessagePack for C# package (for the actual serialization)

## How It Works

1. The source generator scans your code for classes marked with `[MessagePackObjectGenerator]`
2. For each marked class, it generates a partial class with MessagePack attributes
3. Properties and fields are automatically assigned sequential `[Key(n)]` attributes
4. The generated code is compiled alongside your project

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
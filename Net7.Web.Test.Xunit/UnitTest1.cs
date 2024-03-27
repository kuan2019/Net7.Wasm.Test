using System.Collections;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Net7.Web.Test.Xunit;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public string[] GetModels(string make) => make switch
    {
        "Honda" => new[] { "Civic", "Accord", "CR-V" },
        "Tesla" => new[] { "Model S", "Model Y" },
        "Ford" => new[] { "Mustnag" },
        "Skoda" => new[] { "Fabia", "Octavia" },
        _ => throw new ArgumentException("Invalid car make", nameof(make))
    };

    public UnitTest1(ITestOutputHelper output)
    {
        this._output = output;
     
        var config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.test.json", false, true)
                        .Build();

        _output.WriteLine($"Env: {config["Env"]}");
    }

    [Fact]
    public void Test1()
    { 
        _output.WriteLine($"Test...");
        Console.WriteLine($"Console.Test...");

        // _output.WriteLine($"Ford => { System.Text.Json.JsonSerializer.Serialize(GetModels(""))}");

        Assert.True(true,"It's not equivalent");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    //[InlineData(3)]
    public void CheckVal(int val)
    {
        _output.WriteLine($"CheckVal...{val}");
        Console.WriteLine($"Console.CheckVal...{val}");

        // Assert.True((val == 1 || val == 2) ? true : false);
        val.Should().BeOneOf(new int[] {1, 2});
    }

    //////////

    [Theory, MemberData(nameof(SplitCountData))]
    public void SplitCount(string input, int expectedCount)
    {
        var actualCount = input.Split(' ').Count();
        _output.WriteLine($"expectedCount={expectedCount}, actualCount={actualCount}");

        Assert.Equal(expectedCount, actualCount);
    }

    public static IEnumerable<object[]> SplitCountData =>
        new List<object[]>
        {
            new object[] { "xUnit", 1 },
            new object[] { "is fun", 2 },
            new object[] { "to test with", 3 }
        };

    //////////
    
    [Theory]
    [ClassData(typeof(CalculatorTestData))]
    public void CanAddTheoryClassData(int value1, int value2, int expected)
    {
        var calculator = new Calculator();

        var result = calculator.Add(value1, value2);

        Assert.Equal(expected, result);
    }

    public class Calculator
    {
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }
    }

    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { -4, -6, -10 };
            yield return new object[] { -2, 2, 0 };
            yield return new object[] { int.MinValue, -1, int.MaxValue };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();  
    }
}

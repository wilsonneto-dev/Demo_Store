namespace Tests;

public static class StringExtensions
{
    public static string AddSuffix(this string? str, string suffix)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        return str + suffix;
    }
}


public class UnitTest1
{
    [Fact]
    public void AddSuffix_AppendsSuffixCorrectly()
    {
        string original = "test";
        string suffix = "ing";
        string expected = "testing";

        string result = original.AddSuffix(suffix);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddSuffix_NullString_ThrowsArgumentNullException()
    {
        // Arrange
        string? original = null;
        string suffix = "ing";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => original.AddSuffix(suffix));
    }
}
namespace SoftwareCenter.Tests;

[Trait("Category", "Demo")]
public class UnitTest1
{
    [Fact]
    public void CanDotNetAddTenAndTwenty()
    {
        // GIVEN
        int a = 10;
        int b = 20;
        int answer;

        // WHEN
        answer = a + b;

        // THEN
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(2, 2, 4)]
    public void CanAddAnyIntegerTogether(int a, int b, int expected) 
    { 
        var answer = a + b;
        Assert.Equal(expected, answer);
    }
}

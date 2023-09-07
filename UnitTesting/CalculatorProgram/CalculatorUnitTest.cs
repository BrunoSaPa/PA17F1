using Xunit;
using CalculatorLib;
namespace CalculatorProgram;

public class CalculatorUnitTest
{

    //Arrange: Variables de entrada y de salida
    //Act: The part where the function to be testes is executed
    //Assert: Compare the expected and the 
    [Fact]
    public void TestAdding2And2()
    {
        //Arrange
        double a = 2;
        double b = 2;
        double expected = 4;
        Calculator calc = new();

        //ACT
        double actual = calc.Add(a,b);

        //Assert puedes obtener la diferencia o la comparacion entre dos campos y regresa un booleano
        Assert.Equal(expected,actual);
    }

    [Fact]
    public void TestAdding2and3()
    {
        // Arrange
        double a = 2;
        double b = 3;
        double expected = 5;
        Calculator calc = new();   
        // Act
        double actual = calc.Add(a,b);

    
        // Assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void TestingMinus2And3()
    {
        // Arrange
        double a = -2;
        double b = 3;
        double expected = 1;
        Calculator calc = new();   
        // Act
        double actual = calc.Add(a,b);

    
        // Assert
        Assert.Equal(expected, actual);
    }
}
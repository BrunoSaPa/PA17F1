using Xunit;
using CalculatorLib;
using System;
namespace CalculatorProgram;

public class CalculatorUnitTest
{

    //Arrange: Variables 
    //Act: The part where the function to be tested is executed
    //Assert: Compare the expected and the result
    [Fact]
    public void TestOverflowExceptionSum()
    {
        //Arrange
        double a = double.MaxValue;
        double b = double.MaxValue;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<OverflowException>(() => calc.Add(a,b));
    }

    [Fact]
    public void TestInfiniteExceptionSum()
    {
        //Arrange
        double a = double.PositiveInfinity;
        double b = double.NegativeInfinity;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<ArithmeticException>(() => calc.Add(a,b));
    }

    [Fact]
    public void TestNegativeResultSum()
    {
        //Arrange
        double a = 10;
        double b = -100;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => calc.Add(a,b));
    }

    [Fact]
    public void TestInvalidArgumentSum()
    {
        //Arrange
        double a = 10;
        double b = ' ';
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<InvalidOperationException>(() => calc.Add(a,b));
    }


    [Fact]
    public void TestDivideByZeroExceptionDivision()
    {
        //Arrange
        double a = 5;
        double b = 0;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<DivideByZeroException>(() => calc.Division(a,b));
    }

    [Fact]
    public void TestOverflowExceptionDivision()
    {
        //Arrange
        double a = double.MaxValue;
        double b = .25;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<OverflowException>(() => calc.Division(a,b));
    }

    [Fact]
    public void TestNaNDivision()
    {
        //Arrange
        double a = double.NaN;
        double b = 10;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<NotSupportedException>(() => calc.Division(a,b));
    }

    [Fact]
    public void TestInfinityDivision()
    {
        //Arrange
        double a = double.NegativeInfinity;
        double b = double.PositiveInfinity;
        //Act
        Calculator calc = new();
        //Assert
        var exception = Assert.Throws<FormatException>(() => calc.Division(a,b));
    }


}
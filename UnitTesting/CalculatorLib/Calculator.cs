using System.Diagnostics;
using System.Runtime.Serialization;

namespace CalculatorLib;


public class Calculator
{
    public double Add(double a, double b){
        if((a + b) > double.MaxValue){
            throw new OverflowException("The result overflows");
        }
        else if(double.IsPositiveInfinity(a) || double.IsNegativeInfinity(b)){
            throw new ArithmeticException("Infinite numbers aren't accepted");
        }
        else if(double.IsNegative(a + b)){
            throw new ArgumentOutOfRangeException("The result of the sum should be more than 0");
        }
        else if(a == ' ' || b == ' '){
            throw new InvalidOperationException("One of the parameters doesn't have any value");
        }
        return a + b;
    }

    public double Division(double a, double b){

        if(b == 0){
            throw new DivideByZeroException("Cannot divide by zero");
        }
        else if((a / b) > double.MaxValue){
            throw new OverflowException("The result overflows");
        }
        else if(double.IsNaN(a) || double.IsNaN(b)){
            throw new NotSupportedException("One of the arguments is'nt a number");
        }
        else if(double.IsInfinity(a) || double.IsInfinity(b)){
            throw new FormatException("Infinite numbers aren't accepted");
        }

        return a / b;
    }
}

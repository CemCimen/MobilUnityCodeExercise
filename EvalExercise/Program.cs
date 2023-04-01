using EvalExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InfixToPostfix
{
    public class Implementation
    {
       
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter your expression");
            string expression = Console.ReadLine();
            if (expression.Contains("x"))
            {
                Console.WriteLine("Please enter your variable's value");
                string x = Console.ReadLine();
                Console.WriteLine(PostfixEvaluator.Evaluate(InfixToPostFixConverter.InfixToPostfixConversion(expression.Replace("x", x))));
            }
            else
                Console.WriteLine(PostfixEvaluator.Evaluate(InfixToPostFixConverter.InfixToPostfixConversion(expression)));
        }
    }
}

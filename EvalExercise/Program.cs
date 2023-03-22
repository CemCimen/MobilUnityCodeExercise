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
        static void Main(string[] args)
        {
            var imp = new Implementation();
            string input = "((1+(2*x)+1)*(x+1))";
            string PostfixResult = InfixToPostFixConverter.InfixToPostfix(input);
            Console.WriteLine(PostfixResult);
            //PostfixEvaluator.Evaluate(PostfixResult);
        }
    }
}

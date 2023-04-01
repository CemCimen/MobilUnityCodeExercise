using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvalExercise
{
    public static class InfixToPostFixConverter
    {

        static int Precedence(string ch)
        {
            switch (ch)
            {
                case "+":
                case "-":
                    return 1;

                case "*":
                case "/":
                    return 2;

                case "^":
                    return 3;
            }
            return -1;
        }

        public static string InfixToPostfixConversion(string expression)
        {
            string result = "";
            try
            {
                var tokens = Regex.Split(expression, @"([-+*/^()])")
          .Where(token => !string.IsNullOrEmpty(token))
          .ToArray();


                Stack<string> stack = new Stack<string>();

                for (int i = 0; i < tokens.Length; ++i)
                {
                    string c = tokens[i];
                    int temp;
                    if (int.TryParse(c, out temp))
                        result += c + " ";
                    else if (c == "x")
                        result += c + " ";

                    else if (c == "(")
                        stack.Push(c);

                    else if (c == ")")
                    {
                        while (stack.Count > 0 && stack.Peek() != "(")
                        {
                            result += stack.Pop() + " ";
                        }
                        if (stack.Count > 0 && stack.Peek() != "(")
                        {
                            return "Invalid Expression";
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                    else
                    {
                        while (stack.Count > 0 && Precedence(c) <= Precedence(stack.Peek()))
                        {
                            result += stack.Pop() + " ";
                        }
                        stack.Push(c);
                    }
                }

                while (stack.Count > 0)
                {
                    result += stack.Pop() + " ";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid expression!!!");
            }


            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalExercise
{
    public static class PostfixEvaluator
    {
        public static double Evaluate(string expression)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in expression.Split(' '))
            {
                if (double.TryParse(token, out double value))
                {
                    stack.Push(value);
                }
                else
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();

                    switch (token)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1 * operand2);
                            break;
                        case "^":
                            stack.Push(Math.Pow(operand1, operand2));
                            break;
                        default:
                            throw new ArgumentException($"Invalid token: {token}");
                    }
                }
            }

            return stack.Pop();
        }
    }

}

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
        private static readonly Dictionary<char, int> operatorPrecedence = new Dictionary<char, int>
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'^', 3},
            {'(', 0}, // This may not be necessary but just in case
            {')', 0},
            // Add a default value of -1 for unknown operators
            {'#', -1}
        };

        public static string InfixToPostfix(string expression)
        {
            // Remove any whitespace from the input expression, this may not be necessary but just in case
            expression = expression.Replace(" ", "");

            // Initialize empty stacks for operators and output
            var operatorStack = new Stack<char>();
            var outputStack = new Stack<string>();

            // Split the expression into tokens (operators and operands)
            var tokens = Regex.Split(expression, @"([-+*/^()])")
                              .Where(token => !string.IsNullOrEmpty(token))
                              .ToArray();

            foreach (var token in tokens)
            {
                if (int.TryParse(token, out _))
                {
                    // If the token is a number, push it to the output stack
                    outputStack.Push(token);
                }
                else
                {
                    char op = token[0];
                    switch (op)
                    {
                        case '(':
                            // Push opening parentheses to the operator stack
                            operatorStack.Push(op);
                            break;

                        case ')':
                            // Pop operators from the stack until the matching opening parenthesis is found
                            while (operatorStack.Peek() != '(')
                            {
                                outputStack.Push(operatorStack.Pop().ToString());
                            }
                            operatorStack.Pop(); // Remove the opening parenthesis from the stack
                            break;

                        default:
                            // The token is an operator
                            int tokenPrecedence;
                            if (!operatorPrecedence.TryGetValue(op, out tokenPrecedence))
                            {
                                // Handle unknown operators
                                tokenPrecedence = operatorPrecedence['#'];
                            }

                            // Pop higher or equal precedence operators from the stack and push them to the output
                            while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                            {
                                int stackPrecedence;
                                if (!operatorPrecedence.TryGetValue(operatorStack.Peek(), out stackPrecedence))
                                {
                                    // Handle unknown operators
                                    stackPrecedence = operatorPrecedence['#'];
                                }

                                if (stackPrecedence >= tokenPrecedence)
                                {
                                    outputStack.Push(operatorStack.Pop().ToString());
                                }
                                else
                                {
                                    break;
                                }
                            }

                            // Push the current operator to the stack
                            operatorStack.Push(op);
                            break;
                    }
                }
            }

            // Pop any remaining operators from the stack and push them to the output
            while (operatorStack.Count > 0)
            {
                outputStack.Push(operatorStack.Pop().ToString());
            }

            // Reverse the output stack to get the postfix expression
            var postfix = string.Join(" ", outputStack.Reverse());

            return postfix;
        }
    }
}

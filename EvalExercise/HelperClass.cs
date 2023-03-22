using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalExercise
{
    public class HelperClass
    {
    }

    public class Expression
    {
        public ExpressionType Type { get; set; }
        public string Element { get; set; }
        public List<Expression> SubExpressions { get; set; }

        public Expression(ExpressionType type, string element = null, List<Expression> subExpressions = null)
        {
            Type = type;
            Element = element;
            SubExpressions = subExpressions ?? new List<Expression>();
        }

        public int Evaluate(Dictionary<string, int> variables)
        {
            switch (Type)
            {
                case ExpressionType.Element:
                    if (Element.All(char.IsDigit))
                    {
                        return int.Parse(Element);
                    }
                    return variables[Element];

                case ExpressionType.Add:
                    return SubExpressions.Sum(expr => expr.Evaluate(variables));

                case ExpressionType.Multiply:
                    return SubExpressions.Aggregate(1, (prod, expr) => prod * expr.Evaluate(variables));

                default:
                    throw new ArgumentException($"Unknown expression type: {Type}");
            }
        }
    }

    public class ExpressionParser
    {
        private readonly string _input;
        private int _pos;

        public ExpressionParser(string input)
        {
            _input = input;
            _pos = 0;
        }

        public Expression Parse()
        {
            var expression = ParseExpression();
            if (_pos < _input.Length)
            {
                throw new ArgumentException($"Unexpected character: {_input[_pos]}");
            }
            return expression;
        }

        private Expression ParseExpression()
        {
            SkipWhiteSpace();
            if (Peek() == '(')
            {
                return ParseParenthesizedExpression();
            }
            return ParseElement();
        }

        private Expression ParseElement()
        {
            var start = _pos;
            while (_pos < _input.Length && char.IsLetterOrDigit(Peek()))
            {
                _pos++;
            }
            var element = _input.Substring(start, _pos - start);
            return new Expression(ExpressionType.Element, element);
        }

        private Expression ParseParenthesizedExpression()
        {
            Expect('(');
            var expression = ParseExpression();
            Expect(')');
            SkipWhiteSpace();
            if (_pos < _input.Length && Peek() == '+')
            {
                return new Expression(ExpressionType.Add, subExpressions: ParseSubExpressions());
            }
            if (_pos < _input.Length && Peek() == '*')
            {
                return new Expression(ExpressionType.Multiply, subExpressions: ParseSubExpressions());
            }
            return expression;
        }

        private List<Expression> ParseSubExpressions()
        {
            var subExpressions = new List<Expression>();
            while (_pos < _input.Length && Peek() == '+')
            {
                Expect('+');
                subExpressions.Add(ParseExpression());
            }
            if (_pos < _input.Length && Peek() == '*')
            {
                Expect('*');
                subExpressions.Add(ParseExpression());
            }
            return subExpressions;
        }

        private char Peek()
        {
            if (index < input.Length - 1)
            {
                return input[index + 1];
            }
            else
            {
                return '\0';
            }
        }
    }


    public enum ExpressionType
    {
        Element,
        Add,
        Multiply
    }
}

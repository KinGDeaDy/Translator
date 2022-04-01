using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Translator
{
    public enum TokenType { Number, Variable, Function, Parenthesis, Operator, Comma, WhiteSpace, Negative };
    public struct Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public override string ToString() => $"{Type}: {Value}";

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
    public class Parser
    {
        
        private IDictionary<string, Operator> operators = new Dictionary<string, Operator>
        {
            ["+"] = new Operator { Name = "+", Precedence = 1 },
            ["-"] = new Operator { Name = "-", Precedence = 1 },
            ["*"] = new Operator { Name = "*", Precedence = 2 },
            ["/"] = new Operator { Name = "/", Precedence = 2 },
            ["&"] = new Operator { Name = "&", Precedence = 3 },
            ["|"] = new Operator { Name = "|", Precedence = 4 },
            ["!"] = new Operator { Name = "!", Precedence = 5, RightAssociative = true }

        };

        private bool CompareOperators(Operator op1, Operator op2)
        {
            return op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        }

        private bool CompareOperators(string op1, string op2) => CompareOperators(operators[op1], operators[op2]);

        private TokenType DetermineType(char ch)
        {
            if (char.IsLetter(ch))
                return TokenType.Variable;
            if (char.IsDigit(ch) || ch == '.')
                return TokenType.Number;
            if (char.IsWhiteSpace(ch))
                return TokenType.WhiteSpace;
            if (ch == ',')
                return TokenType.Comma;
            if (ch == '(' || ch == ')')
                return TokenType.Parenthesis;
            if (operators.ContainsKey(Convert.ToString(ch)))
                return TokenType.Operator;

            throw new Exception("Wrong character");
        }

        public IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();

            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                var currType = DetermineType(ch);
                if (currType == TokenType.WhiteSpace)
                    continue;

                token.Append(ch);

                var next = reader.Peek();
                var nextType = next != -1 ? DetermineType((char)next) : TokenType.WhiteSpace;
                if (next == '.')
                    continue;
                if ((currType == TokenType.Operator && (char)next == '!') || (ch == '!') && nextType == TokenType.Operator)
                {
                    yield return new Token(currType, token.ToString());
                    token.Clear();
                    continue;
                }
                if (currType != nextType)
                {
                    if (next == '(')
                        yield return new Token(TokenType.Function, token.ToString());
                    else
                        yield return new Token(currType, token.ToString());
                    token.Clear();
                }
            }
        }

        public IEnumerable<Token> ShuntingYard(IEnumerable<Token> tokens)
        {

            var stack = new Stack<Token>();
            foreach (var tok in tokens)
            {
                switch (tok.Type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        yield return tok;
                        break;
                    case TokenType.Function:
                        stack.Push(tok);
                        break;
                    case TokenType.Comma:
                        while (stack.Peek().Value != "(")
                            yield return stack.Pop();
                        break;
                    case TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == TokenType.Operator && CompareOperators(tok.Value, stack.Peek().Value))
                            yield return stack.Pop();
                        stack.Push(tok);
                        break;

                    case TokenType.Parenthesis:
                        if (tok.Value == "(")
                            stack.Push(tok);
                        else
                        {
                            while (stack.Peek().Value != "(")
                                yield return stack.Pop();
                            stack.Pop();
                            if (stack.Count != 0)
                            {
                                if (stack.Peek().Type == TokenType.Function)
                                    yield return stack.Pop();
                            }
                        }
                        break;
                    default:
                        throw new Exception("Wrong token");
                }
            }
            while (stack.Any())
            {
                var tok = stack.Pop();
                if (tok.Type == TokenType.Parenthesis)
                    throw new Exception("Mismatched parentheses");
                yield return tok;
            }
        }
        public double evaluatePostfix(string exp)
        {
            // create a stack
            Stack<double> stack = new Stack<double>();

            // Scan all characters one by one
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];

                if (c == ' ')
                {
                    continue;
                }

                // If the scanned character is an 
                // operand (number here),extract
                // the number. Push it to the stack.
                else if (char.IsDigit(c) || c == '.')
                {
                    double n = 0;
                    bool isDouble = false;
                    int countDouble = 0;
                    // extract the characters and
                    // store it in num
                    while (char.IsDigit(c) || c == '.')
                    {
                        if (c == '.')
                        {
                            isDouble = true;
                            i++;
                            c = exp[i];
                        }
                        else
                        {
                            n = n * 10 + (int)(c - '0');
                            i++;
                            c = exp[i];
                            if (isDouble)
                                countDouble++;
                        }
                    }
                    n /= Math.Pow(10, countDouble);
                    i--;
                    // push the number in stack
                    stack.Push(n);
                }

                // If the scanned character is
                // an operator, pop two elements
                // from stack apply the operator

                else
                {
                    double val1;
                    double val2;
                    switch (c)
                    {
                        case '+':
                            val1 = stack.Pop();
                            val2 = stack.Pop();
                            stack.Push(val2 + val1);
                            break;

                        case '-':
                            val1 = stack.Pop();
                            val2 = 0;
                            if (stack.Count > 0)
                                val2 = stack.Pop();
                            stack.Push(val2 - val1);
                            break;

                        case '/':
                            val1 = stack.Pop();
                            val2 = stack.Pop();
                            stack.Push(val2 / val1);
                            break;

                        case '*':
                            val1 = stack.Pop();
                            val2 = stack.Pop();
                            stack.Push(val2 * val1);
                            break;

                        case '&':
                            val1 = stack.Pop();
                            val2 = stack.Pop();
                            stack.Push((int)Math.Truncate(val2) & (int)Math.Truncate(val1));
                            break;

                        case '|':
                            val1 = stack.Pop();
                            val2 = stack.Pop();
                            stack.Push((int)Math.Truncate(val2) | (int)Math.Truncate(val1));
                            break;
                        case '!':
                            val1 = stack.Pop();
                            stack.Push(~(int)Math.Truncate(val1));
                            break;
                    }
                }
            }
            return stack.Pop();
        }
    }
}

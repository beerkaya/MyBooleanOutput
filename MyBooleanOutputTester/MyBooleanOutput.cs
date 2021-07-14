using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBooleanOutputTester
{
    public partial class MyBooleanOutput : Component
    {
        public MyBooleanOutput()
        {
        }

        public MyBooleanOutput(IContainer container)
        {
            container.Add(this);
        }

        #region Properties

        public bool Result { get; private set; }
        public string Equation { private get; set; } = null;

        #endregion

        #region Methods

        public void SetEquation(string equation)
        {
            this.Equation = equation;
        }

        public void SetParameters(string parameterNames, params int[] parameterValues)
        { 
            if (Equation == null)
                throw new Exception("Equation bos olamaz.");

            for (int i = 0; i < parameterNames.Length; i++)
            {
                Equation = Equation.Replace(parameterNames[i], Convert.ToChar(parameterValues[i].ToString()));
            }
        }

        public bool GetResult()
        {
            if (Equation == null)
                throw new Exception("Equation bos olamaz.");

            string result = Postfix(Equation);
            return result == "1";
        }

        public bool GetResult(string equation)
        {
            if (Equation == null)
                throw new Exception("Equation bos olamaz.");

            string result = Postfix(equation);
            return result == "1";
        }

        private string Postfix(string infix)
        {
            if (infix.Contains('(') && infix.Contains(')'))
            {
                Regex regex = new Regex(@"\([\d\.]+\)");
                MatchCollection collection = regex.Matches(infix);
                for (int i = 0; i < collection.Count; i++)
                {
                    string replacement = infix.Substring(collection[0].Index + 1, collection[0].Value.Length - 2);
                    infix = infix.Replace($"({replacement})", Postfix(replacement));
                    collection = regex.Matches(infix);
                }
            }
            else if (infix.Contains('.') && infix.Contains('+'))
                throw new Exception("İki ayrı operator kullanılıyorsa parantez kullanılmalıdır.");

            Stack<int> numbers = new();
            Stack<char> operators = new();

            foreach (var token in infix)
            {
                if (char.IsDigit(token)) numbers.Push(token);
                else if (IsOperator(token)) operators.Push(token);
            }

            return Calculate(numbers, operators);
        }

        private string Calculate(Stack<int> numbers, Stack<char> operators)
        {
            int res = numbers.Pop(); 
            for (int i = 0; i < numbers.Count + operators.Count; i++)
            {
                char op = operators.Pop();

                if (op == '+')
                {
                    int num1 = numbers.Pop();
                    res = num1 == 49 || res == 49 ? 49 : 48;
                }
                else if (op == '.')
                {
                    int num1 = numbers.Pop();
                    res = num1 == 49 && res == 49 ? 49 : 48;
                }
            }
            return ((char)res).ToString(); // 49 "1" 48 "0"
        }

        private bool IsOperator(char opt)
        {
            string operators = "+.";
            return operators.Contains(opt);
        }

        #endregion
    }
}
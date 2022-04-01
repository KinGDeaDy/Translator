using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Translator
{
    public partial class Form1 : Form
    {
        bool hasOperator = false;
        Dictionary<string, double> operators = new Dictionary<string, double>();
        int currentWord=0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            operators.Clear();
            currentWord = 0;
            debugTextBox.Clear();
            ArrayList words = parseText();
            if (analyzeLanguage(words))
            {
                if (analyzeTerm(words))
                {
                    if (analyzeOperator(words))
                    {
                        if (words.Count-currentWord-1>0)
                        {
                            debugTextBox.Text = "Analyzer Error! Программа должна заканчиваться словом 'Окончание'";
                            selectText(words, inputTextBox.Text, words[currentWord+1].ToString(), currentWord+1);
                        }
                        else
                        {
                            debugTextBox.Text = "Результаты вычислений:" + Environment.NewLine;
                            foreach (var i in operators)
                                debugTextBox.Text += i.Key + " := " + i.Value + Environment.NewLine;
                        }
                        
                    }
                }
            }
        }
        public double computeExp(ArrayList words)
        {
            string text="";
            foreach (var i in words)
                text += i.ToString() + " ";
            text=text.Remove(text.Length - 1);
            if (words.Count == 1)
                return Double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
            using (var reader = new StringReader(text))
            {
                var parser = new Parser();
                var tokens = parser.Tokenize(reader).ToList();
                var rpn = parser.ShuntingYard(tokens);
                return parser.evaluatePostfix(string.Join(" ", rpn.Select(t => t.Value)));
            }
        }
        public int findEntriesNum(string word, ArrayList words, int position)
        {
            int number = -1;
            for (int i = 0; i <= position; i++)
            {
                if (words[i].ToString() == word)
                    number++;
            }
            return number;
        }
        public void selectText(ArrayList words, string text, string substring, int position)
        {
            try
            {
                List<int> indexes = new List<int>();
                if (substring == ";" || substring == "-" || substring == ":" || substring == "+" || substring == ")" || substring == "(" || substring == "&" || substring == "|")
                {

                    int index = 0;
                    if (substring.Length == 1 && getType(substring) == "переменная")
                    {
                        while ((index = text.IndexOf(substring, index)) != -1)
                        {
                            indexes.Add(index);
                            index += substring.Length;
                        }
                    }
                    else
                    {
                        while ((index = text.IndexOf(substring, index)) != -1)
                        {
                            indexes.Add(index);
                            index += substring.Length;
                        }
                    }
                }
                else
                {
                    int i = 0;
                    while (i < text.Length - 1)
                    {
                        string word = "";
                        if (text[i] == ' ')
                        {
                            while (text[i] == ' ')
                            {
                                if (i == text.Length - 1)
                                    break;
                                i++;

                            }
                        }
                        while (text[i] != ' ')
                        {
                            word += text[i];
                            i++;
                            if (i == text.Length)
                                break;
                        }
                        if (word == substring)
                        {
                            indexes.Add(i - word.Length);
                        }
                    }
                }
                inputTextBox.SelectionStart = indexes[findEntriesNum(substring, words, position)];
                inputTextBox.SelectionLength = substring.Length;
            }
            catch
            {

            }
            
        }
        public string getType(string word)
        {
            if (Regex.IsMatch(word, "^\\d+$"))
                return "целое число";
            if (Regex.IsMatch(word, "^\\d+\\.\\d+$"))
                return "вещественное число";
            if (Regex.IsMatch(word, "^[А-я]{1}\\d{0,3}$"))
                return "переменная";
            //if (Regex.IsMatch(word, "^\\*$|^\\/$|^&$|^\\|$|^!$|^\\+$|^-$|^\\($|^\\)$"))
            //    return "операция";
            if (Regex.IsMatch(word, "^Начало$|^Окончание$|^Анализ$|^Синтез$"))
                return "терминал";
            if (word == "=")
                return "знак равенства";
            if (word == ":")
                return "двоеточие";
            if (word == ";")
                return "\";\"";
            if (word == "-")
                return "операция вычитания";
            if (word == "+")
                return "операция сложения";
            if (word == "*")
                return "операция умножения";
            if (word == "/")
                return "операция деления";
            if (word == "&")
                return "конъюнкция";
            if (word == "|")
                return "дизъюнкция";
            if (word == "!")
                return "операция отрицания";
            if (word == "(" || word == ")")
                return "скобки";
            return "неизвестный терминал";
        }    
        public ArrayList parseText()
        {
            var rx = new Regex(@"\s+", RegexOptions.Compiled);
            ArrayList words = new ArrayList(rx.Split(inputTextBox.Text.Replace("-", " - ").Replace("+", " + ").Replace("/", " / ").Replace("*", " * ").Replace("&", " & ").Replace("|", " | ").Replace("!", " ! ").Replace("(", " ( ").Replace(")", " ) ").Replace(";", " ; ").Replace(",", " , ").Replace(":=", " := ").Replace("=", " = ")));
            inputTextBox.Clear();
            words.Remove("");
            foreach (var i in words)
                inputTextBox.Text += i+" ";
            return words;
        }
        public bool analyzeLanguage(ArrayList words)
        {
            if (words.Count == 0)
                debugTextBox.Text = "Parser Error! На вход подана пустая программа.";
            else
            {
                if (words[currentWord].ToString() == "Начало")
                {
                    if (words.Count == currentWord+1)
                    {
                        debugTextBox.Text = "Analyzer Error! После ключевого слова 'Начало' ожидается слагаемое.";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                    currentWord++;
                    return true;
                }                 
                else
                {
                    debugTextBox.Text = "Analyzer error! Программа должна начинаться со слова 'Начало'";
                    selectText(words, inputTextBox.Text, words[0].ToString(), 0);
                }      
            }
            return false;
        }
        public bool analyzeTerm(ArrayList words)
        {
            while (true)
            {
                if (words[currentWord].ToString() == "Анализ")
                {
                    //Начало Анализ ___
                    if (words.Count == currentWord + 1)
                    {
                        debugTextBox.Text = "Analyzer Error! После ключевого слова 'Анализ' ожидается переменная";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                    else
                    {
                        if (getType(words[currentWord + 1].ToString()) == "переменная")
                        {
                            currentWord++;
                            //Начало Анализ а123
                            if (words.Count == currentWord + 1)
                            {
                                debugTextBox.Text = "Analyzer Error! После слагаемых ожидается либо очередное слагаемое либо оператор";
                                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                return false;
                            }
                            currentWord++;
                            //Начало Анализ а123 |________
                            if (words[currentWord].ToString() != ";" && getType(words[currentWord].ToString()) != "переменная")
                            {
                                debugTextBox.Text = "Analyzer Error! После слагаемых не может идти " + getType(words[currentWord].ToString()) + " " + words[currentWord].ToString();
                                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                return false;
                            }
                            if (getType(words[currentWord].ToString()) == "переменная" && getType(words[currentWord - 1].ToString()) == "переменная")
                                return true;
                            if (words[currentWord].ToString() == ";" && words.Count > currentWord + 1 && (words[currentWord + 1].ToString() == "Анализ" || words[currentWord + 1].ToString() == "Синтез"))
                                currentWord++;
                            if (words[currentWord].ToString() == ";" && words.Count == currentWord + 1)
                            {
                                debugTextBox.Text = "Analyzer Error! После \";\" должно идти слагаемое";
                                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                return false;
                            }
                        }
                        //Начало Анализ xxxx
                        else
                        {
                            debugTextBox.Text = "Analyzer Error! После ключевого слова 'Анализ' не может идти " + getType(words[currentWord + 1].ToString()) + " " + words[currentWord + 1].ToString();
                            selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                            return false;
                        }
                    }
                }
                else
                {
                    if (words[currentWord].ToString() == "Синтез")
                    {
                        //Начало Анализ ___
                        if (words.Count == currentWord + 1)
                        {
                            debugTextBox.Text = "Analyzer Error! После ключевого слова 'Синтез' ожидается переменная";
                            selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                            return false;
                        }
                        else
                        {
                            if (getType(words[currentWord + 1].ToString()) == "переменная")
                            {
                                currentWord++;
                                while (words[currentWord].ToString() !=";")
                                {
                                    //Начало Анализ а123
                                    if (words.Count == currentWord + 1)
                                    {
                                        debugTextBox.Text = "Analyzer Error! После слагаемых ожидается либо очередное слагаемое либо оператор";
                                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                        return false;
                                    }
                                    if (words.Count == currentWord + 1 && words[currentWord-1].ToString()==",")
                                    {
                                        debugTextBox.Text = "Analyzer Error! После \",\" ожидалась переменная";
                                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                        return false;
                                    }
                                    else
                                    {
                                        currentWord++;
                                        if (words[currentWord].ToString() == ";")
                                        {
                                            if (words.Count==currentWord+1)
                                            {
                                                debugTextBox.Text = "Analyzer Error! После \";\" ожидалось очередное слагаемое";
                                                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                                return false;
                                            }
                                            else
                                                break;
                                        }
                                            
                                        if (words[currentWord].ToString() == ",")
                                        {
                                            if (words.Count != currentWord + 1)
                                                currentWord++;
                                            else
                                            {
                                                debugTextBox.Text = "Analyzer Error! После \",\" ожидалась переменная";
                                                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (getType(words[currentWord].ToString()) == "переменная" && getType(words[currentWord - 1].ToString()) == "переменная")
                                                return true;

                                            debugTextBox.Text = "Analyzer Error! После слагаемых ожидается либо очередное слагаемое либо оператор либо переменная";
                                            selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                            return false;
                                        }
                                    }
                                    if (words[currentWord].ToString() == ";" && words.Count > currentWord + 1 && (words[currentWord + 1].ToString() == "Анализ" || words[currentWord + 1].ToString() == "Синтез"))
                                        currentWord++;
                                    if (words[currentWord].ToString() == ";" && words.Count == currentWord + 1)
                                    {
                                        debugTextBox.Text = "Analyzer Error! После \";\" должно идти слагаемое";
                                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                        return false;
                                    }

                                }
                                currentWord++;
                            }
                            //Начало Анализ xxxx
                            else
                            {
                                debugTextBox.Text = "Analyzer Error! После ключевого слова 'Синтез' не может идти " + getType(words[currentWord + 1].ToString()) + " " + words[currentWord + 1].ToString();
                                selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (words[currentWord-1].ToString()=="Начало")
                        {
                            debugTextBox.Text = "Analyzer Error! После ключевого слова \"Начало\" не может идти " + getType(words[currentWord].ToString()) + " " + words[currentWord].ToString();
                            selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                            return false;
                        }
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord-1].ToString()) + " не может идти " + getType(words[currentWord].ToString()) + " " + words[currentWord].ToString();
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                }
            }
        }
        public bool analyzeExp(ArrayList words)
        {
            ArrayList words_calc = (ArrayList)words.Clone();
            hasOperator = false;
            string[] operations = new string[] { "+", "-", "*", "/", "&", "|" };
            int startPos = currentWord;
            while (true)
            {
                if (getType(words[currentWord].ToString()) == "неизвестный терминал")
                {
                    debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord - 1].ToString()) + " не может " +
                        "идти " + getType(words[currentWord].ToString() + " " + words[currentWord].ToString());
                    selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                    return false;
                }
                if (words[currentWord].ToString() == ":" || words[currentWord].ToString() == "=" || words[currentWord].ToString() == ";" || getType(words[currentWord].ToString()) == "целое число")
                {
                    debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord - 1].ToString()) + " не может идти " + getType(words[currentWord].ToString());
                    selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                    return false;
                }
                if (getType(words[currentWord].ToString()) == "вещественное число")
                    hasOperator = true;
                //проверка если это оператор
                if (getType(words[currentWord].ToString()) == "переменная")
                {
                    if (!(operations.Contains(words[currentWord - 1].ToString())) && words[currentWord - 1].ToString() != ":")
                    {
                        currentWord--;
                        break;
                    }    
                    //наличие инициализации
                    if (!(operators.ContainsKey(words[currentWord].ToString())))
                    {
                        debugTextBox.Text = "Analyzer Error! Использована неинициализированная переменная " + words[currentWord].ToString();
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                    //подстановка значения
                    else
                    {
                        words_calc[currentWord] = operators[words[currentWord].ToString()].ToString().Replace(",", ".");
                        hasOperator = true;
                    }        
                }
                if (words.Count - (currentWord + 1) > 0)
                {
                    //две операции вместе
                    if (operations.Contains(words[currentWord].ToString()) && operations.Contains(words[currentWord + 1].ToString()))
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    //! и -
                    if (words[currentWord].ToString() == "!" && words[currentWord + 1].ToString() == "-")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    //два числа вместе
                    if (getType(words[currentWord].ToString()) == "вещественное число" && getType(words[currentWord + 1].ToString()) == "вещественное число")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    //два отрицания
                    if (words[currentWord].ToString() == "!" && words[currentWord + 1].ToString() == "!")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    // операция и )
                    if (operations.Contains(words[currentWord].ToString()) && words[currentWord + 1].ToString() == ")")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    // ( и */&| 
                    if (words[currentWord].ToString() == "(" && (words[currentWord+1].ToString() == "*" || words[currentWord + 1].ToString() == "/" || words[currentWord + 1].ToString() == "&" || words[currentWord + 1].ToString() == "|"))
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    //вещ и скобка
                    if (getType(words[currentWord].ToString()) == "вещественное число" && words[currentWord + 1].ToString() == "(")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " не может идти " + getType(words[currentWord + 1].ToString());
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    // - Терминал
                    if ((operations.Contains(words[currentWord].ToString()) || words[currentWord].ToString()=="!") && getType(words[currentWord+1].ToString()) == "терминал")
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " ожидалась переменная, вещественное число, скобки";
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    //(пустые скобки)
                    if (words[currentWord].ToString() == "(" && words[currentWord+1].ToString() == ")")
                    {
                        debugTextBox.Text = "Analyzer Error! Обнаружены пустые скобки";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        inputTextBox.SelectionLength = 3;
                        return false;
                    }
                }
                //Начало Анализ а1 а1 = : +
                else
                {
                    //на конце операция
                    if (operations.Contains(words[currentWord].ToString()))
                    {
                        debugTextBox.Text = "Analyzer Error! После " + getType(words[currentWord].ToString()) + " ожидалоcь либо переменная, либо вещественное число, либо операция инвертирования";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                    //Окончание должно быть
                    if (words[currentWord].ToString()!="Окончание")
                    {
                        debugTextBox.Text = "Analyzer Error! После оператора ожидается либо очередной оператор, либо ключевое слово 'Окончание'";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    } 
                    break;
                }
                if (words[currentWord].ToString() == "Окончание")
                    break;
                currentWord++;
            }
            if (!hasOperator && words[currentWord].ToString() == "Окончание")
            {
                debugTextBox.Text = "Analyzer Error! После " +getType(words[currentWord-1].ToString()) +" не может идти ключевое слово 'Окончание'";
                selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                return false;
            }
            int endPos = currentWord;
            string s = "";
            if (startPos != endPos)
            {
                for (int i = startPos; i < endPos; i++)
                    s += words[i].ToString() + " ";
                s = s.Remove(s.Length - 1);
            }
            else
                s = words[startPos].ToString();

            //считаем выражение
            if (!(BracketCheck(words_calc.GetRange(startPos, endPos - startPos + 1))))
            {
                debugTextBox.Text = "Analyzer Error! Неправильная расстановка скобок!";
                inputTextBox.SelectionStart = inputTextBox.Text.IndexOf(s);
                inputTextBox.SelectionLength = s.Length;
                return false;
            }
            try
            {
                if (operators.ContainsKey(words_calc[startPos - 3].ToString()))
                    operators[words_calc[startPos - 3].ToString()] = computeExp(words_calc.GetRange(startPos, endPos - startPos + 1));
                else
                    operators.Add(words_calc[startPos - 3].ToString(), computeExp(words_calc.GetRange(startPos, endPos - startPos + 1)));
                if (hasOperator == true && words[currentWord].ToString() == "Окончание")
                    return true;
            }
            catch
            {
                debugTextBox.Text = "Analyzer Error! Невозможно посчитать выражение, пустые скобки!";
                inputTextBox.SelectionStart = inputTextBox.Text.IndexOf(s);
                inputTextBox.SelectionLength = s.Length;
                return false;
            }
            return true;
        }
        public bool analyzeOperator(ArrayList words)
        {
            while (true)
            {
                if (words.Count - (currentWord + 1) > 0)
                {
                    if (words[currentWord + 1].ToString() != "=")
                    {
                        debugTextBox.Text = "Analyzer Error! После переменной " + words[currentWord] + " ожидается знак равенства";
                        selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                        return false;
                    }
                    else
                    {
                        //каретка на знаке равества
                        currentWord++;
                        if (words.Count - (currentWord + 1) == 0)
                        {
                            debugTextBox.Text = "Analyzer Error! После знака равенства ожидается двоеточие";
                            selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                            return false;
                        }
                        if (words.Count - (currentWord + 1) > 0)
                        {
                            if (words[currentWord + 1].ToString() != ":")
                            {
                                debugTextBox.Text = "Analyzer Error! После знака равества ожидалось двоеточие";
                                selectText(words, inputTextBox.Text, words[currentWord + 1].ToString(), currentWord + 1);
                                return false;
                            }
                            else
                            {
                                //каретка на двоеточии
                                currentWord++;
                                if (words.Count - (currentWord + 1) == 0)
                                {
                                    debugTextBox.Text = "Analyzer Error! Выражение должно начинаться либо с вещественного числа, либо с переменной," +
                                        "либо с операции отрицания, либо с операции инвертирования";
                                    selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                                    return false;
                                }
                                else
                                {
                                    // Каретка на выражении
                                    currentWord++;
                                    // обработка выражения
                                    if (analyzeExp(words))
                                    {
                                        if (words[currentWord].ToString() == "Окончание")
                                            return true;
                                        else
                                            currentWord++;
                                    }
                                    else
                                        return false;
                                }
                                //перед переводом на след цикл сделать проверку на переменную и Окончание
                            }
                        }
                    }
                }
                else
                {
                    if (words.Count - (currentWord + 1) == 0)
                    {
                        debugTextBox.Text = "Analyzer Error! После переменной " + words[currentWord] + " ожидается знак равенства";
                        selectText(words, inputTextBox.Text, words[currentWord].ToString(), currentWord);
                        return false;
                    }
                }
            }   
        }
        public bool BracketCheck(ArrayList words)
        {
            string s = "";
            foreach (var i in words)
                s += i.ToString() + " ";
            s = s.Remove(s.Length - 1);
            string t = "[{(]})";
            Stack<char> st = new Stack<char>();

            foreach (var x in s)
            {
                int f = t.IndexOf(x);

                if (f >= 0 && f <= 2)
                    st.Push(x);

                if (f > 2)
                {
                    if (st.Count == 0 || st.Pop() != t[f - 3])
                        return false;
                }
            }

            if (st.Count != 0)
                return false;

            return true;
        }

    }
}
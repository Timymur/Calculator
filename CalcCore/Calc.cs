

namespace CalcCore
{
    // All the code in this file is included in all platforms.
    public class Calc
    {
        List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public double Display { get; private set; } // Выводит на экран текущее значение
        public string preDisplay { get; private set; } = ""; // Выводит на экран последние действия

        string strDisplay; // Вспомогательная переменная для дробных чисел, обнуляется при равно либо при нажатии на операцию. равно Дисплей

        private char? _operation = null;  // Переменная которая вычисляется сразу, без равно
        private double? _operand1 = null;
        private double? _operand2 = null;
       

        public double? getOperand1()
        {
            return _operand1;
        }
        public double? getOperand2()
        {
            return _operand2;
        }


        public char? getOperation()
        {
            return _operation;
        }


        private void comma(char argument)
        {
            if (strDisplay.Contains(',')) return; // Если число дробное, то ничего не делаем

            strDisplay += argument.ToString(); // добавляем вспомогательной переменной запятую, следующее введенное число выведет на дисплей дробь
            preDisplay += argument.ToString();
        }

        private void cancel()
        {
            preDisplay = Display.ToString();


            if (preDisplay.Length > 0) // Если на экране еще есть числа
            {
                preDisplay = preDisplay.Remove(preDisplay.Length - 1); // Удаляем последний элемент массива

                if (preDisplay == "-" || preDisplay.Length == 0) //Если остался только минус, либо ничего на экране нет, то на дисплей выводи 0
                {
                    preDisplay = " ";
                    Display = 0;
                    strDisplay = Display.ToString();
                    return;
                }

                Display = Convert.ToDouble(preDisplay);// Иначе возвращаем измененное значение, без последнего элемента
                strDisplay = Display.ToString();
            }


        }

        private void reset()
        {
            preDisplay = "";
            Display = 0;
            _operand1 = null;
            _operand2 = null;
            _operation = null;
            strDisplay = "";

        }

        private void setOperation(char argument)
        {

            if (_operand1 != null) calculate(); 
          // Если операнд1  не равен null, значит что вычисления еще не проводилось, при повторном вводе операции вызывается метод вычисления  с предыдущей операцией

            _operand1 = Display; // Перезаписали операнд1
            _operation = argument; // перезаписываем новую операцию

            preDisplay = Display.ToString() + _operation; // на экран добавили операцию
            Display = 0; // Обнулили дисплей
            strDisplay = Display.ToString(); // Обнулили строковый дисплей

            return;
        }

        private void calculate()
        {
            if (_operation == null) return; //Eсли нет операции, то ничего не делаем

            if (_operand1 == null)// Если есть повторное равно, то используем операнд 2
            {
                preDisplay = Display.ToString();


                if (_operation == '+') Display += _operand2.Value;

                if (_operation == '-') Display -= _operand2.Value;

                if (_operation == '*') Display *= _operand2.Value;


                if (_operation == '^') Display = Math.Pow(Display, _operand2.Value);

                if (_operation == '/')
                {
                    if (_operand2 == 0)
                    {
                        Display = 0;
                        preDisplay = " ";
                        return;
                    }

                    Display /= _operand2.Value;
                }


                preDisplay += "" + _operation + "" + _operand2.ToString() + "=" + Display.ToString();

                strDisplay = Display.ToString();

                return;
            }

            // Если не было потворного равно то 
            _operand2 = Display;// Перезаписываем операнд2

            if (_operation == '+') Display += _operand1.Value;

            if (_operation == '-') Display = _operand1.Value - Display;

            if (_operation == '*') Display *= _operand1.Value;


            if (_operation == '^') Display = Math.Pow(_operand1.Value, Display);

            if (_operation == '/')
            {
                if (_operand2 == 0)
                {
                    Display = 0;
                    preDisplay = "Ай-ай-ай";
                    return;
                }
                Display = _operand1.Value / Display;
            }

            preDisplay += "=" + Display.ToString();
            strDisplay = Display.ToString();

            _operand1 = null;

            return;
        }

        private void plusMinus() 
        {

            if (Display == 0) return;

            strDisplay = Display.ToString();

            var l = preDisplay.Length - strDisplay.Length; // индекс, куда нужно вставить -

            if (Display < 0) preDisplay = preDisplay.Remove(l, 1);

            else             preDisplay = preDisplay.Insert(l, "-");

            Display = Display * -1; // меняем дисплей на отрицательный
            strDisplay = Display.ToString(); // меняем строковый дисплей на отрицательный
        }
       
        public void Input(char argument)
        {




            if (argument == 'C') 
            {
                reset();
                return;
            }

            if (argument == '←')
            {
                cancel();
                return;
            }

            if (argument == '=')
            {
                calculate(); 
                return;
            }


            if (argument == ',')
            {
                comma(argument); 
                return;
            }

            if (argument == '±')
            {
                plusMinus();
                return;

            }

            if (argument == '+' || argument == '-' || argument == '*' || argument == '/' || argument == '^')
            {
                setOperation(argument);
                return;
            }



            

            if (numbers.Contains(argument.ToString())) {

                preDisplay += argument.ToString();  // На экран добавляем нажатую клавишу

                strDisplay += argument.ToString(); // Строковому дисплею добавляем нажатую клавишу, может хранится с запятой на конце, поэтому она стринг
                Display = Convert.ToDouble(strDisplay);// Переводим строковый дисплей в обычный Дисплей


            }

            

        }

    }
}

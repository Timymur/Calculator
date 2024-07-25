

namespace CalcCore
{
    // All the code in this file is included in all platforms.
    public class Calc
    {
        

        public double Display { get; private set; } // Выводит на экран текущее значение
        public string preDisplay { get; private set; } = ""; // Выводит на экран последние действия

        string strDisplay; // Вспомогательная переменная для дробных чисел, обнуляется при равно либо при нажатии на операцию. равно Дисплей


        private char? _operation = null;  // Переменная которая вычисляется сразу, без равно
        private bool _isOperationExist = false; // Вспомогательная переменная для вычесления по введенной операции. Устанавливается при вводе первой операции, стирается после равно. обычная operation всегда будет иметь значение, после первого ввода
        private double? _operand1 = null;
        private double? _operand2 = null;
        private bool _shouldTwiceEqual = false; // флаг для повторного равно
        private bool _comma = false; // флаг для записи десятичной части, для проверки на повторную запятую
        private bool _plusMinus = false; // флаг для унарного минуса


        public double? getOperand1()
        {
            return _operand1;
        }
        public double? getOperand2()
        {
            return _operand2;
        }

        public bool getComma()
        {
            return _comma;
        }
        public char? getOperation()
        {
            return _operation;
        }


        private void comma(char argument)
        {
            if (_comma) return; // Ксли есть запятая, то ничего не делаем
            _comma = true;
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
            _plusMinus = false;

        }

        private void setOperation(char argument)
        {
            if (!_isOperationExist)  // Если еще не было введенной операции
            {
                
                _operand1 = Display; // записываем операнд1
                _operation = argument; // записываем операцию
                _isOperationExist = true; // операция уже есть

                preDisplay = _operand1.ToString() + argument.ToString(); // на экран добавили операцию
                strDisplay = ""; // Обнуляем вспом. переменную
                Display = 0; // обнулили дисплей для ввода второго операнда

                _shouldTwiceEqual = false; // После ввода операции не может быть повторного равно
                _comma = false;// После ввода оперции не может быть второй запятой
                
                
                return;
            }
            else // Если уже есть операция
            {
                calculate(); // Вызываем метод выичсления, с предыдущей операцией

                _shouldTwiceEqual = false; // Повторное равно не может быть
                _operation = argument; // перезаписываем новую операцию
                _isOperationExist = true; // операция есть

                preDisplay = Display.ToString() + _operation; // на экран добавили операцию
                _operand1 = Display; // Перезаписали операнд1

                Display = 0; // Обнулили дисплей
                strDisplay = Display.ToString(); // Обнулили строковый дисплей

                return;

            }


        }

        private void calculate()
        {
            if (_operation == null) return; //Eсли нет операции, то ничего не делаем
            if (_shouldTwiceEqual)// Если есть повторное равно, то используем операнд 2
            {
                _operand1 = Display;
                if (_operation == '+') Display = _operand1.Value + _operand2.Value;
 
                if (_operation == '-') Display = _operand1.Value - _operand2.Value;

                if (_operation == '*') Display = _operand1.Value * _operand2.Value;


                if (_operation == '^') Display = Math.Pow(_operand1.Value, _operand2.Value);

                if (_operation == '/')
                {
                    if (_operand2 == 0)
                    {
                        Display = 0;
                        preDisplay = " ";
                        return;
                    }
                    
                    Display = _operand1.Value / _operand2.Value;
                }

                preDisplay = _operand1.ToString() + _operation + _operand2.ToString() + "=" + Display.ToString();

                _isOperationExist = false; // Снимаем флаг с операции. операция хранится для повторного равно. При следующем вводе операции метод выичсления вызываться не будет
                strDisplay = Display.ToString();
                return;
            }

                                // Если не было потворного равно то 
            _operand2 = Display;// Перезаписываем операнд2

            if (_operation == '+') Display += _operand1.Value;

            if (_operation == '-')  Display = _operand1.Value - Display;

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


            _operand1 = Display;
            preDisplay += "=" + Display.ToString();

            _shouldTwiceEqual = true; // Зписали равно 
            
            strDisplay = Display.ToString();
            _isOperationExist = false; // Снимаем флаг с операции. операция хранится для повторного равно. При следующем вводе операции метод выичсления вызываться не будет
            return;
        }

        private void plusMinus() // унарный минус
        {
            if (_plusMinus)// Если есть флаш удаляем минус с предисплея
            {
                _plusMinus = false;
                var l = preDisplay.Length - strDisplay.Length; // индекс, по которому находится -
                preDisplay = preDisplay.Remove(l, 1);

            }

            else { 
                _plusMinus = true;
                var l = preDisplay.Length - strDisplay.Length; // индекс, куда нужно вставить -
                var m = "-";
                preDisplay = preDisplay.Insert(l, m);
            }
            
            Display = Display * -1; // меняем дисплей на отрицательный
            strDisplay = Display.ToString(); // меняем строковый дисплей на отрицательный
        }
        public void Input(char argument)
        {
            if (argument == 'C') //Сбрасываем все значения
            {
                reset();
                return;
            }

            if (argument == '←')// Убираем крайний элемент
            {
                cancel();
                return;
            }

            if (argument == '=')// Вычисления
            {
                calculate();
                _comma = false; // Второй запятой после равно не может быть
                strDisplay = ""; // обнуляем строковый Дисплей
                if(_operand2 != null) preDisplay += "; "; 
                return;
            }


            if (argument == ',')
            {
                comma(argument); // Проверка на повторную запятую
                return;
            }

            if (argument == '±')
            {
                plusMinus();
                return;

            }

            if (argument == '+' || argument == '-' || argument == '*' || argument == '/'  || argument == '^')
            {
                setOperation(argument);
                return;
            }


            preDisplay += argument.ToString();  // На экран добавляем нажатую клавишу

            strDisplay += argument.ToString(); // Строковому дисплею добавляем нажатую клавишу, может хранится с запятой на конце, поэтому она стринг
            Display = Convert.ToDouble(strDisplay);// Переводим строковый дисплей в обычный Дисплей

            return;
        }
        
    }
}

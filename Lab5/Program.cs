using System;

namespace Lab5
{
    class Program
    {
        private static readonly string[] ActionItems =
        {
            "",
            "Создать новый массив",
            "Назад"
        };

        private static readonly string[] Actions =
        {
            "Добавить элемент в конец массива",
            "Удалить столбец",
            "Добавть строки в конец массива"
        };

        #region MainInterfaceControl

        private static readonly string[] StartItems =
        {
            "Одномерный массив",
            "Двумерный массив",
            "Рваный массив",
            "Выход"
        };

        private static readonly string[] CreateArray =
        {
            "Заполнить вручную",
            "Заполнить массив случайно",
            "Назад"
        };

        public static int SelectorY(string[] items)
        {
            Console.CursorVisible = false;
            int top = Console.CursorTop, left = 2;
            int currentSelection = 0, previousSelection = 1;

            for (int i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.WriteLine(items[i]);
            }

            bool selected = false;
            do
            {
                #region OptionWriting

                Console.SetCursorPosition(left, top + previousSelection);
                Console.WriteLine(items[previousSelection]);

                Console.SetCursorPosition(left, top + currentSelection);
                {
                    var temp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = temp;
                }
                Console.WriteLine(items[currentSelection]);
                {
                    var temp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = temp;
                }

                #endregion

                previousSelection = currentSelection;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        currentSelection++;
                        break;
                    case ConsoleKey.UpArrow:
                        currentSelection--;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }

                if (currentSelection < 0)
                    currentSelection = items.Length - 1;
                else if (currentSelection == items.Length)
                    currentSelection = 0;
            } while (!selected);

            Console.CursorTop = top;
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < Console.WindowWidth - 1; j++)
                    Console.Write(" ");
                Console.Write("\n");
            }
            Console.CursorTop -= items.Length;

            Console.SetCursorPosition(left, top);
            Console.WriteLine(items[currentSelection]);

            Console.CursorTop++;
            Console.CursorVisible = true;
            return currentSelection;
        }

        #endregion

        #region Create

        public static void Create(ref int[] array, int doRandom)
        {
            //array = new int[0];
            switch (doRandom)
            {
                case 0:
                    Console.WriteLine("Введите элементы массива через пробел\n" +
                                      "Будут засчитаны только числа");
                    string input = Console.ReadLine();
                    array = Tasks.StringToArray(input);
                    break;

                case 1:
                    Console.WriteLine("Введите длину массива");
                    int length;
                    while (!int.TryParse(Console.ReadLine(), out length) || length >= 128)
                    {
                        Console.CursorTop--;
                        Console.WriteLine("Длинна должна быть целым числом меньше 128");
                    }
                    array = new int[length];
                    Tasks.Randomise(ref array);
                    break;
            }
            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(array));
        }

        public static void Create(ref int[][] array, int doRandom)
        {
            //array = new int[0][];
            switch (doRandom)
            {
                case 0:
                    Console.WriteLine("Введите элементы массива через пробел \nБудут засчитаны только числа");
                    string[] input = new string[1];
                    for (int i = 0;; i++)
                    {
                        input[i] = Console.ReadLine();
                        if (input[i] == "")
                            break;

                        string[] temp = new string[input.Length + 1];
                        input.CopyTo(temp, 0);
                        input = temp;
                    }
                    array = Tasks.StringToArray(input);
                    break;

                case 1:
                    Console.WriteLine("Введите количество строк массива");
                    int length;
                    while (!int.TryParse(Console.ReadLine(), out length) || length >= 128)
                    {
                        Console.CursorTop--;
                        Console.WriteLine("Длинна должна быть целым числом меньше 128");
                    }
                    array = new int[length][];

                    for (int i = 0; i < array.Length; i++)
                    {
                        Console.WriteLine("Введите длину {0} строки", i + 1);
                        int width;
                        while (!int.TryParse(Console.ReadLine(), out width) || width > 128)
                        {
                            Console.CursorTop--;
                            Console.WriteLine("Длинна должна быть целым числом меньше 128");
                        }
                        array[i] = new int[width];
                    }
                    Tasks.Randomise(ref array);
                    break;
            }
            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(array));
        }

        public static void Create(ref int[,] array, int doRandom)
        {
            //array = new int[0, 0];
            switch (doRandom)
            {
                case 0:
                    Console.WriteLine("Введите элементы массива через пробел\n" +
                                      "Будут засчитаны только числа");
                    string[] input = new string[1];
                    for (int i = 0;; i++)
                    {
                        input[i] = Console.ReadLine();
                        if (input[i] == "")
                            break;

                        string[] temp = new string[input.Length + 1];
                        input.CopyTo(temp, 0);
                        input = temp;

                    }
                    array = Tasks.JaggedTo2D(Tasks.StringToArray(input));
                    break;

                case 1:
                    Console.WriteLine("Введите количество строк массива");
                    int length;
                    while (!int.TryParse(Console.ReadLine(), out length) || length >= 128)
                    {
                        Console.CursorTop--;
                        Console.WriteLine("Длинна должна быть целым числом меньше 128");
                    }
                    Console.WriteLine("Введите количество столбцов массива");
                    int width;
                    while (!int.TryParse(Console.ReadLine(), out width) || width >= 128)
                    {
                        Console.CursorTop--;
                        Console.WriteLine("Длинна должна быть целым числом меньше 128");
                    }
                    array = new int[length, width];
                    Tasks.Randomise(ref array);
                    break;
            }
            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(array));
        }

        #endregion
        
        public static void Action(int index, int arrayType, ref int[] ar1D, ref int[,] ar2D, ref int[][] arJ)
        {
            switch (index)
            {
                case 0:
                    int temp = 0;

                    switch (arrayType)
                    {
                        case 0:
                            Console.WriteLine("Введите новый элемент");
                            while (!int.TryParse(Console.ReadLine(), out temp))
                            {
                                Console.CursorTop--;
                                Console.WriteLine("Новый элемент должен быть целым числом меньше  {0}", int.MaxValue);
                            }
                            ar1D = Tasks.Extend1Dim(ar1D, temp);
                            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(ar1D));
                            break;

                        case 1:
                            if (ar2D == null || ar2D.Length == 0)
                            {
                                Console.WriteLine("Нельзя удалить столбец из пустого массива\n");
                                break;
                            }
                            Console.WriteLine("Введите номер лишнго столбца");
                            while (!int.TryParse(Console.ReadLine(), out temp) || temp <= 0 || temp > ar2D.GetLength(1))
                            {
                                Console.CursorTop--;
                                Console.WriteLine("Номер столбца должен быть целым числом от 1 до {0}", ar2D.GetLength(1));
                            }

                            temp--;
                            ar2D = Tasks.Shrink2Dim(ar2D, temp);
                            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(ar2D));
                            break;

                        case 2:
                            int[][] newStuff = null;
                            Console.WriteLine("Как получить новые строки?");
                            temp = SelectorY(CreateArray);
                            Create(ref newStuff, temp);

                            arJ = Tasks.ExtendJagged(arJ, newStuff);
                            Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(arJ));
                            break;
                    }
                    break;

                case 1:
                    int action = SelectorY(CreateArray);
                    switch (arrayType)
                    {
                        case 0:
                            Create(ref ar1D, action);
                            break;
                        case 1:
                            Create(ref ar2D, action);
                            break;
                        case 2:
                            Create(ref arJ, action);
                            break;
                    }
                    break;
            }
        }

        static void Main(string[] args)
        {
            int[] ar1D = null;
            int[,] ar2D = null;
            int[][] arJ = null;
            Console.WriteLine(" ");
            do
            {
                int index = SelectorY(StartItems);
                if (index == 3)
                    break;

                ActionItems[0] = Actions[index];
                int action = 1;

                switch (index)
                {
                    case 0:
                        Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(ar1D));
                        break;
                    case 1:
                        Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(ar2D));
                        break;
                    case 2:
                        Console.WriteLine("\nВаш массив: \n{0}", Tasks.ArrayToString(arJ));
                        break;
                }

                do
                {
                    action = SelectorY(ActionItems);
                    Action(action, index, ref ar1D, ref ar2D, ref arJ);
                } while (action != 2);

                ActionItems[0] = "";

            } while (true);
            Console.Beep();
        }
    }
}
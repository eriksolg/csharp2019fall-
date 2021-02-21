using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Game!");
            MenuMain();
        }

        static void MenuMain()
        {
            var command = "";
            do
            {
                Console.WriteLine("Main menu");
                Console.WriteLine("====================");
                Console.WriteLine("A New Game");
                Console.WriteLine("O Options");
                Console.WriteLine("X Exit");
                
                Console.WriteLine("----------");
                Console.Write(">");

                // If left side equals null, return ""
                command = Console.ReadLine()?.Trim().ToUpper() ?? "";

                switch (command)
                {
                    case "A":
                        command = MenuGame();
                        break;
                    case "O":
                        Console.WriteLine("O was selected");
                        break;
                    case "":
                        Console.WriteLine("Please choose an action");
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("Unknown command!");
                        break;
                }

                if (command != "X")
                {
                    Console.WriteLine("Press any key to continue!");
                    Console.WriteLine();

                    Console.Clear();          
                }


            } while (command != "X");

            Console.WriteLine();
            Console.WriteLine("Good bye!");        
        }

        static String MenuGame()
        {
            var command = "";
            do
            {
                Console.WriteLine("Game menu");
                Console.WriteLine("====================");
                Console.WriteLine("A Game 1");
                Console.WriteLine("B Game 2");
                Console.WriteLine("R return to previous");
                Console.WriteLine("X Exit");
                
                Console.WriteLine("----------");
                Console.Write(">");

                // If left side equals null, return ""
                command = Console.ReadLine()?.Trim().ToUpper() ?? "";

                switch (command)
                {
                    case "A":
                        Console.WriteLine("A was selected");
                        break;
                    case "B":
                        Console.WriteLine("B was selected");
                        break;
                    case "":
                        Console.WriteLine("Please choose an action");
                        break;
                    case "X":
                    case "R":
                        break;
                    default:
                        Console.WriteLine("Unknown command!");
                        break;
                }

                if (command != "X" && command != "R")
                {
                    Console.WriteLine("Press any key to continue!");
                    Console.WriteLine();

                    Console.Clear();          
                }


            } while (command != "X" && command != "R");

            return command;
        }
    }
}
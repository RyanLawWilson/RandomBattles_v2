using System;
using System.Collections.Generic;
using System.Text;

namespace RandomBattles_v2
{
    // A class that takes care of showing and evaluating math problems
    public static class MathProblems
    {
        private static Random rand = new Random();
        private static int num1 { get; set;}
        private static int num2 { get; set; }
        private static string operation { get; set; }

        // Shows addition problems that get harder based on the number passed to the method.
        public static void ShowAdditionProblem(int difficulty)
        {
            num1 = rand.Next(0, 10 + difficulty);
            operation = "+";
            num2 = rand.Next(0, 10 + difficulty);
            Console.Write("\t" + num1 + " + " + num2 + " = ");
        }

        // Shows multiplication problems that get harder based on the number passed to the method.
        public static void ShowMultiplicationProblem(int difficulty)
        {
            num1 = rand.Next(0, 10 + difficulty);
            operation = "*";
            num2 = rand.Next(0, 10 + difficulty);
            Console.Write("\t" + num1 + " * " + num2 + " = ");
        }

        // Shows division problems that get harder based on the number passed to the method.
        public static void ShowDivisonProblem(int difficulty)
        {
            num1 = rand.Next(0, 20 + difficulty);
            operation = "/";
            num2 = rand.Next(0, 5);
            Console.Write("\t" + num1 + " / " + num2 + " = ");
        }

        // Given num1 and num2, determine if the player's answer is correct
        public static bool Evaluate(int solution)
        {
            if (operation.Equals("+"))
            {
                if (num1 + num2 == solution)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\tINCORRECT!\n");
                    return false;
                }
            }
            else if (operation.Equals("*"))
            {
                if (num1 * num2 == solution)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\tINCORRECT!\n");
                    return false;
                }
            }
            else if (operation.Equals("/"))
            {
                if (num1 / num2 == solution)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\tINCORRECT!\n");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("INVALID OPERATION");
                return false;
            }
        }
    }
}

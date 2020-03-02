using System;
using System.Diagnostics;

namespace RandomBattles_v2
{
    class Program
    {
        // Global variable player and monster
        private static Minotaur monster;
        private static Character player;

        static void Main(string[] args)
        {
            // Character creation
            awesomeWrite("Before you start adventuring, create your character\n");
            System.Threading.Thread.Sleep(750);
            awesomeWrite("What is your character's name?  ");
            string name = Console.ReadLine();
            System.Threading.Thread.Sleep(200);
            awesomeWrite("What is your character's class? (Warrior, Assassin, Mage)  ");
            string characterClass = Console.ReadLine();
            System.Threading.Thread.Sleep(200);

            player = new Character(name, characterClass);     // Create the player

            awesomeWrite("Great!  These are your character's stats:\n");

            player.Stats();

            System.Threading.Thread.Sleep(2000);

            awesomeWrite("Press Enter to continue!");
            Console.ReadLine();

            System.Threading.Thread.Sleep(200);
            awesomeWrite("You head off on your journey... ");
            System.Threading.Thread.Sleep(1000);
            awesomeWrite(" remember ");
            System.Threading.Thread.Sleep(1000);
            awesomeWrite("monsters are vulnerable to math.\n");

            System.Threading.Thread.Sleep(200);

            // Stopwatch to time how long it takes to answer a question.  The longer you take, the less damage you deal.
            Stopwatch sw = new Stopwatch();         
            bool playGame = true;
            while (playGame)
            {
                awesomeWrite("You walk on");
                displayDotDotDot();

                monster = new Minotaur();       // Make the monster

                awesomeWrite("\nA deadly " + monster.Name + " wants to fight!\n");
                System.Threading.Thread.Sleep(500);
                monster.Stats();
                System.Threading.Thread.Sleep(1000);
                awesomeWrite("Quickly answer these three math questions to damage the monster!", 25);
                System.Threading.Thread.Sleep(500);

                // If one person is faster than the other, add excess speed until the faster person attacks twice.
                int playerExcessSpeed = 0;
                int monsterExcessSpeed = 0;
                int difficulty = 0;
                while (monster.IsAlive && player.IsAlive)
                {
                    awesomeWrite("\nThe questions will appear in");
                    awesomeWrite("  4");
                    System.Threading.Thread.Sleep(1000);
                    awesomeWrite("  3");
                    System.Threading.Thread.Sleep(1000);
                    awesomeWrite("  2");
                    System.Threading.Thread.Sleep(1000);
                    awesomeWrite("  1\n\n");
                    System.Threading.Thread.Sleep(1000);

                    // The player answers math questions
                    int addAttack, mulAttack, divAttack;
                    sw.Reset();
                    try
                    {
                        MathProblems.ShowAdditionProblem(difficulty);
                        sw.Start();
                        addAttack = MathProblems.Evaluate(Convert.ToInt32(Console.ReadLine())) ? player.AdditionAttack((int)sw.ElapsedMilliseconds) : 0;
                    }
                    catch (Exception) { Console.WriteLine("\tInvalid Answer!\n\n"); addAttack = 0; }
                    sw.Reset();
                    try
                    {
                        MathProblems.ShowMultiplicationProblem(difficulty);
                        sw.Start();
                        mulAttack = MathProblems.Evaluate(Convert.ToInt32(Console.ReadLine())) ? player.MultiplicationAttack((int)sw.ElapsedMilliseconds) : 0;
                    }
                    catch (Exception) { Console.WriteLine("\tInvalid Answer!\n\n"); mulAttack = 0; }
                    sw.Reset();
                    try
                    {
                        MathProblems.ShowSubtractionProblem(difficulty);
                        sw.Start();
                        divAttack = MathProblems.Evaluate(Convert.ToInt32(Console.ReadLine())) ? player.SubtractionAttack((int)sw.ElapsedMilliseconds) : 0;
                    }
                    catch (Exception) { Console.WriteLine("\tInvalid Answer!\n\n"); divAttack = 0; }
                    sw.Reset();
                    sw.Stop();
                    Console.WriteLine();

                    int allAttacks = addAttack + mulAttack + divAttack;     // The sum of all of the player's damage.
                    int monsterAttack = monster.Attack();                   // The damage the monster will deal.

                    // The player is faster than the monster
                    if (player.Speed > monster.Speed)
                    {
                        // Player attacks first
                        monster.TakeDamage(allAttacks);

                        if (monster.IsAlive)
                        {
                            playerExcessSpeed += player.Speed - monster.Speed;
                            awesomeWrite("You dealt " + allAttacks + " damage\n");

                            // Player attacks additionaly until they are out of excess speed
                            while (playerExcessSpeed > monster.Speed)
                            {
                                monster.TakeDamage(allAttacks);
                                if (!monster.IsAlive)
                                {
                                    awesomeWrite("Your final blow dealt " + allAttacks + "!\n");
                                    break;
                                }
                                
                                awesomeWrite("You dealt " + allAttacks + " again because you are so fast\n");
                                
                                playerExcessSpeed -= monster.Speed;
                            }
                            
                            if (monster.IsAlive)
                            {
                                //If the monster survived this far, it attacks the player.
                                player.TakeDamage(monsterAttack);
                                awesomeWrite("You took " + monsterAttack + " damage\n");
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            awesomeWrite("You dealt " + allAttacks + " damage and eliminated the minotaur!\n");
                            System.Threading.Thread.Sleep(1000);
                        }
                    }

                    // The monster is faster than the player
                    else if (monster.Speed > player.Speed)
                    {
                        // Monster attacks first
                        player.TakeDamage(monsterAttack);

                        if (player.IsAlive)
                        {
                            monsterAttack = monster.Attack();
                            monsterExcessSpeed += monster.Speed - player.Speed;
                            awesomeWrite("You took " + monsterAttack + " damage\n");

                            // Player attacks additionaly until they are out of excess speed
                            while (monsterExcessSpeed > player.Speed)
                            {

                                player.TakeDamage(monsterAttack);
                                if (player.IsAlive)
                                {
                                    awesomeWrite("You took " + allAttacks + " again because you are slower\n");
                                }
                                else
                                {
                                    awesomeWrite("You took " + allAttacks + " again because you are slower.  It was too much...\n");
                                    break;
                                }
                                monsterExcessSpeed -= player.Speed;
                            }

                            //If the player survived this far, it attacks the monster.
                            if (player.IsAlive)
                            {
                                monster.TakeDamage(allAttacks);
                                awesomeWrite("You dealt " + allAttacks + " damage\n");
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            awesomeWrite("You took " + allAttacks + " damage and succumbed to death...\n");
                            System.Threading.Thread.Sleep(1000);
                        }

                    }

                    // They both have the same speed
                    else
                    {
                        // Flip a coin to see who attacks first
                        Random rand = new Random();
                        if (rand.Next(0, 100) <= 50)
                        {
                            monster.TakeDamage(allAttacks);
                            monsterHitText(allAttacks);

                            player.TakeDamage(monsterAttack);
                            playerHitText( monsterAttack);
                        }
                        else
                        {
                            player.TakeDamage(monsterAttack);
                            playerHitText(monsterAttack);

                            monster.TakeDamage(allAttacks);
                            monsterHitText(allAttacks);
                        }

                    }

                    // Increase difficulty the longer the fight goes on
                    difficulty += 2;

                    // If the round ends and player and monster are still alive, prepare for next round.
                    if (player.IsAlive && monster.IsAlive)
                    {
                        awesomeWrite(
                            "\n\nThis fight isn't over!" +
                            "\n=======================" +
                            "\n " + player.Name + " : " + player.Health + 
                            "\n " + monster.Name + " : " + monster.Health +
                            "\n=======================\n\n");

                        awesomeWrite("Press Enter to start next round!!");
                        Console.ReadLine();
                    }
                }

                // The player defeated the monster
                if (player.IsAlive)
                {
                    awesomeWrite("Well done!\n");

                    awesomeWrite("Do you want to continue your journey? (y or n)  ");
                    string keepPlaying = Console.ReadLine();
                    if (keepPlaying.Equals("y"))
                    {
                        Console.WriteLine("\n\n");
                        player.Health += 50;                    // Heal some HP
                        player.CurrentXP += monster.Xp;         // Get some XP
                    }
                    else
                    {
                        playGame = false;
                    }
                }

                // The player died to the monster
                else
                {
                    awesomeWrite("The monster won...\n");

                    awesomeWrite("Do you want to try again (y or n)  ");
                    string keepPlaying = Console.ReadLine();
                    if (keepPlaying.Equals("y"))
                    {
                        Console.WriteLine("\n\n");
                        player.IsAlive = true;                              // Revive the player
                        player.Health = player.MaxHealth;                   // Heal all HP
                        player.CurrentXP += monster.Xp / 3;                 // Get 1/3 XP
                    }
                    else
                    {
                        playGame = false;
                    }
                }

                // Level up the player if they have enough XP.
                if (player.CurrentXP > player.LevelUpXP)
                {
                    player.Level++;                                 // Increase player level
                    player.CurrentXP -= player.LevelUpXP;           // Add overflow XP to currentXP
                    player.LevelUpXP += 10;                         // 10 more XP needed for next level
                    awesomeWrite(
                        "\n=============================================" +
                        "\n Congratulations!  You leveled up to level " + player.Level + "!" +
                        "\n=============================================\n\n");
                }
            }

            awesomeWrite("\n\nThanks for playing!");
            Console.Read();
        }

        // Determines if the monster died or not and displays text accordingly
        static void monsterHitText(int allAttacks)
        {
            if (monster.IsAlive)
            {
                awesomeWrite("You dealt " + allAttacks + " damage\n");
            }
            else
            {
                awesomeWrite("You dealt " + allAttacks + " damage and took down the monster!\n");
                monster.IsAlive = false;
            }
        }

        // Determines if the player died or not and displays text accordingly
        static void playerHitText(int monsterAttack)
        {
            if (player.IsAlive)
            {
                awesomeWrite("You took " + monsterAttack + " damage\n");
            }
            else
            {
                awesomeWrite("You took " + monsterAttack + " damage and you accept death...\n");
                player.IsAlive = false;
            }
        }

        // A method that types your string letter by letter.
        static void awesomeWrite(string text)
        {
            char[] letters = text.ToCharArray();
            foreach (char letter in letters)
            {
                System.Threading.Thread.Sleep(10);
                Console.Write(letter);
            }
        }

        // Overloaded method.  Allows you to change the speed that characters appear.  Bigger number = slower. 1 < speed < 100
        static void awesomeWrite(string text, int speed)
        {
            if (speed < 1 || speed > 100)
            {
                speed = 10;
            }
            char[] letters = text.ToCharArray();
            foreach (char letter in letters)
            {
                System.Threading.Thread.Sleep(speed);
                Console.Write(letter);
            }
        }

        // Dramatically writes dots to the screen
        static void displayDotDotDot()
        {
            Random rand = new Random();

            int numberOfDots = rand.Next(6, 11);

            short baseDelay = 400;
            short maxDelay = 200;

            for (int i = 0; i < numberOfDots; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(baseDelay + rand.Next(0, maxDelay + 1));
            }
        }
    }
}

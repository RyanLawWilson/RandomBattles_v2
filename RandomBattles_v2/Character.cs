using System;
using System.Collections.Generic;
using System.Text;

namespace RandomBattles_v2
{
    public class Character
    {
        // Build your character based on the class you picked
        public Character(string Name, string characterClass)
        {
            this.Name = Name;
            this.characterClass = characterClass;
            Level = 1;
            CurrentXP = 0;
            LevelUpXP = 20;
            IsAlive = true;

            switch (characterClass)
            {
                case "Warrior":
                    MaxHealth = 200;
                    Health = 200;
                    Damage = 65;
                    Speed = 20;
                    break;
                case "Assassin":
                    MaxHealth = 120;
                    Health = 120;
                    Damage = 45;
                    Speed = 60;
                    break;
                case "Mage":
                    MaxHealth = 100;
                    Health = 100;
                    Damage = 100;
                    Speed = 10;
                    break;
                case "GOD":
                    MaxHealth = 1000;
                    Health = 1000;
                    Damage = 10;
                    Speed = 100;
                    break;
                case "Shrimp":
                    MaxHealth = 100;
                    Health = 100;
                    Damage = 10;
                    Speed = 3;
                    break;
                default:        // Random stats if you pick a different class
                    int hpTemp = rand.Next(0, 100);
                    MaxHealth = 100 + hpTemp;
                    Health = 100 + hpTemp;
                    Damage = 50 + rand.Next(0, 100);
                    Speed = 25 + rand.Next(0, 50);
                    break;
            }
        }

        private Random rand = new Random();         // Random variable

        public int MaxHealth { get; set; }
        public bool IsAlive { get; set; }
        public string Name { get; set;}
        public string characterClass { get; }
        public int Level { get; set; }
        public int CurrentXP { get; set; }
        public int LevelUpXP { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        // A method that shows everything about the character
        public void Stats()
        {
            Console.WriteLine(String.Format("\n Your Stats:\n{0,-45}", "= = = = = = = = = = = = = = = = ="));
            Console.WriteLine(String.Format("{0,20} {1,-30}","Name:",Name));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Class:", characterClass));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Level:", Level));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Health:", Health));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Damage:", Damage));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Speed", Speed));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Current XP:", CurrentXP));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Next level in:", LevelUpXP - CurrentXP));
            Console.WriteLine(String.Format("{0,-45}\n", "= = = = = = = = = = = = = = = = ="));
        }

        // Three methods that take in an int and return an int.
        // These methods used to be a lot bigger but I optimized things a lot and added new stuff.
        // The difficulty can be decreased by increasing the grace period (2nd argument of DamageCalculation)

        // Calculates the Damage dealt by answering the addition question.
        public int AdditionAttack(int ellapsedTime)
        {
            return DamageCalculation(ellapsedTime, 3);
        }

        // Calculates the Damage dealt by answering the subtraction question.
        public int SubtractionAttack(int ellapsedTime)
        {
            return DamageCalculation(ellapsedTime, 4);
        }

        // Calculates the Damage dealt by answering the addition question.
        public int MultiplicationAttack(int ellapsedTime)
        {
            return DamageCalculation(ellapsedTime, 4);
        }

        // Calculates the Damage dealt by answering the addition question.
        // DOESN"T WORK VERY WELL BECAUSE OF DECIMALS.  NEEDS WORK.
        public int DivisionAttack(int ellapsedTime)
        {
            return DamageCalculation(ellapsedTime, 5);
        }

        // Calculates damage based on grace period and ellapsed time.
        private int DamageCalculation(int ellapsedTime, byte gracePeriod)
        {
            byte max = 2;       // The maximum the damage multiplier can be.
            byte poly = 3;      // The polynomial function | 2 = quadratic, 3 = cubic, etc...

            // Inverted polynomial function
            double damageMultiplier = -(max / Math.Pow(gracePeriod, poly)) * Math.Pow(ellapsedTime / 1000, poly) + max;
            int damage = (int)((Damage + rand.Next(-5, 5)) * damageMultiplier);
            damage = damage >= 0 ? damage : 0;          // If damage is less than 0, just return 0.

            Console.WriteLine("\tEllapsed Time: " + ellapsedTime + " | Damage Dealt: " + damage);

            return damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            IsAlive = Health > 0 ? true : false;        
        }
    }
}

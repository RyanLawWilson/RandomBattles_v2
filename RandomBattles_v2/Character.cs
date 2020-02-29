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
                    MaxHealth = 200;
                    Health = 120;
                    Damage = 45;
                    Speed = 60;
                    break;
                case "Mage":
                    MaxHealth = 200;
                    Health = 100;
                    Damage = 100;
                    Speed = 10;
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

        // Calculates the Damage dealt by answering the addition question.
        public int AdditionAttack(int ellapsedTime)
        {
            Console.WriteLine("\tEllapsed Time: " + ellapsedTime);
            if ((3 - (ellapsedTime / 1000)) <= 0)
            {
                return 0;
            }
            else
            {
                return (Damage + rand.Next(-5, 5)) * (3 - (ellapsedTime / 1000));
            }
        }

        // Calculates the Damage dealt by answering the addition question.
        public int MultiplicationAttack(int ellapsedTime)
        {
            Console.WriteLine("\tEllapsed Time: " + ellapsedTime);
            if ((4 - (ellapsedTime / 1000)) <= 0)
            {
                return 0;
            }
            else
            {
                return (Damage + rand.Next(-5, 5)) * (4 - (ellapsedTime / 1000));
            }
        }

        // Calculates the Damage dealt by answering the addition question.
        public int DivisionAttack(int ellapsedTime)
        {
            Console.WriteLine("\tEllapsed Time: " + ellapsedTime);
            if ((5 - (ellapsedTime / 1000)) <= 0)
            {
                return 0;
            }
            else
            {
                return (Damage + rand.Next(-5, 5)) * (5 - (ellapsedTime / 1000));
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            IsAlive = Health > 0 ? true : false;        // If you have no health, D I E

            //if (IsAlive)
            //{
            //    Console.WriteLine("You took " + damage + " damage");
            //}
            //else
            //{
            //    Console.WriteLine("You took " + damage + " damage and succumbed to death...");
            //}
        }
    }
}

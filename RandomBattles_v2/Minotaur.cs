using System;
using System.Collections.Generic;
using System.Text;

namespace RandomBattles_v2
{
    public class Minotaur
    {
        public Minotaur()
        {
            Name = "Minotaur";
            Health = rand.Next(500, 750);
            Damage = rand.Next(25, 32);
            Speed = rand.Next(12, 15);
            Xp = rand.Next(10, 20);
            IsAlive = true;
        }

        // Eventually I may create a class called Loot table that is better.
        private readonly string[] LOOT_TABLE = { "Gold", "Minotaur Corpse", "Gold Armor", "Boots of Speed"};
        private readonly double[] DROP_CHANCE = { 75.00,  100.00,            12.50,           10.00 };

        private Random rand = new Random();

        public bool IsAlive { get; set; }
        public string Name { get; }
        public int Health { get; set; }
        public int Damage { get; }
        public int Speed { get; }
        public int Xp { get; }

        public int Attack()
        {
            return Damage + rand.Next(-3, 4);
        }

        // When the minotaur dies, drop loot and XP.
        public void Die()
        {
            for (int i = 0; i < LOOT_TABLE.Length; i++)
            {
                if (rand.Next(0, 100) <= DROP_CHANCE[i])
                {
                    System.Threading.Thread.Sleep(400);
                    Console.WriteLine("The " + Name + " dropped " + LOOT_TABLE[i]);
                    System.Threading.Thread.Sleep(400);
                }
            }
            Console.WriteLine("You gained " + Xp + " XP!");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            IsAlive = Health > 0 ? true : false;
            if (!IsAlive)               // If monster has no health, D I E
            {
                Die();
            }
        }

        // A method that shows relevant info about the monster
        public void Stats()
        {
            Console.WriteLine(String.Format("\n{0,-45}", "* * * * * * * * * * * * * * * *"));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Health:", Health));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Damage:", Damage));
            Console.WriteLine(String.Format("{0,20} {1,-30}", "Speed", Speed));
            Console.WriteLine(String.Format("{0,-45}\n", "* * * * * * * * * * * * * * * *"));
        }
    }
}

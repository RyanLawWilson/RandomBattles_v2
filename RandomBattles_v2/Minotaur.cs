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
            if (!IsAlive)
            {
                Die();
            }

            // Putting this here saves a lot of room in Program.cs
            //if (IsAlive)
            //{
            //    Console.WriteLine("You dealt " + damage + " damage");
            //}
            //else
            //{
            //    Console.WriteLine("You dealt " + damage + " damage and took down the monster!");
            //}
        }
    }
}

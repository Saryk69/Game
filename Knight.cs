using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Knight : Hero
    {
        public Knight(string name, int fullhp, Colors color) : base(name, fullhp, color)
        {

        }

        public override void DefaultAttack(Hero hero)
        {
            int damage = random.Next(100, 151);
            hero.ActualHP -= damage;
            Console.WriteLine($"\nGracz {Name} zadał {damage} punktów obrażeń graczowi {hero.Name}.");
        }

        public override void Heal(Hero hero)
        {
            double fullHealth = FullHP;
            int heal = random.Next(25, 60);

            if (ActualHP < fullHealth)
            {
                ActualHP += heal;
                Console.WriteLine($"\nGracz {Name} uzdrowił się za {heal} punktów życia.");
            }
            else
            {
                if (random.Next(1, 4) == 3) // 25% chance
                {
                    int smallHeal = random.Next(0, 12);
                    ActualHP += smallHeal;
                    Console.WriteLine($"\nGracz {Name} uleczył się za {smallHeal} punktów życia (mała regeneracja).");
                }
                else
                {
                    Console.WriteLine($"\nGracz {Name} nie zdołał się uleczyć.");
                }
            }
        }
    }
}

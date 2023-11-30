using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Wizard : Hero, ISpecialAttack
    {
        public Wizard(string name, int fullhp, Colors color) : base(name, fullhp, color)
        {

        }

        public override void DefaultAttack(Hero hero)
        {
            int damage = random.Next(50, 201);
            hero.ActualHP -= damage;
            Console.WriteLine($"\nGracz {Name} zadał {damage} punktów obrażeń graczowi {hero.Name}.");
        }

        public override void Heal(Hero hero)
        {
            double fullHealth = hero.FullHP;
            int heal = random.Next(60, 150);
            int smallheal = random.Next(0, 12);

            if (hero.ActualHP < fullHealth)
            {
                hero.ActualHP += heal;
                Console.WriteLine($"\nGracz {Name} uzdrowił się za {heal} punktów życia.");
            }
            else if (hero.ActualHP > fullHealth)
            {
                hero.ActualHP += smallheal;
                Console.WriteLine($"\nGracz {Name} z trudem uleczył się za {smallheal} punktów życia.");
            }
        }


        public void SpecialAttack(Hero hero)
        {
            int damage = random.Next(100) < 5 ? 42 : 420; // 5% chance for 42, 95% chance for 420
            hero.ActualHP -= damage;
            Console.WriteLine($"\nAtak Specjalny: Gracz {Name} zadał {damage} punktów obrażeń graczowi {hero.Name}.");

        }
    }
}
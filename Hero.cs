using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    enum Colors
    {
        red, green, blue
    }
    // NIe in ?interface?
    // ??????????

    // Interface
    // Czyli klucz
    interface ISpecialAttack
    {
        void SpecialAttack(Hero hero);
    }

    abstract class Hero
    {
        public string Name { get; }
        public int FullHP { get; }
        private int actualHP;

        public int ActualHP
        {
            get { return  actualHP; }
            set 
            {
                if (value < 0)
                {
                    actualHP = 0;
                } 
                else if (value > FullHP)
                {
                    actualHP = FullHP;
                }
                else
                {
                    actualHP = value;
                }
            }
        }

        public Colors Color { get; set; }    

        protected Random random = new Random();

        public bool UsedSpecialAttack {  get; set; } = false;

        public Hero(string name, int fullhp, Colors color)
        {
            Name = name;
            FullHP = fullhp;
            ActualHP = FullHP;
            Color = color;
        }

        // Hero hero jest to object atakowyny \/
        public abstract void DefaultAttack(Hero hero);
        public abstract void Heal(Hero hero);

        public override string ToString()
        {
            return $"{Name} - {ActualHP}/{FullHP} hp";
        }
    }
}

using System.ComponentModel.Design;

namespace Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Jest to lista zrobiona z klasy
             * Krtóra trzyma w sobobie lite bohaterrów do wyświetlenia
             */
            List<Hero> heroes = new List<Hero>(); 
            heroes.Add(new Wizard("Wizard", 500, Colors.blue));
            heroes.Add(new Knight("Knight", 700, Colors.red));
            Hero player1;
            Hero player2;

            // isChosed sprawda czy gracz wybrał super bohatera
            bool isChosed = false;

            // Wybranie SuperHero dla gracza 1
            do
            {
                isChosed = ChooseHero(out player1, 1, ref heroes);
            } while (!isChosed);

            // Wybranie SuperHero dla gracza 2
            do
            {
                isChosed = ChooseHero(out player2, 2, ref heroes);
            } while (!isChosed);

            bool isPlayer1Turn = true;

            do
            {
                Console.Clear();
                switch (player1.Color)
                {
                    case Colors.red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Colors.green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Colors.blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Player 1");
                Console.WriteLine(player1);

                switch (player2.Color)
                {
                    case Colors.red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Colors.green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Colors.blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    default:
                        break;
                }
                Console.CursorTop = 0;
                Console.CursorLeft = 25;
                Console.WriteLine("Player 2");
                Console.CursorTop = 1;
                Console.CursorLeft = 25;
                Console.WriteLine(player2);
                Console.ResetColor();
                 
                //         czy jest pierwszy ruch gracza 1 czy 2
                Hero actualPlayer = isPlayer1Turn ? player1 : player2;
                Hero otherPlayer = isPlayer1Turn ? player2 : player1;

                Console.WriteLine($"\nRuch gracza: {(isPlayer1Turn ? 1 : 2)}");
                Console.WriteLine("Co chcesz zrobić?\n" +
                    "1. Podstawowy atak\n" +
                    "2. Ulecz");

                // Sprawda czy dany gracz ma taki ruch i czy nie został jeszcz użyty 
                if (actualPlayer is ISpecialAttack && !actualPlayer.UsedSpecialAttack)
                {
                    Console.WriteLine("3. Atak specjalny");
                }

                ConsoleKey key;
                do
                {
                    key = Console.ReadKey().Key;

                    switch (key)
                    {
                        case ConsoleKey.D1:
                            actualPlayer.DefaultAttack(otherPlayer);
                            break;
                        case ConsoleKey.D2:
                            actualPlayer.Heal(actualPlayer);
                            break;
                        case ConsoleKey.D3:
                            if (actualPlayer is ISpecialAttack && !actualPlayer.UsedSpecialAttack)
                            {
                                // Teraz to jest rzutowanie jawne 
                                // Już sam niewiem
                                ((ISpecialAttack)actualPlayer).SpecialAttack(otherPlayer);
                                // Tutaj zezwala aby funkcja się skończyła
                                actualPlayer.UsedSpecialAttack = true;
                            }
                            else
                            {
                                actualPlayer.DefaultAttack(otherPlayer);
                            }
                            break;
                        default:
                            WyswietlBlad("Nie ma takiego ruchu!!!"); 
                            break;
                    }
                } while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.D3);
                //           /\ - nie będzie różne | różne 

                if (player1.ActualHP == 0 || player2.ActualHP == 0)
                {
                    WyswietlPrzegrana($"Niestety! Gracz {(isPlayer1Turn ? 2 : 1)} zginął!");
                    WyswietlWygrana($"Wygrywa gracz: {(isPlayer1Turn ? 1 : 2)}.");
                }
                else
                {
                    // Jeśli już był ruch gracza1 zakończ go 
                    isPlayer1Turn = !isPlayer1Turn;
                }
                 
             Console.ReadKey();
            } while (player1.ActualHP > 0 && player2.ActualHP > 0);
        }

        static bool ChooseHero(out Hero hero, int player, ref List<Hero> heroes)
        {
            Console.Clear();
            Console.WriteLine($"Gracz {player} wybiera swoją postać:");

            for (int i = 0; i < heroes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {heroes[i].Name}");
            }

            int num;
            ConsoleKeyInfo keyInput = Console.ReadKey(intercept: true);

            // Check if the key pressed is a digit
            if (char.IsDigit(keyInput.KeyChar))
            {
                // Parse the digit to get the numeric choice
                num = int.Parse(keyInput.KeyChar.ToString());

                // Check if the choice is within the valid range
                if (num >= 1 && num <= heroes.Count)
                {
                    hero = heroes[num - 1];
                    heroes.Remove(hero);
                    return true;
                    // Return true or perform other actions as needed
                }
                else
                {
                    WyswietlBlad("\nNie ma takiego bohatera.");
                    //"\nNieprawidłowe wprowadzenie cyfry."
                    hero = null;
                    return false;
                    // Return false or perform other actions as needed
                }
            }
            else
            {
                WyswietlBlad("\nNieprawidłowe dane wejściowe. Wprowadź cyfrę.");
                hero = null;
                return false;
                // Return false or perform other actions as needed
            }
        }

        // Custom method to read a key and display a message
        static void WyswietlBlad(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey(intercept: true);
        }

        static void WyswietlPrzegrana(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void WyswietlWygrana(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
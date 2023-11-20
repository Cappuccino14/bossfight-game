using System;
using System.ComponentModel;
using System.Media;
using System.Threading;
using System.Windows;

namespace BossFight
{
    internal class Program
    {
        static Scene currentScene;
        static int playerHealth = 5;
        static int playerEnergy = 5;
        static int bossHealth = 10;

        static void Main(string[] args)
        {
            LoadScene(Scene.menu);
        }

        static void DamagePlayer(int damage)
        {
            playerHealth -= damage;
            if (playerHealth <= 0)
                LoadScene(Scene.death);
        }

        static void DamageBoss(int damage)
        {
            bossHealth -= damage;   
            if (bossHealth <= 0)
                LoadScene(Scene.win);
        }

        static void DeductEnergy(int energy)
        {
            if (playerEnergy >= energy)
                playerEnergy -= energy;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("not enough energy");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void AddEnergy(int energy)
        {
            playerEnergy += energy;
            if (playerEnergy > 5)
                playerEnergy = 5;
        }

        static void ShowError(string error)
        {
            Console.Beep(500, 300);
            MessageBox.Show(new Exception().Message, error);
        }

        static void InvalidSyntax()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Syntax");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            LoadScene(currentScene);
        }

        static int Seconds(int seconds)
        {
            return seconds * 1000;
        }

        #region SceneManagement
        enum Scene
        {
            menu,
            game,
            win,
            death,
            about
        }

        static void LoadScene(Scene scene)
        {
            currentScene = scene;
            Console.Clear();
            Console.Title = scene.ToString();

            if (scene == Scene.menu)
                Menu();
            else if (scene == Scene.game)
                Game();
            else if (scene == Scene.win)
                Win();
            else if (scene == Scene.death)
                Death();
            else if (scene == Scene.about)
                About();

        }
        #endregion

        #region Scenes
        static void Menu()
        {
            var drawmenu = new[]
            {
                @" __  __       _         __  __                  ",
                @"|  \/  |     (_)       |  \/  |                 ",
                @"| \  / | __ _ _ _ __   | \  / | ___ _ __  _   _ ",
                @"| |\/| |/ _` | | '_ \  | |\/| |/ _ \ '_ \| | | |",
                @"| |  | | (_| | | | | | | |  | |  __/ | | | |_| |",
                @"|_|  |_|\__,_|_|_| |_| |_|  |_|\___|_| |_|\__,_|",
                @"",
                @"[1] start",
                @"[2] about",
                @"[3] quit"
            };

            foreach (string line in drawmenu)
                Console.WriteLine(line);

            string answer = Console.ReadLine();
            if (answer == "1")
                LoadScene(Scene.game);
            else if (answer == "2")
                LoadScene(Scene.about);
            else if (answer == "3")
                Environment.Exit(1);
            else
                InvalidSyntax();
        }

        static void Game()
        {
            while (true)
            {

                Console.Clear();
                var drawmenu = new[]
                {
                    @"          _   _             _      _ ",
                    @"     /\  | | | |           | |    | |",
                    @"    /  \ | |_| |_ __ _  ___| | __ | |",
                    @"   / /\ \| __| __/ _` |/ __| |/ / | |",
                    @"  / ____ \ |_| || (_| | (__|   <  |_|",
                    @" /_/    \_\__|\__\__,_|\___|_|\_\ (_)",
                    @"",
                    @"[1] slice (-2 ∞)",
                    @"[2] coinflip (0 ∞)",
                    @"[3] sacrifice (-1 ♥, +Max ∞)",
                    @"[4] generate (+1 ∞)"
                };

                foreach (string line in drawmenu)
                    Console.WriteLine(line);

                Console.WriteLine("\nplayer health: " + playerHealth + "\nplayer energy: " + playerEnergy);
                Console.WriteLine("boss health: " + bossHealth + "\n");

                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    if (playerEnergy >= 2)
                    {
                        DamageBoss(1);
                        DeductEnergy(2);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("not enough energy!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(Seconds(1));
                    }

                }
                else if (answer == "2")
                {
                    Random rnd1 = new Random();
                    int num1 = rnd1.Next(1, 3);

                    Console.Write("pick a number between 1 - 2: ");
                    int _answer = Convert.ToInt32(Console.ReadLine());

                    if (_answer == num1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("correct!");
                        Console.ForegroundColor = ConsoleColor.White;
                        DamageBoss(2);
                        Thread.Sleep(Seconds(1)); 
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("incorrect! -2 ♥");
                        Console.ForegroundColor = ConsoleColor.White;
                        DamagePlayer(2);
                        Thread.Sleep(Seconds(1));
                    }
                }
                else if (answer == "3")
                {
                    AddEnergy(5);
                    DamagePlayer(1);
                }
                else if (answer == "4")
                {
                    AddEnergy(1);
                }
                else
                    InvalidSyntax();

                Console.Clear();
                var drawdefend = new[]
                {
                @"  _____        __               _   _ ",
                @" |  __ \      / _|             | | | |",
                @" | |  | | ___| |_ ___ _ __   __| | | |",
                @" | |  | |/ _ \  _/ _ \ '_ \ / _` | | |",
                @" | |__| |  __/ ||  __/ | | | (_| | |_|",
                @" |_____/ \___|_| \___|_| |_|\__,_| (_)",
                @"",
                };

                foreach (string line in drawdefend)
                    Console.WriteLine(line);

                Console.WriteLine("\nplayer health: " + playerHealth + "\nplayer energy: " + playerEnergy);
                Console.WriteLine("boss health: " + bossHealth + "\n");

                Random rnd2 = new Random();
                int num2 = rnd2.Next(1, 3);

                Console.Write("pick a number between 1 - 2: ");
                int __answer = Convert.ToInt32(Console.ReadLine());

                if (__answer == num2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("correct!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(Seconds(1));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("incorrect! -1 ♥");
                    Console.ForegroundColor = ConsoleColor.White;
                    DamagePlayer(1);
                    Thread.Sleep(Seconds(1));
                }

            }
        }

        static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var drawmenu = new[]
            {
                @" __     __          __          ___       ",
                @" \ \   / /          \ \        / (_)          ",
                @"  \ \_/ /__  _   _   \ \  /\  / / _ _ __  ",
                @"   \   / _ \| | | |   \ \/  \/ / | | '_ \ ",
                @"    | | (_) | |_| |    \  /\  /  | | | | |",
                @"    |_|\___/ \__,_|     \/  \/   |_|_| |_|",
                @"",
                @"[1] main menu",
                @"[2] quit"
            };

            foreach (string line in drawmenu)
                Console.WriteLine(line);

            Console.ForegroundColor = ConsoleColor.White;
            string answer = Console.ReadLine();
            if (answer == "1")
                LoadScene(Scene.menu);
            else if (answer == "2")
                Environment.Exit(2);
            else
                InvalidSyntax();
        }

        static void Death()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var drawmenu = new[]
            {
                @" __     __           _____  _          _ ",
                @" \ \   / /          |  __ \(_)        | |",
                @"  \ \_/ /__  _   _  | |  | |_  ___  __| |",
                @"   \   / _ \| | | | | |  | | |/ _ \/ _` |",
                @"    | | (_) | |_| | | |__| | |  __/ (_| |",
                @"    |_|\___/ \__,_| |_____/|_|\___|\__,_|",
                @"",
                @"[1] main menu",
                @"[2] quit"
            };

            foreach (string line in drawmenu)
                Console.WriteLine(line);

            Console.ForegroundColor = ConsoleColor.White;
            string answer = Console.ReadLine();
            if (answer == "1")
                LoadScene(Scene.menu);
            else if (answer == "2")
                Environment.Exit(2);
            else
                InvalidSyntax();
        }

        static void About()
        {
            var drawhelp = new[]
            {
                @"           _                 _   ",
                @"     /\   | |               | |  ",
                @"    /  \  | |__   ___  _   _| |_ ",
                @"   / /\ \ | '_ \ / _ \| | | | __|",
                @"  / ____ \| |_) | (_) | |_| | |_ ",
                @" /_/    \_\_.__/ \___/ \__,_|\__|",
                @"",
                @"made by cappuccino.",
                @"",
                @"player attacks: ",
                @"slice: deals 1 damage, cost 2 energy points.",
                @"coinflip: if you win the coinflip you deal 2 damage, if you lose the coinflip you lose 2 hearts, cost 0 energy points.",
                @"sacrifice: gives back full energy points at the cost of 1 heart",
                @"generate: generates 1 energy point",
                @"",
                @"Other: ",
                @"player has 5 hearts and 5 energy points",
                @"boss has 10 hearts and infinite energy points",
                @"",
                @"[1] back"
            };

            foreach (string line in drawhelp)
                Console.WriteLine(line);

            string answer = Console.ReadLine();
            if (answer == "1")
                LoadScene(Scene.menu);
            else
                InvalidSyntax();
        }
        #endregion
    }
}
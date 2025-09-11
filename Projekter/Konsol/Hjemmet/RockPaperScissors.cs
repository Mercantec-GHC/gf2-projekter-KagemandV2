namespace Hjemmet
{
    public class RockPaperScissors
    {
        public void Start()
        {
           
           
            Random rnd = new Random();
            int modstander = rnd.Next(1, 4);
            int scorePlayer = 0;
            int scoreComputer = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sten, Saks, Papir");
                Console.WriteLine($"Din score: {scorePlayer} - Computerens score: {scoreComputer}");
                Console.WriteLine("Vælg din handling:");
                Console.WriteLine("1. Sten");
                Console.WriteLine("2. Saks");
                Console.WriteLine("3. Papir");
                Console.WriteLine("4. Afslut spillet");
                string input = Console.ReadLine();
                int player;
                if (input == "4")
                {
                    Console.WriteLine($"du sluttede med scoren {scorePlayer} mod min score på {scoreComputer}");
                    if (scorePlayer > scoreComputer)
                    {
                        Console.WriteLine("OG DU VANDT SPILLET! WOOOOOOOOOOOOOOOOOO!");
                        Thread.Sleep(5000);
                        break;
                    }
                    else if (scorePlayer < scoreComputer)
                    {
                        Console.WriteLine("og Du tabte, og har dermed dømt mennesketheden til at lever under mine SKO. MUHAHAHAHAHAH");
                        Thread.Sleep(10000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kedeligt... Uafgjort... Du kunne lave hvilket som helt andet, og det ville være mere produktiv!");
                        Thread.Sleep(10000);
                        break;
                    }
                }
                else if (int.TryParse(input, out player) && player >= 1 && player <= 3)
                {
                    Console.WriteLine($"Du valgte: {(player == 1 ? "Sten" : player == 2 ? "Saks" : "Papir")}");
                    Console.WriteLine($"Computeren valgte: {(modstander == 1 ? "Sten" : modstander == 2 ? "Saks" : "Papir")}");
                    if (player == modstander)
                    {
                        Console.WriteLine("Uafgjort!");
                    }
                    else if ((player == 1 && modstander == 2) || (player == 2 && modstander == 3) || (player == 3 && modstander == 1))
                    {
                        Console.WriteLine("Du vinder!");
                        scorePlayer++;
                    }
                    else
                    {
                        Console.WriteLine("Computeren vinder!");
                        scoreComputer++;
                    }
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg, prøv igen.");
                }
                modstander = rnd.Next(1, 4);
                Console.WriteLine("Tryk på en tast for at fortsætte...");
                Console.ReadKey();
            }
        }
    }
}

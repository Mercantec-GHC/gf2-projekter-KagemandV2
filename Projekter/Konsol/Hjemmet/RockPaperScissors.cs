namespace Hjemmet
{
    public class RockPaperScissors
    {
        public void Start()
        {


            // Sten, Saks, Papir spillet, og her bliver der også tilføjet en score, som bliver gemt igennem spillet
            int scorePlayer = 0;
            int scoreComputer = 0;
            while (true)
            {
                Console.Clear();
                // tilfældig nummer generator til computerens valg
                Random rnd = new Random();
                int modstander = rnd.Next(1, 4);
                // Informations display for spillet, og hvordan det fungerer
                Console.WriteLine("Sten, Saks, Papir");
                Console.WriteLine($"Din score: {scorePlayer} - Computerens score: {scoreComputer}");
                Console.WriteLine("Vælg din handling:");
                Console.WriteLine("1. Sten");
                Console.WriteLine("2. Saks");
                Console.WriteLine("3. Papir");
                Console.WriteLine("4. Afslut spillet");
                string input = Console.ReadLine(); // her bliver inputtet taget ind fra brugeren
                int player;
                if (input == "4") // hvis brugeren vælger 4, så afsluttes spillet, og den endelige score bliver vist
                {
                    Console.WriteLine($"du sluttede med scoren {scorePlayer} mod min score på {scoreComputer}");
                    if (scorePlayer > scoreComputer) // hvis spilleren har en højere score, så vinder de
                    {
                        Console.WriteLine("OG DU VANDT SPILLET! WOOOOOOOOOOOOOOOOOO!");
                        Thread.Sleep(5000);
                        break;
                    }
                    else if (scorePlayer < scoreComputer) // hvis computeren har en højere score, så vinder den
                    {
                        Console.WriteLine("og Du tabte, og har dermed dømt menneskeheden til at leve under mine SKO. MUHAHAHAHAHAH");
                        Thread.Sleep(10000);
                        break;
                    }
                    else // hvis de har samme score, så er det uafgjort
                    {
                        Console.WriteLine("Kedeligt... Uafgjort... Du kunne lave hvilket som helt andet, og det ville være mere produktiv!");
                        Thread.Sleep(10000);
                        break;
                    }
                }
                else if (int.TryParse(input, out player) && player >= 1 && player <= 3) // tjekker om inputtet er et heltal mellem 1 og 3, og sætter det til player
                {
                    Console.WriteLine($"Du valgte: {(player == 1 ? "Sten" : player == 2 ? "Saks" : "Papir")}"); // viser hvad spilleren valgte
                    Console.WriteLine($"Computeren valgte: {(modstander == 1 ? "Sten" : modstander == 2 ? "Saks" : "Papir")}"); // viser hvad computeren valgte
                    if (player == modstander) // tjekker om de valgte det samme, og hvis ja, så er det uafgjort
                    {
                        Console.WriteLine("Uafgjort!");
                    }
                    else if ((player == 1 && modstander == 2) || (player == 2 && modstander == 3) || (player == 3 && modstander == 1))
                    // tjekker alle vindende kombinationer for spilleren
                    {
                        Console.WriteLine("Du vinder!");
                        scorePlayer++;
                    }
                    else
                    // hvis ingen af de andre betingelser er opfyldt, så må computeren have vundet
                    {
                        Console.WriteLine("Computeren vinder!");
                        scoreComputer++;
                    }
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg, prøv igen.");
                }
                Console.WriteLine("Tryk på en tast for at fortsætte...");
                Console.ReadKey(); // venter på at brugeren trykker en tast, før den fortsætter loopen
            }
        }
    }
}

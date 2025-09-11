namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            Console.WriteLine("Guess a number between 1 and 1000");

            Random brains = new Random();
            int brainsnum = brains.Next(1, 1000);
            int guess = 0;
            int attempts = 0;
            int secretNumber = brainsnum;


            Console.WriteLine($"The brain may be thinking of {brainsnum}");
            Console.WriteLine("G�t et tal, som computeren t�nker p�");

            while (guess != secretNumber)
            {
                Console.Write("Dit g�t: ");

                if (int.TryParse(Console.ReadLine(), out guess))
                {
                    attempts++;

                    if (guess < secretNumber)
                    {
                        Console.WriteLine("For lavt!");
                    }
                    else if (guess > secretNumber)
                    {
                        Console.WriteLine("For h�jt!");
                    }
                    else
                    {
                        Console.WriteLine($"Korrekt! Du brugte {attempts} fors�g.");
                    }
                }
                else
                {
                    Console.WriteLine("Skriv et gyldigt heltal.");
                }
            }
            Console.WriteLine("Tak for spillet!");
            Thread.Sleep(2000);
            Console.WriteLine("Nu er det min tur til at g�tte");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("T�nk p� et tal mellem 1-1000. S� vil jeg pr�ve at g�tte det");
            Thread.Sleep(5000);
            int min = 0;
            int max = 1001;
            int computerGuess = 0;
            int antalFors�g = 0;
            while (true)
            {
                Random rand = new Random();
                computerGuess = rand.Next(min, max);
                Console.WriteLine($"Mit g�t er: {computerGuess}");
                Console.WriteLine("Er det for h�jt (H), for lavt (L) eller korrekt (K)?");
                string feedback = Console.ReadLine().ToLower();
                if (feedback == "k")
                {
                    antalFors�g++;
                    Console.WriteLine($"Yay! Jeg g�ttede dit tal som var {computerGuess} p� {antalFors�g} fors�g.");
                    Thread.Sleep(10000);
                    break;
                }
                else if (feedback == "h")
                {
                    max = computerGuess;
                    antalFors�g++;
                }
                else if (feedback == "l")
                {
                    min = computerGuess + 1;
                    antalFors�g++;
                }
                else
                {
                    Console.WriteLine("Ugyldigt input. Skriv H, L eller K.");
                }
                

            }

            }
    }
}

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
            Console.WriteLine("Gæt et tal, som computeren tænker på");

            while (guess != secretNumber)
            {
                Console.Write("Dit gæt: ");

                if (int.TryParse(Console.ReadLine(), out guess))
                {
                    attempts++;

                    if (guess < secretNumber)
                    {
                        Console.WriteLine("For lavt!");
                    }
                    else if (guess > secretNumber)
                    {
                        Console.WriteLine("For højt!");
                    }
                    else
                    {
                        Console.WriteLine($"Korrekt! Du brugte {attempts} forsøg.");
                    }
                }
                else
                {
                    Console.WriteLine("Skriv et gyldigt heltal.");
                }
            }
            Console.WriteLine("Tak for spillet!");
            Thread.Sleep(2000);
            Console.WriteLine("Nu er det min tur til at gætte");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Tænk på et tal mellem 1-1000. Så vil jeg prøve at gætte det");
            Thread.Sleep(5000);
            int min = 0;
            int max = 1001;
            int computerGuess = 0;
            int antalForsøg = 0;
            while (true)
            {
                Random rand = new Random();
                computerGuess = rand.Next(min, max);
                Console.WriteLine($"Mit gæt er: {computerGuess}");
                Console.WriteLine("Er det for højt (H), for lavt (L) eller korrekt (K)?");
                string feedback = Console.ReadLine().ToLower();
                if (feedback == "k")
                {
                    antalForsøg++;
                    Console.WriteLine($"Yay! Jeg gættede dit tal som var {computerGuess} på {antalForsøg} forsøg.");
                    Thread.Sleep(10000);
                    break;
                }
                else if (feedback == "h")
                {
                    max = computerGuess;
                    antalForsøg++;
                }
                else if (feedback == "l")
                {
                    min = computerGuess + 1;
                    antalForsøg++;
                }
                else
                {
                    Console.WriteLine("Ugyldigt input. Skriv H, L eller K.");
                }
                

            }

            }
    }
}

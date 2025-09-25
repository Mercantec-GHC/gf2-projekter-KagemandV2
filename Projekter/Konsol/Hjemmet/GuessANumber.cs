namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            Console.WriteLine("Guess a number between 1 and 1000");
            /* F�rste spil her, er hvor du som person g�tter en random tal. 
             * F�rst bliver en random tal genereret og vigtive integers bliver startet op */

            Random brains = new Random();
            int brainsnum = brains.Next(1, 1000);
            int guess = 0;
            int attempts = 0;
            int secretNumber = brainsnum;


            //Console.WriteLine($"The brain may be thinking of {brainsnum}"); // denne linje er bare for at teste,
            Console.WriteLine("G�t et tal, som computeren t�nker p�");

            while (guess != secretNumber) // s� l�nge g�ttet ikke er det samme som det hemmelige tal, s� k�rer den videre
            {
                Console.Write("Dit g�t: ");

                if (int.TryParse(Console.ReadLine(), out guess))
                { // tjekker om inputtet er et heltal, og hvis det er, s� s�tter den det til guess, hvis ikke, s� beder den om et nyt input
                    attempts++;
                    //attempts stiger hver gang du g�tter med et korrekt heltal
                    if (guess < secretNumber)
                    {
                        Console.WriteLine("For lavt!");
                    }
                    else if (guess > secretNumber) //her tjekker den om g�ttet er h�jere eller lavere end det hemmelige tal, og informere herfra
                    {
                        Console.WriteLine("For h�jt!");
                    }
                    else
                    { // hvis g�ttet er ikke h�jere eller lavere, s� m� det v�re korrekt, dermed g�r den videre hertil og l�ser spillet
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
            // Andet spil her, er hvor computeren g�tter et tal som du t�nker p�, og lidt grafisk er tilf�jet, for at det ser p�nere ud
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("T�nk p� et tal mellem 1-1000. S� vil jeg pr�ve at g�tte det");
            Thread.Sleep(5000);
            // Vigtige integers bliver startet op, og en uendelig loop bliver startet op, som kun kan stoppes n�r computeren g�tter rigtigt
            int min = 0;
            int max = 1001;
            int computerGuess = 0;
            int antalFors�g = 0;
            while (true)
            {
                Random rand = new Random();
                computerGuess = rand.Next(min, max);
                //laver et random tal mellem min og max, som bliver opdateret hver gang computeren g�tter forkert, hvorefter den sp�rger om feedback
                Console.WriteLine($"Mit g�t er: {computerGuess}");
                Console.WriteLine("Er det for h�jt (H), for lavt (L) eller korrekt (K)?");
                string feedback = Console.ReadLine().ToLower();
                //her bliver feedbacken l�st, og gjort om til sm� bogstaver, s� der ikke er kapitaliseringsproblemer
                if (feedback == "k")
                { // hvis feedbacken er korrekt, s� stopper den loopen og fort�ller hvor mange fors�g det tog
                    antalFors�g++;
                    Console.WriteLine($"Yay! Jeg g�ttede dit tal som var {computerGuess} p� {antalFors�g} fors�g.");
                    Thread.Sleep(10000);
                    break;
                }
                else if (feedback == "h")
                // hvis feedbacken er for h�jt, s� s�tter den max til computerens g�t, og �ger antal fors�g med et
                {
                    max = computerGuess;
                    antalFors�g++;
                }
                else if (feedback == "l")
                // hvis feedbacken er for lavt, s� s�tter den min til computerens g�t + 1, og �ger antal fors�g med et
                {
                    min = computerGuess + 1;
                    antalFors�g++;
                }
                else
                { // hvis feedbacken er noget andet end H, L eller K, s� beder den om et gyldigt input
                    Console.WriteLine("Ugyldigt input. Skriv H, L eller K.");
                }


            }

        }
    }
}

namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            Console.WriteLine("Guess a number between 1 and 1000");
            /* Første spil her, er hvor du som person gætter en random tal. 
             * Først bliver en random tal genereret og vigtive integers bliver startet op */

            Random brains = new Random();
            int brainsnum = brains.Next(1, 1000);
            int guess = 0;
            int attempts = 0;
            int secretNumber = brainsnum;


            //Console.WriteLine($"The brain may be thinking of {brainsnum}"); // denne linje er bare for at teste,
            Console.WriteLine("Gæt et tal, som computeren tænker på");

            while (guess != secretNumber) // så længe gættet ikke er det samme som det hemmelige tal, så kører den videre
            {
                Console.Write("Dit gæt: ");

                if (int.TryParse(Console.ReadLine(), out guess))
                { // tjekker om inputtet er et heltal, og hvis det er, så sætter den det til guess, hvis ikke, så beder den om et nyt input
                    attempts++;
                    //attempts stiger hver gang du gætter med et korrekt heltal
                    if (guess < secretNumber)
                    {
                        Console.WriteLine("For lavt!");
                    }
                    else if (guess > secretNumber) //her tjekker den om gættet er højere eller lavere end det hemmelige tal, og informere herfra
                    {
                        Console.WriteLine("For højt!");
                    }
                    else
                    { // hvis gættet er ikke højere eller lavere, så må det være korrekt, dermed går den videre hertil og løser spillet
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
            // Andet spil her, er hvor computeren gætter et tal som du tænker på, og lidt grafisk er tilføjet, for at det ser pænere ud
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Tænk på et tal mellem 1-1000. Så vil jeg prøve at gætte det");
            Thread.Sleep(5000);
            // Vigtige integers bliver startet op, og en uendelig loop bliver startet op, som kun kan stoppes når computeren gætter rigtigt
            int min = 0;
            int max = 1001;
            int computerGuess = 0;
            int antalForsøg = 0;
            while (true)
            {
                Random rand = new Random();
                computerGuess = rand.Next(min, max);
                //laver et random tal mellem min og max, som bliver opdateret hver gang computeren gætter forkert, hvorefter den spørger om feedback
                Console.WriteLine($"Mit gæt er: {computerGuess}");
                Console.WriteLine("Er det for højt (H), for lavt (L) eller korrekt (K)?");
                string feedback = Console.ReadLine().ToLower();
                //her bliver feedbacken læst, og gjort om til små bogstaver, så der ikke er kapitaliseringsproblemer
                if (feedback == "k")
                { // hvis feedbacken er korrekt, så stopper den loopen og fortæller hvor mange forsøg det tog
                    antalForsøg++;
                    Console.WriteLine($"Yay! Jeg gættede dit tal som var {computerGuess} på {antalForsøg} forsøg.");
                    Thread.Sleep(10000);
                    break;
                }
                else if (feedback == "h")
                // hvis feedbacken er for højt, så sætter den max til computerens gæt, og øger antal forsøg med et
                {
                    max = computerGuess;
                    antalForsøg++;
                }
                else if (feedback == "l")
                // hvis feedbacken er for lavt, så sætter den min til computerens gæt + 1, og øger antal forsøg med et
                {
                    min = computerGuess + 1;
                    antalForsøg++;
                }
                else
                { // hvis feedbacken er noget andet end H, L eller K, så beder den om et gyldigt input
                    Console.WriteLine("Ugyldigt input. Skriv H, L eller K.");
                }


            }

        }
    }
}

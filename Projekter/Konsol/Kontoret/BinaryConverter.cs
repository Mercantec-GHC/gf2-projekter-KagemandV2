using System;
using System.Diagnostics;
using System.Threading;
namespace Kontoret
{
    public class BinaryConverter
    {
        public void Start()
        {
            // Boolean der styrer hovedløkken
            bool running = true;
           


            // Udskriver menuen første gang
            Console.Clear();
            Console.WriteLine("1. Base10/IPv4 to Binary\n2. Binary/IPv4 to Base10\n3. The Game!!!\n4. Exit");

            // Programmet kører i en while-løkke indtil brugeren vælger "Exit"
            while (running)
            {
                // Brugerens menuvalg
                string choice = "";

                // Bliver ved med at spørge indtil der tastes 1, 2, 3 eller 4
                while (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                {
                    Console.Write("Vælg en mulighed (1-4): ");
                    choice = Console.ReadLine();
                }

                // Switch håndterer brugerens valg
                switch (choice)
                {
                    // Valg 1: Base10 eller IPv4 → Binary
                    case "1":
                        Console.Write("Indtast et tal eller en IPv4 adresse i Base10: ");

                        // Rens inputtet så kun tal og punktummer er tilladt
                        string decimalInput = RemoveNonNumericOrDot(Console.ReadLine());

                        // Hvis input indeholder punktum, behandler vi det som en IPv4-adresse
                        if (decimalInput.Contains('.'))
                        {
                            Console.WriteLine($"\nBase10 IPv4 {decimalInput} konverteres til Binary IPv4 {IPv4ToBinary(decimalInput)}");
                        }
                        else
                        {
                            // Ellers er det et enkelt heltal
                            if (int.TryParse(decimalInput, out int decimalNumber))
                                Console.WriteLine($"\nBase10 {decimalNumber} konverteres til Binary {DecimalToBinary(decimalNumber)}");
                        }
                        break;

                    // Valg 2: Binary eller Binary-IPv4 → Base10
                    case "2":
                        Console.Write("Indtast et Binært tal eller en Binær IPv4 adresse ");

                        string binaryInput = RemoveNonNumericOrDot(Console.ReadLine());

                        // Hvis der er punktummer, antages det at være en IPv4 i binær
                        if (binaryInput.Contains('.'))
                            Console.WriteLine($"\nBinær IPv4 {binaryInput} konverteres til Base10 IPv4 {BinaryToIPv4(binaryInput)}");
                        else
                            // Ellers er det et enkelt binært tal
                            Console.WriteLine($"\nBinær {binaryInput} konverteres til Base10 {BinaryToDecimal(binaryInput)}");
                        break;

                    // Valg 3: Starter et lille gæt-spil
                    case "3":
                        gæt();
                        break;

                    // Valg 4: Afslutter programmet
                    case "4":
                        running = false;
                        break;
                }

                // Pause inden næste iteration
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Fjerner alt fra input der ikke er tal eller punktummer
        private static string RemoveNonNumericOrDot(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            char[] allowedChars = input.Where(c => char.IsDigit(c) || c == '.').ToArray();
            return new string(allowedChars);
        }

        // Konverterer et heltal til binær streng
        public static string DecimalToBinary(int number)
        {
            if (number == 0) return "0";

            string result = "";

            // Finder binær repræsentation ved gentagne divisioner med 2
            while (number > 0)
            {
                // Tilføjer resten (0 eller 1) foran resultatet, baseret på modulus
                result = (number % 2) + result;
                //rykker tallet ned ved at dividere med 2
                number /= 2;
            }
            
            return result;
        }
          
        // Konverterer en binær streng til heltal
        public static int BinaryToDecimal(string binær)
        {
            int result = 0;
            int power = 1;

            // Starter fra højre mod venstre
            for (int i = binær.Length - 1; i >= 0; i--)
            {
                // Hvis bit er '1', tilføj den aktuelle 2^n værdi
                if (binær[i] == '1')
                    result += power;

                // Gang vægten med 2 for næste bit
                power *= 2;
            }

            return result;
        }

        // Konverterer en IPv4 (fx 192.168.0.1) til binær IPv4
        public static string IPv4ToBinary(string ipv4)
        {
            // Splitter IPv4 adressen i dens 4 oktetter og laver en array til binære repræsentationer
            string[] parts = ipv4.Split('.');
            string[] binaryParts = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                int number = int.Parse(parts[i]);

                // Hver oktet repræsenteres på 8 bits
                string binær = DecimalToBinary(number).PadLeft(8, '0'); ;
                binaryParts[i] = binær;
            }

            return string.Join(".", binaryParts);
        }
         // Konverterer en binær IPv4 til en decimal IPv4
        public static string BinaryToIPv4(string binaryIP)
        {
            string[] parts = binaryIP.Split('.');
            string[] decimalParts = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                int decimalNumber = BinaryToDecimal(parts[i]);
                decimalParts[i] = decimalNumber.ToString();
            }

            return string.Join(".", decimalParts);
        }

        // Lille spil hvor brugeren skal gætte korrekt konvertering
        public void gæt()
        {
            Random random = new Random();
            //initialisere stopwatch til at tage tid med
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();

            int successfulAttempts = 0;
            Console.WriteLine("Du har valgt THE GAME");
            Console.WriteLine("Konverter det givne tal til enten binær eller Base10 tal");
            Console.WriteLine("du har 30 + 5 sekunder for hvert korrekt spørgsmål");
            Thread.Sleep(4000);
            stopwatch.Start();
            int tid = 30;
            while (stopwatch.Elapsed.TotalSeconds < tid)
            {
                //giver tid tilbage til brugeren
                Console.WriteLine($"Tid tilbage: {tid - (int)stopwatch.Elapsed.TotalSeconds} sekunder");
                Console.WriteLine("\n---------------------------------------------------------------------------\n");

                // Tilfældigt tal mellem 1 og 255
                int numberToGuess = random.Next(1, 256);

                // 1 = Decimal til Binær, 2 = Binær til Decimal
                int konvertion = random.Next(1, 3);

                int attempts = 1;
                bool fail = false;

                

                switch (konvertion)
                {
                    case 1:
                        // Decimal til Binær
                        Console.WriteLine($"\nKonvetere {numberToGuess} til Binær");
                        string userInputBinary = Console.ReadLine();
                        // Korrekt binær løsning for det tilfældige tal
                        string correctBinary = DecimalToBinary(numberToGuess).PadLeft(8, '0');

                        // Bruger skal gætte korrekt binær streng
                        while (userInputBinary != correctBinary)
                        {
                            Console.WriteLine("Forkert. Prøv igen.");
                            
                            attempts++;
                            if (stopwatch.Elapsed.TotalSeconds > tid)
                            {
                                fail = true;
                                
                                break;
                                

                            }
                            userInputBinary = Console.ReadLine();
                        }
                        if (fail == false || (fail == true && userInputBinary == correctBinary))
                        {
                            successfulAttempts++;
                            tid += 5;
                            Console.WriteLine($"Korrekt! Du svarede  rigtigt med {correctBinary} på {attempts} forsøg");
                        }
                        else
                        {
                            Console.WriteLine($"Tiden er oppe. Det rigtige svar var {correctBinary}. Bedre held næste gang");
                        }
                            break;

                    case 2:
                        // Binær til Decimal
                        Console.WriteLine($"Konvetere {DecimalToBinary(numberToGuess).PadLeft(8, '0')} til Base10");
                       
                        if (stopwatch.Elapsed.TotalSeconds > tid)
                        {
                            fail = true;

                            break;


                        }

                        string userInputDecimal = Console.ReadLine();

                        // Bruger skal gætte korrekt decimaltal
                        while (userInputDecimal != numberToGuess.ToString())
                        {
                            Console.WriteLine("Forkert. Prøv igen.");
                            userInputDecimal = Console.ReadLine();
                            attempts++;
                        }
                        if (fail == false || (fail == true && userInputDecimal == numberToGuess.ToString()))
                        {
                            successfulAttempts++;
                            tid += 5;
                            Console.WriteLine($"Korrekt! Du svarede rigtigt med {numberToGuess} på {attempts} antal forsøg");
                        }
                        else
                        {
                            Console.WriteLine($"Tiden er oppe. Det rigtige svar var {numberToGuess}. Bedre held næste gang");
                        }
                            break;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Du svarede rigtigt på {successfulAttempts} konversioner på {stopwatch.Elapsed.TotalSeconds} sekunder. Godt gået!! ");

        }
    }
}

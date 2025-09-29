namespace Kontoret
{
    public class BinaryConverter
    {
        public void Start()
        {
            bool running = true;
            Console.Clear();
            Console.WriteLine("1. Decimal/IPv4 to Binary\n2. Binary/IPv4 to Decimal\n3. Exit");

            while (running) //while løkken kører indtil brugeren vælger at afslutte
            {
                
                
                // Menuen for programmet, og beder brugeren om at vælge en mulighed
                string choice = "";
                while (choice != "1" && choice != "2" && choice != "3")
                { // tjekker om inputtet er 1, 2 eller 3, og hvis ikke, så beder den om et nyt input
                    Console.Write("Choose an option (1-3): ");
                    choice = Console.ReadLine();
                }

                switch (choice)
                { // switch statement til at håndtere de forskellige muligheder, og kalder de relevante metoder baseret på brugerens valg
                    case "1":
                        Console.Write("Enter a number or IPv4 address: "); // beder brugeren om at indtaste et tal, som kan være et heltal eller en IPv4 adresse
                        string decimalInput = RemoveNonNumericOrDot(Console.ReadLine()); // fjerner alle ikke-numeriske tegn undtagen '.'

                        if (decimalInput.Contains('.')) // tjekker om inputtet indeholder en '.', hvilket indikerer at det er en IPv4 adresse
                        {//og aktiverer den relevante metode til at konvertere IPv4 til binær
                            Console.WriteLine($"\nDecimal IPv4 {decimalInput} Binary IPv4 {IPv4ToBinary(decimalInput)}");
                        }
                        else
                        { // ellers antager den at det er et heltal og konverterer det til binær
                            if (int.TryParse(decimalInput, out int decimalNumber))
                                Console.WriteLine($"\nDecimal {decimalNumber} konveteres til Binary {DecimalToBinary(decimalNumber)}");
                        }
                        break;

                    case "2": // beder brugeren om at indtaste en binær kode eller en binær IPv4 adresse
                        Console.Write("Enter a binær code or binær IPv4: ");
                        string binaryInput = RemoveNonNumericOrDot(Console.ReadLine()); // fjerner alle ikke-numeriske tegn undtagen '.'

                        if (binaryInput.Contains('.')) // tjekker om inputtet indeholder en '.', hvilket indikerer at det er en binær IPv4 adresse
                            Console.WriteLine($"\nBinary IPv4 {binaryInput} konveteres til Decimal IPv4 {BinaryToIPv4(binaryInput)}");
                        else // ellers antager den at det er en binær kode og konverterer det til et heltal
                            Console.WriteLine($"\nBinary {binaryInput} konveteres til Decimal {BinaryToDecimal(binaryInput)}");
                        break;

                    case "3": // afslutter programmet ved at sætte running til false
                        running = false;
                        continue;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static string RemoveNonNumericOrDot(string input) //metode til at fjerne alle ikke-numeriske tegn undtagen '.'
        {
            if (string.IsNullOrEmpty(input))
                return "";

            char[] allowedChars = input.Where(c => char.IsDigit(c) || c == '.').ToArray(); // bruger LINQ til at filtrere tegnene og beholder kun cifre og '.'
            return new string(allowedChars);
        }

        public static string DecimalToBinary(int number) //metode til at konvertere et heltal til binær
        {
            if (number == 0) return "0"; //håndterer specialtilfældet hvor tallet er 0
            string result = "";
            while (number > 0) // konverterer tallet til binær ved at dividere det med 2 og gemme resten
            {
                result = (number % 2) + result;
                number /= 2;
                // Magic Code, hehe
            }
            return result;
        }

        public static int BinaryToDecimal(string binær)
        {
            int result = 0, power = 1; //metode til at konvertere en binær streng til et heltal
            for (int i = binær.Length - 1; i >= 0; i--) //går igennem strengen fra højre mod venstre og beregner værdien baseret på positionen
            {
                if (binær[i] == '1') result += power; //hvis tegnet er '1', så lægger den den nuværende power til resultatet
                power *= 2; //opdaterer power ved at multiplicere den med 2 for hver position, da binær er base 2
            }
            return result;
        }

        public static string IPv4ToBinary(string ipv4)
        {//metode til at konvertere en IPv4 adresse til binær
            string[] parts = ipv4.Split('.'); //splitter adressen op i dens fire oktetter, eller hvor mange der nu er
            string[] binaryParts = new string[parts.Length]; //opretter et array til at holde de binære repræsentationer af hver oktet

            for (int i = 0; i < parts.Length; i++) //går igennem hver del, konverterer den til et heltal, derefter til binær, 
            {//og sørger for at den er 8 bits lang ved at padde med nuller foran hvis nødvendigt, da en oktet altid er 8 bits
                //koden er dog sat op til at håndtere flere eller færre end 4 oktetter, og mere end 255 i hver oktet
                int number = int.Parse(parts[i]);//konverterer strengen til et heltal
                string binær = DecimalToBinary(number).PadLeft(8, '0'); //Konvetere til heltal, ved at kalde DecimalToBinary metoden
                binaryParts[i] = binær; //gemmer den binære repræsentation i arrayet
            }

            return string.Join(".", binaryParts);//samler de binære oktetter tilbage til en enkelt streng adskilt af punktummer
        }

        public static string BinaryToIPv4(string binaryIP)
        { //gør det samme som IPv4ToBinary, bare den anden vej rundt, og fra binær af
            string[] parts = binaryIP.Split('.');
            string[] decimalParts = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                int decimalNumber = BinaryToDecimal(parts[i]);
                decimalParts[i] = decimalNumber.ToString();
            }

            return string.Join(".", decimalParts);
        }
        public void gæt()
        {
            Random random = new Random();
            int numberToGuess = random.Next(1, 256);
            int konvertion = random.Next(1, 3);

            
        }
    }
}

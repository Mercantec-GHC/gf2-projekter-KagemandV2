using System;

namespace Opgaver
{
    public class Arrays
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Arrays, List og Dictionary!");

            // Opgaverne herunder går igennem ting vi skal kunne med arrays, list og dictionary
            // Da I ikke har lært omkring loops og metoder endnu, er det ikke nødvendigt at bruge dem her
            // I må dog gerne bruge loops og metoder i opgaverne herunder

            //Array1();
            //Array2();
            //Array3();
            //List1();
            //List2();
            //List3();
            //List4();
            //List5();
            //Dict1();
            //Dict2();
           
            //MiniProjektKlasseliste();
            MiniProjektIndkøbsliste();
        }

        public static void Array1()
        {
            Console.WriteLine("Opgave 1 (Array):");
            Console.WriteLine(
                "Lav et program som gemmer 5 fornavne som brugeren indtaster i et array."
            );
            // Lav opgaven herunder!
            string[] navne = new string[5];
            for (int i = 0; i < navne.Length; i++)
            {
                Console.Write($"Indtast navn {i + 1}: ");
                navne[i] = Console.ReadLine();
            }
            Console.WriteLine("De indtastede navne er:");
            foreach (string navn in navne)
            {
                Console.WriteLine(navn);
            }
            Console.WriteLine();
        }

        public static void Array2()
        {
            Console.WriteLine("Opgave 2 (Array):");
            Console.WriteLine(
                "Lav et program som gemmer 5 tal i et array og udskriver det største tal."
            );
            // Lav opgaven herunder!
            
               
                
                    int[] tal = new int[5];
                    for (int i = 0; i < tal.Length; i++)
                    {
                        Console.Write($"Indtast tal {i + 1}: ");
                        while (true)
                        {
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int nummer))
                            {
                                tal[i] = nummer;
                                break; // Gå videre til næste tal
                            }
                            else
                            {
                                Console.Write("Ugyldigt input. Indtast venligst et heltal: ");
                            }
                        }
                    }
                    int størsteTal = tal[0];
                    foreach (int t in tal)
                    {
                        if (t > størsteTal)
                        {
                            størsteTal = t;
                        }
                    }
                    Console.WriteLine($"Det største tal er: {størsteTal}");
                    Console.WriteLine();
                   
                
                
            
        }

        public static void Array3()
        {
            Console.WriteLine("Opgave 3 (Array):");
            Console.WriteLine(
                @"Lav et program som gemmer 5 bynavne i et array 
                og udskriver dem alle i omvendt rækkefølge."
            );
            // Lav opgaven herunder!
            string[] bynavne = new string[5];
            for (int i = 0; i < bynavne.Length; i++)
            {
                Console.Write($"Indtast bynavn {i + 1}: ");
                bynavne[i] = Console.ReadLine();
            }
            Console.WriteLine("Bynavnene i omvendt rækkefølge er:");
            for (int i = bynavne.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(bynavne[i]);
            }
            Console.WriteLine();

        }

        public static void List1()
        {
            Console.WriteLine("Opgave 1 (List):");
            Console.WriteLine(
                @"Lav et program som gemmer 5 fornavne 
                som brugeren indtaster i en liste."
            );
            // Lav opgaven herunder!
            List<string> navne = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Indtast navn {i + 1}: ");
                navne.Add(Console.ReadLine());
            }
            Console.WriteLine("De indtastede navne er:");
            foreach (string navn in navne)
            {
                Console.WriteLine(navn);
            }
            Console.WriteLine();
        }

        public static void List2()
        {
            Console.WriteLine("Opgave 2 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren kan blive ved med at indtaste 
                navne indtil de skriver 'stop'. Udskriv alle navnene til sidst."
            );
            // Lav opgaven herunder!
            List<string> navne = new List<string>();
            while (true)
            {
                Console.Write("Indtast et navn (eller 'stop' for at afslutte): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "stop")
                {
                    break;
                }
                navne.Add(input);
            }
            Console.WriteLine("De indtastede navne er:");
            foreach (string navn in navne)
            {
                Console.WriteLine(navn);
            }
            Console.WriteLine();

        }

        public static void List3()
        {
            Console.WriteLine("Opgave 3 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster 5 tal i en liste 
                og programmet udskriver gennemsnittet."
            );
            // Lav opgaven herunder!
            int[] tal = new int[5];
            for (int i = 0; i < tal.Length; i++)
            {
                Console.Write($"Indtast tal {i + 1}: ");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int nummer))
                    {
                        tal[i] = nummer;
                        break; // Gå videre til næste tal
                    }
                    else
                    {
                        Console.Write("Ugyldigt input. Indtast venligst et heltal: ");
                    }
                }
            }
            double gennemsnit = tal.Average();
            Console.WriteLine($"Gennemsnittet af de indtastede tal er: {gennemsnit}");
        }

        public static void List4()
        {
            Console.WriteLine("Opgave 4 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster navne på ting de skal købe, 
                og kan fjerne ting fra listen igen. Udskriv listen til sidst."
            );
            // Lav opgaven herunder!
            List<string> indkøbsliste = new List<string>();
            while (true)
            {
                Console.Write("Indtast en ting til indkøbslisten, eller skriv fjern for at fjerne noget (eller 'stop' for at afslutte): ");
                Console.WriteLine("");
                string input = Console.ReadLine();
                if (input.ToLower() == "stop")
                {
                    break;
                }
                else if (input.ToLower().StartsWith("fjern"))
                {
                    string tingAtFjerne = input.Substring(6).Trim();
                    if (indkøbsliste.Contains(tingAtFjerne))
                    {
                        indkøbsliste.Remove(tingAtFjerne);
                        Console.WriteLine($"{tingAtFjerne} er fjernet fra indkøbslisten.");
                    }
                    else
                    {
                        Console.WriteLine($"{tingAtFjerne} findes ikke på indkøbslisten.");
                    }
                }
                else
                    indkøbsliste.Add(input);
            }
            Console.WriteLine("Din indkøbsliste indeholder:");
            foreach (string ting in indkøbsliste)
            {
                Console.WriteLine(ting);
            }
            Console.WriteLine();

        }

        public static void List5()
        {
            Console.WriteLine("Opgave 5 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster navne på sine venner 
                i en liste og programmet udskriver hvor mange navne der starter med 'A'."
            );
            // Lav opgaven herunder!
            List<string> venner = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Indtast navn på ven {i + 1}: ");
                venner.Add(Console.ReadLine());
            }
            int antalMedA = venner.Count(navn => navn.StartsWith("A", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Antal venner der starter med 'A': {antalMedA}");
            Console.WriteLine();

        }

        public static void Dict1()
        {
            Console.WriteLine("Opgave 1 (Dictionary):");
            Console.WriteLine(
                @"Lav et program hvor du gemmer navne og alder på 3 personer 
                i en dictionary og udskriver dem alle."
            );
            // Lav opgaven herunder!
            // Husk syntaxen for Dictionary<type, type> navn = new Dictionary<type, type>();
            Dictionary<string, int> personer = new Dictionary<string, int>();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Indtast navn på person {i + 1}: ");
                string navn = Console.ReadLine();
                Console.Write($"Indtast alder på {navn}: ");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int alder))
                    {
                        personer[navn] = alder;
                        break; // Gå videre til næste person
                    }
                    else
                    {
                        Console.Write("Ugyldigt input. Indtast venligst et heltal for alder: ");
                    }
                }
            }
            Console.WriteLine("De indtastede personer er:");
            foreach (var person in personer)
            {
                Console.WriteLine($"{person.Key} er {person.Value} år gammel.");
            }
            Console.WriteLine();

        }

        public static void Dict2()
        {
            Console.WriteLine("Opgave 2 (Dictionary):");
            Console.WriteLine(
                @"Lav et program hvor brugeren kan indtaste et navn 
                og få alderen på personen ud fra dictionaryen fra før."
            );
            // Lav opgaven herunder!
            Dictionary<string, int> personer = new Dictionary<string, int>
            {
                { "Alice", 30 },
                { "Bob", 25 },
                { "Charlie", 35 }
            };
            Console.Write("Indtast et navn for at få alderen: ");
            string navnInput = Console.ReadLine();
            if (personer.TryGetValue(navnInput, out int alder))
            {
                Console.WriteLine($"{navnInput} er {alder} år gammel.");
            }
            else
            {
                Console.WriteLine($"{navnInput} findes ikke i systemet.");
            }
            Console.WriteLine();
        }

        public static void MiniProjektKlasseliste()
        {
            Console.WriteLine("\nMini-projekt: Klasseliste (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                "Lav et program, hvor brugeren indtaster navnene på alle elever i en klasse (fx 5 navne)."
            );
            Console.WriteLine(
                @"Gem navnene i en liste og udskriv hele klasselisten 
                  i konsollen."
            );
            // Lav opgaven herunder!
            List<string> klasseliste = new List<string>();
            Console.WriteLine("hvor mange elever er det i klassen?");
            int antalElever;
            while (!int.TryParse(Console.ReadLine(), out antalElever) || antalElever <= 0)
            {
                Console.WriteLine("Ugyldigt input. Indtast venligst et positivt heltal for antal elever:");
            }
            for (int i = 0; i < antalElever; i++)
            {
                Console.Write($"Indtast navn på elev {i + 1}: ");
                klasseliste.Add(Console.ReadLine());
            }
            Console.WriteLine("Klasselisten indeholder:");
            foreach (string elev in klasseliste)
            {
                Console.WriteLine(elev);
            }
            Console.WriteLine();
        }

        public static void MiniProjektIndkøbsliste()
        {
            Console.WriteLine("\nMini-projekt: Indkøbsliste (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                @"Lav et program, hvor brugeren indtaster navnet på tre ting og deres pris, 
                de skal købe i supermarkedet."
            );
            Console.WriteLine(
                @"Gem tingene i et key-value par med navn og pris, 
                og udskriv en indkøbsliste med total pris til brugeren."
            );
            // Lav opgaven herunder!
           
            Dictionary<string, double> indkøbsliste = new Dictionary<string, double>();
            Console.WriteLine("indtast antallet af ting du vil købe");
            int antalTing;
            while (!int.TryParse(Console.ReadLine(), out antalTing) || antalTing <= 0)
            {
                Console.WriteLine("Ugyldigt input. Indtast venligst et positivt heltal for antal ting du vil købe:");
            }
            for (int i = 0; i < antalTing; i++)
                {
                Console.Write($"Indtast navn på ting {i + 1}: ");
                string ting = Console.ReadLine();
                Console.Write($"Indtast pris på {ting}: ");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (double.TryParse(input, out double pris))
                    {
                        indkøbsliste[ting] = pris;
                        break; // Gå videre til næste ting
                    }
                    else
                    {
                        Console.Write("Ugyldigt input. Indtast venligst et tal for pris: ");
                    }
                }
            }
            Console.WriteLine("Din indkøbsliste indeholder:");
            double totalPris = 0;
            foreach (var item in indkøbsliste)
            {
                Console.WriteLine($"{item.Key}: {item.Value} kr");
                totalPris += item.Value;
            }
            Console.WriteLine($"Total pris: {totalPris} kr");
            Console.WriteLine();


        }
    }
}

namespace Hjemmet
{
    public class TicTacToe
    {
        public void Start()
        {
            Console.WriteLine("⚙️ Tic-Tac-Toe mod MIG; Den bedste Maskin Ånd!");
            char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' }; // starter bordet op
            char currentPlayer = 'X'; // mennesket starter
            int moves = 0; // tæller antal moves
            bool gameWon = false; // tjekker om spillet er vundet
            Random rng = new Random(); // random nummer generator til maskinens træk

            while (moves < 9 && !gameWon)
            {
                // Tegner bordet. Dette gælder kun, mens man er under 9 moves, og spillet ikker er vundet
                Console.Clear();
                Console.WriteLine("╔═══╦═══╦═══╗");
                Console.WriteLine($"║ {board[0]} ║ {board[1]} ║ {board[2]} ║");
                Console.WriteLine("╠═══╬═══╬═══╣");
                Console.WriteLine($"║ {board[3]} ║ {board[4]} ║ {board[5]} ║");
                Console.WriteLine("╠═══╬═══╬═══╣");
                Console.WriteLine($"║ {board[6]} ║ {board[7]} ║ {board[8]} ║");
                Console.WriteLine("╚═══╩═══╩═══╝");

                int choice = -1; // initialiserer choice til et ugyldigt tal

                if (currentPlayer == 'X')
                {
                    // Menneskets tur
                    Console.WriteLine($"\nSpiller {currentPlayer}, vælg et felt (1-9): "); // beder spilleren om at vælge et felt
                    string input = Console.ReadLine(); // læser inputtet 

                    if (int.TryParse(input, out int num) && num >= 1 && num <= 9)
                    { //accepter kun Int's mellem 1-9, mens de andre tal giver ikke et output, og lader dig skrive igen
                        if (board[num - 1] != 'X' && board[num - 1] != 'O') //tjekker om feltet er optaget og ellers accepterer det
                        {
                            choice = num; // sætter choice til det valgte nummer
                        }
                        else
                        {
                            Console.WriteLine("Feltet er optaget. Tryk en tast for at prøve igen.");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt input. Tryk en tast for at prøve igen.");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    // Maskinens tur
                    Console.WriteLine("Den Ædle Maskinånd overvejer sit valg...");
                    System.Threading.Thread.Sleep(3000); // lille pause for dramatik

                    // Find tilfældigt ledigt felt
                    do
                    {
                        choice = rng.Next(1, 10); // 1–9
                    } while (board[choice - 1] == 'X' || board[choice - 1] == 'O'); // tjekker om feltet er optaget

                    Console.WriteLine($"⚙️ Maskinånd vælger felt {choice}");
                    System.Threading.Thread.Sleep(1000);
                }

                // Udfør træk
                board[choice - 1] = currentPlayer;
                moves++;

                // Check for sejr
                int[,] winConditions = new int[,]
                {
                    {0,1,2}, {3,4,5}, {6,7,8}, // rows
                    {0,3,6}, {1,4,7}, {2,5,8}, // cols
                    {0,4,8}, {2,4,6}           // diagonals
                };

                for (int i = 0; i < winConditions.GetLength(0); i++) // tjekker alle vinde betingelser
                {
                    if (board[winConditions[i, 0]] == currentPlayer &&  // hvis en af betingelserne er opfyldt, så vinder den spiller
                        board[winConditions[i, 1]] == currentPlayer &&
                        board[winConditions[i, 2]] == currentPlayer)
                    {
                        Console.Clear();
                        Console.WriteLine("╔═══╦═══╦═══╗");
                        Console.WriteLine($"║ {board[0]} ║ {board[1]} ║ {board[2]} ║");
                        Console.WriteLine("╠═══╬═══╬═══╣");
                        Console.WriteLine($"║ {board[3]} ║ {board[4]} ║ {board[5]} ║");
                        Console.WriteLine("╠═══╬═══╬═══╣");
                        Console.WriteLine($"║ {board[6]} ║ {board[7]} ║ {board[8]} ║");
                        Console.WriteLine("╚═══╩═══╩═══╝"); // tegner det endelige bord

                        Console.WriteLine($" Spiller {currentPlayer} har vundet! Maskinen bøjer sig overfor sin sande mester");
                        gameWon = true; // sætter gameWon til true for at stoppe spillet
                        Console.ReadKey();
                        break;
                    }
                }

                if (!gameWon)
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // skifter spiller, hvis spillet ikke er vundet
                }
            }

            if (!gameWon) // hvis spillet ikke er vundet efter 9 moves, så er det uafgjort
            {
                Console.Clear();
                Console.WriteLine("╔═══╦═══╦═══╗");
                Console.WriteLine($"║ {board[0]} ║ {board[1]} ║ {board[2]} ║");
                Console.WriteLine("╠═══╬═══╬═══╣");
                Console.WriteLine($"║ {board[3]} ║ {board[4]} ║ {board[5]} ║");
                Console.WriteLine("╠═══╬═══╬═══╣");
                Console.WriteLine($"║ {board[6]} ║ {board[7]} ║ {board[8]} ║");
                Console.WriteLine("╚═══╩═══╩═══╝"); // tegner det endelige bord ved en uafgjort slutning
                Console.WriteLine(" afgjort! Maskinen er sur, fordi den vandt eller tabte ikke, Giv den en ekstra dosis røgelse");
                Console.ReadKey();
            }
        }
    }
}
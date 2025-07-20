using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace MidProject
{
    internal class GameManager
    {
        private Deck deck;
        private Queue stockpile;
        private Queue wastePile;
        private Stack[] stacks = new Stack[7];
        private Stack[] foundations = new Stack[4];
        private int score;
        private Stack undoStack;
        private Stack redoStack;
        SoundPlayer player = new SoundPlayer(@"D:\Semester_3\DSA\Solitaire Game\CSC200M24PID79\MidProject\Resources\pageturn-102978.wav");
        
       
        public GameManager()
        {
            deck = new Deck();
            stockpile = new Queue();
            wastePile = new Queue();
            for (int i = 0; i < 7; i++)
            {
                stacks[i] = new Stack();
            }
            for (int i = 0; i < 4; i++)
            {
                foundations[i] = new Stack();
            }
            deck.LoadDeck();
            undoStack = new Stack();
            redoStack = new Stack();
        }
        
        public void StartGame()
        {
            
            CenterConsole(45,33);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;

            deck.Shuffle();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Card card = deck.DrawCard();
                    if (j == i)
                    {
                        card.FaceUp = true;
                    }
                    stacks[i].Push(card);
                }
            }
           
            for (int i = 0; i < 24; i++)
            {
                stockpile.Enqueue(deck.DrawCard());
            }
            
            GamePlayMenu();



        }
        public void PrintStacks()
        {
            int maxHeight = stacks.Max(stack => stack.GetLength());

            for (int i = 0; i < maxHeight; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i < stacks[j].GetLength())
                    {
                        Node temp = stacks[j].Head;
                        for (int k = 0; k < stacks[j].GetLength() - i - 1; k++)
                        {
                            temp = temp.Next;
                        }
                        if (temp.Data.FaceUp)
                        {
                            Console.Write(temp.Data.CardName + "\t");
                        }
                        else
                        {
                            Console.Write("×\t");
                        }
                    }
                    else
                    {
                        Console.Write("\t");
                    }
                }
                Console.WriteLine();
            }

        }
        public void PrintFoundations()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Foundation " + (i + 1) + ": ");
                if (foundations[i].Head != null)
                {
                    Console.Write(foundations[i].Head.Data.CardName);
                }
                Console.WriteLine();
            }
            
        }
        public void PrintStockpile()
        {
            Console.Write("Stockpile: ");
            if (!stockpile.IsEmpty())
            {
                Card topCard = stockpile.Peek().Data;
                Console.Write(topCard.CardName);
            }
            else if (!wastePile.IsEmpty())
            {
                Console.Write("WastePile: ");
                Card topCard = wastePile.Peek().Data;
                Console.Write(topCard.FaceUp ? topCard.CardName : "×");
            }
            else
            {
                Console.Write("Empty");
            }
            Console.WriteLine();
        }
        public void GamePlayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("                    → ♥");
                Console.Write(" → ♦");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" → ♣");
                Console.WriteLine(" → ♠");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("(1)----(2)-----(3)-----(4)-----(5)-----(6)-----(7)-----");
                PrintStacks();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                PrintStockpile();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                PrintFoundations();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("1. Move card from stockpile to stack");
                Console.WriteLine("2. Move card from stockpile to foundation");
                Console.WriteLine("3. Move card from stack to foundation");
                Console.WriteLine("4. Move card from stack to stack");
                Console.WriteLine("5. Next Card in Stockpile");
                Console.WriteLine("6. Undo");
                Console.WriteLine("7. Redo");
                Console.WriteLine("8. Quit");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Enter your choice: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("-------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
               
                string choice = Console.ReadLine();

                
                    switch (choice)
                {
                    case "1":
                        player.Play();
                        MoveStockpileToStack();
                        break;
                    case "2":
                        player.Play();
                        MoveStockpileToFoundation();
                        break;
                    case "3":
                        player.Play();
                        MoveStackToFoundation();
                        break;
                    case "4":
                        player.Play();
                        MoveStackToStack();
                        break;
                    case "5":
                        player.Play();
                        if (stockpile.IsEmpty() && !wastePile.IsEmpty())
                        {
                            while (!wastePile.IsEmpty())
                            {
                                Card card = wastePile.Dequeue();
                                card.FaceUp = false;
                                stockpile.Enqueue(card);
                            }
                        }
                        else if (!stockpile.IsEmpty())
                        {
                            Card card = stockpile.Dequeue();
                            card.FaceUp = true;
                            wastePile.Enqueue(card);
                        }
                        else
                        {
                            Console.WriteLine("Both stockpile and waste pile are empty.");
                        }
                        break;

                    case "6":
                        player.Play();
                        Undo();
                        break;
                    case "7":
                        player.Play();
                        Redo();
                        break;
                    case "8":
                        player.Play();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        GamePlayMenu();
                        break;
                }
            }
        } 
      
        public static int GetRankValue(string rank)
        {
            switch (rank)
            {
                case "A": return 1;
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "10": return 10;
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                default: throw new ArgumentException("Invalid card rank");
            }
        }
        public void MoveStockpileToStack()
        {
            if (stockpile.IsEmpty())
            {
                Console.WriteLine("Stockpile is empty.");
                return;
            }

            Console.Write("Enter the stack number (1-7): ");
            string input = Console.ReadLine();
            int stackNumber;
            if (!int.TryParse(input, out stackNumber) || stackNumber < 1 || stackNumber > 7)
            {
                Console.WriteLine("Invalid stack number.");
                return;
            }

            Card stockpileCard = stockpile.Peek().Data;

            if (stacks[stackNumber - 1].IsEmpty())
            {
                if (stockpileCard.Rank == "K")
                {
                    stockpileCard = stockpile.Dequeue();
                    stockpileCard.FaceUp = true;
                    stacks[stackNumber - 1].Push(stockpileCard);
                    Console.WriteLine("Card moved to empty stack.");
                }
                else
                {
                    Console.WriteLine("Invalid move. Only Kings can be placed on an empty stack.");
                }
            }
            else
            {
                Card topCard = stacks[stackNumber - 1].Peek().Data;

                if (GetRankValue(topCard.Rank) == GetRankValue(stockpileCard.Rank) + 1 && topCard.Color != stockpileCard.Color)
                {
                    stockpileCard = stockpile.Dequeue();
                    stockpileCard.FaceUp = true;
                    stacks[stackNumber - 1].Push(stockpileCard);
                    Console.WriteLine("Card moved to stack.");
                }
                else
                {
                    Console.WriteLine("Invalid move. Card must be one rank lower and of opposite color.");
                }
            }

            Node move = new Node
            {
                MoveType = MoveType.StockpileToStack,
                Card = stockpileCard,
                StackNumber = stackNumber
            };
            undoStack.Push(move);
        }
        public void MoveStockpileToFoundation()
        {
            if (stockpile.IsEmpty())
            {
                Console.WriteLine("Stockpile is empty.");
                return;
            }
            Console.Write("Enter the foundation number (1-4): ");
            string input = Console.ReadLine();
            int foundationNumber;
            if (!int.TryParse(input, out foundationNumber) || foundationNumber < 1 || foundationNumber > 4)
            {
                Console.WriteLine("Invalid foundation number.");
                return;
            }

            Card card = stockpile.Peek().Data;

            if (foundations[foundationNumber - 1].IsEmpty())
            {
                if (card.Rank == "A")
                {
                    card = stockpile.Dequeue();
                    card.FaceUp = true; 
                    foundations[foundationNumber - 1].Push(card); 
                    Console.WriteLine("Card moved to foundation.");
                }
                else
                {
                    Console.WriteLine("Only an Ace can be placed on an empty foundation.");
                }
            }
            else
            {
                Card topCard = foundations[foundationNumber - 1].Peek().Data;

                if (GetRankValue(card.Rank) == GetRankValue(topCard.Rank) + 1 && card.Suit == topCard.Suit)
                {
                    card = stockpile.Dequeue(); 
                    card.FaceUp = true;
                    foundations[foundationNumber - 1].Push(card); 
                    Console.WriteLine("Card moved to foundation.");
                }
                else
                {
                    Console.WriteLine("Invalid move. Card must be one rank higher and of the same suit.");
                }
            }
            Node move = new Node
            {
                MoveType = MoveType.StockpileToFoundation,
                Card = card,
                FoundationNumber = foundationNumber
            };
            undoStack.Push(move);
        }
        public void MoveStackToFoundation()
        {
            Console.Write("Enter the stack number (1-7): ");
            if (!int.TryParse(Console.ReadLine(), out int stackNumber) || stackNumber < 1 || stackNumber > 7)
            {
                Console.WriteLine("Invalid stack number.");
                return;
            }

            if (stacks[stackNumber - 1].IsEmpty())
            {
                Console.WriteLine("Selected stack is empty.");
                return;
            }

            Card card = stacks[stackNumber - 1].Peek().Data;

            Console.Write("Enter the foundation number (1-4): ");
            if (!int.TryParse(Console.ReadLine(), out int foundationNumber) || foundationNumber < 1 || foundationNumber > 4)
            {
                Console.WriteLine("Invalid foundation number.");
                return;
            }

            if (foundations[foundationNumber - 1].IsEmpty())
            {
                if (card.Rank == "A")
                {
                    card = stacks[stackNumber - 1].Pop().Data;
                    card.FaceUp = true;
                    foundations[foundationNumber - 1].Push(card);

                    if (!stacks[stackNumber - 1].IsEmpty())
                    {
                        stacks[stackNumber - 1].Peek().Data.FaceUp = true;
                    }

                    Console.WriteLine("Card moved to foundation.");
                }
                else
                {
                    Console.WriteLine("Invalid move. Only an Ace can start a foundation.");
                }
            }
            else
            {
                Card topCard = foundations[foundationNumber - 1].Peek().Data;
                if (GetRankValue(topCard.Rank) + 1 == GetRankValue(card.Rank) && topCard.Suit == card.Suit)
                {
                    card = stacks[stackNumber - 1].Pop().Data;
                    card.FaceUp = true;
                    foundations[foundationNumber - 1].Push(card);

                    if (!stacks[stackNumber - 1].IsEmpty())
                    {
                        stacks[stackNumber - 1].Peek().Data.FaceUp = true;
                    }

                    Console.WriteLine("Card moved to foundation.");
                }
                else
                {
                    Console.WriteLine("Invalid move. Card must be of the same suit and one rank higher.");
                }
            }
            Node move = new Node
            {
                MoveType = MoveType.StackToFoundation,
                Card = card,
                SourceStackNumber = stackNumber,
                FoundationNumber = foundationNumber
            };
            undoStack.Push(move);
        }
        public void MoveStackToStack()
        {
            Console.Write("Enter the source stack number (1-7): ");
            if (!int.TryParse(Console.ReadLine(), out int sourceStackNumber) || sourceStackNumber < 1 || sourceStackNumber > 7)
            {
                Console.WriteLine("Invalid stack number.");
                return;
            }

            if (stacks[sourceStackNumber - 1].IsEmpty())
            {
                Console.WriteLine("Source stack is empty.");
                return;
            }

            Node topNode = stacks[sourceStackNumber - 1].Peek();

            Console.Write("Enter the destination stack number (1-7): ");
            if (!int.TryParse(Console.ReadLine(), out int destinationStackNumber) || destinationStackNumber < 1 || destinationStackNumber > 7)
            {
                Console.WriteLine("Invalid stack number.");
                return;
            }

            Card cardToMove = topNode.Data;

            if (!stacks[destinationStackNumber - 1].IsEmpty())
            {
                Card topCard = stacks[destinationStackNumber - 1].Peek().Data;

                if (!(GetRankValue(topCard.Rank) == GetRankValue(cardToMove.Rank) + 1 && topCard.Color != cardToMove.Color))
                {
                    Console.WriteLine("Invalid move.");
                    return;
                }
            }
            else if (cardToMove.Rank != "K")
            {
                Console.WriteLine("Only a King can be moved to an empty stack.");
                return;
            }

            stacks[sourceStackNumber - 1].Pop(); 
            stacks[destinationStackNumber - 1].Push(cardToMove);  

            if (!stacks[sourceStackNumber - 1].IsEmpty())
            {
                stacks[sourceStackNumber - 1].Peek().Data.FaceUp = true;
            }

            Node move = new Node
            {
                MoveType = MoveType.StackToStack,
                Card = cardToMove,
                SourceStackNumber = sourceStackNumber,
                DestinationStackNumber = destinationStackNumber
            };
            undoStack.Push(move);
        }



        public void Undo()
        {
            if (undoStack.IsEmpty())
            {
                Console.WriteLine("No moves to undo.");
                return;
            }
            Node node = undoStack.Pop();
            redoStack.Push(node);
            switch (node.MoveType)
            {
                case MoveType.StockpileToStack:
                    Card card = node.Card;
                    int stackNumber = node.StackNumber;
                    stacks[stackNumber - 1].Pop();
                    stockpile.Enqueue(card);
                    break;
                case MoveType.StockpileToFoundation:
                    card = node.Card;
                    int foundationNumber = node.FoundationNumber;
                    foundations[foundationNumber - 1].Pop();
                    stockpile.Enqueue(card);
                    break;
                case MoveType.StackToFoundation:
                    card = node.Card;
                    int sourceStackNumber = node.SourceStackNumber;
                    foundationNumber = node.FoundationNumber;
                    foundations[foundationNumber - 1].Pop();
                    stacks[sourceStackNumber - 1].Push(card);
                    break;
                case MoveType.StackToStack:
                    card = node.Card;
                    sourceStackNumber = node.SourceStackNumber;
                    int destinationStackNumber = node.DestinationStackNumber;
                    stacks[destinationStackNumber - 1].Pop();
                    stacks[sourceStackNumber - 1].Push(card);
                    break;
            }
        }

        
        public void Redo()
        {
            if (redoStack.IsEmpty())
            {
                Console.WriteLine("No moves to redo.");
                return;
            }
            Node node = redoStack.Pop();
            undoStack.Push(node);
            switch (node.MoveType)
            {
                case MoveType.StockpileToStack:
                    Card card = node.Card;
                    int stackNumber = node.StackNumber;
                    stockpile.Dequeue();
                    card.FaceUp = true;
                    stacks[stackNumber - 1].Push(card);
                    break;
                case MoveType.StockpileToFoundation:
                    card = node.Card;
                    int foundationNumber = node.FoundationNumber;
                    stockpile.Dequeue();
                    card.FaceUp = true;
                    foundations[foundationNumber - 1].Push(card);
                    break;
                case MoveType.StackToFoundation:
                    card = node.Card;
                    int sourceStackNumber = node.SourceStackNumber;
                    foundationNumber = node.FoundationNumber;
                    stacks[sourceStackNumber - 1].Pop();
                    card.FaceUp = true;
                    foundations[foundationNumber - 1].Push(card);
                    break;
                case MoveType.StackToStack:
                    card = node.Card;
                    sourceStackNumber = node.SourceStackNumber;
                    int destinationStackNumber = node.DestinationStackNumber;
                    stacks[sourceStackNumber - 1].Pop();
                    card.FaceUp = true;
                    stacks[destinationStackNumber - 1].Push(card);
                    break;
            }
        }
        public void OptionsMenu()
        {
            player.Play();
            CenterConsole(70,30);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine(@"
 __    __     _                            _          __       _ _ _        _            _ 
/ / /\ \ \___| | ___ ___  _ __ ___   ___  | |_ ___   / _\ ___ | (_) |_ __ _(_)_ __ ___  / \
\ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  \ \ / _ \| | | __/ _` | | '__/ _ \/  /
 \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | _\ \ (_) | | | || (_| | | | |  __/\_/ 
  \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/  \__/\___/|_|_|\__\__,_|_|_|  \___\/   
                                                                                           
 _     __ _             _       ___                     _ _                                
/ |   / _\ |_ __ _ _ __| |_    / _ \__ _ _ __ ___   ___( | )                               
| |   \ \| __/ _` | '__| __|  / /_\/ _` | '_ ` _ \ / _ \V V                                
| |_  _\ \ || (_| | |  | |_  / /_\\ (_| | | | | | |  __/                                   
|_(_) \__/\__\__,_|_|   \__| \____/\__,_|_| |_| |_|\___|                                   
                                                                                           
 ____        __      _ _   _ _                                                             
|___ \      /__\_  _(_) |_( | )                                                            
  __) |    /_\ \ \/ / | __|V V                                                             
 / __/ _  //__  >  <| | |_                                                                 
|_____(_) \__/ /_/\_\_|\__|                                                                
                                                                                           
 _ _  __      _                                            _           _              _ _  
( | )/__\ __ | |_ ___ _ __   _   _  ___  _   _ _ __    ___| |__   ___ (_) ___ ___ _  ( | ) 
 V V/_\| '_ \| __/ _ \ '__| | | | |/ _ \| | | | '__|  / __| '_ \ / _ \| |/ __/ _ (_)  V V  
   //__| | | | ||  __/ |    | |_| | (_) | |_| | |    | (__| | | | (_) | | (_|  __/_        
   \__/|_| |_|\__\___|_|     \__, |\___/ \__,_|_|     \___|_| |_|\___/|_|\___\___(_)       
                             |___/                                                         
");
        
        string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    player.Play();
                    Console.Clear();
                    StartGame();
                    break;
                case "2":
                    player.Play();
                    player.Play();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    OptionsMenu();
                    break;
            }
        }
        public void GameOver()
        {
            CenterConsole(100, 30);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine(@"
 _ _  ____                            _         _       _   _                 _  __   __            _                                             _   _                                       _ _ _ 
( | )/ ___|___  _ __   __ _ _ __ __ _| |_ _   _| | __ _| |_(_) ___  _ __  ___| | \ \ / /__  _   _  | |__   __ ___   _____  __      _____  _ __   | |_| |__   ___    __ _  __ _ _ __ ___   ___| ( | )
 V V| |   / _ \| '_ \ / _` | '__/ _` | __| | | | |/ _` | __| |/ _ \| '_ \/ __| |  \ V / _ \| | | | | '_ \ / _` \ \ / / _ \ \ \ /\ / / _ \| '_ \  | __| '_ \ / _ \  / _` |/ _` | '_ ` _ \ / _ \ |V V 
    | |__| (_) | | | | (_| | | | (_| | |_| |_| | | (_| | |_| | (_) | | | \__ \_|   | | (_) | |_| | | | | | (_| |\ V /  __/  \ V  V / (_) | | | | | |_| | | |  __/ | (_| | (_| | | | | | |  __/_|    
     \____\___/|_| |_|\__, |_|  \__,_|\__|\__,_|_|\__,_|\__|_|\___/|_| |_|___(_)   |_|\___/ \__,_| |_| |_|\__,_| \_/ \___|   \_/\_/ \___/|_| |_|  \__|_| |_|\___|  \__, |\__,_|_| |_| |_|\___(_)    
 _ ___        __      |___/     _                       _ _ _          _                _                                 _      ___ _ _                           |___/                            
( | ) \      / /__  _   _| | __| |  _   _  ___  _   _  | (_) | _____  | |_ ___    _ __ | | __ _ _   _    __ _  __ _  __ _(_)_ __|__ ( | )                                                           
 V V \ \ /\ / / _ \| | | | |/ _` | | | | |/ _ \| | | | | | | |/ / _ \ | __/ _ \  | '_ \| |/ _` | | | |  / _` |/ _` |/ _` | | '_ \ / /V V                                                            
      \ V  V / (_) | |_| | | (_| | | |_| | (_) | |_| | | | |   <  __/ | || (_) | | |_) | | (_| | |_| | | (_| | (_| | (_| | | | | |_|                                                                
 _ _ _ \_/\_/ \___/ \__,_|_|\__,_|  \__, |\___/ \__,_| |_|_|_|\_\___|  \__\___/  | .__/|_|\__,_|\__, |  \__,_|\__, |\__,_|_|_| |_(_)                                                                
( | ) |   \ \ / /__  ___( | )       |___/                                        |_|            |___/         |___/                                                                                 
 V V| |    \ V / _ \/ __|V V                                                                                                                                                                        
    | |_    | |  __/\__ \                                                                                                                                                                           
 _ _|_(_)   |_|\___||___/ _ _                                                                                                                                                                       
( | )___ \    | \ | | ___( | )                                                                                                                                                                      
 V V  __) |   |  \| |/ _ \V V                                                                                                                                                                       
     / __/ _  | |\  | (_) |                                                                                                                                                                         
 _ _|_____(_) |_|_\_|\___/                                    _           _              _ _                                                                                                        
( | ) ____|_ __ | |_ ___ _ __   _   _  ___  _   _ _ __    ___| |__   ___ (_) ___ ___ _  ( | )                                                                                                       
 V V|  _| | '_ \| __/ _ \ '__| | | | |/ _ \| | | | '__|  / __| '_ \ / _ \| |/ __/ _ (_)  V V                                                                                                        
    | |___| | | | ||  __/ |    | |_| | (_) | |_| | |    | (__| | | | (_) | | (_|  __/_                                                                                                              
    |_____|_| |_|\__\___|_|     \__, |\___/ \__,_|_|     \___|_| |_|\___/|_|\___\___(_)                                                                                                             
                                |___/                                                                                                                                                                             
");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    StartGame();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    GameOver();
                    break;
            }
        }
        private void CenterConsole(int consoleWidth, int consoleHeight)
        {

            Console.SetWindowSize(consoleWidth, consoleHeight);


            int screenWidth = Console.LargestWindowWidth;
            int screenHeight = Console.LargestWindowHeight;


            int posX = (screenWidth - consoleWidth) / 2;
            int posY = (screenHeight - consoleHeight) / 2;


            IntPtr handle = GetConsoleWindow();
            MoveWindow(handle, posX * 6, posY * 20, consoleWidth * 10, consoleHeight * 20, true);
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }
   
}

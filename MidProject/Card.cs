using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    public class Card
    {
        public string Rank;
        public string Suit;
        public string CardName;

        public bool FaceUp;
        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;
            CardName = rank + " → " + suit;
            FaceUp = false;
        }
        public void Flip(bool FaceUp)
        {
            this.FaceUp = FaceUp;


        }
        
        public string Color
        {
            get
            {
                if (Suit == "♥" || Suit == "♦") 
                    return "Red";
                else if (Suit == "♣" || Suit == "♠") 
                    return "Black";
                else
                    throw new ArgumentException("Invalid suit");
            }
        }
        public override int GetHashCode()
        {
            return (Rank.GetHashCode() + Suit.GetHashCode()) % 10; 
        }

        public override bool Equals(object obj)
        {
            if (obj is Card other)
            {
                return this.Rank == other.Rank && this.Suit == other.Suit;
            }
            return false;
        }


    }
    }

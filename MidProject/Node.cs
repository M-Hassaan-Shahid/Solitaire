using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class Node
    {
        public MoveType MoveType { get; set; }
        public Card Card { get; set; }
        public int StackNumber { get; set; }
        public int FoundationNumber { get; set; }
        public int SourceStackNumber { get; set; }
        public int DestinationStackNumber { get; set; }
        public Node Next { get; set; }
        public Card Data { get; set; }

        public Node(Card card)
        {
            this.Card = card;

            Data = card;
        }
        public Node()
        {
            MoveType = MoveType.StackToStack;
            Card = null;
            StackNumber = 0;
            FoundationNumber = 0;
            SourceStackNumber = 0;
            DestinationStackNumber = 0;
            Next = null;
            Data = null;


        }
    }
}

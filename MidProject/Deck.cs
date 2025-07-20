using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class Deck
    {
        public LinkedList  cards = new LinkedList();
        private Random random;
        private string[] Suits = { "♥", "♣", "♦", "♠" };
        private string[] Ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        public Deck()
        {
            random = new Random();
        }
        public void LoadDeck()
        {
            foreach (string suit in Suits)
            {
                foreach (string rank in Ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }


        public void Shuffle()
        {
            List<Card> cardList = new List<Card>();
            Node current = cards.Head;
            while (current != null)
            {
                cardList.Add(current.Data);
                current = current.Next;
            }

            int n = cardList.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cardList[i];
                cardList[i] = cardList[j];
                cardList[j] = temp;
            }

            cards = new LinkedList();
            foreach (Card card in cardList)
            {
                cards.Add(card);
            }
        }


        public Card DrawCard()
        {
            Node lastNode = cards.GetLastNode();
            cards.RemoveNode(lastNode);
            return lastNode.Data;
        }
        
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{

    internal class Hashmap
    {
        LinkedList<Card>[] cards = new LinkedList<Card>[10];

        public Hashmap()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = new LinkedList<Card>();
            }
        }

        private int GetHash(Card card)
        {
            return card.GetHashCode() % cards.Length;
        }

        public int GetIndex(Card card)
        {
            return GetHash(card);
        }

        public void Add(Card card)
        {
            int index = GetIndex(card);
            cards[index].AddLast(card); 
        }

        public bool Remove(Card card)
        {
            int index = GetIndex(card);
            return cards[index].Remove(card); 
        }

        public bool Find(Card card)
        {
            int index = GetIndex(card);
            return cards[index].Contains(card); 
        }

        public List<Card> GetAllCards()
        {
            List<Card> allCards = new List<Card>();
            foreach (var bucket in cards)
            {
                allCards.AddRange(bucket); 
            }
            return allCards;
        }
    }




}

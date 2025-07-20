using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class Queue
    {
        private Node head;
        private Node tail;
        private LinkedList<Card> cards;

        public Queue(LinkedList<Card> cards)
        {
            this.cards = cards;
        }
        public Queue()
        {
        }
        public Node Peek()
        {
            return head;
        }
        public void Enqueue(Card card)
        {
            var newNode = new Node(card);
            if (tail == null)
                head = tail = newNode;
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
        }

        public Card Dequeue()
        {
            if (head == null)
                throw new InvalidOperationException("The stockpile is empty.");

            Card card = head.Data; 
            head = head.Next;

            if (head == null)
                tail = null;

            return card;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}

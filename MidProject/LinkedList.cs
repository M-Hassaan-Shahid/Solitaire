using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class LinkedList
    {
        public Node Head { get; private set; }
        
        public LinkedList()
        {
            Head = null;
        }
        public void Add(Card card)
        {
            var newNode = new Node(card);
            if (Head == null)
                Head = newNode;
            else
            {
                Node temp = Head;
                while (temp.Next != null)
                    temp = temp.Next;
                temp.Next = newNode;
            }
        }
        public Node GetRandomNode()
        {
            if (Head == null)
                return null;

            int randomIndex = new Random().Next(0, GetLength());
            Node temp = Head;
            for (int i = 0; i < randomIndex; i++)
                temp = temp.Next;

            return temp;
        }
       
        public Node GetLastNode()
        {
            if (Head == null)
                return null;

            Node temp = Head;
            while (temp.Next != null)
                temp = temp.Next;

            return temp;
        }
        public void RemoveNode(Node node)
        {
            if (Head == null)
                return;

            if (Head == node)
            {
                Head = Head.Next;
                return;
            }

            Node temp = Head;
            while (temp.Next != null)
            {
                if (temp.Next == node)
                {
                    temp.Next = temp.Next.Next;
                    return;
                }
                temp = temp.Next;
            }
        }



        public Card RemoveLast()
        {
            if (Head == null)
                return null;

            if (Head.Next == null)
            {
                Card card = Head.Data;
                Head = null;
                return card;
            }

            Node temp = Head;
            while (temp.Next.Next != null)
                temp = temp.Next;

            Card lastCard = temp.Next.Data;
            temp.Next = null;
            return lastCard;
        }
        private int GetLength()
        {
            int length = 0;
            Node temp = Head;
            while (temp != null)
            {
                length++;
                temp = temp.Next;
            }
            return length;
        }
    }
}

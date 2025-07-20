using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidProject
{
    internal class Stack
    {
        public Node Head { get; private set; }

        public Stack()
        {
            Head = null;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }
        public void Push(Card card)
        {
            var newNode = new Node(card);
            if (Head == null)
                Head = newNode;
            else
            {
                newNode.Next = Head;
                Head = newNode;
            }
        }
        public void Push(Node node)
        {
            if (Head == null)
                Head = node;
            else
            {
                node.Next = Head;
                Head = node;
            }
        }
        public Node Pop()
        {
            if (Head == null)
                return null;

            Node temp = Head;
            Head = Head.Next;
            return temp;
        }
        public Node Peek()
        {
            return Head;
        }
        public int GetLength()
        {
            if (Head == null)
                return 0;

            int count = 0;
            Node temp = Head;
            while (temp != null)
            {
                count++;
                temp = temp.Next;
            }
            return count;
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

        public Stack GetCardsFrom(Node startNode)
        {
            var resultStack = new Stack();
            Node current = startNode;

            while (current != null)
            {
                resultStack.Push(current.Data);
                current = current.Next;
            }

            resultStack.Reverse();
            return resultStack;
        }

        public void Reverse()
        {
            Node prev = null, current = Head, next;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            Head = prev;
        }
        public Node GetNodeAtPosition(int position)
        {
            if (Head == null)
                return null;

            Node temp = Head;
            int count = 0;
            while (temp != null)
            {
                if (count == position)
                    return temp;

                count++;
                temp = temp.Next;
            }

            return null;
        }

        public void RemoveSequence(Node startNode)
        {
            if (Head == null || startNode == null) return;

            Node temp = Head;
            while (temp != null && temp.Next != startNode)
            {
                temp = temp.Next;
            }

            if (temp == null)
            {
                Head = null;
            }
            else
            {
                temp.Next = null; 
            }
        }
    }
}

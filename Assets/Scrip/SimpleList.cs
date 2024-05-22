using System;
public class SimpleList<T>
{
    private Node<T> head;
    private Node<T> tail;
    private int count;

    private class Node<U>
    {
        public U Data;
        public Node<U> Next;

        public Node(U data)
        {
            Data = data;
            Next = null;
        }
    }

    public SimpleList()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public void Add(T item)
    {
        Node<T> node = new Node<T>(item);
        if (head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail.Next = node;
            tail = node;
        }
        count++;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException();

        Node<T> current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current.Data;
    }

    public int Count
    {
        get { return count; }
    }
}
using UnityEngine;

public class RingBuffer<T>
{
    private T[] buf;
    private int bufLen;
    private int head;
    private int count;

    public RingBuffer(int bufLen)
    {
        buf = new T[bufLen];
        this.bufLen = bufLen;
        head = 0;
        count = 0;
    }

    public void Enqueue(T item)
    {
        if (count == bufLen)
        {
            Debug.Log("ring buffer overflowed :O :/");
        }
        buf[(head + count) % bufLen] = item;
        count++;
    }

    public T Dequeue()
    {
        T front = buf[head];
        head = (head + 1) % bufLen;
        count--;
        return front;
    }

    public T Get(int i)
    {
        if (i >= count)
        {
            Debug.Log("Ring buffer index out of bounds");
        }
        return buf[(head + i) % bufLen];
    }

    public int Count()
    {
        return count;
    }
}

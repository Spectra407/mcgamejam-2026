using UnityEngine;

public class RingBuffer<T>
{
    private T[] buf;
    private int bufLen;
    private int head;
    private int count;

    public RingBuffer(int bufLen)
{
    // Prevent the size from ever being 0
    this.bufLen = Mathf.Max(1, bufLen); 
    buf = new T[this.bufLen];
    head = 0;
    count = 0;
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

    public void Clear()
{
    head = 0;
    count = 0;
    // Optional: Array.Clear(buf, 0, bufLen); // To help Garbage Collection
}

public void Enqueue(T item)
{
    if (count == bufLen)
    {
        Debug.LogWarning("Ring buffer overflow! Ignoring request.");
        return; // Don't allow adding if full
    }
    buf[(head + count) % bufLen] = item;
    count++;
}

 

}

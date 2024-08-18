namespace SamHowes.Extensions.Collections;

/// <param name="compare">Given 'a' and 'b' this function returns greater than zero if 'a' is greater than 'b'; less
/// than zero if 'a' is less than 'b', and 0 if 'a' is equal to 'b'.
/// </param>
public abstract class Heap<T>(Func<T, T, int> compare)
{
    private readonly List<T> _items = [];
    public int Size { get; private set; } = 0;
    
    public void Push(T item)
    {
        Size++;
        if (Size > _items.Count)
        {
            _items.Add(item);
        }
        else
        {
            _items[Size - 1] = item;
        }

        BubbleUp(Size - 1);
    }

    public T Pop()
    {
        var item = _items[0];

        // move the lowest item to the top
        Swap(0, Size - 1);
        Size--;
        
        // sink it down to its correct place
        Heapify(0);
        
        return item;
    }

    protected abstract bool ShouldPushDown(int parent, int child);
    protected abstract bool ShouldPushUp(int child, int parent);
    
    private void Heapify(int index)
    {
        var cur = index;
        while (cur < Size - 1)
        {
            var left = Left(cur);
            var right = Right(cur);
            var nextParent = cur;

            if (left < Size && ShouldPushUp(left, nextParent))
                nextParent = left;
            if (right < Size && ShouldPushUp(right, nextParent))
                nextParent = right;

            if (nextParent == cur)
                break;
            Swap(cur, nextParent);
            cur = nextParent;
        }
    }

    /// <summary>
    /// Push the item at <see cref="index"/> up the heap so long as it is greater than its parent
    /// </summary>
    private void BubbleUp(int index)
    {
        var cur = index;
        for (var parent = Parent(cur); 
             ShouldPushUp(cur, parent);
             cur = parent, parent = Parent(cur))
        {
            Swap(cur, parent);
        }
    }

    protected int Compare(int i, int j)
    {
        return compare(_items[i], _items[j]);
    }

    private void Swap(int i, int j)
    {
        (_items[i], _items[j]) = (_items[j], _items[i]);
    }

    private static int Parent(int index)
    {
        return (index - 1) / 2;
    }
    private static int Right(int index)
    {
        return (index * 2) + 2;
    }
    
    private static int Left(int index)
    {
        return (index * 2) + 1;
    }
    
}

public class MinHeap<T>(Func<T, T, int> compare) : Heap<T>(compare)
{
    protected override bool ShouldPushDown(int parent, int child)
    {
        // MinHeap: A parent should be less than its child
        return Compare(parent, child) > 0;
    }
    
    protected override bool ShouldPushUp(int child, int parent)
    {
        // MinHeap: A child should be greater than its parent 
        return Compare(child, parent) < 0;
    }
}

public class MaxHeap<T>(Func<T, T, int> compare) : Heap<T>(compare)
{
    protected override bool ShouldPushDown(int parent, int child)
    {
        // MaxHeap: A parent should be greater than its child
        return Compare(parent, child) < 0;
    }
    
    protected override bool ShouldPushUp(int child, int parent)
    {
        // MaxHeap: A child should be smaller than its parent
        return Compare(child, parent) > 0;
    }
}
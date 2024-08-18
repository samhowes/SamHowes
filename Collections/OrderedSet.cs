using System.Collections;

namespace SamHowes.Extensions.Collections;

/// <summary>
/// https://stackoverflow.com/a/17853085/2524934
/// </summary>
public class OrderedSet<T> : ICollection<T> where T : notnull
{
    private readonly IDictionary<T, LinkedListNode<T>> _dict;
    private readonly LinkedList<T> _linkedList;

    public OrderedSet()
        : this(EqualityComparer<T>.Default)
    {
    }

    public OrderedSet(IEqualityComparer<T> comparer)
    {
        _dict = new Dictionary<T, LinkedListNode<T>>(comparer);
        _linkedList = new LinkedList<T>();
    }

    public int Count => _dict.Count;

    public virtual bool IsReadOnly => _dict.IsReadOnly;

    void ICollection<T>.Add(T item)
    {
        Add(item);
    }

    public void Add(T item)
    {
        if (_dict.ContainsKey(item))
            return;
        var node = _linkedList.AddLast(item);
        _dict.Add(item, node);
    }

    public void Clear()
    {
        _linkedList.Clear();
        _dict.Clear();
    }

    public bool Remove(T item)
    {
        if (item == null) return false;
        var found = _dict.TryGetValue(item, out var node);
        if (!found) return false;
        _dict.Remove(item);
        _linkedList.Remove(node);
        return true;
    }

    public bool TryPopFirst(out T item)
    {
        if (_linkedList.Count == 0)
        {
            item = default;
            return false;
        }
        item = _linkedList.First!.Value;
        _linkedList.RemoveFirst();
        _dict.Remove(item);
        return true;
    }

    public T PopFirst()
    {
        if (!TryPopFirst(out var item))
            throw new Exception("Cannot pop from an empty set.");
        return item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _linkedList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(T item)
    {
        return item != null && _dict.ContainsKey(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _linkedList.CopyTo(array, arrayIndex);
    }
}
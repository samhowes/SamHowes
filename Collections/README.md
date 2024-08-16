# Commonly used collections
> I got tired of rewriting this code

## Heap<T>

Simple implementation of a priority heap

### MaxHeap
```csharp
public record Item(int Value);

var heap = new MaxHeap<Item>((a,b) => a.Value - b.Value);

heap.Push(new Item(5));
heap.Push(new Item(10));
heap.Push(new Item(1));

heap.Pop() // 10
heap.Pop() // 5
heap.Pop() // 1
```
### MinHeap
```csharp
public record Item(int Value);

var heap = new MinHeap<Item>((a,b) => a.Value - b.Value);

heap.Push(new Item(5));
heap.Push(new Item(10));
heap.Push(new Item(1));

heap.Pop() // 1
heap.Pop() // 5
heap.Pop() // 10
```

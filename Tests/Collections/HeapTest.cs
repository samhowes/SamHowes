using FluentAssertions;
using SamHowes.Extensions.Collections;

namespace SamHowes.Extensions.Tests.Collections;

public class HeapTest
{
    private record Item(int Value);
    
    [Fact]
    public void MaxHeap_Works()
    {
        var heap = new MaxHeap<Item>((a, b) => a.Value - b.Value);

        heap.Push(new Item(5));
        heap.Push(new Item(10));
        heap.Push(new Item(1));

        var item = heap.Pop();
        
        item.Value.Should().Be(10);

        item = heap.Pop();
        item.Value.Should().Be(5);
        
        item = heap.Pop();
        item.Value.Should().Be(1);
    }
    
    [Fact]
    public void MinHeap_Works()
    {
        var heap = new MinHeap<Item>((a, b) => a.Value - b.Value);

        heap.Push(new Item(5));
        heap.Push(new Item(10));
        heap.Push(new Item(1));

        var item = heap.Pop();
        
        item.Value.Should().Be(1);

        item = heap.Pop();
        item.Value.Should().Be(5);
        
        item = heap.Pop();
        item.Value.Should().Be(10);
    }
}
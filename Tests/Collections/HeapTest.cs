using FluentAssertions;
using SamHowes.Extensions.Collections;

namespace SamHowes.Extensions.Tests.Collections;

public class HeapTest
{
    private record Item(int Value);
    
    [Fact]
    public void MaxHeap_Works()
    {
        var heap = MaxHeap();

        heap.Push(new Item(5));
        heap.Size.Should().Be(1);
        
        heap.Push(new Item(10));
        
        heap.Size.Should().Be(2);
        heap.Push(new Item(1));
        heap.Size.Should().Be(3);

        var item = heap.Pop();
        heap.Size.Should().Be(2);
        item.Value.Should().Be(10);

        item = heap.Pop();
        heap.Size.Should().Be(1);
        item.Value.Should().Be(5);
        
        item = heap.Pop();
        heap.Size.Should().Be(0);
        item.Value.Should().Be(1);
    }

    private MaxHeap<Item> MaxHeap()
    {
        return new MaxHeap<Item>((a, b) => a.Value - b.Value);
    }

    [Fact]
    public void Peek_Works()
    {
        var heap = MaxHeap();
        
        var badPeek = heap.Peek();
        badPeek.Should().BeNull();
        
        heap.Push(new Item(5));
        var peek = heap.Peek();
        peek!.Value.Should().Be(5);
        heap.Pop();

        badPeek = heap.Peek();
        badPeek.Should().BeNull();
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
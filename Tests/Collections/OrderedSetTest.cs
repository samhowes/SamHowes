using FluentAssertions;
using SamHowes.Extensions.Collections;

namespace SamHowes.Extensions.Tests.Collections;

public class OrderedSetTest
{
    [Fact]
    public void It_Works()
    {
        var set = new OrderedSet<int>();
        
        set.Add(3);
        set.Add(2);
        set.Add(1);

        set.TryPopFirst(out var result);
        result.Should().Be(3);
        
        set.TryPopFirst(out result);
        result.Should().Be(2);
        
        set.TryPopFirst(out result);
        result.Should().Be(1);
    }
}
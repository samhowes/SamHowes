using FluentAssertions;
using SamHowes.Extensions.DependencyInjection.Modules;

namespace SamHowes.Extensions.Tests.DependencyInjection.Modules;

public class DepSetTests
{
    public class A : TestModule<B>;
    public class B : TestModule<D>;
    public class C : TestModule;
    public class D : TestModule<C>;
    
    public class TestModule<T> : InjectionModule where T : InjectionModule
    {
        public TestModule()
        {
            Deps<T>();
        }
        public override void Configure(InjectorBuilder builder)
        {
        }
    }

    [Fact]
    public void DepSet_Works()
    {
        var modules = new List<InjectionModule>(){new A(), new B(), new C(), new D()};

        var depSet = new DepSet(modules);

        var deps = depSet.Enumerate();

        deps[0].Should().BeAssignableTo(typeof(C));
        deps[1].Should().BeAssignableTo(typeof(D));
        deps[2].Should().BeAssignableTo(typeof(B));
        deps[3].Should().BeAssignableTo(typeof(A));
    }

    public class E : TestModule<F>;
    public class F : TestModule<E>;

    [Fact]
    public void DepSet_DetectsCircularDependencies()
    {
        var modules = new List<InjectionModule>(){new E(), new F()};
        
        var depSet = new DepSet(modules);
        var action = () => depSet.Enumerate();
        action.Should().Throw<CircularDependencyException>();
    }
}
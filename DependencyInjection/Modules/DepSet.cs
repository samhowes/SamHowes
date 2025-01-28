using System.Reflection;
using SamHowes.Extensions.Collections;

namespace SamHowes.Extensions.DependencyInjection.Modules;

public class DepSet
{
    private readonly OrderedSet<Type> _path = new();
    private readonly OrderedSet<Type> _set = new();
    private readonly Dictionary<Type, InjectionModule> _modules = new();

    private List<InjectionModule> Items { get; set; }
    
    public DepSet(List<InjectionModule> items)
    {
        Items = items;
    }


    public List<InjectionModule> Enumerate()
    {
        foreach (var item in Items)
        {
            VisitItem(item);
        }

        return _set.Select(t => _modules[t]).ToList();
    }

    private void VisitItem(Type type)
    {
        AssertNoCircular(type);
        if (_modules.TryGetValue(type, out _))
            return;
        if (!typeof(InjectionModule).IsAssignableFrom(type))
            throw new ArgumentException($"Type {type.FullName} is not a InjectionModule");
        var ctor = type
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(c => c.GetParameters().Length == 0);
        if (ctor == null)
            throw new ArgumentException($"Type {type.FullName} cannot be used as an InjectionModule dependency because it does not have a parameterless constructor");
        var module = (InjectionModule)Activator.CreateInstance(type)!;
        
        VisitItem(module);
    }

    private void AssertNoCircular(Type type)
    {
        if (_path.Contains(type))
        {
            var path = string.Join(" -> ", _path);
            throw new CircularDependencyException($"Circular InjectionModule dependency detected: {path}");
        }
    }

    private void VisitItem(InjectionModule item)
    {
        var type = item.GetType();
        AssertNoCircular(type);
        
        if (_set.Contains(type))
            return;
        _path.Add(type);
        
        foreach (var dep in item.Dependencies)
        {
            if (_set.Contains(dep))
                continue;
            
            VisitItem(dep);
        }

        _set.Add(type);
        _modules[type] = item;
        _path.Remove(type);
    }
}

public class CircularDependencyException : Exception
{
    public CircularDependencyException(string message) : base(message)
    {
        
    }
}
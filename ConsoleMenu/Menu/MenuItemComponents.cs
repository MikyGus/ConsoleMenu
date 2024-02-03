using ConsoleMenu.Components;
using ConsoleMenu.Menu;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemComponents
{
    private List<IComponent> _components;

    public void AddComponent(IComponent component)
    {
        component.Owner = this;
        _components.Add(component);
    }
    public IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : IComponent
    {
        foreach (TComponent component in _components.OfType<TComponent>())
        {
            yield return component;
        }
    }
    public IEnumerable<T> Values<T>()
    {
        IEnumerable<T> values = GetComponents<ValueComponent<T>>().Select(x => x.Value);
        foreach (T component in values)
        {
            yield return component;
        }
    }

    public void RemoveComponent(IComponent component)
    {
        _components.Remove(component);
    }
}
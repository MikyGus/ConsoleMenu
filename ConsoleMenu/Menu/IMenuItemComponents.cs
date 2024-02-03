using ConsoleMenu.Components;

namespace ConsoleMenu.Menu;
public interface IMenuItemComponents
{
    void AddComponent(IComponent component);
    IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : IComponent;
    void RemoveComponent(IComponent component);
    IEnumerable<T> Values<T>();
}
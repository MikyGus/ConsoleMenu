namespace ConsoleMenu.Components;
public interface IValueComponent<T> : IComponent
{
    T Value { get; set; }
}
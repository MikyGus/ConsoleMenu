namespace ConsoleMenu.Library.Components;
public interface IValueComponent<T> : IComponent
{
    T Value { get; set; }
}
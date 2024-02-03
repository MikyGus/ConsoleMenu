namespace ConsoleMenu.Components;
public class ValueComponent<T> : IValueComponent<T>
{
    public IMenuItem Owner { get; set; } = null; // Is set when added as a component
    public T Value { get; set; }
    public ValueComponent(T value)
    {
        Value = value;
    }
}
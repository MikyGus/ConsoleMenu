namespace ConsoleMenu.Library.Abstracts;
public interface IOwner<T>
{
    /// <summary>
    /// Reference to the owner
    /// </summary>
    T Owner { get; init; }
}

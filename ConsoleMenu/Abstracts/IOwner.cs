namespace ConsoleMenu.Abstracts;
internal interface IOwner<T>
{
    /// <summary>
    /// Reference to the owner
    /// </summary>
    T Owner { get; init; }
}
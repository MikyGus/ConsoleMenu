namespace ConsoleMenu.Library.Abstracts;
public interface ISelectionControls
{
    /// <summary>
    /// Increments the selection by one increment and renders the new selection.
    /// </summary>
    /// <returns>Returns a bool indicating if the selection were moved. Tried, but failed, returns false.</returns>
    bool Increment();
    /// <summary>
    /// Decrements the selection by one decrement and reders the new selection.
    /// </summary>
    /// <returns>Returns a bool indicating if the selection were moved. Tried, but failed, returns false.</returns>
    bool Decrement();
    /// <summary>
    /// Holds the current index of our selection
    /// </summary>
    int CurrentIndex { get; }
}
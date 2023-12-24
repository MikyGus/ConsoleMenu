namespace ConsoleMenu.Library.Abstracts;
public interface ISelectionControls
{
    void IncrementSelection();
    void DecrementSelection();
    int CurrentSelection { get; }
}

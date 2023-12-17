using ConsoleMenu.Library.Models.EventArg;

namespace ConsoleMenu.Library.Managers;

internal interface ISelectionManager
{
    event Action<SelectionChangedEventArgs> SelectionChanged;

    bool Add(int index);
    bool Remove(int index);
    bool MoveSelection(int removeSelection, int addSelection);
    IEnumerable<int> SelectedItems();
    void SetSelectionMinMax(int min, int max);
}
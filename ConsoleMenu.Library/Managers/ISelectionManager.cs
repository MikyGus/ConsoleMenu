using ConsoleMenu.Library.Models.EventArg;

namespace ConsoleMenu.Library.Managers;

internal interface ISelectionManager
{
    event Action<SelectionChangedEventArgs> SelectionChanged;

    bool Add(int index);
    IEnumerable<int> SelectedItems();
    void SetSelectionMinMax(int min, int max);
}
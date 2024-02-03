using ConsoleMenu.Events;

namespace ConsoleMenu.Menu;
public interface IMenuItemSelection
{
    event Action<SelectionChangedEvent> OnSelectionChanged;
    event Action<SelectionRenderedEvent> OnSelectionRendered;
    IMenuItem GetSelectedChild();
    bool IncrementSelection();
    bool DecrementSelection();
}
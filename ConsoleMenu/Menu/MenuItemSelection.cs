using ConsoleMenu.Events;
using ConsoleMenu.Menu;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemSelection
{
    public event Action<SelectionChangedEvent> OnSelectionChanged
    {
        add => _childrenManager.Selection.OnSelectionChanged += value;
        remove => _childrenManager.Selection.OnSelectionChanged -= value;
    }
    public event Action<SelectionRenderedEvent> OnSelectionRendered
    {
        add => _childrenManager.Selection.OnSelectionRendered += value;
        remove => _childrenManager.Selection.OnSelectionRendered -= value;
    }
    public IMenuItem GetSelectedChild() => _childrenManager.Selection.GetSelectedChild();
    public bool IncrementSelection() => _childrenManager.Selection.Increment();
    public bool DecrementSelection() => _childrenManager.Selection.Decrement();
}
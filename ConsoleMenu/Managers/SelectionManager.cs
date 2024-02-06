using ConsoleMenu.Events;

namespace ConsoleMenu.Managers;
internal class SelectionManager : ISelectionManager
{
    public int CurrentIndex { get; private set; }

    public ChildrenManager Owner { get; init; }

    public SelectionManager(ChildrenManager childrenManager)
    {
        Owner = childrenManager;
        CurrentIndex = 0;
    }

    public event Action<SelectionChangedEvent> OnSelectionChanged;
    public event Action<SelectionRenderedEvent> OnSelectionRendered;

    public bool Decrement()
    {
        if (MayDecrement(out int newIndex))
        {
            SelectionChangedEvent selectionChangedInfo = new() { OldItem = GetSelectedChild(), Sender = Owner.Owner };

            RenderSelection(false);
            CurrentIndex = newIndex;
            RenderSelection(true);

            selectionChangedInfo.NewItem = GetSelectedChild();
            OnSelectionChanged?.Invoke(selectionChangedInfo);
            return true;
        }
        return false;
    }

    public bool Increment()
    {
        if (MayIncrement(out int newIndex))
        {
            SelectionChangedEvent selectionChangedInfo = new() { OldItem = GetSelectedChild(), Sender = Owner.Owner };

            RenderSelection(false);
            CurrentIndex = newIndex;
            RenderSelection(true);

            selectionChangedInfo.NewItem = GetSelectedChild();
            OnSelectionChanged?.Invoke(selectionChangedInfo);
            return true;
        }
        return false;
    }
    public IMenuItem GetSelectedChild()
        => Owner.GetChildren().Any() == false
        ? throw new InvalidOperationException()
        : Owner.GetChild(CurrentIndex);

    private void RenderSelection(bool isSelected)
    {
        if (Owner.GetChildren().Any() == false)
        {
            throw new ArgumentNullException("No children present!");
        }
        IMenuItem childItem = GetSelectedChild();
        childItem.Configure(x => x.IsSelected = isSelected);
        childItem.Render();

        SelectionRenderedEvent selectionRendered = new() { Sender = Owner.Owner, Item = childItem, IsSelected = isSelected };
        OnSelectionRendered?.Invoke(selectionRendered);
    }

    private bool MayDecrement(out int newIndex)
    {
        newIndex = -1;
        if (Owner.IsVisible == false || CurrentIndex <= 0)
        {
            return false;
        }

        int i = CurrentIndex;
        while (i > 0)
        {
            i--;
            if (Owner.GetChild(i).IsVisible)
            {
                newIndex = i;
                return true;
            }
        }
        return false;
    }

    private bool MayIncrement(out int newIndex)
    {
        newIndex = -1;
        int childrenCount = Owner.GetChildren().Count();
        if (Owner.IsVisible == false || CurrentIndex >= childrenCount)
        {
            return false;
        }
        int i = CurrentIndex;
        while (i < childrenCount - 1)
        {
            i++;
            if (Owner.GetChild(i).IsVisible)
            {
                newIndex = i;
                return true;
            }
        }
        return false;
    }
}
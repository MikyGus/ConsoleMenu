using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Models;
public class ChildItem : IChildItem
{
    public int Priority { get; }

    public IMenuItem Parent { get; }

    public IMenuItem Item { get; }

    public ChildItem(IMenuItem parent, IMenuItem item, int priority)
    {
        Parent = parent;
        Item = item;
        Priority = priority;
    }
}

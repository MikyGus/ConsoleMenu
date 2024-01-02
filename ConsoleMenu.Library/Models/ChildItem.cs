using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Models;
public class ChildItem : IChildItem
{
    public int Priority { get; }


    public IMenuItem Item { get; }

    public ChildItem(IMenuItem item, int priority)
    {
        Item = item;
        Priority = priority;
    }
}

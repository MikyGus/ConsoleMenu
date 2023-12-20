using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Models;
public interface IChildItem
{
    int Priority { get; }
    IMenuItem Parent { get; }
    IMenuItem Item { get; }
}

using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Models;
public interface IChildItem
{
    int Priority { get; }
    IMenuItem Item { get; }
}
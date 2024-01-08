using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public interface IContentRenderer
{
    Vector2 AreaNeeded(IMenuItem menuItem);
    void Render(IMenuItem menuItem);
}
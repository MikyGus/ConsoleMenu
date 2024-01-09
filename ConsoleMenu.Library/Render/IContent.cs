using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public interface IContent
{
    bool IsSelected { get; set; }
    bool IsMarked { get; set; }
    string Title { get; set; }
    IMenuItem Owner { get; set; }
    void SetRenderer(Action<IMenuItem> action, Func<IMenuItem, Vector2> areaNeeded);
    void Render();
    Vector2 AreaNeeded();
}
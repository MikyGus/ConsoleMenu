using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem
{
    public Vector2 Position { get; set; }
    void Render();
    Vector2 AreaNeeded();
    void SetRender(IContentRender contentRender);
    public IContentRender ContentRenderer();
    void AddChildItem(int priority, IMenuItem item);
    void RemoveChildItem(IMenuItem item);
    IEnumerable<IChildItem> GetChildren();
}

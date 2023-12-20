using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render.Contents;
public interface IContentRender
{
    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }
    void Render(Vector2 position);
    Vector2 AreaNeeded();
}

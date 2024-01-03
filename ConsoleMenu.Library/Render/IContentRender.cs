using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Render;
public interface IContentRender
{
    bool IsSelected { get; set; }
    bool IsMarked { get; set; }
    string Content { get; set; }
    void Render(Vector2 position);
    Vector2 AreaNeeded();
}

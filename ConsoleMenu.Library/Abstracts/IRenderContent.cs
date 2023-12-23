using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Abstracts;
public interface IRenderContent
{
    void Render();
    Vector2 AreaNeeded();
}

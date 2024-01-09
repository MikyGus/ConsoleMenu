using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Abstracts;
public interface IRenderContent
{
    void Render(bool hideChildren = false);
    Vector2 AreaNeeded();
}
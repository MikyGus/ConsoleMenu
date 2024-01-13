using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Abstracts;
public interface IRenderContent
{
    /// <summary>
    /// Render node and children
    /// </summary>
    /// <param name="showNodes">Specifies if the nodes should be drawn or erased. (true == draw the node)</param>
    void Render(bool showNodes = true);
    Vector2 AreaNeeded();
}
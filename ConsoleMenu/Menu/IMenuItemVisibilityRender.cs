using ConsoleMenu.Abstracts;
using ConsoleMenu.Render;

namespace ConsoleMenu.Menu;
public interface IMenuItemVisibilityRender : IVisibility, IRenderContent
{
    /// <summary>
    /// Sets the renderer of the content of the item. 
    /// </summary>
    /// <typeparam name="T">ContentRenderer</typeparam>
    void SetRenderer<T>() where T : IContentRenderer, new();

    /// <summary>
    /// Removes ALL nodes, starting at the root, and renders them again.
    /// </summary>
    void ReRender();
}
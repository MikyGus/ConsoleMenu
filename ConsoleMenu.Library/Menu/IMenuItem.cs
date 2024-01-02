using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem : IRenderContent
{
    IMenuItem Parent { get; set; }
    Vector2 Position { get; set; }
    /// <summary>
    /// Pushes the pressed key down all selected children.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Returns a bool stating if the key were used for an action or not (true=used)</returns>
    bool PerformAction(ConsoleKeyInfo key);
    void SetRender(IContentRender contentRender);
    IContentRender ContentRenderer { get; }
    IChildrenManager Children { get; }
}

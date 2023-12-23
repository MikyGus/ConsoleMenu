using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render.Contents;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem : IRenderContent
{
    Vector2 Position { get; set; }
    void SetRender(IContentRender contentRender);
    IContentRender ContentRenderer { get; }
    IChildrenManager Children { get; }
}

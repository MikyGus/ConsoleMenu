using ConsoleMenu.Abstracts;
using ConsoleMenu.Menu;
using ConsoleMenu.Render;

namespace ConsoleMenu;
public interface IMenuItem :
    IRenderContent, IVisibility, IMenuItemComponents,
    IMenuItemSelection, IMenuItemChildren, IMenuItemActionPerformed, IMenuItemVisibilityRender
{
    IMenuItem Parent { get; set; }
    Vector2 Position { get; set; }
    IContent Content { get; }
    void Configure(Action<MenuItemOption> option);
}
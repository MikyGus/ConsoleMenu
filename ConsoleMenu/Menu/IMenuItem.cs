using ConsoleMenu.Abstracts;
using ConsoleMenu.Menu;

namespace ConsoleMenu;
public interface IMenuItem :
    IRenderContent, IVisibility, IMenuItemComponents,
    IMenuItemSelection, IMenuItemChildren, IMenuItemActionPerformed,
    IMenuItemVisibilityRender, IMenuItemContent
{
    IMenuItem Parent { get; set; }
    Vector2 Position { get; set; }
    void Configure(Action<MenuItemOption> option);
}
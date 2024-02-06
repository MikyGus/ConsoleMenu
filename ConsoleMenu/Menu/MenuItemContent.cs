using ConsoleMenu.Menu;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemContent
{
    public bool IsMarked => _content.IsMarked;
    public bool IsSelected => _content.IsSelected;
    public string Title => _content.Title;
}
namespace ConsoleMenu.Menu;
public interface IMenuItemContent
{
    bool IsMarked { get; }
    bool IsSelected { get; }
    string Title { get; }
}
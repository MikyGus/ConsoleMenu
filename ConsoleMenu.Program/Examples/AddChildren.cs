namespace ConsoleMenu.Program.Examples;
internal class AddChildren
{
    public static void Run()
    {
        IMenuItem menuSettings = new MenuItem("Settings");
        // Add a normal menu
        menuSettings.AddChild("Sub 1");

        // Add a submenu to "Sub 1"
        menuSettings["Sub 1"].AddChild("Sub Sub 1");

        // Same as above, but using index
        menuSettings[0].AddChild("Sub Sub 2");

        // Add a normal menu at position 1. Will be ordered by lowest 'positionInList'-value first among its siblings.
        // No 'positionInList' set is valuated as int.MaxValue
        menuSettings.AddChild("Sub 2", x => x.PositionInList = 1);

        // Add a normal menu with a value associated with it
        menuSettings.AddChild<int>(42, "My SubMenu with a value");
        menuSettings.AddChild<string>("42", "My SubMenu with a value");

        // Supplying both value, title and positionInList.
        menuSettings.AddChild(42, "Menu with value at a specified position", 2);
        menuSettings.AddChild("42", "Menu with value at a specified position", 2);

        menuSettings.Render();
    }
}
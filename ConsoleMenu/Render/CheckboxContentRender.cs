namespace ConsoleMenu.Render;
public class CheckboxContentRender : IContentRenderer
{
    public Vector2 AreaNeeded(IMenuItem menuItem) => new(menuItem.Title.Length + 4, 1);

    public void Render(IMenuItem menuItem)
    {
        string IsMarkedChar = menuItem.IsMarked ? "X" : " ";
        ConsoleColor fgColor = menuItem.IsSelected ? ConsoleColor.Blue : ConsoleColor.Black;
        ContentHelpers.WriteAtPosition(menuItem.Position, $"[{IsMarkedChar}] {menuItem.Title}", fgColor, ConsoleColor.Gray);
    }
}
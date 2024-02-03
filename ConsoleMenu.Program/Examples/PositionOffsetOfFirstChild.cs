namespace ConsoleMenu.Program.Examples;
internal class PositionOffsetOfFirstChild
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Menu 1");
        menu.AddChild("Menu 2");
        menu.AddChild("Menu 3");
        menu.OrientationOfChildren = Orientation.Horizontal;
        menu.PositionOffsetOfFirstChild = new Vector2(10, 0);
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            _ = menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}
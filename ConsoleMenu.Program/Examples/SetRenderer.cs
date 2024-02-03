using ConsoleMenu.Render;

namespace ConsoleMenu.Program.Examples;
internal class SetRenderer
{
    public static void Run()
    {
        IMenuItem menu = new MenuItem("Simple menu")
        {
            Position = new Vector2(0, 1)
        };
        menu.AddChild("Menu 1");
        menu[0].SetRenderer<CheckboxContentRender>();
        menu.AddChild("Menu 2");
        menu[1].SetRenderer<CheckboxContentRender>();
        menu[1].Content.IsMarked = true;
        menu.AddChild("Menu 3");
        menu[2].SetRenderer<CheckboxContentRender>();
        menu.OrientationOfChildren = Orientation.Horizontal;
        menu.Render();

        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            menu.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);
    }
}
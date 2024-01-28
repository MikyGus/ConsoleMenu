using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Program.Playground;
internal class Evolution
{
    public static void Run()
    {
        IMenuItem menuSettings = new MenuItem("Settings");
        menuSettings.AddChild("Sub 1");
        //menuSettings["Sub 1"].Content.Title = "New Sub 1";
        menuSettings["Sub 1"].AddChild("Sub Sub 1");
        menuSettings[0].AddChild("Sub Sub 2");
        //menuSettings[0][0].Content.Title = "New Sub Sub 1";
        menuSettings.AddChild("Sub 2");

        //menuSettings.RemoveChild(1); // with index
        //menuSettings.RemoveChild(menuSettings[0]); // by reference

        //Console.WriteLine(menuSettings[0].Content.Title);

        menuSettings.Render();


        ConsoleKeyInfo keyInput;
        do
        {
            keyInput = Console.ReadKey(true);
            _ = menuSettings.KeyPressed(keyInput);
        } while (keyInput.Key != ConsoleKey.Escape);

        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
    }
}
using ConsoleMenu.Components;
using ConsoleMenu.Render;

namespace ConsoleMenu.Program.Playground;
internal class Evolution
{
    public static void Run()
    {
        IMenuItem menuSettings = new MenuItem("Settings", x =>
        {
            x.IsVisible = true;
            x.MayCollapse = true;
            x.Title = "Hello... :)";
            x.OrientationOfChildren = Orientation.Horizontal;
            x.PositionOffsetOfFirstChild = new(0, 5);
            x.PositionOffsetToNextChild = 20;
        });
        menuSettings.AddChild("Sub 1");
        menuSettings[0].Configure(x =>
        {
            x.ContentRenderer = new CheckboxContentRender();
        });
        //menuSettings["Sub 1"].Content.Title = "New Sub 1";
        menuSettings["Sub 1"].AddChild("Sub Sub 1");
        menuSettings[0].AddChild("Sub Sub 2");
        //menuSettings[0][0].Content.Title = "New Sub Sub 1";
        menuSettings.AddChild("Sub 2");
        menuSettings["Sub 2"].AddComponent(new ListPriorityComponent(2000));
        menuSettings.AddChild<int>(42, "My SubMenu with a value", 1);
        //menuSettings.OrientationOfChildren = Orientation.Horizontal;

        menuSettings.AddChild("Count");

        menuSettings["Count"].AddComponent(new ValueComponent<int>(7));
        menuSettings["Count"].AddComponent(new ValueComponent<int>(70));
        IComponent component = new ValueComponent<string>("Hello");
        menuSettings["Count"].AddComponent(component);
        menuSettings["Count"].RemoveComponent(component);
        IEnumerable<IValueComponent<int>> components = menuSettings["Count"].GetComponents<IValueComponent<int>>();
        IEnumerable<ValueComponent<int>> components2 = menuSettings["Count"].GetComponents<ValueComponent<int>>();
        IEnumerable<ValueComponent<string>> components3 = menuSettings["Count"].GetComponents<ValueComponent<string>>();

        menuSettings.AddChild<int>(33, "Hello");
        IEnumerable<string> values = menuSettings["Hello"].Values<string>().ToList();
        //Console.WriteLine("Start");
        //foreach (IValueComponent<int> component in components)
        //{
        //    Console.WriteLine(component.Value);
        //}
        //Console.WriteLine("Stop");

        //menuSettings["Count"].AddComponent<ValueComponent<int>, string>("Hello");
        //menuSettings["Count"].AddComponent<ValueComponent<int>, int>(5);

        //menuSettings.AddChild<int>("Player Count");
        //menuSettings.AddChild<string>("Player Count");
        //int number = menuSettings["Player Count"].Value<int>;
        //menuSettings["Player Count"].Value<int> = 4;
        //menuSettings["Player Count"].AddComponent<int>(4);
        //menuSettings["Player Count"].RemoveComponent<int>(4);

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
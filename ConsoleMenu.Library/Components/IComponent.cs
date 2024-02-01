using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Components;

public interface IComponent
{
    IMenuItem Parent { get; set; }
}
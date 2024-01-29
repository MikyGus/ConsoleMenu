using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Events;
public class SelectionChangedEvent
{
    public IMenuItem Sender { get; set; }
    public IChildItem OldItem { get; set; }
    public IChildItem NewItem { get; set; }
}
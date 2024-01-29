using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Events;
public class SelectionRenderedEvent
{
    public IMenuItem Sender { get; set; }
    public IChildItem Item { get; set; }
    public bool IsSelected { get; set; }
}
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Events;
public class SelectionRenderedEvent
{
    public ISelectionManager Sender { get; set; }
    public IChildItem Item { get; set; }
    public bool IsSelected { get; set; }
}
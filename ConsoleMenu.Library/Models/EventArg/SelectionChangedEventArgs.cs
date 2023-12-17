using ConsoleMenu.Library.Managers;

namespace ConsoleMenu.Library.Models.EventArg;
internal class SelectionChangedEventArgs
{
    public ISelectionManager Sender { get; set; }
}

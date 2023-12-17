using ConsoleMenu.Library.Managers;

namespace ConsoleMenu.Library.Models.EventArg;
internal class SelectionChangedEventArgs : ISenderEvent<ISelectionManager>
{
    public required ISelectionManager Sender { get; set; }
}

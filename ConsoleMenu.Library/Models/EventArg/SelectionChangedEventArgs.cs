using ConsoleMenu.Library.Managers;

namespace ConsoleMenu.Library.Models.EventArg;
internal class SelectionChangedEventArgs : ISenderEvent<ISelectionManager>
{
    public ISelectionManager Sender { get; set; }
}

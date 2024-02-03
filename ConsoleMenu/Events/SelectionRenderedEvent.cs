namespace ConsoleMenu.Events;
public class SelectionRenderedEvent
{
    public IMenuItem Sender { get; set; }
    public IMenuItem Item { get; set; }
    public bool IsSelected { get; set; }
}
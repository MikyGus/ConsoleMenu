using ConsoleMenu.Library.Managers;

namespace ConsoleMenu.Library.Models.EventArg;
internal interface ISenderEvent<T> where T : class
{
    T Sender { get; set; }
}

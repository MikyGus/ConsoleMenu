using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Events;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;
public interface ISelectionManager : ISelectionControls, IOwner<ChildrenManager>
{
    event Action<SelectionChangedEvent> OnSelectionChanged;
    event Action<SelectionRenderedEvent> OnSelectionRendered;

    IChildItem GetSelectedChild();
}
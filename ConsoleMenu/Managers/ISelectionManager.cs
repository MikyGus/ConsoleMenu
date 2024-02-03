using ConsoleMenu.Abstracts;
using ConsoleMenu.Events;

namespace ConsoleMenu.Managers;
internal interface ISelectionManager : ISelectionControls, IOwner<ChildrenManager>
{
    event Action<SelectionChangedEvent> OnSelectionChanged;
    event Action<SelectionRenderedEvent> OnSelectionRendered;

    IMenuItem GetSelectedChild();
}
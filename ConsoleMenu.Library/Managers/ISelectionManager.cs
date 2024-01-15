using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;
public interface ISelectionManager : ISelectionControls, IOwner<ChildrenManager>
{
    IChildItem GetSelectedChild();
}
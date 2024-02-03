﻿using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Events;
using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Managers;
internal interface ISelectionManager : ISelectionControls, IOwner<ChildrenManager>
{
    event Action<SelectionChangedEvent> OnSelectionChanged;
    event Action<SelectionRenderedEvent> OnSelectionRendered;

    IMenuItem GetSelectedChild();
}
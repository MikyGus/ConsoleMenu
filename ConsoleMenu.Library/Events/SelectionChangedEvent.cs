﻿using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Events;
public class SelectionChangedEvent
{
    public ISelectionManager Sender { get; set; }
    public IChildItem OldItem { get; set; }
    public IChildItem NewItem { get; set; }
}
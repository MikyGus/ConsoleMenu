﻿using ConsoleMenu.Library.Menu;

namespace ConsoleMenu.Library.Events;
public class SelectionChangedEvent
{
    public IMenuItem Sender { get; set; }
    public IMenuItem OldItem { get; set; }
    public IMenuItem NewItem { get; set; }
}
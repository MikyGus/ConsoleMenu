﻿using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.PerformAction;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private readonly ChildrenManager _childrenManager;
    private Func<IMenuItem, ConsoleKeyInfo, bool> _onAction;

    public Vector2 Position { get; set; }
    public IMenuItem Parent { get; set; }

    public MenuItem(string title)
    {
        Parent = null;
        Position = Vector2.ZERO;
        Content = new Content() { Owner = this, Title = title };
        _childrenManager = new ChildrenManager(this);
    }

    public Vector2 AreaNeeded()
    {
        Vector2 contentArea = Content.AreaNeeded();
        Vector2 childrenArea = _childrenManager.AreaNeeded();
        return contentArea.MaxAdd_Vertical(childrenArea);
    }

    public void SetRenderer<T>() where T : IContentRenderer, new()
    {
        IContentRenderer contentRenderer = new T();
        Content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
    }

    public void Render()
    {
        Content.Render();
        Vector2 areaNeeded = Content.AreaNeeded();
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
    }

    public IContent Content { get; private set; }

    public IChildrenManager Children => _childrenManager;

    public bool KeyPressed(ConsoleKeyInfo key)
    {
        if (Children.HaveChildren())
        {
            if (Children.GetSelectedChild().Item.KeyPressed(key))
            {
                return true;
            }
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public bool PerformAction(ConsoleKeyInfo key) => _onAction?.Invoke(this, key) ?? false;
    public void SetAction(Func<IMenuItem, ConsoleKeyInfo, bool> action) => _onAction = action;
}
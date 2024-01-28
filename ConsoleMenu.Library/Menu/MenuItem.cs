﻿using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.PerformAction;
using ConsoleMenu.Library.Render;
using System.Diagnostics;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private readonly ChildrenManager _childrenManager;
    private bool _isCurrentlyVisible;

    public Vector2 Position { get; set; }
    public IMenuItem Parent { get; set; }
    public bool IsVisible { get; set; }
    public bool MayCollapse { get; set; }
    public event Action<IMenuItem, ConsoleKeyInfo> OnKeyPressed;

    public MenuItem(string title)
    {
        _childrenManager = new ChildrenManager(this);
        _isCurrentlyVisible = false;
        Position = Vector2.ZERO;
        Parent = null;
        IsVisible = true;
        MayCollapse = true;
        Content = new Content() { Owner = this, Title = title };
        Debug.WriteLine($"MenuItem: '{Content.Title}' have now been created.", "Ctor");
    }

    public Vector2 AreaNeeded()
    {
        if ((_isCurrentlyVisible == false || IsVisible == false) && MayCollapse)
        {
            return Vector2.ZERO;
        }
        Vector2 contentArea = Content.AreaNeeded();
        Vector2 childrenArea = _childrenManager.AreaNeeded();
        return contentArea.MaxAdd_Vertical(childrenArea);
    }

    public void SetRenderer<T>() where T : IContentRenderer, new()
    {
        IContentRenderer contentRenderer = new T();
        if (Content is Content c && c.IsCurrentlyVisible)
        {
            Content.EraseContent();
            Content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
            Content.Render();
            return;
        }
        Content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
        Debug.WriteLine($"MenuItem: '{Content.Title}' have now changed renderer.", "SetRenderer");
    }

    public void Render()
    {
        if (IsVisible == false)
        {
            return;
        }
        _isCurrentlyVisible = true;

        Content.Render();
        Vector2 areaNeeded = Content.AreaNeeded();
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
        Debug.WriteLine($"MenuItem: '{Content.Title}' have now been Rendered", "Render");
    }

    public void EraseContent()
    {
        if (_isCurrentlyVisible)
        {
            Content.EraseContent();
            _childrenManager.EraseContent();
            _isCurrentlyVisible = false;
        }
        Debug.WriteLine($"MenuItem: '{Content.Title}' have now been erased.", "EraseContent");
    }

    internal MenuItem GetRoot(MenuItem menuItem)
    {
        if (menuItem.Parent is null)
        {
            return menuItem;
        }
        else if (menuItem.Parent is MenuItem parent)
        {
            return menuItem.GetRoot(parent);
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public void ReRender()
    {
        IMenuItem root = GetRoot(this);
        root.EraseContent();
        root.Render();
    }

    public IContent Content { get; private set; }

    public IChildrenManager Children => _childrenManager;

    public IMenuItem this[int i] => _childrenManager.GetChild(i).Item;
    public IMenuItem this[string s] => _childrenManager.GetChildren().FirstOrDefault(x => x.Item.Content.Title == s).Item;
    public void AddChild(string title) => _childrenManager.Add(9999, new MenuItem(title));
    public void RemoveChild(int i) => _childrenManager.Remove(i);
    public void RemoveChild(IMenuItem menuItem) => _childrenManager.Remove(menuItem);
    public IEnumerable<IMenuItem> GetChildren() => _childrenManager.GetChildren().Select(m => m.Item);
    public bool HaveChildren() => _childrenManager.GetChildren().Any();

    public bool KeyPressed(ConsoleKeyInfo key)
    {
        Debug.WriteLine($"MenuItem: '{Content.Title}' is about to process pressed keys", "KeyPressed");
        if (HaveChildren())
        {
            if (Children.Selection.GetSelectedChild().Item.KeyPressed(key))
            {
                return true;
            }
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public void PerformAction(ConsoleKeyInfo key) => OnKeyPressed?.Invoke(this, key);
}
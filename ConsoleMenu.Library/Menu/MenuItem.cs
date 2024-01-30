using ConsoleMenu.Library.Components;
using ConsoleMenu.Library.Events;
using ConsoleMenu.Library.Extensions;
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
    private List<IComponent> _components;

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
        _components = new List<IComponent>();
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

    #region Components
    public void AddComponent(IComponent component)
    {
        component.Parent = this;
        _components.Add(component);
    }
    public IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : IComponent
    {
        foreach (TComponent component in _components.OfType<TComponent>())
        {
            yield return component;
        }
    }
    public void RemoveComponent(IComponent component)
    {
        _components.Remove(component);
    }
    #endregion

    #region Children

    public Orientation OrientationOfChildren
    {
        get => _childrenManager.OrientationOfChildren;
        set => _childrenManager.OrientationOfChildren = value;
    }
    public bool IsChildrenVisible { get => _childrenManager.IsVisible; set => _childrenManager.IsVisible = value; }
    public IMenuItem this[int i] => _childrenManager.GetChild(i).Item;
    public IMenuItem this[string s] => _childrenManager.GetChildren().FirstOrDefault(x => x.Item.Content.Title == s).Item;
    public void AddChild(string title) => _childrenManager.Add(9999, new MenuItem(title));
    public void RemoveChild(int i) => _childrenManager.Remove(i);
    public void RemoveChild(IMenuItem menuItem) => _childrenManager.Remove(menuItem);
    public IEnumerable<IMenuItem> GetChildren() => _childrenManager.GetChildren().Select(m => m.Item);
    public bool HaveChildren() => _childrenManager.GetChildren().Any();
    public Vector2 PositionOffsetOfFirstChild
    {
        get => _childrenManager.PositionOffsetOfFirstChild;
        set => _childrenManager.PositionOffsetOfFirstChild = value;
    }
    public int PositionOffsetToNextChild
    {
        get => _childrenManager.PositionOffsetToNextChild;
        set => _childrenManager.PositionOffsetToNextChild = value;
    }
    #endregion

    #region Selection
    public event Action<SelectionChangedEvent> OnSelectionChanged
    {
        add => _childrenManager.Selection.OnSelectionChanged += value;
        remove => _childrenManager.Selection.OnSelectionChanged -= value;
    }
    public event Action<SelectionRenderedEvent> OnSelectionRendered
    {
        add => _childrenManager.Selection.OnSelectionRendered += value;
        remove => _childrenManager.Selection.OnSelectionRendered -= value;
    }
    public IMenuItem GetSelectedChild() => _childrenManager.Selection.GetSelectedChild().Item;
    public bool IncrementSelection() => _childrenManager.Selection.Increment();
    public bool DecrementSelection() => _childrenManager.Selection.Decrement();
    #endregion


    public bool KeyPressed(ConsoleKeyInfo key)
    {
        Debug.WriteLine($"MenuItem: '{Content.Title}' is about to process pressed keys", "KeyPressed");
        if (HaveChildren())
        {
            if (GetSelectedChild().KeyPressed(key))
            {
                return true;
            }
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public void PerformAction(ConsoleKeyInfo key) => OnKeyPressed?.Invoke(this, key);
}
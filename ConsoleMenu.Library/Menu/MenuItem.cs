using ConsoleMenu.Library.Extensions;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.PerformAction;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public class MenuItem : IMenuItem
{
    private readonly ChildrenManager _childrenManager;
    private Func<IMenuItem, ConsoleKeyInfo, bool> _onAction;
    private bool _isCurrentlyVisible;

    public Vector2 Position { get; set; }
    public IMenuItem Parent { get; set; }
    public bool IsVisible { get; set; }
    public bool MayCollapse { get; set; }

    public MenuItem(string title)
    {
        _childrenManager = new ChildrenManager(this);
        _isCurrentlyVisible = false;
        Position = Vector2.ZERO;
        Parent = null;
        IsVisible = true;
        MayCollapse = true;
        Content = new Content() { Owner = this, Title = title };
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
        Content.EraseContent();
        Content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
        Content.Render();
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
    }

    public void EraseContent()
    {
        if (_isCurrentlyVisible)
        {
            Content.EraseContent();
            _childrenManager.EraseContent();
            _isCurrentlyVisible = false;
        }
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

    public bool KeyPressed(ConsoleKeyInfo key)
    {
        if (Children.HaveChildren())
        {
            if (Children.Selection.GetSelectedChild().Item.KeyPressed(key))
            {
                return true;
            }
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public bool PerformAction(ConsoleKeyInfo key) => _onAction?.Invoke(this, key) ?? false;
    public void SetAction(Func<IMenuItem, ConsoleKeyInfo, bool> action) => _onAction = action;
}
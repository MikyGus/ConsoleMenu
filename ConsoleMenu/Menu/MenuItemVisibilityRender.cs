using ConsoleMenu.Extensions;
using ConsoleMenu.Menu;
using ConsoleMenu.Render;
using System.Diagnostics;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemVisibilityRender
{
    private IContentRenderer _contentRenderer;
    public IContentRenderer ContentRenderer
    {
        get => _contentRenderer;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _contentRenderer = value;
            SetRenderer(_contentRenderer);
        }
    }
    private void SetRenderer(IContentRenderer contentRenderer)
    {
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
    public void ReRender()
    {
        IMenuItem root = GetRoot(this);
        root.EraseContent();
        root.Render();
    }

    private MenuItem GetRoot(MenuItem menuItem)
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

    //************************************************************
    // IVisibility
    //************************************************************
    public bool IsVisible { get; set; }
    public bool MayCollapse { get; set; }

    //************************************************************
    // IRenderContent
    //************************************************************
    private bool _isCurrentlyVisible;
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
}
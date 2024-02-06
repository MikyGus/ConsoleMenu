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
        if (_content is Content c && c.IsCurrentlyVisible)
        {
            _content.EraseContent();
            _content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
            _content.Render();
            return;
        }
        _content.SetRenderer(contentRenderer.Render, contentRenderer.AreaNeeded);
        Debug.WriteLine($"MenuItem: '{_content.Title}' have now changed renderer.", "SetRenderer");
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

        _content.Render();
        Vector2 areaNeeded = _content.AreaNeeded();
        _childrenManager.PositionOfFirstChild = new Vector2(Position.X, Position.Y + areaNeeded.Y);
        _childrenManager.Render();
        Debug.WriteLine($"MenuItem: '{_content.Title}' have now been Rendered", "Render");
    }
    public void EraseContent()
    {
        if (_isCurrentlyVisible)
        {
            _content.EraseContent();
            _childrenManager.EraseContent();
            _isCurrentlyVisible = false;
        }
        Debug.WriteLine($"MenuItem: '{_content.Title}' have now been erased.", "EraseContent");
    }
    public Vector2 AreaNeeded()
    {
        if ((_isCurrentlyVisible == false || IsVisible == false) && MayCollapse)
        {
            return Vector2.ZERO;
        }
        Vector2 contentArea = _content.AreaNeeded();
        Vector2 childrenArea = _childrenManager.AreaNeeded();
        return contentArea.MaxAdd_Vertical(childrenArea);
    }
}
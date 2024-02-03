using ConsoleMenu.Components;
using ConsoleMenu.Managers;
using ConsoleMenu.Render;
using System.Diagnostics;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItem
{
    public IMenuItem Parent { get; set; }
    public Vector2 Position { get; set; }
    public IContent Content { get; private set; }

    public MenuItem(string title)
    {
        _childrenManager = new ChildrenManager(this);
        ISelectionManager selection = new SelectionManager(_childrenManager);
        _childrenManager.Selection = selection;
        _isCurrentlyVisible = false;
        Position = Vector2.ZERO;
        Parent = null;
        IsVisible = true;
        MayCollapse = true;
        Content = new Content() { Owner = this, Title = title };
        _components = new List<IComponent>();
        Debug.WriteLine($"MenuItem: '{Content.Title}' have now been created.", "Ctor");
    }
}
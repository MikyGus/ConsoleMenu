using ConsoleMenu.Components;
using ConsoleMenu.Managers;
using ConsoleMenu.Render;
using System.Diagnostics;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItem
{
    private MenuItemOption _menuItemSettings = new();

    public IMenuItem Parent { get; set; }
    public Vector2 Position { get; set; }
    public IContent Content { get; private set; }

    public MenuItem(string title, Action<MenuItemOption> option = null)
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
        ContentRenderer = new DefaultContentRender();

        _menuItemSettings = GetCurrentSettings();
        if (option is not null)
        {
            Configure(option);
        }

        Debug.WriteLine($"MenuItem: '{Content.Title}' have now been created.", "Ctor");
    }

    public void Configure(Action<MenuItemOption> option)
    {
        option?.Invoke(_menuItemSettings);
        SetConfiguredSettings(_menuItemSettings);
    }
    private MenuItemOption GetCurrentSettings() => new()
    {
        IsVisible = IsVisible,
        MayCollapse = MayCollapse,

        // Content
        Title = Content.Title,
        IsMarked = Content.IsMarked,

        // ChildrenManager
        PositionInList = GetComponents<ListPriorityComponent>().FirstOrDefault()?.Value ?? int.MaxValue,
        OrientationOfChildren = _childrenManager.OrientationOfChildren,
        IsChildrenVisible = _childrenManager.IsVisible,
        PositionOffsetOfFirstChild = _childrenManager.PositionOffsetOfFirstChild,
        PositionOffsetToNextChild = _childrenManager.PositionOffsetToNextChild,

        // Visibility / Renderer
        ContentRenderer = ContentRenderer,
    };
    private void SetConfiguredSettings(MenuItemOption menuItemOption)
    {
        IsVisible = menuItemOption.IsVisible;
        MayCollapse = menuItemOption.MayCollapse;

        // Content
        Content.Title = menuItemOption.Title;
        Content.IsMarked = menuItemOption.IsMarked;

        // ChildrenManager
        SetPositionInList(menuItemOption.PositionInList);
        _childrenManager.OrientationOfChildren = menuItemOption.OrientationOfChildren;
        _childrenManager.IsVisible = menuItemOption.IsChildrenVisible;
        _childrenManager.PositionOffsetOfFirstChild = menuItemOption.PositionOffsetOfFirstChild;
        _childrenManager.PositionOffsetToNextChild = menuItemOption.PositionOffsetToNextChild;

        // Visibility / Renderer
        ContentRenderer = menuItemOption.ContentRenderer;
    }

    private void SetPositionInList(int positionInList)
    {
        IEnumerable<ListPriorityComponent> prioValues = GetComponents<ListPriorityComponent>();
        int prioCount = prioValues.Count();
        if (prioCount > 1)
        {
            foreach (ListPriorityComponent item in prioValues)
            {
                RemoveComponent(item);
            }
            prioCount = 0;
        }
        if (prioCount == 1)
        {
            prioValues.FirstOrDefault().Value = positionInList;
        }
        else
        {
            AddComponent(new ListPriorityComponent(positionInList));
        }
    }
}
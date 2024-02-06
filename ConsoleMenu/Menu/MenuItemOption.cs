using ConsoleMenu.Render;

namespace ConsoleMenu;
public class MenuItemOption
{
    public bool IsVisible { get; set; }
    public bool MayCollapse { get; set; }

    // ChildrenManager
    public int PositionInList { get; set; }
    public Orientation OrientationOfChildren { get; set; }
    public bool IsChildrenVisible { get; set; }
    public Vector2 PositionOffsetOfFirstChild { get; set; }
    public int PositionOffsetToNextChild { get; set; }

    // Visibility / Renderer
    public IContentRenderer ContentRenderer { get; set; }

    // Content
    public bool IsSelected { get; set; }
    public bool IsMarked { get; set; }
    public string Title { get; set; }
}
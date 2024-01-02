using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Menu;
using ConsoleMenu.Library.Models;

namespace ConsoleMenu.Library.Managers;

public enum ContentOrientation { Vetical, Horizontal }
public interface IChildrenManager : IRenderContent, ISelectionControls
{
    /// <summary>
    /// Owner of the nearest childrens.
    /// </summary>
    IMenuItem Owner { get; }
    Vector2 PositionOfFirstChild { get; set; }
    int PositionOffsetToNextChild { get; set; }
    ContentOrientation ContentOrientation { get; set; }
    void Add(int positionInList, IMenuItem item);
    void Remove(IMenuItem item);
    IEnumerable<IChildItem> GetChildren();
    bool HaveChildren();
    IChildItem GetSelectedChild();
}

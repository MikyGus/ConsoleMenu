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
    /// <summary>
    /// Position offset for the first child to be rendered. 
    /// Mainly used for indentation below the owner rendation.
    /// </summary>
    Vector2 PositionOffsetOfFirstChild { get; set; }
    /// <summary>
    /// How many position-steps from current childs upper-left corner
    /// to next childs upper-left corner. 
    /// This is the minimum steps. The area taken by the previous child might expand the steps to next child.
    /// </summary>
    int PositionOffsetToNextChild { get; set; }
    /// <summary>
    /// Set if the children should be rendered in a horizontal or vertical orientation.
    /// </summary>
    ContentOrientation ContentOrientation { get; set; }
    void Add(int positionInList, IMenuItem item);
    void Remove(IMenuItem item);
    IEnumerable<IChildItem> GetChildren();
    bool HaveChildren();
    IChildItem GetSelectedChild();
}
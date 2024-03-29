﻿using ConsoleMenu.Abstracts;

namespace ConsoleMenu.Managers;

internal interface IChildrenManager : IRenderContent, IVisibility, IOwner<IMenuItem>
{
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
    void Add(IMenuItem item);
    void Remove(IMenuItem item);
    void Remove(int itemIndex);
    IEnumerable<IMenuItem> GetChildren();
    IMenuItem GetChild(int index);

    ISelectionManager Selection { get; set; }
    Orientation OrientationOfChildren { get; set; }
}
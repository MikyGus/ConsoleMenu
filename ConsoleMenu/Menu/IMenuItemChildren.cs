namespace ConsoleMenu.Menu;
public interface IMenuItemChildren
{
    IMenuItem this[int i] { get; }
    IMenuItem this[string s] { get; }

    ///// <summary>
    ///// Set if the children should be rendered in a horizontal or vertical orientation.
    ///// </summary>
    Orientation OrientationOfChildren { get; set; }
    /// <summary>
    /// Add a new MenuItem as a child.
    /// </summary>
    /// <param name="title">Title of the new MenuItem</param>
    /// <param name="positionInList">Position of the menuItem in the list</param>
    void AddChild(string title, int positionInList = int.MaxValue);
    /// <summary>
    /// Add a new MenuItem as a child.
    /// Also adds a ValueComponent to this new child. 
    /// </summary>
    /// <typeparam name="T">Valuetype to add</typeparam>
    /// <param name="title">Title of the new MenuItem</param>
    /// <param name="value">Value to add to ValueComponent</param>
    /// <param name="positionInList">Position of the menuItem in the list</param>
    void AddChild<T>(T value, string title, int positionInList = int.MaxValue);
    void RemoveChild(int i);
    void RemoveChild(IMenuItem menuItem);
    IEnumerable<IMenuItem> GetChildren();
    bool HaveChildren();

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
    bool IsChildrenVisible { get; set; }
}
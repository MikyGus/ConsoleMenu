using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Components;
using ConsoleMenu.Library.Events;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem : IRenderContent, IVisibility
{
    IMenuItem Parent { get; set; }
    Vector2 Position { get; set; }


    /// <summary>
    /// Pushes the pressed key down all selected children.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Returns a bool stating if the key were used for an action or not (true=used)</returns>
    bool KeyPressed(ConsoleKeyInfo key);

    /// <summary>
    /// Perform acton specified in OnAction
    /// </summary>
    void PerformAction(ConsoleKeyInfo key);

    /// <summary>
    /// Specify what to happen when user presses any key with this MenuItem selected.
    /// </summary>
    event Action<IMenuItem, ConsoleKeyInfo> OnKeyPressed;

    /// <summary>
    /// Sets the renderer of the content of the item. 
    /// </summary>
    /// <typeparam name="T">ContentRenderer</typeparam>
    void SetRenderer<T>() where T : IContentRenderer, new();

    /// <summary>
    /// Removes ALL nodes, starting at the root, and renders them again.
    /// </summary>
    void ReRender();

    IContent Content { get; }

    #region Children

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

    #endregion

    #region Selection
    event Action<SelectionChangedEvent> OnSelectionChanged;
    event Action<SelectionRenderedEvent> OnSelectionRendered;
    IMenuItem GetSelectedChild();
    bool IncrementSelection();
    bool DecrementSelection();
    #endregion

    #region Components
    void AddComponent(IComponent component);
    IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : IComponent;
    void RemoveComponent(IComponent component);
    IEnumerable<T> Values<T>();
    #endregion
}
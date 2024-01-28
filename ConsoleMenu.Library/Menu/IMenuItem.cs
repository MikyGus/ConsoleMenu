using ConsoleMenu.Library.Abstracts;
using ConsoleMenu.Library.Managers;
using ConsoleMenu.Library.Models;
using ConsoleMenu.Library.Render;

namespace ConsoleMenu.Library.Menu;
public interface IMenuItem : IRenderContent, IVisibility
{
    IMenuItem this[int i] { get; }
    IMenuItem this[string s] { get; }
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
    void AddChild(string title);
    void RemoveChild(int i);
    void RemoveChild(IMenuItem menuItem);
    IEnumerable<IMenuItem> GetChildren();
    bool HaveChildren();

    IContent Content { get; }
    IChildrenManager Children { get; }
}
namespace ConsoleMenu.Menu;
public interface IMenuItemActionPerformed
{
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
}
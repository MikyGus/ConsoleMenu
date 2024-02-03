using ConsoleMenu.Menu;
using ConsoleMenu.PerformAction;
using System.Diagnostics;

namespace ConsoleMenu;
public partial class MenuItem : IMenuItemActionPerformed
{
    public event Action<IMenuItem, ConsoleKeyInfo> OnKeyPressed;

    public bool KeyPressed(ConsoleKeyInfo key)
    {
        Debug.WriteLine($"MenuItem: '{Content.Title}' is about to process pressed keys", "KeyPressed");
        if (HaveChildren())
        {
            if (GetSelectedChild().KeyPressed(key))
            {
                return true;
            }
        }
        return ActionToPerform.MoveSelection(key, this);
    }

    public void PerformAction(ConsoleKeyInfo key) => OnKeyPressed?.Invoke(this, key);
}
# Console Menu
**Display a menu in your Console-app.**

[![Code Tests](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml/badge.svg?branch=master)](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml)

- [Console Menu](#console-menu)
	- [Create a simple menu](#create-a-simple-menu)
	- [Vector2](#vector2)
	- [Position](#position)
	- [Children of MenuItem](#children-of-menuitem)
		- [Add children](#add-children)
		- [Remove children](#remove-children)
		- [Content orientation](#content-orientation)
		- [PositionOffsetOfFirstChild](#positionoffsetoffirstchild)
	- [Renderers](#renderers)
		- [SetRenderer](#setrenderer)
		- [Render](#render)
	- [Actions](#actions)
		- [KeyPressed](#keypressed)
		- [SetAction](#setaction)




## Create a simple menu
Below is an example of a simple menu. This will print a simple menu with three items.
```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Render();
```

**Output**
```bash
 Simple menu 
  Menu 1
  Menu 2
  Menu 3
```

## Vector2
```class Vector2 : IEquatable<Vector2>```

Represents a point in a 2D space or 2D area with integers for X and Y.

**Properties**
```csharp
    public int X { get; set; }
    public int Y { get; set; }
```

**Methods**
```csharp
	Vector2 Duplicate();
```
**Static**
```csharp
	public static Vector2 ZERO => new(0, 0);
	public static Vector2 UP => new(0, -1);
	public static Vector2 DOWN => new(0, 1);
	public static Vector2 LEFT => new(-1, 0);
	public static Vector2 RIGHT => new(1, 0);
	public static Vector2 LEFT_UP => new(-1, -1);
	public static Vector2 LEFT_DOWN => new(-1, 1);
	public static Vector2 RIGHT_UP => new(1, -1);
	public static Vector2 RIGHT_DOWN => new(1, 1);
```

## Position
```(Vector2) MenuItem.Position```

The first menuItem is by default placed at the coordinates 0,0. With the ```Vector2``` class. To change position use the ```Position``` property on menuItem. 

	**WARNING!!**
	The positions of the menuItems children will be overwritten! 
	You can therefore not change the position of the children.

```csharp
	MenuItem menu = new MenuItem("Simple menu");
->> menu.Position = new Vector2(0,1);
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Render();
```
## Children of MenuItem
### Add children
To add children to a menuItem we use the method ```Add()```.

The method ```(void) Add(int, IMenuItem)``` takes 2 arguments. 
1. **Position in list**: An integer value stating the order to display the children. Lowest number is displayed first. Children may have the same position-number.
2. **MenuItem**: \<IMenuItem> The menuItem to add as a child. 


```csharp
	MenuItem subMenu = new MenuItem("My SubMenu #1");
->> subMenu.Children.Add(1, new MenuItem("Sub1"));
->> subMenu.Children.Add(2, new MenuItem("Sub2"));
	subMenu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;

	MenuItem subMenu2 = new MenuItem("My SubMenu #2");
->> subMenu2.Children.Add(1, new MenuItem("Sub1"));
->> subMenu2.Children.Add(2, new MenuItem("Sub2"));

	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
->> menu.Children.Add(1, subMenu);
->> menu.Children.Add(2, subMenu2);
->> menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.Render();
```

**Output**
```bash
Simple menu
  My SubMenu #1  My SubMenu #2  Menu 3
   Sub1  Sub2     Sub1
                  Sub2
```

### Remove children
If you need to remove child from a menuItems children you use the ```Remove()``` method.

The method ```Remove(IMenuItem)``` takes one (1) argument, ```IMenuItem```.

```csharp
	IMenuItem menuItem = new MenuItem("Menu 1");

	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, menuItem);
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
->> menu.Children.Remove(menuItem);
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.Render();
```

**Output**
```bash
 Simple menu 
  Menu 2  Menu 3 
```

### Content orientation
As default the children to a menuItem have a vertical orientation. To print in a horizontal orientation we add a ContentOrientation.

```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0,1); 
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
->>	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.Render();
```

The options for ContentOrientation are ```Vertical``` (default) and ```Horizontal```
```csharp
menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Vetical;
menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
```

**Output - Vertical**
```bash
 Simple menu 
  Menu 1
  Menu 2
  Menu 3
```

**Output - Horizontal**
```bash
 Simple menu 
  Menu 1  Menu 2  Menu 3 
```


### PositionOffsetOfFirstChild
```Vector2 IChildrenManager.PositionOffsetOfFirstChild { get; set; }```

Offsets the first child and the rest follows. If set to Vector2(0,0), the first child is positioned directly below the parent menuItem.
```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
->> menu.Children.PositionOffsetOfFirstChild = new Vector2(10, 0);
	menu.Render();
```

**Output**
```bash
Simple menu]
          [Menu 1][Menu 2][Menu 3]
```

## Renderers
### SetRenderer
```void IMenuItem.SetRenderer<T>() where T : ContentRender, new();```
**Default**: DefaultContentRender

```csharp
	IMenuItem menuItem1 = new MenuItem("Menu 1");
->> menuItem1.SetRenderer<CheckboxContentRender>();
	IMenuItem menuItem2 = new MenuItem("Menu 2");
->> menuItem2.SetRenderer<CheckboxContentRender>();
	menuItem2.ContentRenderer.IsMarked = true;
	IMenuItem menuItem3 = new MenuItem("Menu 3");
->> menuItem3.SetRenderer<CheckboxContentRender>();

	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, menuItem1);
	menu.Children.Add(2, menuItem2);
	menu.Children.Add(3, menuItem3);
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.Render();
```

**Output**
```bash
[Simple menu]
 [ ] Menu 1[X] Menu 2[ ] Menu 3
```

### Render
- ```void IMenuItem.Render()```: Renders the menuItem **and** all its children.
- ```void IMenuItem.ContentRenderer.Render(Vector2)```: Renders the menuItem.
- ```void IMenuItem.Children.Render()```: Renders the menuItems children.

```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.Render();
	
->> menu.ContentRenderer.Render(menu.Position);
->> menu.Children.Render();
```

## Actions
### KeyPressed
```bool IMenuItem.KeyPressed(ConsoleKeyInfo key);```
Through all the menuItems **selected** children, starting at the leaf item, an action to move selection or perform a custom action is made. Each menuItem decides if the parent menuItem should move selection or perform a custom action.

A selection is moved by UP, DOWN, LEFT and RIGHT arrows.

The first rendered selection is not automatic, but is instead rendered first time the selection moves. Use ```IMenuItem.ContentRenderer.IsSelected``` property to set the first selection and the selection will be rendered at the next call of ```Render()```

```csharp
	MenuItem subMenu = new MenuItem("My SubMenu #1");
	subMenu.Children.Add(1, new MenuItem("Sub1"));
	subMenu.Children.Add(2, new MenuItem("Sub2"));
	subMenu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;

	MenuItem subMenu2 = new MenuItem("My SubMenu #2");
	subMenu2.Children.Add(1, new MenuItem("Sub1"));
	subMenu2.Children.Add(2, new MenuItem("Sub2"));

	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, subMenu);
	menu.Children.Add(2, subMenu2);
	menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
->> menu.ContentRenderer.IsSelected = true;
	menu.Render();
		
->> ConsoleKeyInfo keyInput;
->> do
->> {
->> 	keyInput = Console.ReadKey(true);
->> 	menu.KeyPressed(keyInput);
->> } while (keyInput.Key != ConsoleKey.Escape);
```

### SetAction
```void IMenuItem.SetAction(Func<IMenuItem, ConsoleKeyInfo, bool> action);```

If another key is pressed than the arrow-keys the custom action is invoked (if set). 

Here are some example actions

```csharp
	MenuItem subMenu = new MenuItem("My SubMenu #1");
	subMenu.Children.Add(1, new MenuItem("Sub1"));
	subMenu.Children.Add(2, new MenuItem("Sub2"));
	subMenu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
->> subMenu.SetAction(SetItemMarkOnParent);

	MenuItem subMenu2 = new MenuItem("My SubMenu #2");
	subMenu2.Children.Add(1, new MenuItem("Sub1"));
	subMenu2.Children.Add(2, new MenuItem("Sub2"));
->> subMenu2.SetAction(SetItemMarkChildren);

	MenuItem subMenu3 = new MenuItem("My SubMenu #3");
->> subMenu3.SetAction(SetItemMark);

	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0, 1);
	menu.Children.Add(1, subMenu);
	menu.Children.Add(2, subMenu2);
	menu.Children.Add(3, subMenu3);
	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
	menu.ContentRenderer.IsSelected = true;
	menu.Render();

	ConsoleKeyInfo keyInput;
	do
	{
		keyInput = Console.ReadKey(true);
		menu.KeyPressed(keyInput);
	} while (keyInput.Key != ConsoleKey.Escape);
```

```csharp
	static bool SetItemMark(IMenuItem item, ConsoleKeyInfo key)
	{
		if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
		{
			return false;
		}

		item.ContentRenderer.IsMarked = !item.ContentRenderer.IsMarked;
		item.ContentRenderer.Render(item.Position);
		return false;
	}
```

```csharp
    static bool SetItemMarkOnParent(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return false;
        }

        if (item.Parent is not null)
        {
            item.Parent.ContentRenderer.IsMarked = !item.Parent.ContentRenderer.IsMarked;
            item.Parent.ContentRenderer.Render(item.Parent.Position);
        }
        item.ContentRenderer.Render(item.Position);
        return false;
    }
```

```csharp
    static bool SetItemMarkChildren(IMenuItem item, ConsoleKeyInfo key)
    {
        if (key.Key is not ConsoleKey.Enter and not ConsoleKey.E)
        {
            return false;
        }

        if (item.Children.HaveChildren())
        {
            foreach (var child in item.Children.GetChildren())
            {
                child.Item.ContentRenderer.IsMarked = !child.Item.ContentRenderer.IsMarked;
            }
            item.Render();
        }
        return false;
    }
```
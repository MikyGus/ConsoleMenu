# Console Menu
**Helps you display a menu in your Console-app.**

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
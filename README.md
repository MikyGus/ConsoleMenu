# Console Menu
**Helps you display a menu in your Console-app.**

[![Code Tests](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml/badge.svg?branch=master)](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml)

- [Console Menu](#console-menu)
	- [Create a simple menu](#create-a-simple-menu)
	- [Content orientation](#content-orientation)
	- [Position](#position)
	- [Add children](#add-children)




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

## Content orientation
As default the children to a menuItem have a vertical orientation. To print in a horizontal orientation we add a ContentOrientation.

```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0,1); 
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));

	menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal; <<---
	
	menu.Render();
```

The options for ContentOrientation are ```Vertical``` (default) and ```Horizontal```
```csharp
menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Vetical;
menu.Children.ContentOrientation = Library.Managers.ContentOrientation.Horizontal;
```
## Position
The first menuItem is by default placed at the coordinates 0,0. With the ```Vector2``` class. To change position use the ```Position``` property on menuItem. 

!!! Warning The positions of the menuItems children will be overwritten! 
	You can therefore not change the position of the children.

```csharp
	MenuItem menu = new MenuItem("Simple menu");
	menu.Position = new Vector2(0,1); <<--
	menu.Children.Add(1, new MenuItem("Menu 1"));
	menu.Children.Add(2, new MenuItem("Menu 2"));
	menu.Children.Add(3, new MenuItem("Menu 3"));
	menu.Render();
```

## Add children

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
	menu.Render();
```

**Output**
```bash
Simple menu
  My SubMenu #1  My SubMenu #2  Menu 3
   Sub1  Sub2     Sub1
                  Sub2
```
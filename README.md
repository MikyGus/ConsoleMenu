# Console Menu
**Helps you display a menu in your Console-app.**

[![Code Tests](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml/badge.svg?branch=master)](https://github.com/MikyGus/ConsoleMenu/actions/workflows/ci.yaml)

- [Console Menu](#console-menu)
	- [Create a simple menu](#create-a-simple-menu)
	- [Position](#position)
	- [Children of MenuItem](#children-of-menuitem)
		- [Add children](#add-children)
		- [Remove children](#remove-children)
		- [Content orientation](#content-orientation)




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



## Position
The first menuItem is by default placed at the coordinates 0,0. With the ```Vector2``` class. To change position use the ```Position``` property on menuItem. 

	**NOTE!!**
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

The method ```Add(int, IMenuItem)``` takes 2 arguments. 
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
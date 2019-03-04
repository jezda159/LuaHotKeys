
# LuaHotKey.cs

A small, cranky project for generating interlinked AutoHotKey and LuaMacros code from single, easier to take care of syntax. I wanted to quickly create shortcuts for my secondary numpad/hotkey machine.

## Requirements

You need these to make the generated files work:
 * [AutoHotKey](https://autohotkey.com/)
 * [LuaMacros](https://github.com/me2d13/luamacros)

## Quick example to get you interested:
```
Keyboard hotKeyboard = new Keyboard("fantech", "A3243CA", "GIMP", new KeyPress[] 
{
    new KeyPress(96, "0", "", "empty", new string[] {
    }),
    new KeyPress(97, "1", "fast", "cut", new string[] {
        "i"
    }),
    new KeyPress(98, "2", "fast", "remove", new string[] {
        ahk.Keys("+e")
    }),
    new KeyPress(101, "5", "default", "osamostatnit", new string[] {
        ahk.MouseMode("Screen"),
        ahk.Click(1800, 900, "right"),
        ahk.Click(1700, 380, "left"),
        ahk.Keys("^v"),
        ahk.MouseMode("Window"),
        ahk.Click(300, 50, "left"),
        ahk.Click(300, 180, "left")
    })
}
```

## Understanding parts of project

All you need to be working with is the Keyboard in Program.cs file, other things are for programmers (though i'm not discouraging you). The idea is that you can only setup this/these keyboard and make LuaHotKey work for you!

### Keyboard Class

Keyboard actually represents a physical keyboard on (or under) your desk, you specify its: 
 * **Keyboard Label** - You have free hand at this, use it to differentiate easier in your code
 * **Keyboard Code** - A specification for the intended keyboard use code below in LuaMacros window to find it out, it will be 8 characters long and between two &
```
clear()
lmc_device_set_name("your_keyboard", "12341234") 
lmc_print_devices()

lmc_set_handler("your_keyboard", function(btn, dir)
  print("button " .. btn .. " in direction " .. dir)
end
```
 * **Application Name** - AutoHotKey will only work inside window, that has this in the header => use 'Chrome' for Google Chrome etc.
 * **Key Presses** - A list of clicks, that you want to asign something to

### KeyPress Class

A single KeyPress represents a key press on specified keyboard:
 * **Key ID** - Number of key that you get from the LuaMacros code shown several lines higher
 * **Key Label** - Thing that helps you remember what key on the keyboard you wanted this code to work on
 * **Hotkey** - You can set custom Hotkeys to trigger the code, there are also two presets:
	- default - Application sets a Ctrl+Shift+{0,1,2,...x,y,z} shortcut - this may interfier with the app youre gonna be using it in! Please aknowledge it and set something else by yourself.
	- fast - This process skips linking LuaMacros to AutoHotKey and imidiately makes LuaMacros do the desired key press
 * **Action Label** - Name your function so you know what this should do
 * **Action Set** - A list of AHKActions - AutoHotKey scripts covered in C#

### AHKActions Class

A list of possible AutoHotKey scripts:
 * Click(x, y, button)
	- x, y - desired position of cursor, use AutoHotKeys window inspector to find it out
	- button - right, left, 2 (left double-click)
 * Keys(string keys)
	- keys - single or multiple characters or numbers, function buttons need bracets => {up} {enter} {f1} etc.
 * KeyDown(string key)
 	- keys - press down single character, number or function button => dont use bracets => UP ENTER F1 etc.
 * KeyUp(string key)
	- keys - stop pressing down key
 * Wait(int milisecond)
	- miliseconds - script will wait before it does the next function
 * MouseMode(string mode)
	- mode - select how mouse system should be interpreting x, y values
	- Window (0, 0 is in top right corner of current window)
	- Screen (0, 0 is in top right corner of your screen)
 * MouseMove(int x, int y)
	- x, y - desired position of cursor, use AutoHotKeys window inspector to find it out
 * MouseDrag(string button, int x1, int y1, int x2, int y2)
	- button - right, left
	- x1, y1 - initial mouse position
	- x2, y2 - final mouse position
 * Open(string item)
	- item - name of program, but you should rather fill in the full path to it

### Helpers

AutoHotKey special buttons: ^ = Control,  ! = Alt, + = Shift, # = Windows, ~ = Enter
 
Lua special buttons: ^ = Control,  % = Alt, + = Shift, # = Windows, ~ = Enter

As you can see, only Alt keys have different chars, program can work with that and set the right one!

## What to do next

 * Tests! Tests! Tests! - currently not working due to the long-string-multiple-line-type outputs of methods 
 * Allow multiple keyboards by default
 * More AutoHotkey functions

## Authors

* **Matej Jezdinsky** - *Initial work* - [jezda159](https://github.com/jezda159) - [matejjezdinsky.cz](https://matejjezdinsky.cz)

See also the list of [contributors](https://github.com/jezda159/luahotkey.cs/contributors) who participated in this project.

## Inspiration

Taran's (from Linus tech tips) second keyboard setup [Youtube](https://www.youtube.com/watch?v=Arn8ExQ2Gjg) [GitHub](https://github.com/TaranVH/2nd-keyboard)

Tom's video that came up with the idea of connecting lua and autohotkey [Youtube](https://youtu.be/lIFE7h3m40U)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
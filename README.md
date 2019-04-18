
# LuaHotKey.cs

A small, cranky project for generating interlinked AutoHotKey and LuaMacros code from single, easier to take care of syntax. I wanted to quickly create shortcuts and hotkeys for my secondary numpad.

## Requirements

You need these to make the generated files work:
 * [AutoHotKey](https://autohotkey.com/)
 * [LuaMacros](https://github.com/me2d13/luamacros)

## Quick example to get you interested:
```
Keyboard hotkeyBoard = new Keyboard("GIMP 2", true, new KeyPress[] 
{
    new KeyPress("1", "fast", "cut", new string[] {
        "i"
    }),
    new KeyPress("2", "fast", "remove", new [] {
        ahk.Keys("+e")
    }),
	new KeyPress("3", "get mouse position and color", new [] {
        ahk.GetMousePosition("mouseX", "mouseY"),
        ahk.GetPixelUnderMouse("color"),
        ahk.Alert("[%mouseX%, %mouseY%] %color%")
    }),
    new KeyPress("4", "export as png", new [] {
        ahk.Keys("^+e"),
        ahk.Wait(1000),
        ahk.Keys("{right}{right}{delete}{delete}{delete}png"),
        ahk.MouseMode("Screen"),
        ahk.Click(1200, 350, "left"),
        ahk.Click(1000, 510, "2"),
        ahk.Click(1300, 830, "left"),
        ahk.MouseMove(1000, 740)
    })
}

```

## Understanding parts of project

Basic configuration of the hotkeys is all inside the Setup.cs file. All other things are for piecing together the scripts.
You connect your secondary keyboard and using LuaMacros find it's code and set it into the app. Then add a new keyboard, fill it with keypresses and set the AutoHotKey actions that you want to do. Run the project, open the generated files.

You'll probably need to update the keycodes to fit other types of keyboards out there, this can also be done trough LuaMacros.

You can add any AutoHotKey function you'd like or update the main body of scripts.

### Helpers

AutoHotKey special buttons: ^ = Control,  ! = Alt, + = Shift, # = Windows, ~ = Enter
 
Lua special buttons: ^ = Control,  % = Alt, + = Shift, # = Windows, ~ = Enter

As you can see, only Alt keys have different chars, program can work with that and set the right one!

## What to do next

 * Tests! Tests! Tests!
 * Add double-click functionality
 * Create multi copy&paste functionality
 * Allow multiple keyboards by default
 * Add more AutoHotkey functions

## Authors

* **Matej Jezdinsky** - *Initial work* - [jezda159](https://github.com/jezda159) - [matejjezdinsky.cz](https://matejjezdinsky.cz)

See also the list of [contributors](https://github.com/jezda159/luahotkey.cs/contributors) who participated in this project.

## Inspiration

Taran's (from Linus tech tips) second keyboard setup [Youtube](https://www.youtube.com/watch?v=Arn8ExQ2Gjg) [GitHub](https://github.com/TaranVH/2nd-keyboard)

Tom's video that came up with the idea of connecting lua and autohotkey [Youtube](https://youtu.be/lIFE7h3m40U)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

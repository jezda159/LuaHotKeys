
# LuaHotKey.cs

A small, cranky project for generating interlinked AutoHotKey and LuaMacros code from single, easier to take care of syntax. I wanted to quickly create shortcuts and hotkeys for my secondary numpad.

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

Basic configuration of the hotkeys is all inside the Setup.cs file. All other things are for piecing together the scripts.
You connect your secondary keyboard and using LuaMacros find it's code and set it into the app. Then add a new keyboard, fill it with keypresses and set the AutoHotKey actions that you want to do. Run the project, open the generated files.

### Helpers

AutoHotKey special buttons: ^ = Control,  ! = Alt, + = Shift, # = Windows, ~ = Enter
 
Lua special buttons: ^ = Control,  % = Alt, + = Shift, # = Windows, ~ = Enter

As you can see, only Alt keys have different chars, program can work with that and set the right one!

## What to do next

 * Tests! Tests! Tests!
 * Add double-click functionality
 * Create multi copy&paste functionality
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

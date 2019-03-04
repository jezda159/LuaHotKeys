using LuaHotKey.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LuaHotKey
{
    class Setup
    {
        /// <summary>
        /// Keyboard identifiers for mapping the LuaMacros script
        /// </summary>
        /// <returns></returns>
        public KeyboardIdentifier KeyboardID()
        {
            /* Code will be revealed once your LuaMacros app is setup
             * otherwise, you can use this one especialy to find it out
             * print_devices will show you avalible keyboards
             * its code is in the string of letters, between two ampersands (&'s)
             * place it instead of "12341234" and watch if keyboard will 
             * print yout inputs
clear()
  lmc_device_set_name("your_keyboard", "12341234") 
  lmc_print_devices()

lmc_set_handler("your_keyboard", function(btn, dir)
  print("button " .. btn .. " in direction " .. dir)
end
             */
            KeyboardIdentifier hardware_keyboard = new KeyboardIdentifier() { Title = "keyboard", Code = "34736889" };
            KeyboardIdentifier cheap_gamepad = new KeyboardIdentifier() { Title = "gamepad", Code = "EFDA8610" };
            KeyboardIdentifier ctech_numpad = new KeyboardIdentifier() { Title = "numpad", Code = "12EE289" };

            // This one will be used for setup
            return ctech_numpad;
        }

        /// <summary>
        /// Keyboard setting, used to create the hotkeys
        /// </summary>
        /// <returns></returns>
        public Keyboard KeyboardSet() { 

            AHKActions ahk = new AHKActions();
            
            // Customize these to create custom hotkeys
            Keyboard testingKb = new Keyboard("testing", false, new KeyPress[]
            {
                new KeyPress("1", "pohni", new []
                {
                    ahk.MouseMove(100, 100),
                    ahk.MouseMove(150, 200),
                    ahk.MouseMove(200, 300),
                    ahk.MouseMove(250, 400),
                    ahk.MouseMove(300, 500)
                }),
                new KeyPress("2", "podminky", new []
                {
                    ahk.If("10 < 100", new [] {

                        ahk.Alert("deset je menší než sto", 4)

                    }, new [] {

                        ahk.Alert("deset je vetsi nez sto")
                    })
                }),
                new KeyPress("3", "get mouse pos", new []
                {
                    ahk.GetMousePosition("mouseX", "mouseY"),
                    ahk.GetPixelRGB("color", "mouseX", "mouseY"),
                    ahk.Alert("[%mouseX%, %mouseY%] %color%")
                }),
                new KeyPress("4", "show the area of Scratch", new []
                {
                    ahk.MouseSpeed(2),
                    ahk.CoordMode("Pixel", "Screen"),
                    ahk.CoordMode("Mouse", "Screen"),


                    ahk.GetMousePosition("mouseX", "mouseY"),

                    ahk.DefineVariable("nX", "1310"),
                    ahk.DefineVariable("nY", "250"),
                    ahk.DefineVariable("mX", "1900"),
                    ahk.DefineVariable("mY", "650"),

                    ahk.MouseMove("nX", "nY"),
                    ahk.MouseMove("nX", "mY"),
                    ahk.MouseMove("mX", "mY"),
                    ahk.MouseMove("mX", "nY"),

                    ahk.MouseMove("mouseX", "mouseY")
                }),
                new KeyPress("5", "click tha Scratch", new []
                {
                    ahk.MouseSpeed(0),
                    ahk.CoordMode("Pixel", "Screen"),
                    ahk.CoordMode("Mouse", "Screen"),

                    ahk.SearchPixelRGB("foundX", "foundY", "1310", "250", "1900", "650", "19ABFF", 4, new []
                    {
                        ahk.Raw("MouseClick, left, foundX, foundY")
                    }, new [] { "" })
                }),

                new KeyPress("C", "enter", new []
                {
                    ahk.Keys("{enter}")
                })
            });

            Keyboard gimpKb = new Keyboard("GIMP", true, new KeyPress[]
            {
                new KeyPress("1", "nuzky", new string[] {
                    ahk.Keys("i")
                }),
                new KeyPress("2", "guma", new string[] {
                    ahk.Keys("+e")
                }),
                new KeyPress("3", "zrusit vyber", new string[] {
                    ahk.Keys("^+a")
                }),
                new KeyPress("4", "690", new string[] {
                    ahk.Keys("{Numpad6}{Numpad9}{Numpad0}")
                }),
                new KeyPress("5", "zmensit", new string[] {
                    ahk.MouseMode("Screen"),
                    ahk.Click(1800, 190, "right"),
                    ahk.Click(1700, 380, "left"),
                    ahk.Keys("^v"),
                    ahk.MouseMode("Window"),
                    ahk.Click(300, 50, "left"),
                    ahk.Click(300, 180, "left")
                }),
                new KeyPress("6", "ctverecek", new string[] {
                    ahk.Click(300, 50, "left"),
                    ahk.Click(300, 150, "left"),
                    ahk.Keys("690{tab}{tab}690"),
                    ahk.Wait(100),
                    ahk.Keys("{tab}{tab}{tab}{tab}{tab}{enter}"),
                    ahk.Wait(100),
                    ahk.Keys("{tab}{tab}{tab}{tab}{enter}")
                }),
                new KeyPress("7", "export PNG", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}png"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 480, "2"),
                    ahk.Click(1300, 830, "left"),
                    ahk.MouseMove(1000, 740)
                }),
                new KeyPress("8", "export png", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}png"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 510, "2"),
                    ahk.Click(1300, 830, "left"),
                    ahk.MouseMove(1000, 740)
                }),
                new KeyPress("9", "export jpg", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}jpg"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 420, "2"),
                    ahk.Click(1300, 840, "left"),
                    ahk.MouseMove(1000, 680)
                }),
                new KeyPress("C", "just enter", new string[]
                {
                    ahk.Keys("{enter}")
                })
            });

            Keyboard gdocsKb = new Keyboard("gdocs", false, new KeyPress[]
            {
                new KeyPress("-", "vybrat vlevo", new [] {
                    ahk.Keys("+{left}")
                }),
                new KeyPress("+", "vybrat vpravo", new [] {
                    ahk.Keys("+{right}")
                }),

                new KeyPress("B", "page break", new []
                {
                    ahk.Keys("^{enter}")
                }),
                new KeyPress("C", "nový řádek s odrážkou", new []
                {
                    ahk.Keys("{ENTER}-{SPACE}")
                }),

                new KeyPress("X", "tlustě", new [] {
                    ahk.Keys("^b")
                }),
                new KeyPress("Y", "šikmě", new [] {
                    ahk.Keys("^i")
                }),
                new KeyPress("Z", "podtrhnut", new [] {
                    ahk.Keys("^u")
                })
            });

            Keyboard donatoliveKb = new Keyboard("donatolive", new KeyPress[]
            {
                new KeyPress("1", "srdicka", new [] {
                    ahk.Keys("<3 <3 <3{Space}")
                }),
                new KeyPress("2", "otazniky", new [] {
                    ahk.Keys("?????????")
                }),
                new KeyPress("3", "WutFace", new [] {
                    ahk.Keys("WutFace{Space}")
                }),
                new KeyPress("4", "Keepo", new [] {
                    ahk.Keys("Keepo{Space}")
                }),
                new KeyPress("5", "LUL", new [] {
                    ahk.Keys("LUL{Space}")
                }),
                new KeyPress("6", "NotLikeThis", new [] {
                    ahk.Keys("NotLikeThis{Space}")
                }),
                new KeyPress("+", "jsme pro", new [] {
                    ahk.Keys("11")
                }),
                new KeyPress("-", "jsme proti", new [] {
                    ahk.Keys("22")
                }),

                new KeyPress("A", "[at]donatolive", new [] {
                    ahk.Keys("@donatolive")
                }),
                new KeyPress("B", "copy this", new [] {
                    ahk.Keys("^c"),
                    ahk.Keys("{right}"),
                    ahk.Keys("^v")
                }),
                new KeyPress("C", "enter", new [] {
                    ahk.Keys("{Enter}")
                })
            });

            // thi skeyboard will be used it final setup
            Keyboard useThis = testingKb;
            
            //Customize final directory, (blank uses the root of app)
            useThis.FilePath = "D:/C#/LuaHotKey-v2";

            return useThis;
        }
    }

    // Model for KeyboardIDs
    class KeyboardIdentifier{

        public string Title { get; set; }
        public string Code { get; set; }
        
    }
}
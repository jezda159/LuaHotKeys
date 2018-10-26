using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            AHKactions ahk = new AHKactions();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            //Keyboard: alias for keyboard - it's 8 digit code - name of the target program - set of commands
            Keyboard basic_app = new Keyboard("fantech", "A3243CA", "GIMP2", new KeyPress[] 
            {
                //KeyPress: number of key - label of key - desired hotkey (default for automatic, fast for direct input) 
                //  - label for function (empty) - set of commands
                new KeyPress(96, "0", "", "empty", new string[] {
                }),
                new KeyPress(97, "1", "fast", "nuzky", new string[] {
                    ahk.KeysBin("i")
                }),
                new KeyPress(98, "2", "fast", "guma", new string[] {
                    ahk.KeysBin("+e")
                }),
                new KeyPress(99, "3", "fast", "zrusit vyber", new string[] {
                    ahk.KeysBin("^+a")
                }),
                new KeyPress(100, "4", "default", "690", new string[] {
                    ahk.Keys("690")
                }),
                new KeyPress(101, "5", "default", "ctverecek", new string[] {
                    ahk.MouseMode("Screen"),
                    ahk.Click(1800, 900, "right"),
                    ahk.Click(1700, 380, "left"),
                    ahk.Keys("^v"),
                    ahk.MouseMode("Window"),
                    ahk.Click(300, 50, "left"),
                    ahk.Click(300, 180, "left")
                }),
                new KeyPress(102, "6", "", "empty", new string[] {
                }),
                new KeyPress(103, "7", "!q", "export PNG", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}png"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 480, "2"),
                    ahk.Click(1300, 830, "left"),
                    ahk.MouseMove(1000, 740)
                }),
                new KeyPress(104, "8", "!w", "export png", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}png"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 510, "2"),
                    ahk.Click(1300, 830, "left"),
                    ahk.MouseMove(1000, 740)
                }),
                new KeyPress(105, "9", "!e", "export jpg", new string[] {
                    ahk.Keys("^+e"),
                    ahk.Wait(1000),
                    ahk.Keys("{right}{right}{delete}{delete}{delete}jpg"),
                    ahk.MouseMode("Screen"),
                    ahk.Click(1200, 350, "left"),
                    ahk.Click(1000, 420, "2"),
                    ahk.Click(1300, 840, "left"),
                    ahk.MouseMove(1000, 680)
                })
            });
            
            // Show the result of translation
            Console.WriteLine(basic_app.Construct());

            // Ask to generate the final files
            Console.WriteLine("Are you happy with results? y/n");
            string buildIT = Console.ReadLine();

            path = "D:/C#/LuaHotKey";

            if(buildIT == "y")
            {
                bool success = SaveFiles(basic_app, path);

                if (success)
                {
                    Console.WriteLine("All probably went right, the files are in '" + path + "'");
                }
                else
                {
                    Console.WriteLine("Saving the files failed");
                }
                
            }
            else
            {
                Console.WriteLine("OK, no files were created");
            }

            Console.ReadLine();

            bool SaveFiles(Keyboard kb, string FilePath)
            {
                string outPath = "";

                outPath = FilePath + "/" + kb.AppName + ".lhk.lua";
                System.IO.File.WriteAllText(outPath, kb.LuaCode);

                outPath = FilePath + "/" + kb.AppName + ".lhk.ahk";
                System.IO.File.WriteAllText(outPath, kb.AHKCode);

                return true;
            }
        }
        
        public class Keyboard
        {
            private string KeyboardLabel { get; set; }
            private string KeyboardCode { get; set; }
            public string AppName { get; set; }
            private KeyPress[] KeyPresses { get; set; }

            public Keyboard(string keyboardLabel, string keyboardCode, string appName, KeyPress[] keyPresses)
            {
                KeyboardLabel = keyboardLabel;
                KeyboardCode = keyboardCode;
                AppName = appName;
                KeyPresses = keyPresses;
            }

            public string LuaCode = "";
            public string AHKCode = "";
            public string Construct()
            {
                string luaLines = "";
                string ahkLines = "";

                // From every KeyPress, create specific code in lua and ahk
                for (int n = 0; n < KeyPresses.Length; n++)
                {
                    
                    if (KeyPresses[n].ActionLabel != "empty")
                    {
                        luaLines += KeyPresses[n].LuaMakeLine(n);

                        if (KeyPresses[n].HotKey != "fast")
                        {
                            ahkLines += KeyPresses[n].AHKMakeLine(n);
                        }
                    }
                }

                // Wrap lua and ahk codes with necesary components
                this.LuaCode = fillLua(KeyboardLabel, KeyboardCode, luaLines);
                this.AHKCode = fillAHK(AppName, ahkLines);

                return "\n >> Lua: \n\n" + LuaCode + "\n\n\n >> AutoHotKey: \n\n" + AHKCode + "\n\n";
            }

            private string fillLua(string KeyboardLabel, string KeyboardCode, string luaLines)
            {
                return "clear() \n"
                    + "lmc_device_set_name('wired_joy', 'EFDA8610') \n"
                    + "lmc_device_set_name('base_keyboard', '34736889') \n"
                    + "lmc_device_set_name('" + KeyboardLabel + "', '" + KeyboardCode + "') \n"
                    + "lmc_print_devices() \n\n"
                    + "lmc_set_handler('" + KeyboardLabel + "', function(button, direction) \n"
                    + "  --print('button ' .. button .. ' in direction ' .. direction) \n\n"
                    + "  if direction == 1 then \n\n"
                    + luaLines
                    + "\n    end \n  end \nend)";
            }

            private string fillAHK(string AppName, string ahkLines)
            {
                return "SetTitleMatchMode, 2 ; look in any part of header \n"
                    + "SetTitleMatchMode, Fast \n"
                    + "#IfWinActive " + AppName + " \n\n"
                    + ahkLines
                    + "#IfWinActive";
            }
        }

        public class KeyPress
        {
            private int KeyID { get; set; }
            private string KeyLabel { get; set; }
            public string HotKey { get; set; }
            public string ActionLabel { get; set; }
            public string[] ActionSet { get; set; }
            
            public KeyPress(int keyID, string keyLabel, string hotKey, string actionLabel, string[] actionSet)
            {
                KeyID = keyID;
                KeyLabel = keyLabel;
                HotKey = hotKey;
                ActionLabel = actionLabel;
                ActionSet = actionSet;
            }

            private string[] keyMatrix = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            public string LuaMakeLine(int nthLine)
            {
                string luaOutcome = "";

                luaOutcome += (nthLine == 0) ? "    if     " : "    elseif ";
                
                // If no Hotkey was presset, create one
                if(HotKey == "default")
                {
                    HotKey = "^!" + keyMatrix[nthLine];
                }

                if(HotKey == "fast")
                {
                    luaOutcome += "button == " + KeyID + " then "
                        + "print('" + KeyLabel + ": " + ActionLabel + "') "
                        + "lmc_key_press('" + ActionSet[0] + "') \n";
                }
                else
                {
                    luaOutcome += "button == " + KeyID + " then "
                        + "print('" + KeyLabel + ": " + ActionLabel + "') "
                        + "lmc_key_press('" + HotKey + "') \n";
                }

                return luaOutcome;
            }

            public string AHKMakeLine(int nthLine)
            {
                // If no hotkey presset was used, create one
                if (HotKey == "default")
                {
                    HotKey = "^!" + keyMatrix[nthLine];
                }

                string ahkOutcome = HotKey + ":: ; " + ActionLabel + "\n";

                foreach(string line in ActionSet)
                {
                    ahkOutcome += line;
                }

                ahkOutcome += "Return \n\n";

                if (HotKey == "fast")
                {
                    ahkOutcome = "\n";
                }

                return ahkOutcome; 
            }
        }

        public class AHKactions
        {
            public string Click(int x, int y, string button) => "  Click, " + x + ", " + y + ", " + button + " \n";
            
            public string Keys(string item) => "  Send, " + item + " \n";

            public string KeysBin(string item) => item;

            public string Wait(int item) => "  Sleep, " + item + " \n";

            public string MouseMode(string item) => "  CoordMode, Mouse, " + item + " \n";

            public string MouseMove(int x, int y) => "  MouseMove, " + x + ", " + y + " \n";

            public string MouseDrag(string button, int x1, int y1, int x2, int y2) => "  MouseClickDrag, " + button + ", " + x1 + ", " + y1 + ", " + x2 + ", " + y2 + " \n";

            public string Open(string item) => "  Run, " + item + " \n";
        }
    }
}

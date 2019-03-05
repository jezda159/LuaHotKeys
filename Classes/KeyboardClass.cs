using System;

namespace LuaHotKey.Classes
{
    class Keyboard
    {
        private Setup setup = new Setup();
        private string name { get; set; }
        private string code { get; set; }

        private KeyPress[] keyPresses { get; set; }

        public string App { get; set; }
        public bool UseHeaderSearch { get; set; } = true;

        public string FilePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        private string fileName = "LHKPasser";

        /// <summary>
        /// Setup for single Keyboard nest
        /// </summary>
        /// <param name="app">Name of the keyboard setup</param>
        /// <param name="KeyPresses">Nest for KeyPresses</param>
        public Keyboard(string app, KeyPress[] KeyPresses)
        {
            name = setup.KeyboardID().Title;
            code = setup.KeyboardID().Code;
            App = app;
            keyPresses = KeyPresses;
        }

        /// <summary>
        /// Setup for single Keyboard nest
        /// </summary>
        /// <param name="app">Name of the keyboard setup</param>
        /// <param name="useHeaderSearch">Make scripts work only inside window, which header contains previous "app" string</param>
        /// <param name="KeyPresses">Nest for KeyPresses</param>
        public Keyboard(string app, bool useHeaderSearch, KeyPress[] KeyPresses)
        {
            name = setup.KeyboardID().Title;
            code = setup.KeyboardID().Code;
            App = app;
            UseHeaderSearch = useHeaderSearch;
            keyPresses = KeyPresses;
        }

        // Renders every LuaMacros line of code
        public string luaOutside()
        {
            string luaSet = "";

            foreach (KeyPress kp in keyPresses)
                luaSet += kp.luaInside();

            luaSet = removeLastCommaFrom(luaSet);

            return FillLua(luaSet);
        }

        // Renders whole AutoHotKeys script
        public string ahkOutside()
        {
            string ahkSet = "";

            foreach (KeyPress kp in keyPresses)
                ahkSet += kp.ahkInside();
            
            ahkSet = removeFirstElseIf(ahkSet);

            return FillAhk(ahkSet);
        }

        // Structure used to wrap the written scripts
        private string FillLua(string luaInside)
        {
            string o = "\n";
            string d = "\n\n";

            string outcome = "clear()" + o +
                "lmc_device_set_name('" + name + "', '" + code + "')" + o +
                "lmc_print_devices()" + d +

                "lmc_set_handler('" + name + "', function(button, direction)" + o +
                "  --print('button '..button.. ' in direction '..direction)" + d +

                "  local code = {" + o +
                luaInside +
                "  }" + d +

                "  if direction == 1 then" + d +

                "    print(button.. ' (' .. code[button] .. ') was pressed')" + d +

                "    local file = io.open('" + FilePath + "/" + fileName + ".txt', 'w')" + o +
                "    file:write(code[button])" + o +
                "    file:flush()" + o +
                "    file:close()" + d +

                "    lmc_send_keys('{F24}')" + d +

                "  end" + o +
                "end)";
            return outcome;
        }

        // Structure used to wrap the written scripts
        private string FillAhk(string ahkInside)
        {
            string o = "\n";
            string d = "\n\n";

            string outcome = "";

            if (UseHeaderSearch)
            {
                outcome = "SetTitleMatchMode, 2 ; look into any part of header" + o +
                    "SetTitleMatchMode, Fast" + o +
                    "#IfWinActive " + App + " " + d;
            }

            outcome +=
                "~F24::" + o +
                "  FileRead, code, " + FilePath + "/" + fileName + ".txt" + d +

                ahkInside +
                "Return" + d +

                "Tippy(tipsHere, wait:= 1000)" + o +
                "{" + o +
                "  ToolTip, %tipsHere% HK,,,8" + o +
                "  SetTimer, noTip, %wait%" + o +
                "}" + o +
                "noTip:" + o +
                "  ToolTip,,,,8" + o +
                "return" + d +

            "#IfWinActive";

            return outcome;
        }

        // Cleans up the generated LuaMacros script to make it work
        private string removeLastCommaFrom(string s)
        {
            int len = s.Length - 1;

            for (int x = 0; x < s[len]; x++)
            {
                string duo = $"{s[len - x]}{s[len - (x + 1)]}";
                if (duo == "--")
                {
                    s = s.Remove(len - (x + 3), 1);
                    break;
                }
            }

            return s;
        }

        // Cleans up the generated AutoHotKey script to make it work
        private string removeFirstElseIf(string s)
        {
            return "  If" + s.Remove(0, 9);
        }
    }
}

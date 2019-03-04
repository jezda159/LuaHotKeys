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
        public bool UseHeaderSearch { get; set; }

        public string FilePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        private string fileName = "LHKPasser";


        public Keyboard(string app, KeyPress[] KeyPresses)
        {
            name = setup.KeyboardID().Title;
            code = setup.KeyboardID().Code;
            App = app;
            UseHeaderSearch = true;
            keyPresses = KeyPresses;
        }

        public Keyboard(string app, bool useHeaderSearch, KeyPress[] KeyPresses)
        {
            name = setup.KeyboardID().Title;
            code = setup.KeyboardID().Code;
            App = app;
            UseHeaderSearch = useHeaderSearch;
            keyPresses = KeyPresses;
        }

        public string luaOutside()
        {
            string luaSet = "";

            foreach (KeyPress kp in keyPresses)
                luaSet += kp.luaInside();

            luaSet = RemoveLastCommaFrom(luaSet);

            return FillLua(luaSet);
        }

        public string ahkOutside()
        {
            string ahkSet = "";

            foreach (KeyPress kp in keyPresses)
                ahkSet += kp.ahkInside();

            //Remove "else if" from first line
            ahkSet = ahkSet.Remove(0, 9);
            ahkSet = "  If" + ahkSet;

            return FillAhk(ahkSet);
        }

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

                AddTippy +

                "#IfWinActive";

            return outcome;
        }

        private string RemoveLastCommaFrom(string s)
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

        private string AddTippy => "Tippy(tipsHere, wait:= 1000) \n" +
            "{ \n" +
            "ToolTip, %tipsHere% HK,,,8 \n" +
            "SetTimer, noTip, %wait% \n" +
            "} \n" +
            "noTip: \n" +
            "  ToolTip,,,,8 \n" +
            "return \n\n";
    }
}

using System;

namespace LuaHotKey.Classes
{
    class KeyPress
    {
        private string key { get; set; }
        private int code { get; set; }
        private string label { get; set; }
        private string[] ahkLines { get; set; }
        KeyList kl = new KeyList();

        public KeyPress(string Key, string Label, string[] AhkLines)
        {
            key = Key;
            label = Label;
            ahkLines = AhkLines;

            code = kl.Dic(Key);
        }

        public string luaInside()
        {
            return $"    [{code}] = '{code}', -- {key}: {label} \n";
        }

        public string ahkInside()
        {
            string outcome = "  else if(code = \"" + code + "\") ; " + key + ": " + label + "\n  {\n" + 
                "    tippy(\"" + label + "\") \n";

            foreach (string l in ahkLines)
                outcome += "  " + l;

            return outcome + "  }\n";
        }
    }
}

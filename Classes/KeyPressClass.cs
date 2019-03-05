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

        /// <summary>
        /// Setup for single KeyPress nest
        /// </summary>
        /// <param name="Key">Label od the coresponding key on your keyboard and inside KeyList</param>
        /// <param name="Label">Name your thing</param>
        /// <param name="AhkLines">Nest for AutoHotKey functions</param>
        public KeyPress(string Key, string Label, string[] AhkLines)
        {
            key = Key;
            label = Label;
            ahkLines = AhkLines;

            code = kl.Dic(Key);
        }

        // Structure of single KeyPress LuaMacros script
        public string luaInside()
        {
            return $"    [{code}] = '{code}', -- {key}: {label} \n";
        }

        // Structure of single KeyPress AutoHotKey script
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

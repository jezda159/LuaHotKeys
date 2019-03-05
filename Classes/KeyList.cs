using System;
using System.Collections.Generic;

namespace LuaHotKey.Classes
{
    class KeyList
    {
        public static Bictionary<int, string> keyList { get; set; }

        public KeyList()
        {
            /* These keys are setup for my specific usecase, use this Lua code:
clear()
lmc_device_set_name("your_keyboard", "12341234") 
lmc_print_devices()

lmc_set_handler("your_keyboard", function(btn, dir)
  print("button " .. btn .. " in direction " .. dir)
end
             * to find your preffered key codes. Every key can be labeled personaly,
             * i have labeled no-numerical keys on my numpad with alphabet to 
             * make it easier to use.
             */

            //Both-way-dictionary for keys
            keyList = new Bictionary<int, string>()
            {
                //Extra
                {144, "numlock"},
                {  9, "tab"},

                //Function keys
                { 32, "A"}, //space
                {110, "B"}, //dot
                { 13, "C"}, //enter

                {111, "X"}, //backslash
                {106, "Y"}, //asterisk
                {  8, "Z"}, //backspace
                
                {107, "+"},
                {109, "-"},

                //Numkeys
                { 96, "0"},
                { 97, "1"},
                { 98, "2"},
                { 99, "3"},
                {100, "4"},
                {101, "5"},
                {102, "6"},
                {103, "7"},
                {104, "8"},
                {105, "9"},

                //Numkeys without NumLock
                {45, "0nl"},
                {35, "1nl"},
                {40, "2nl"},
                {34, "3nl"},
                {37, "4nl"},
                {12, "5nl"},
                {39, "6nl"},
                {36, "7nl"},
                {38, "8nl"},
                {33, "9nl"}
            };
        }
        
        public string Dic(int v) => keyList[v];

        public int Dic(string v) => keyList[v];


        /* GENERIC KEYLIST FOR ARCHIVAL PURPOUSES
            keyList = new Bictionary<int, string>()
            {
                { 48, "000"},
                
                {  8, "back"},
                {  9, "tab"},
                { 13, "enter"},
                {106, "*"},
                {107, "+"},
                {109, "-"},
                {110, "."},
                {111, "/"},
                
                { 96, "0"},
                { 97, "1"},
                { 98, "2"},
                { 99, "3"},
                {100, "4"},
                {101, "5"},
                {102, "6"},
                {103, "7"},
                {104, "8"},
                {105, "9"},
                
                {45, "0nl"},
                {35, "1nl"},
                {40, "2nl"},
                {34, "3nl"},
                {37, "4nl"},
                {12, "5nl"},
                {39, "6nl"},
                {36, "7nl"},
                {38, "8nl"},
                {33, "9nl"}
            };
        */
    }
}

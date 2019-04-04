using System;
using System.IO;
using LuaHotKey.Classes;

namespace LuaHotKey
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup setup = new Setup();

            // Use the keyboard selected in setup 
            Keyboard selectedKeyboard = setup.KeyboardSet();
            
            // Present the generated scrpts in console
            Console.WriteLine($"{selectedKeyboard.luaOutside()} \n\n\n {selectedKeyboard.ahkOutside()}");

            // Ask to generate the comleted files
            Console.WriteLine($"\n\n'{selectedKeyboard.App}' was succesfully loaded.\nGenerate files? y/n");
            string buildIT = Console.ReadLine();
            
            if (buildIT == "y")
            {
                bool success = SaveFiles(selectedKeyboard);
                Console.WriteLine( (success) ? $"All probably went right, your files are in '{selectedKeyboard.FilePath}'" : "Files are NOT saved" );
            }
            else
            {
                Console.WriteLine("OK, no files were created");
            }

            Console.ReadLine();

            // An attempt to save files to desired path
            bool SaveFiles(Keyboard kb)
            {
                if (kb.App == "")
                    kb.App = "lhk-generated-file";
                
                string LuaFilePath = $"{kb.FilePath}/{kb.App}.lhk.lua";
                string AutoHotKeyFilePath = $"{kb.FilePath}/{kb.App}.lhk.ahk";

                File.WriteAllText(LuaFilePath, kb.luaOutside());
                if (File.Exists(LuaFilePath))
                {
                    File.WriteAllText(AutoHotKeyFilePath, kb.ahkOutside());
                    if (File.Exists(AutoHotKeyFilePath))
                    {
                        return true;
                    }
                }
                
                return false;
            }
        }
    }
}

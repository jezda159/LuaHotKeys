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
            Keyboard keyboard = setup.KeyboardSet();
            
            // Present the generated scrpts in console
            Console.WriteLine(keyboard.luaOutside() + "\n\n\n" + keyboard.ahkOutside());

            // Ask to generate the comleted files
            Console.WriteLine($"\n\n'{keyboard.App}' was succesfully loaded.\nGenerate files? y/n");
            string buildIT = Console.ReadLine();
            
            if (buildIT == "y")
            {
                bool success = SaveFiles(keyboard);
                Console.WriteLine( (success) ? "All probably went right, your files are in '" + keyboard.FilePath + "'" : "Files are NOT saved" );
            }
            else
            {
                Console.WriteLine("OK, no files were created");
            }

            Console.ReadLine();

            // An attempt to save files to desired path
            bool SaveFiles(Keyboard kb)
            {
                bool isDone = false;

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
                        isDone = true;
                    }
                }
                
                return isDone;
            }
        }
    }
}

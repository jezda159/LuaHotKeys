using System;

namespace LuaHotKey
{
    public class AHKActions
    {

        // Essentials, Tool

        public string Blank() => " \n";

        public string Raw(string line) => $"  {line} \n";

        public string Variable(string name, int value) => $"  {name} := {value} \n";
        public string Variable(string name, string value) => $"  {name} := {value} \n";

        public string Open(string item) => $"  Run, {item} \n";

        public string Wait(int milisec) => $"  Sleep, {milisec} \n";

        public string Alert(string text) => $"  MsgBox, {text} \n";
        public string Alert(string text, int seconds) => $"  MsgBox, , , {text}, {seconds} \n";
        public string Alert(string text, string seconds) => $"  MsgBox, , , {text}, {seconds} \n";

        public string Beep() => "  SoundBeep \n";
        public string Beep(int milisec) => $"  SoundBeep, , {milisec} \n";
        public string Beep(int pitch, int milisec) => $"  SoundBeep, {pitch}, {milisec} \n";

        public string Shutdown() => "  Shutdown, 1 \n";
        public string Restart() =>  "  Shutdown, 2 \n";
        public string Logoff() =>   "  Shutdown, 0 \n";


        // Keystrokes

        public string Keys(string keys) => $"  Send, {keys} \n";

        public string Keys(string[] keySet)
        {
            string output = "";
            bool stopper = true;

            foreach (var keys in keySet)
            {
                if (stopper)
                {
                    stopper = false;
                    output += Keys(keys);
                }
                else
                {
                    output += "  " + Keys(keys);
                }
            }

            return output;
        }
        
        public string KeyDown(string key) => "  Send, {" + key + " down} \n";

        public string KeyUp(string key) => "  Send, {" + key + " up} \n";


        // Mouse

        public string CoordMode(string target, string mode) => $"  CoordMode, {target}, {mode} \n";

        public string MouseMode(string mode) => $"  CoordMode, Mouse, {mode} \n";

        public string GetMousePosition(string outX, string outY) => $"  MouseGetPos, {outX}, {outY} \n";

        public string MouseSpeed(int speed) => $"  SetDefaultMouseSpeed, {speed} \n";

        public string MouseMove(int x, int y) =>        $"  MouseMove, {x}, {y} \n";
        public string MouseMove(string x, int y) =>     $"  MouseMove, {x}, {y} \n";
        public string MouseMove(int x, string y) =>     $"  MouseMove, {x}, {y} \n";
        public string MouseMove(string x, string y) =>  $"  MouseMove, {x}, {y} \n";

        public string Click(int x, int y, string button) => $"  Click, {x}, {y}, {button} \n";
        public string Click(string x, int y, string button) => $"  Click, {x}, {y}, {button} \n";
        public string Click(int x, string y, string button) => $"  Click, {x}, {y}, {button} \n";
        public string Click(string x, string y, string button) => $"  Click, {x}, {y}, {button} \n";

        public string MouseDrag(string button, int x1, int y1, int x2, int y2) =>           $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, int y1, int x2, int y2) =>        $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, string y1, int x2, int y2) =>     $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, string y1, string x2, int y2) =>  $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, string y1, string x2, string y2)=>$"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, string y1, string x2, string y2) =>  $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, int y1, string x2, string y2) =>     $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, int y1, int x2, string y2) =>        $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, string y1, string x2, int y2) =>     $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, int y1, int x2, string y2) =>     $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, string y1, int x2, int y2) =>        $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, int x1, int y1, string x2, int y2) =>        $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, int y1, string x2, string y2) =>  $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";
        public string MouseDrag(string button, string x1, string y1, int x2, string y2) =>  $"  MouseClickDrag, {button}, {x1}, {y1}, {x2}, {y2} \n";


        // Conditions
        
        public string If(string condition, string[] result)
        {
            string output = "  if (" + condition + ") \n    { \n";

            foreach (var expression in result)
                output += "    " + expression;

            return output + "    } \n";
        }

        public string If(string condition, string[] positive, string[] negative)
        {
            string output = "  if (" + condition + ") \n    { \n";

            foreach (var expression in positive)
                output += "    " + expression;

            output += "    } else { \n";

            foreach (var expression in negative)
                output += "    " + expression;

            return output + "    } \n";
        }


        // Pixel RGB stuff

        public string GetPixelRGB(string outputName, int posX, int posY) =>     $"  PixelGetColor, {outputName}, {posX}, {posY} \n";
        public string GetPixelRGB(string outputName, string posX, int posY) =>  $"  PixelGetColor, {outputName}, {posX}, {posY} \n";
        public string GetPixelRGB(string outputName, int posX, string posY) =>  $"  PixelGetColor, {outputName}, {posX}, {posY} \n";
        public string GetPixelRGB(string outputName, string posX, string posY)=>$"  PixelGetColor, {outputName}, {posX}, {posY} \n";
        
        public string GetPixelUnderMouse(string outputName) => GetMousePosition("FAEDRx", "FEARDy") + "  " + GetPixelRGB(outputName, "FAEDRx", "FEARDy");
        
        
        public string SearchPixelRGBInArea(string outX, string outY, int x1, int y1, int x2, int y2, string HEXrgb, int range, string[] negative, string[] positive)
        {
            string output = $"  PixelSearch, {outX}, {outY}, {x1}, {y1}, {x2}, {y2}, 0x{HEXrgb}, {range}, Fast \n" +
                "    if (ErrorLevel) \n    { \n";

            foreach (var expression in negative)
                output += "    " + expression;
            
            output += "} else { \n";

            foreach (var expression in positive)
                output += "    " + expression;

            return output + "    } \n";
        }

        public string SearchPixelRGBInArea(string outX, string outY, string x1, string y1, string x2, string y2, string HEXrgb, int range, string[] positive, string[] negative)
        {
            string output = $"  PixelSearch, {outX}, {outY}, {x1}, {y1}, {x2}, {y2}, 0x{HEXrgb}, {range}, Fast \n" +
                "    if (ErrorLevel) \n    { \n";

            foreach (var expression in negative)
                output += "    " + expression;

            output += "} else {" +
                " \n";

            foreach (var expression in positive)
                output += "    " + expression;

            return output + "    } \n";
        }
    }
}

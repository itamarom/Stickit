using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlLib
{
    public class KeyAction
    {
        public string Key { get; set; }
        public bool Down { get; set; }
       
        public KeyAction()
        {
            
        }

        public KeyAction(string key, bool down)
        {
            this.Key = key;
            this.Down = down;
        }

        public string ToXml()
        {
            return ("<Key>" + this.Key + "</Key>\n\t" +
                "<Down>" + this.Down.ToString().ToLower() + "</Down>");
        }

        public override string ToString()
        {
            return this.Key + "=>" + (this.Down ? "DOWN" : "UP");
        }
    }
}

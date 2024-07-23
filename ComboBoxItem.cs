using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAMREC
{
    public class ComboBoxItem
    {
        public ComboBoxItem(string Text, int Value) 
        {
            this.Text = Text;
            this.Value = Value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

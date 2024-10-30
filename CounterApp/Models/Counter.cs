using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Maui.Graphics.Color;

namespace CounterApp.Models
{
    public class Counter
    {
        public string Name { get; set; }
        public int InitialValue { get; set; }
        public int Value { get; set; }
        public Color CounterColor { get; set; }

        public Counter(string name, int initialValue, Color counterColor)
        {
            Name = name;
            InitialValue = initialValue;
            Value = initialValue;
            CounterColor = counterColor;
        }
    }
}

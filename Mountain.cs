using System;
using System.Collections.Generic;
using System.Text;

namespace Györffy_Dániel_Hegyek_WPF_11A
{
    class Mountain
    {
        public string Name { get; set; }
        public string MountainTeritory { get; set; } 
        public int Height { get; set; }

        public Mountain(string name, string mountainTeritory, int height)
        {
            Name = name;
            MountainTeritory = mountainTeritory;
            Height = height;
        }
    }
}

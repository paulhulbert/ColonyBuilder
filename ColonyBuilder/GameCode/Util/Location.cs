using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColonyBuilder.GameCode.Util
{
    class Location
    {
        private double x;
        private double y;

        public Location(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
    }
}

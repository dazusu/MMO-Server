using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMO.Entities;

namespace MMO
{
    public static class MathUtilities
    {
        public static double Distance(EntityLocation origin, EntityLocation dest)
        {
            double a = dest.X - origin.X;
            double b = dest.Y - origin.Y;
            return Math.Sqrt(a * a + b * b);
        }
    }
}

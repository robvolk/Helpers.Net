using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers.Math
{
    public static class MathExtensions
    {
        /// <summary>
        /// Computes the half-life for a number: Nt = N0*(1/2)^(t/t halflife)
        /// </summary
        public static double HalfLife(this int number, double time, double halfLife)
        {
            return HalfLife((double)number, time, halfLife);
        }

        /// <summary>
        /// Computes the half-life for a number: Nt = N0*(1/2)^(t/t halflife)
        /// </summary
        public static double HalfLife(this double number, double time, double halfLife)
        {
            // todo: unit test this puppy.  make sure the halflife is <= the original number.. never <= 0

            if (halfLife <= 0)
                throw new ArgumentOutOfRangeException("halfLife", "halfLife must be greater than zero");

            return number * System.Math.Pow(0.5, time / halfLife);
        }
    }
}

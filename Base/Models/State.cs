using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class State: BaseSignal
    {
        private static readonly double _max_range = 4095;
        public static readonly double _min_range = 256;
        public static readonly double _bad_range = _max_range + _min_range;
        public override double GetBadSignal(double tict)
        {
            return GetRandomNumber(_max_range, _bad_range);
        }

        public override double GetGoodSignal(double tict)
        {
            return GetRandomNumber(_min_range, _max_range);
        }

        public override bool Validation(double num, double min, double max)
        {
            return (num < max) && (num > min);
        }
    }
}

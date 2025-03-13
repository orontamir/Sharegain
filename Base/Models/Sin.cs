using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Sin : BaseSignal
    {
        public static readonly int _timeToRun = 3000;//Configuration
        public static readonly int _everyEsc = 2;//Configuration
        public static readonly double _sampleRate = _timeToRun * _everyEsc;//Configuration
        private static readonly double _amplitude = 32.0;//Configuration
        private static readonly double _frequency = 1000;//Configuration
        private static readonly double _bad_amplitude = _amplitude * 2;//Configuration

        public override double GetBadSignal(double tict)
        {
            return GetRandomNumber(_amplitude, _bad_amplitude);
        }

        public override double GetGoodSignal(double tict)
        {
            return ((_amplitude / 2.0 ) * (Math.Sin((2 * Math.PI * tict * _frequency) / _sampleRate )) ) + (_amplitude / 2.0);
        }

        public override bool Validation(double num, double min, double max)
        {
            return (num < max) && (num > min);
        }
    }
}

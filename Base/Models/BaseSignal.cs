using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public abstract class BaseSignal
    {
        public double Value { get; set; }
        public virtual double GetGoodSignal(double tict)
        {
            return tict;
        }
        public virtual double GetBadSignal(double tict)
        {
            return tict;
        }
        protected double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public virtual bool Validation(double num, double min, double max)
        {
            return false;
        }
    }
}

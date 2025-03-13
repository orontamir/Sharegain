using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Signal
    {
        public DateTime DateTime { get; set; }
        public long Time {  get; set; }
        public Sin Sin { get; set; }
        public State State { get; set; }
        public Signal() 
        {
            Sin = new Sin();
            State = new State();
        }
    }
}

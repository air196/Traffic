using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic
{
    class TimeElapsedException : Exception
    {
        public TimeElapsedException() : base("Автобус больше не на маршруте") { }
        public TimeElapsedException(string message) : base(message) { }
    }
}

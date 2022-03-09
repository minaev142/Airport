using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Plane
{
    internal enum StateEnum
    {
        InAir,
        Landing,
        Unloading,
        TakesOff,
        OutAir,

        WaitAfterLand
    }
}

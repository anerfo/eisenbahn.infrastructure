using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaProvider
{
    [Flags]
    public enum Options
    {
        None = 0,
        Parallel = 1,
        Restart = 2,
        StopAllOthers = 4
    }
}

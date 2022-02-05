using System;

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

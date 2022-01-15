using System;
using System.Collections.Generic;
using System.Text;


namespace TVRemote.Configuration
{

    public enum Mode
    {
        Production = 1,
        Development = 2
    }

    public static class Configuration
    {
        public static Mode mode = Mode.Production;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmRegistrator.Automation
{
    [Flags]
    public enum LogicErrorEnum
    {
        None=0,
        PortInOpenError=1,
        PortOutOpenError = 256,
    }
}

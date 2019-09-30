using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ArmRegistrator.MultiScreen
{
    [Obsolete]
    public class ScreenInformation
    {
        public static LinkedList<DeskScreen> GetAllScreens()
        {
            GetMonitorCount();
            return AllScreens;
        }
        public static int GetMonitorCount()
        {
            AllScreens.Clear();
            int monCount = 0;
            MonitorEnumProc callback = (IntPtr hDesktop, IntPtr hdc, ref ScreenRectangle prect, int d) =>
            {
                //Console.WriteLine("Left {0}", prect.left);
                //Console.WriteLine("Right {0}", prect.right);
                //Console.WriteLine("Top {0}", prect.top);
                //Console.WriteLine("Bottom {0}", prect.bottom);
                AllScreens.AddLast(new DeskScreen(prect));
                return ++monCount > 0;
            };

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0);
            //if (EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0))
            //    Console.WriteLine("You have {0} monitors", monCount);
            //else
            //    Console.WriteLine("An error occured while enumerating monitors");

            return monCount;
        }

        [DllImport("user32")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref ScreenRectangle pRectangle, int dwData);

        static readonly LinkedList<DeskScreen> AllScreens = new LinkedList<DeskScreen>();
        
    }
}

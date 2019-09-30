using System.Drawing;
using System.Runtime.InteropServices;

namespace ArmRegistrator.MultiScreen
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ScreenRectangle
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}

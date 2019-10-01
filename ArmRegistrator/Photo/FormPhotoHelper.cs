using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArmRegistrator.Photo
{
    class FormPhotoHelper
    {
        static FormPhotoHelper()
        {
            FormControls = GetDefaultControlParams();
        }

        public static void SetDefaultState(FormPhoto frm)
        {
            foreach (KeyValuePair<string, DefaultSizeAndLocation> pair in FormControls)
            {
                frm.Controls[pair.Key].Size = pair.Value.CtrlSize;
                frm.Controls[pair.Key].Location = pair.Value.CtrlLocation;
            }
        }

        public static void ScaleControls(Control.ControlCollection ctrls, Size newFormSize)
        {
            var scaler = ScalingIndex(newFormSize);
            foreach (Control control in ctrls)
            {
                control.Width = (int)(control.Width * scaler);
                control.Height = (int)(control.Height * scaler);
                control.Location = new Point((int)(control.Location.X * scaler), (int)(control.Location.Y * scaler));
            }
        }

        private static Dictionary<string,DefaultSizeAndLocation> GetDefaultControlParams()
        {
            var dict = new Dictionary<string,DefaultSizeAndLocation>();
            using (var frm = new FormPhoto())
            {
                foreach (Control control in frm.Controls)
                {
                    dict.Add(control.Name,new DefaultSizeAndLocation {CtrlSize = control.Size, CtrlLocation = control.Location});
                }
            }

            return dict;
        }

        private static Size FormSizeNormal
        {
            get
            {
                using (var frm = new FormPhoto())
                {
                    return frm.Size;
                }
            }
        }

        private static double ScalingIndex(Size newSize)
        {
            var oldSize = FormSizeNormal;

            var widthIndex = newSize.Width / (double)oldSize.Width;
            var heightIndex = newSize.Height / (double)oldSize.Height;

            return widthIndex < heightIndex ? widthIndex : heightIndex;
        }

        internal struct DefaultSizeAndLocation
        {
            public Size CtrlSize;
            public Point CtrlLocation;
        }

        private static readonly Dictionary<string, DefaultSizeAndLocation> FormControls;
    }
}

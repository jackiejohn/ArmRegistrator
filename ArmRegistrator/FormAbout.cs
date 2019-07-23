using System.Windows.Forms;

namespace ArmRegistrator
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }
        public FormAbout(string user, string sn):this()
        {
            labelUser.Text = user;
            labelSn.Text = sn;
        }
    }
}

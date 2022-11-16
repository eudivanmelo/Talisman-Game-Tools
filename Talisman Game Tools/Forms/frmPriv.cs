using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Talisman_Game_Tools.Data;

namespace Talisman_Game_Tools.Forms
{
    public partial class frmPriv : Form
    {
        private List<Priv_Cfg> priv_Cfgs = new List<Priv_Cfg>();

        public frmPriv()
        {
            InitializeComponent();
        }
    }
}

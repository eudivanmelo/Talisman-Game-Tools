using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private List<Priv_Cmd> priv_Cmds = new List<Priv_Cmd>();

        /// <summary>
        /// Campos importantes
        /// </summary>
        private string importantLine1;
        private string importantLine2;
        private string importantLine1_1;

        public frmPriv()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carregar arquivo priv_cfg.csv
        /// </summary>
        private void Load_PrivCfg()
        {
            priv_Cfgs.Clear(); // Limpar lista para evitar incrementos

            // Carregar dados dos arquivo CSV
            var path = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/priv_cfg.csv";

            if (File.Exists(path))
            {
                var priv_cfg = File.ReadAllLines(path); // Ler todas as linhas

                // Separar as duas primeiras linhas para retornar ao arquivo final
                importantLine1 = priv_cfg[0];
                importantLine2 = priv_cfg[1];

                // Ler linhas dos dados
                for(var i = 2; i < priv_cfg.Length; i++)
                {
                    var a = Program.SplitString(priv_cfg[i], ',', 5);

                    priv_Cfgs.Add(new Priv_Cfg(a));
                } // End for
            }
        }

        /// <summary>
        /// Carregar arquivo priv_cmd.csv
        /// </summary>
        private void Load_PrivCmd()
        {
            priv_Cmds.Clear(); // Limpar lista para evitar incrementos

            // Carregar dados dos arquivo CSV
            var path = $"{Properties.Settings.Default.lastFolder}/game_server/local/server/{Program.Local}/priv_cmd.csv";

            if (File.Exists(path))
            {
                var priv_cmd = File.ReadAllLines(path); // Ler todas as linhas

                // Separar as duas primeiras linhas para retornar ao arquivo final
                importantLine1_1 = priv_cmd[0];

                // Ler linhas dos dados
                for (var i = 1; i < priv_cmd.Length; i++)
                {
                    var a = Program.SplitString(priv_cmd[i], ',', 3);

                    priv_Cmds.Add(new Priv_Cmd(a));
                } // End for
            }
        }

        /// <summary>
        /// Ao carregar formulário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPriv_Load(object sender, EventArgs e)
        {
            // Carregar arquivos csv
            Load_PrivCfg();
            Load_PrivCmd();

            // Mostrar dados dos arquivos
            UpdateControls();
        }

        /// <summary>
        /// Atualizar dados dos constroles com base nos dados do arquivos
        /// </summary>
        private void UpdateControls()
        {
            // Carregar priv_cmd
            for(var i = 0; i < priv_Cmds.Count; i++)
            {
                // CMD's
                var lblCmd = this.Controls.Find($"lblCmd{i+1}", true);

                if (lblCmd != null)
                    lblCmd[0].Text = priv_Cmds[i].Cmd;

                // Desc's
                var txtDesc = this.Controls.Find($"txtDesc{i + 1}", true);

                if (txtDesc != null)
                    txtDesc[0].Text = priv_Cmds[i].Desc;

                // PV's
                var txtPriv = this.Controls.Find($"txtPriv{i + 1}", true);

                if (txtPriv != null)
                    txtPriv[0].Text = priv_Cmds[i].Priv.ToString();
            }

            // Carregar priv_cfg
            for(var j = 0; j < priv_Cfgs.Count; j++)
            {
                // ID's
                var txtID = this.Controls.Find($"txtID{j + 1}", true);

                if (txtID != null)
                    txtID[0].Text = priv_Cfgs[j].Id.ToString();

                // Prefix's
                var txtPrefix = this.Controls.Find($"txtPrefix{j + 1}", true);

                if (txtPrefix != null)
                    txtPrefix[0].Text = priv_Cfgs[j].PrefixName;

                // ChatLimit's
                var chkChatLimit = this.Controls.Find($"chkChatLimit{j + 1}", true);

                if (chkChatLimit != null)
                {
                    var chk = chkChatLimit[0] as CheckBox;
                    chk.Checked = priv_Cfgs[j].WorldChatLimit;
                }

                // ShowInClient's
                var chkVisible = this.Controls.Find($"chkVisible{j + 1}", true);

                if (chkVisible != null)
                {
                    var chk = chkVisible[0] as CheckBox;
                    chk.Checked = priv_Cfgs[j].ShowInClient;
                }

                // IsGM's
                var chkIsGM = this.Controls.Find($"chkIsGM{j + 1}", true);

                if (chkIsGM != null)
                {
                    var chk = chkIsGM[0] as CheckBox;
                    chk.Checked = priv_Cfgs[j].IsGM;
                }
            }
        }
    }
}

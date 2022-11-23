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
using static System.Windows.Forms.LinkLabel;

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
        private bool Edited;

        public frmPriv()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carregar arquivo priv_cfg.csv
        /// </summary>
        private bool Load_PrivCfg()
        {
            priv_Cfgs.Clear(); // Limpar lista para evitar incrementos

            try
            {
                // Carregar dados dos arquivo CSV
                var path = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/priv_cfg.csv";

                if (File.Exists(path))
                {
                    var priv_cfg = File.ReadAllLines(path); // Ler todas as linhas

                    // Separar as duas primeiras linhas para retornar ao arquivo final
                    importantLine1 = priv_cfg[0];
                    importantLine2 = priv_cfg[1];

                    // Ler linhas dos dados
                    for (var i = 2; i < priv_cfg.Length; i++)
                    {
                        var a = Program.SplitString(priv_cfg[i], ',', 5);

                        priv_Cfgs.Add(new Priv_Cfg(a));
                    } // End for

                    return true;
                }
                else return false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar arquivo priv_cfg.csv");
                return false;
            }
        }

        /// <summary>
        /// Carregar arquivo priv_cmd.csv
        /// </summary>
        private bool Load_PrivCmd()
        {
            priv_Cmds.Clear(); // Limpar lista para evitar incrementos

            try
            {
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

                    return true;
                }
                else return false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar arquivo priv_cmd.csv");
                return false;
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
            grpPriv_Cfg.Enabled = Load_PrivCfg();
            grpPriv_Cmd.Enabled = Load_PrivCmd();

            // Mostrar dados dos arquivos
            UpdateControls();

            Edited = false; // Iniciar sem edições
        }

        /// <summary>
        /// Atualizar dados dos constroles com base nos dados do arquivos
        /// </summary>
        private void UpdateControls()
        {
            // Carregar priv_cmd
            //Limpa os controles existentes
            tableLayoutPanel.Controls.Clear();

            //limpa os estilos das linhas e colunas
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowCount = priv_Cmds.Count;

            for (var i = 0; i < priv_Cmds.Count; i++)
            {
                // Adicionar linha
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));

                // Criar txtCMD
                TextBox txtCMD = new TextBox
                {
                    Name = $"txtCmd{i}",
                    Text = priv_Cmds[i].Cmd,
                    Margin = new Padding(1),
                    Dock = DockStyle.Fill,
                    Enabled = false
                };
                tableLayoutPanel.Controls.Add(txtCMD, 0, i);

                // Criar txtDesc
                TextBox txtDesc = new TextBox
                {
                    Name = $"txtDesc{i}",
                    Text = priv_Cmds[i].Desc,
                    Margin = new Padding(1),
                    Dock = DockStyle.Fill
                };
                txtDesc.TextChanged += new EventHandler(this.txtDesc_TextChanged);
                tableLayoutPanel.Controls.Add(txtDesc, 1, i);

                // Criar txtPV
                TextBox txtPriv = new TextBox
                {
                    Name = $"txtPriv{i}",
                    Text = priv_Cmds[i].Priv.ToString(),
                    Margin = new Padding(1),
                    Dock = DockStyle.Fill
                };
                txtPriv.TextChanged += new EventHandler(this.txtPriv_TextChanged);
                txtPriv.KeyPress += new KeyPressEventHandler(this.NumberOnly_KeyPress);
                tableLayoutPanel.Controls.Add(txtPriv, 2, i);
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

        /// <summary>
        /// Alterar descrição do priv_Cmds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            var Row = tableLayoutPanel.GetRow(sender as TextBox);
            if (Row >= 0 && Row <= priv_Cmds.Count)
            {
                priv_Cmds[Row].Desc = (sender as TextBox).Text;
                Edited = true;
            }
        }

        /// <summary>
        /// Alterar privilégios do priv_Cmds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriv_TextChanged(object sender, EventArgs e)
        {
            var Row = tableLayoutPanel.GetRow(sender as TextBox);
            if (Row >= 0 && Row <= priv_Cmds.Count)
                if (!string.IsNullOrEmpty((sender as TextBox).Text))
                {
                    priv_Cmds[Row].Priv = byte.Parse((sender as TextBox).Text);
                    Edited = true;
                }          
        }

        /// <summary>
        /// Alterar Id do priv_Cfgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtId_TextChanged(object sender, EventArgs e)
        {
            var Row = tablePriv_Cfg.GetRow(sender as TextBox);
            if (Row > 0 && Row <= priv_Cfgs.Count)
                if (!string.IsNullOrEmpty((sender as TextBox).Text))
                {
                    priv_Cfgs[Row - 1].Id = byte.Parse((sender as TextBox).Text);
                    Edited = true;
                }      
        }

        /// <summary>
        /// Alterar PrefixName do priv_Cfgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            var Row = tablePriv_Cfg.GetRow(sender as TextBox);
            if (Row > 0 && Row <= priv_Cfgs.Count)
            {
                priv_Cfgs[Row - 1].PrefixName = (sender as TextBox).Text;
                Edited = true;
            }     
        }

        /// <summary>
        /// Permitir apenas numeros no textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Alterar ChatLimit do priv_Cfgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkChatLimit_CheckedChanged(object sender, EventArgs e)
        {
            var Row = tablePriv_Cfg.GetRow(sender as CheckBox);
            if (Row > 0 && Row <= priv_Cfgs.Count)
            {
                priv_Cfgs[Row - 1].WorldChatLimit = (sender as CheckBox).Checked;
                Edited = true;
            }      
        }

        /// <summary>
        /// Alterar ShowInClient do priv_Cfgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            var Row = tablePriv_Cfg.GetRow(sender as CheckBox);
            if (Row > 0 && Row <= priv_Cfgs.Count)
            {
                priv_Cfgs[Row - 1].ShowInClient = (sender as CheckBox).Checked;
                Edited = true;
            }  
        }

        /// <summary>
        /// Alterar IsGM do priv_Cfgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsGM_CheckedChanged(object sender, EventArgs e)
        {
            var Row = tablePriv_Cfg.GetRow(sender as CheckBox);
            if (Row > 0 && Row <= priv_Cfgs.Count)
            {
                priv_Cfgs[Row - 1].IsGM = (sender as CheckBox).Checked;
                Edited = true;
            }
        }

        /// <summary>
        /// Ao pressionar no botão cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Recarregar arquivos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (Edited) { // Verificar se existe alguma edição no arquivo
                var Diag = MessageBox.Show("Todas as alterações não salvas serão perdidas. Tem certeza que deseja recarregar os arquivos?", "Recarregar Arquivos", MessageBoxButtons.YesNo);
                if (Diag == DialogResult.No)
                {
                    return;
                }
            }

            // Recarregar arquivos
            grpPriv_Cfg.Enabled = Load_PrivCfg();
            grpPriv_Cmd.Enabled = Load_PrivCmd();
            UpdateControls();
            Edited = false;
        }

        /// <summary>
        /// Salvar arquivos editados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Edited) // Verificar se houve alguma edição
            {
                // Arquivos do cliente
                var path_cfg_client = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/priv_cfg.csv";

                // Arquivo do servidor
                var path_cfg_server = $"{Properties.Settings.Default.lastFolder}/game_server/local/{Program.Local}/priv_cfg.csv";
                var path_cmd = $"{Properties.Settings.Default.lastFolder}/game_server/local/server/{Program.Local}/priv_cmd.csv";

                // Criar string com as informações que irão para o arquivo
                List<string> cfg_lines = new List<string>()
                { this.importantLine1, this.importantLine2 };
                List<string> cmd_lines = new List<string>()
                { this.importantLine1_1 };

                // Ler todos os priv_cfg's
                foreach (var priv_Cfg in priv_Cfgs)
                {
                    cfg_lines.Add(priv_Cfg.ToString());
                }

                // Ler todos os priv_cmd's
                foreach (var priv_Cmd in priv_Cmds)
                {
                    cmd_lines.Add(priv_Cmd.ToString());
                }

                File.WriteAllLines(path_cfg_server, cfg_lines.ToArray()); // Salvar no servidor
                File.WriteAllLines(path_cmd, cmd_lines.ToArray());

                File.WriteAllLines(path_cfg_client, cfg_lines.ToArray()); // Salvar no cliente

                Edited = false; // Zerar edição
            }
        }

        /// <summary>
        /// Ao fechar a janela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPriv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Edited)
            {
                var Diag = MessageBox.Show("Você possui edições não salvas, tem certeza que deseja fechar?", "Cancelar edição", MessageBoxButtons.YesNo);
                if (Diag == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

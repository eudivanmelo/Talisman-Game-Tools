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
using Talisman_Game_Tools.Forms;

namespace Talisman_Game_Tools
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Construtor do formulário principal
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            // Verificar se existe alguma pasta salva nas configurações do programa
            if (!string.IsNullOrEmpty(Properties.Settings.Default.lastFolder))
            {
                txtFolder.Text = Properties.Settings.Default.lastFolder;
                txtFolder.Enabled = false;
                btnFolder.Enabled = false;
            }
        }

        /// <summary>
        /// Ao alterar o texto do folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            // Verificar se é uma pasta válida
            if (Program.IsValidFolder(txtFolder.Text))
            {
                // Ultima pasta válida será salva
                Properties.Settings.Default.lastFolder = txtFolder.Text;
                Properties.Settings.Default.Save();

                // Carregar arquivo local_and_language.ini
                if (TryLoadLocalAndLanguage())
                {
                    // Habilitar grupos caso seja uma pasta válida
                    groupSettings.Enabled = true;
                }
                else
                    txtSettings.Text = "Não foi possível carregar o arquivo ( local_and_language.ini )," +
                                       "verifique se o mesmo existe na pasta local do game_client ou game_server.";
            }
            else{ // Caso não seja uma pasta válida, manter os grupos desabilitados
                groupTools.Enabled = false;
                groupSettings.Enabled = false;
            }
        }

        /// <summary>
        /// Bloquear alterações no caminho da pasta do projeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            txtFolder.Enabled = !txtFolder.Enabled;
            btnFolder.Enabled = txtFolder.Enabled;
        }

        /// <summary>
        /// Tentar carregar arquivo de configurações locais e linguagem
        /// </summary>
        private bool TryLoadLocalAndLanguage()
        {
            // Verificar se o arquivo existe no game_server
            if (File.Exists($"{txtFolder.Text}/game_server/local/local_and_language.ini"))
            {
                txtSettings.Text = File.ReadAllText($"{txtFolder.Text}/game_server/local/local_and_language.ini");
                return true;
            }

            // Verificar se o arquivo existe no game_server
            if (File.Exists($"{txtFolder.Text}/game_client/local/local_and_language.ini"))
            {
                txtSettings.Text = File.ReadAllText($"{txtFolder.Text}/game_client/local/local_and_language.ini");
                return true;
            }

            return false; // Caso não encontre nada retorna falso
        }

        /// <summary>
        /// Ao clicar no botão de aplicar, salvar arquivo INI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            // Salvar no cliente
            File.WriteAllText($"{txtFolder.Text}/game_client/local/local_and_language.ini", txtSettings.Text);

            // Salvar no servidor
            File.WriteAllText($"{txtFolder.Text}/game_server/local/local_and_language.ini", txtSettings.Text);
        }

        /// <summary>
        /// Ao clicar no botão folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolder_Click(object sender, EventArgs e)
        {
            // Abrir buscador de pastas
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Ao editar arquivo INI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSettings_TextChanged(object sender, EventArgs e)
        {
            foreach(var line in txtSettings.Lines)
            {
                var setting = line.Split('=');
                if (setting[0].Trim().ToLower() == "local")
                {
                    Program.Local = setting[1].Split(';')[0].Replace("\"", "").Trim();
                }else if(setting[0].Trim().ToLower() == "path")
                {
                    Program.Path = setting[1].Split(';')[0].Replace("\"", "").Trim();
                }
            }

            // Verificar se os diretórios baseados no arquivo ini é válido
            if (Directory.Exists($"{txtFolder.Text}/game_client/local/{Program.Local}"))
            {
                groupTools.Enabled = true;
            }else
                groupTools.Enabled = false;
        }

        /// <summary>
        /// Ao clicar no botão de abrir o frmItemSuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuits_Click(object sender, EventArgs e)
        {
            // Verificar se ester formulário já está aberto
            var frmItemSuit = Application.OpenForms["frmItemsuit"];
            if (frmItemSuit == null) // Verificar se é nulo, se sim criar um novo
                frmItemSuit = new frmItemsuit();
            
            // Verificar se o formulário está visível, de sim focar
            if (frmItemSuit.Visible) 
                frmItemSuit.Focus();
            else
                frmItemSuit.Show(); // Abrir formulário
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            // Verificar se ester formulário já está aberto
            var frmItem = Application.OpenForms["frmItem"];
            if (frmItem == null) // Verificar se é nulo, se sim criar um novo
                frmItem = new frmItem();

            // Verificar se o formulário está visível, de sim focar
            if (frmItem.Visible)
                frmItem.Focus();
            else
                frmItem.Show(); // Abrir formulário
        }
    }
}

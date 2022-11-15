using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Windows.Forms;
using Talisman_Game_Tools.Data;

namespace Talisman_Game_Tools.Forms
{
    public partial class frmItemsuit : Form
    {
        /// <summary>
        /// Lista de trajes carregados
        /// </summary>
        private readonly List<Suit> Suits = new List<Suit>();

        /// <summary>
        /// Linhas temporárias
        /// </summary>
        private string itemsuitLine1;
        private string itemsuitLine2;
        private string itemsuit_descLine1;
        private string itemsuit_descLine2;

        /// <summary>
        /// Construtor do formulário suits
        /// </summary>
        public frmItemsuit()
        {
            InitializeComponent();

            // Carregar arquivos ao iniciar o componentes
            LoadFiles();
        }

        /// <summary>
        /// Carregar arquivos
        /// </summary>
        private void LoadFiles()
        {
            Suits.Clear(); // Limpar lista para evitar incrementos

            // Carregar dados dos arquivo CSV
            var path_itemsuit = $"{Properties.Settings.Default.lastFolder}/game_client/local/common/table/itemsuit.csv";
            var path_itemsuit_desc = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/language/itemsuit_desc.csv";

            if (File.Exists(path_itemsuit) && File.Exists(path_itemsuit_desc))
            {
                var itemsuit = File.ReadAllLines(path_itemsuit); // Ler todas as linhas
                var itemsuit_desc = File.ReadAllLines(path_itemsuit_desc);

                // Separar as duas primeiras linhas para retornar ao arquivo final
                itemsuitLine1 = itemsuit[0];
                itemsuitLine2 = itemsuit[1];
                itemsuit_descLine1 = itemsuit_desc[0];
                itemsuit_descLine2 = itemsuit_desc[1];

                // Ler linhas dos dados
                foreach (var suit in itemsuit)
                {
                    var a = Program.SplitString(suit, ',', 18);

                    if (int.TryParse(a[0], out int itemsuit_id))
                    {
                        // Ler todos os dados do itemsuit_desc para encontrar a ID compatível
                        foreach (var suit_desc in itemsuit_desc)
                        {
                            var b = Program.SplitString(suit_desc, ',', 12);

                            if (int.TryParse(b[0], out int itemsuit_desc_id))
                            {
                                if (itemsuit_id == itemsuit_desc_id)
                                {
                                    Suits.Add(new Suit(a, b));
                                    break;
                                }
                            }
                        } // End foreach
                    }
                } // End foreach
            }
        }

        /// <summary>
        /// Ao clicar na lista de suits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSuits_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Selecionar item clicado
                listSuits.SelectedIndex = listSuits.IndexFromPoint(e.Location);

                // Verificar se é um item válido para abrir o menu
                if (listSuits.SelectedIndex != -1)
                {
                    contextMenuStrip.Show(MousePosition);
                }
            }
        }

        /// <summary>
        /// Ao clicar no botão novo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNew_Click(object sender, System.EventArgs e)
        {
            AddNewSuit_byId();
        }

        /// <summary>
        /// Ao clicar no botão deletar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, EventArgs e)
        {
            var msgResult = MessageBox.Show("Tem certeza de deseja excluir este traje?", "Deletar traje", MessageBoxButtons.YesNo);
            if (msgResult == DialogResult.Yes) // Caso o mesmo confirme
            {
                Suits.RemoveAt(SelectedIndex());
                UpdateList(); // Atualizar lista
            }
        }

        /// <summary>
        /// Duplicar um item selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuDuplicate_Click(object sender, EventArgs e)
        {
            var index = SelectedIndex(); // Selecionar suit
            if (index >= 0)
            {
                var copySuit = Suits[index].ToString_ItemSuit();
                var copySuitdesc = Suits[index].ToString_ItemSuit_Desc();

                var data = copySuit.Split(',');
                int id = 0; // Indice inicial

                // Procurar na lista de suits um ID disponível
                foreach (var suit in Suits)
                {
                    if (id < suit.Id)
                    {
                        id = suit.Id;
                    }
                }
                data[0] = (id +1).ToString(); // Setar o novo ID

                // Adicionar o novo traje após copiar os dados
                Suits.Add(new Suit(data, copySuitdesc.Split(',')));
                UpdateList(); // Atualizar lista

                // Selecionar item criado
                listSuits.SelectedIndex = Suits.Count - 1;
            }
        }

        /// <summary>
        /// Ao carregar formulário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmItemsuit_Load(object sender, System.EventArgs e)
        {
            // Carregar lista após carregar formulário
            UpdateList();
        }

        /// <summary>
        /// Ao pesquisar, atualizar lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            // Atualizar lista com base na pesquisa
            UpdateList();
        }

        #region Functions

        /// <summary>
        /// Atualizar lista
        /// </summary>
        private void UpdateList()
        {
            listSuits.Items.Clear(); // Limpar lista atual
            // Verificar se existe algum traje carregado na lista
            if (Suits.Count == 0)
                return;
            
            // Salvar ultimo indice selecionado
            var Index = listSuits.SelectedIndex;

            ListBox listBox = new ListBox();
            // Buscar item pelo nome
            foreach (var suit in Suits)
            {
                if (suit.Name.Contains(txtSearch.Text))
                {
                    listBox.Items.Add($"{suit.Id}: {suit.Name}");
                }
            }

            if (listBox.Items.Count > 0) // Verificar se foi encontrado algum item
            {
                listSuits.Items.AddRange(listBox.Items);
            }
            else
            {
                foreach (var suit in Suits)
                {
                    listSuits.Items.Add($"{suit.Id}: {suit.Name}");
                }
            }

            // Retornar indice seleciona
            if (Index >= 0 && Index < listSuits.Items.Count)
                listSuits.SelectedIndex = Index;
        }

        /// <summary>
        /// Adicionar um novo item limpo
        /// </summary>
        private void AddNewSuit_byId()
        {
            int id = 0; // Indice inicial

            // Procurar na lista de suits um ID disponível
            foreach (var suit in Suits)
            {
                if (id < suit.Id)
                {
                    id = suit.Id;
                }
            }

            Suits.Add(new Suit(id + 1));
            UpdateList(); // Atualizar lista

            // Selecionar item criado
            listSuits.SelectedIndex = Suits.Count - 1;
        }

        /// <summary>
        /// Obter indice do item selecionado
        /// </summary>
        /// <returns></returns>
        private int SelectedIndex()
        {
            if (listSuits.SelectedItems.Count > 0)
            {
                var Id = listSuits.SelectedItem.ToString().Split(':')[0];

                if (int.TryParse(Id, out int Result))
                {
                    foreach (var suit in Suits)
                    {
                        if (suit.Id == Result)
                        {
                            return Suits.IndexOf(suit);
                        }
                    }
                }
            }

            return -1;
        }

        #endregion

        /// <summary>
        /// Ao alterar indice mostra dados do item selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSuits_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Index = SelectedIndex();

            if (Index >= 0)
            {
                // itemsuit.csv
                tabControl.Enabled = true; // Habilitar editor
                txtID.Text = Suits[Index].Id.ToString();
                txtName.Text = Suits[Index].Name;
                txtItems.Text = Suits[Index].Items;
                txtIcon.Text = Suits[Index].Icon;
                txtAppearence.Text = Suits[Index].AppearenceEffect;
                txtEffect.Text = Suits[Index].EffectLink;
                txtAppearenceWoman.Text = Suits[Index].AppearenceEffectWoman;
                txtEffectWoman.Text = Suits[Index].EffectLinkWoman;
                txtAction1.Text = Suits[Index].Actions[0];
                txtAction2.Text = Suits[Index].Actions[1];
                txtAction3.Text = Suits[Index].Actions[2];
                txtAction4.Text = Suits[Index].Actions[3];
                txtAction5.Text = Suits[Index].Actions[4];
                txtAction6.Text = Suits[Index].Actions[5];
                txtAction7.Text = Suits[Index].Actions[6];
                txtAction8.Text = Suits[Index].Actions[7];
                txtAction9.Text = Suits[Index].Actions[8];
                txtAction10.Text = Suits[Index].Actions[9];

                // itemsuit_desc.csv
                txtActionHint1.Text = Suits[Index].ActionsDesc[0];
                txtActionHint2.Text = Suits[Index].ActionsDesc[1];
                txtActionHint3.Text = Suits[Index].ActionsDesc[2];
                txtActionHint4.Text = Suits[Index].ActionsDesc[3];
                txtActionHint5.Text = Suits[Index].ActionsDesc[4];
                txtActionHint6.Text = Suits[Index].ActionsDesc[5];
                txtActionHint7.Text = Suits[Index].ActionsDesc[6];
                txtActionHint8.Text = Suits[Index].ActionsDesc[7];
                txtActionHint9.Text = Suits[Index].ActionsDesc[8];
                txtActionHint10.Text = Suits[Index].ActionsDesc[9];
            }
            else
            {
                ClearControls(); // Limpar todos os controles
                tabControl.Enabled = false;
            }    
        }

        /// <summary>
        /// Limpar todos os controles criados
        /// </summary>
        private void ClearControls()
        {
            txtID.Text = string.Empty;
        }


        #region itemsuit.csv Validated

        /// <summary>
        /// Validar ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtID_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            if (int.TryParse(txtID.Text, out int Result))
            {
                // Verificar se o ID é válido
                foreach (var suit in Suits)
                {
                    if (suit.Id == Result && suit != Suits[index])
                    {
                        MessageBox.Show("ID inválido, não é possível ter um traje com o mesmo ID.");
                        txtID.Text = Suits[index].Id.ToString();
                        return;
                    }
                }
            
                Suits[index].Id = Result;

                UpdateList(); // Atualizar lista
            }
        }

        /// <summary>
        /// Validar nome do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;
            
            Suits[index].Name = txtName.Text;
            UpdateList(); // Atualizar lista
        }

        /// <summary>
        /// Validar items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItems_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Items = txtItems.Text;
        }

        /// <summary>
        /// Validar Ação 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction1_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[0] = txtAction1.Text;
        }

        /// <summary>
        /// Validar Ação 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[1] = txtAction2.Text;
        }

        /// <summary>
        /// Validar Ação 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction3_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[2] = txtAction3.Text;
        }

        /// <summary>
        /// Validar Ação 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction4_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[3] = txtAction4.Text;
        }

        /// <summary>
        /// Validar Ação 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction5_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[4] = txtAction5.Text;
        }

        /// <summary>
        /// Validar Ação 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction6_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[5] = txtAction6.Text;
        }

        /// <summary>
        /// Validar Ação 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction7_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[6] = txtAction7.Text;
        }

        /// <summary>
        /// Validar Ação 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction8_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[7] = txtAction8.Text;
        }

        /// <summary>
        /// Validar Ação 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction9_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[8] = txtAction9.Text;
        }

        /// <summary>
        /// Validar Ação 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAction10_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Actions[9] = txtAction10.Text;
        }

        /// <summary>
        /// Validar alteração do icone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIcon_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].Icon = txtIcon.Text;
        }

        /// <summary>
        /// Validar aparencia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAppearence_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].AppearenceEffect = txtAppearence.Text;
        }

        /// <summary>
        /// Validar link do efeito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEffect_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].EffectLink = txtEffect.Text;
        }

        /// <summary>
        /// Validar aparencia Woman
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAppearenceWoman_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].AppearenceEffectWoman = txtAppearenceWoman.Text;
        }

        /// <summary>
        /// Validar efeito woman
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEffectWoman_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].EffectLinkWoman = txtEffectWoman.Text;
        }

        #endregion

        #region itemsuit_desc.csv Validated

        /// <summary>
        /// Validar descrição do efeito 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint1_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[0] = txtActionHint1.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[1] = txtActionHint2.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint3_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[2] = txtActionHint3.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint4_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[3] = txtActionHint4.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint5_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[4] = txtActionHint5.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint6_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[5] = txtActionHint6.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint7_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[6] = txtActionHint7.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint8_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[7] = txtActionHint8.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint9_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[8] = txtActionHint9.Text;
        }

        /// <summary>
        /// Validar descrição do efeito 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtActionHint10_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Suits.Count)
                return;

            Suits[index].ActionsDesc[9] = txtActionHint10.Text;
        }

        #endregion

        /// <summary>
        /// Ao clicar no link direcionar ao forum de origem do projeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.aldeiatalisman.com.br");
        }

        /// <summary>
        /// Recarregar arquivo para acabar com as edições
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            var _title = "Recarregar Arquivos";
            var _message = "Todas as suas edições não salvas serão perdidas! Tem certeza que deseja recarregar os arquivos?";

            if (MessageBox.Show(_message, _title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LoadFiles(); // Recarregar arquivos
                UpdateList(); // Atualiza lista

                listSuits.SelectedIndex = -1; // Resetar também seleção
                ClearControls(); // Limpar todos os controles
                tabControl.Enabled = false;
            }
        }

        /// <summary>
        /// Ao clicar no botão de cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var _title = "Cancelar Edição";
            var _message = "Todas as suas edições não salvas serão perdidas! Tem certeza que deseja cancelar a edição?";

            if (MessageBox.Show(_message, _title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Quando o editor estiver sendo fechado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmItemsuit_FormClosing(object sender, FormClosingEventArgs e)
        {
            var _title = "Fechar editor";
            var _message = "Todas as suas edições não salvas serão perdidas! Tem certeza que deseja encerrar a edição?";

            if (MessageBox.Show(_message, _title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }else
                e.Cancel = true;
        }

        /// <summary>
        /// Salvar edição nos arquivos CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Arquivos do cliente
            var path_itemsuit_client = $"{Properties.Settings.Default.lastFolder}/game_client/local/common/table/itemsuit.csv";
            var path_itemsuit_desc_client = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/language/itemsuit_desc.csv";

            // Arquivo do servidor
            var path_itemsuit_server = $"{Properties.Settings.Default.lastFolder}/game_server/local/common/table/itemsuit.csv";
            var path_itemsuit_desc_server = $"{Properties.Settings.Default.lastFolder}/game_server/local/{Program.Local}/language/itemsuit_desc.csv";


            // Criar string com as informações que irão para o arquivo
            List<string> itemsuit = new List<string>()
                { this.itemsuitLine1, this.itemsuitLine2 };
            List<string> itemsuit_desc = new List<string>()
                { this.itemsuit_descLine1, this.itemsuit_descLine2 };

            // Ler todos os trajes
            foreach (var suit in Suits)
            {
                itemsuit.Add(suit.ToString_ItemSuit());
                itemsuit_desc.Add(suit.ToString_ItemSuit_Desc());
            }

            File.WriteAllLines(path_itemsuit_server, itemsuit.ToArray()); // Salvar no servidor
            File.WriteAllLines(path_itemsuit_desc_server, itemsuit_desc.ToArray());

            File.WriteAllLines(path_itemsuit_client, itemsuit.ToArray()); // Salvar no cliente
            File.WriteAllLines(path_itemsuit_desc_client, itemsuit_desc.ToArray());
        }
    }
}

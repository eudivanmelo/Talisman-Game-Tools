using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Windows.Forms;
using Talisman_Game_Tools.Data;
using Pfim;
using Talisman_Game_Tools.Properties;
using System.Linq;

namespace Talisman_Game_Tools.Forms
{
    public partial class frmItem : Form
    {
        /// <summary>
        /// Lista de trajes carregados
        /// </summary>
        private readonly List<Item> Items = new List<Item>();

        /// <summary>
        /// Linhas temporárias
        /// </summary>
        private string itemLine1;
        private string itemLine2;
        private string itemLine3;
        private string itemLine4;
        private string item_descLine1;

        /// <summary>
        /// Construtor do formulário suits
        /// </summary>
        public frmItem()
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
            Items.Clear(); // Limpar lista para evitar incrementos

            // Carregar dados dos arquivo CSV
            var path_item = $"{Properties.Settings.Default.lastFolder}/game_client/local/common/table/item.csv";
            var path_item_desc = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/language/item_desc.csv";

            if (File.Exists(path_item) && File.Exists(path_item_desc))
            {
                var items = File.ReadAllLines(path_item); // Ler todas as linhas
                var item_descs = File.ReadAllLines(path_item_desc);

                // Separar as duas primeiras linhas para retornar ao arquivo final
                itemLine1 = items[0];
                itemLine2 = items[1];
                itemLine3 = items[2];
                itemLine4 = items[3];

                item_descLine1 = item_descs[0];

                // Ler linhas dos dados
                foreach (var item in items)
                {
                    var a = Program.SplitString(item, ',', 134);

                    if (int.TryParse(a[0], out int item_id))
                    {
                        // Ler todos os dados do itemsuit_desc para encontrar a ID compatível
                        foreach (var item_desc in item_descs)
                        {
                            var b = Program.SplitString(item_desc, ',', 4);

                            if (int.TryParse(b[0], out int item_desc_id))
                            {
                                if (item_id == item_desc_id)
                                {
                                    Items.Add(new Item(a, b));
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
                listItems.SelectedIndex = listItems.IndexFromPoint(e.Location);

                // Verificar se é um item válido para abrir o menu
                if (listItems.SelectedIndex != -1)
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
                Items.RemoveAt(SelectedIndex());
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
                var copy = Items[index].ToString();
                var copyDesc = Items[index].ToString_Desc();

                var data = Program.SplitString(copy, ',', 134);
                int id = 0; // Indice inicial

                // Procurar na lista de suits um ID disponível
                foreach (var item in Items)
                {
                    if (id < item.Id)
                    {
                        id = item.Id;
                    }
                }
                data[0] = (id +1).ToString(); // Setar o novo ID

                // Adicionar o novo traje após copiar os dados
                Items.Add(new Item(data, copyDesc.Split(',')));
                UpdateList(); // Atualizar lista

                // Selecionar item criado
                listItems.SelectedIndex = listItems.Items.Count - 1;
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
            listItems.Items.Clear(); // Limpar lista atual
            // Verificar se existe algum traje carregado na lista
            if (Items.Count == 0)
                return;
            
            // Salvar ultimo indice selecionado
            var Index = listItems.SelectedIndex;

            ListBox listBox = new ListBox();
            // Buscar item pelo nome
            foreach (var item in Items)
            {
                if (item.Name.Contains(txtSearch.Text))
                {
                    listBox.Items.Add($"{item.Id}: {item.Name}");
                }
            }

            if (listBox.Items.Count > 0) // Verificar se foi encontrado algum item
            {
                listItems.Items.AddRange(listBox.Items);
            }
            else
            {
                foreach (var item in Items)
                {
                    listItems.Items.Add($"{item.Id}: {item.Name}");
                }
            }

            // Retornar indice seleciona
            if (Index >= 0 && Index < listItems.Items.Count)
                listItems.SelectedIndex = Index;
        }

        /// <summary>
        /// Adicionar um novo item limpo
        /// </summary>
        private void AddNewSuit_byId()
        {
            int id = 0; // Indice inicial

            // Procurar na lista de suits um ID disponível
            foreach (var item in Items)
            {
                if (id < item.Id)
                {
                    id = item.Id;
                }
            }

            Items.Add(new Item(id + 1));
            UpdateList(); // Atualizar lista

            // Selecionar item criado
            txtSearch.Text = "";
            listItems.SelectedIndex = listItems.Items.Count - 1;
        }

        /// <summary>
        /// Obter indice do item selecionado
        /// </summary>
        /// <returns></returns>
        private int SelectedIndex()
        {
            if (listItems.SelectedItems.Count > 0)
            {
                var Id = listItems.SelectedItem.ToString().Split(':')[0];

                if (int.TryParse(Id, out int Result))
                {
                    foreach (var item in Items)
                    {
                        if (item.Id == Result)
                        {
                            return Items.IndexOf(item);
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Limpar todos os controles criados
        /// </summary>
        private void ClearControls()
        {
            foreach (var control in this.Controls.OfType<TextBox>())
            {
                control.Text = string.Empty;
            }
        }

        /// <summary>
        /// Procurar tipo do item, com base no combobox
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        private byte GetTypeIndex(byte Index)
        {
            byte i = 0;

            foreach (var item in cmbType.Items)
            {
                var typeindex = item.ToString().Split(':')[0];

                if (byte.Parse(typeindex) == Index)
                    break;

                i++;
            }

            return i;
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
                // item.csv
                tabControl.Enabled = true; // Habilitar editor

                // TextBox's
                txtID.Text = Items[Index].Id.ToString();
                txtName.Text = Items[Index].Name;
                txtRegion.Text = Items[Index].Region;
                txtRemark.Text = Items[Index].Remark;
                txtDesc.Text = Items[Index].Description;
                txtLevel.Text = Items[Index].Level.ToString();
                txtMaxLevel.Text = Items[Index].MaxLevel.ToString();
                txtReqLevel.Text = Items[Index].ReqLevel.ToString();
                txtGodLevel.Text = Items[Index].ReqPrivilege.ToString();
                txtDamageLevel.Text = Items[Index].DamageLevel.ToString();
                txtDefaultAttrib.Text = Items[Index].DefaultAttrib.ToString();
                txtDefaultAttribNum.Text = Items[Index].DefaultAttibNum.ToString();
                txtFactor.Text = Items[Index].Factor;
                txtDecomposeId.Text = Items[Index].DecomposeId.ToString();
                txtInitCount.Text = Items[Index].InitCount.ToString();
                txtMaxCount.Text = Items[Index].MaxCount.ToString();
                txtLimitTime.Text = Items[Index].TimeLimit.ToString();
                txtValue.Text = Items[Index].Value.ToString();
                txtCardPoints.Text = Items[Index].CardPoints.ToString();
                txtMaxHardiness.Text = Items[Index].MaxHardiness.ToString();
                txtUseCondition.Text = Items[Index].UseCondition;
                txtEquipCondition.Text = Items[Index].EquipCondition;
                txtPickAction.Text = Items[Index].PickAction;
                txtLevel_TableUse.Text = Items[Index].Level_TableUse.ToString();
                txtEquipAction.Text = Items[Index].EquipAction;
                txtBindBuff.Text = Items[Index].BindBuff.ToString();
                txtEquipPhyArmor.Text = Items[Index].EquipPhyArmor;
                txtEquipArm_Fire.Text = Items[Index].EquipeArm_Fire;
                txtEquipArm_Wator.Text = Items[Index].EquipArm_Wator;
                txtEquipArm_Poison.Text = Items[Index].EquipArm_Poison;
                txtEquipArm_Light.Text = Items[Index].EquipArm_Light;
                txtEquipArm_Fo.Text = Items[Index].EquipArm_Fo;
                txtTargetType.Text = Items[Index].TargetType.ToString();
                txtEquipDamageAbsorb.Text = Items[Index].EquipDamageAbsorb;
                txtEquipDps.Text = Items[Index].EquipDps;
                txtEquipParry.Text = Items[Index].EquipParry;
                txtInitValue.Text = Items[Index].InitValue;
                txtGemmyLevel.Text = Items[Index].GemmyLevel.ToString();
                txtGemmyType.Text = Items[Index].GemmyType;
                txtPreStartPose.Text = Items[Index].PreStartPose;
                txtPrePose.Text = Items[Index].PrePose;
                txtEndPose.Text = Items[Index].EndPose;
                txtOtherParam.Text = Items[Index].OtherParam;
                txtOtherParam2.Text = Items[Index].OtherParam2;
                txtOtherParam3.Text = Items[Index].OtherParam3;
                txtItemUseMessage.Text = Items[Index].ItemUseMessage;
                txtTrumpSkills.Text = Items[Index].TrumpSkills;
                txtTrumpFinalSkills.Text = Items[Index].TrumpFinalSkills;
                txtTrumpAttackSkill.Text = Items[Index].TrumpAttackSkill.ToString();
                txtTrumpTalents.Text = Items[Index].TrumpTalents;
                txtBenefit.Text = Items[Index].Benefit.ToString();
                txtSpecial.Text = Items[Index].Special.ToString();
                txtTargetCount.Text = Items[Index].TargetCount.ToString();
                txtTargetCondition.Text = Items[Index].TargetCondition;
                txtTargetRadius.Text = Items[Index].TargetRadius.ToString();
                txtTargetRange.Text = Items[Index].TargetRange.ToString();
                txtPreTime.Text = Items[Index].PreTime.ToString();
                txtBeforeLoadingTmpSelf.Text = Items[Index].BeforeLoadingTmpSelf.ToString();
                txtBeforeLoadingTmpTarget.Text = Items[Index].BeforeLoadingTmpTarget.ToString();
                txtBeforeLoadingSelf.Text = Items[Index].BeforeLoadingSelf;
                txtBeforeLoadingTarget.Text = Items[Index].BeforeLoadingTarget;
                txtLoadingTime.Text = Items[Index].LoadingTime.ToString();
                txtLoadingCount.Text = Items[Index].LoadingCount.ToString();

                txtIcon.Text = Items[Index].Icon;
                txtModel.Text = Items[Index].Model;
                txtMode2.Text = Items[Index].Model_Blue;
                txtModel3.Text = Items[Index].Model_Golden;
                txtModel4.Text = Items[Index].Model_10;

                // Checkbox's
                chkIsTest.Checked = Items[Index].IsTestItem;
                chkNoDBID.Checked = Items[Index].NoDBID;
                chkSuit.Checked = Items[Index].Suit;
                chkPKDropLock.Checked = Items[Index].PkDropLock;
                chkCanTrade.Checked = Items[Index].CanTrade;
                chkPointShop.Checked = Items[Index].PointShop;
                chkCanShop.Checked = Items[Index].CanShop;
                chkCanBank.Checked = Items[Index].CanBank;
                chkCanDrop.Checked = Items[Index].CanDrop;
                chkCanDelete.Checked = Items[Index].CanDelete;
                chkCanSmithing.Checked = Items[Index].CanSmithing;
                chkCountable.Checked = Items[Index].Countable;
                chkConsume.Checked = Items[Index].Consume;
                chkTaskItem.Checked = Items[Index].TaskItem;
                chkSingle.Checked = Items[Index].Single;
                chkBothHands.Checked = Items[Index].BothHand;
                chkDungeonOnly.Checked = Items[Index].DungeonOnly;

                // ComboBox's
                cmbParty.SelectedIndex = Items[Index].Party;
                cmbColor.SelectedIndex = Items[Index].DefaultColor;
                cmbUseType.SelectedIndex = Items[Index].UseType;
                cmbWeaponType.SelectedIndex = Items[Index].WeaponType;
                cmbReleaseType.SelectedIndex = Items[Index].ReleaseType;
                cmbType.SelectedIndex = GetTypeIndex(Items[Index].Type);
            }
            else
            {
                ClearControls(); // Limpar todos os controles
                tabControl.Enabled = false;
            }    
        }

        /// <summary>
        /// Ao clicar no link direcionar ao forum de origem do projeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://aldeiatalisman.forumeiros.com/");
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

                listItems.SelectedIndex = -1; // Resetar também seleção
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
            this.Close();
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
            var path_item_client = $"{Properties.Settings.Default.lastFolder}/game_client/local/common/table/item.csv";
            var path_item_desc_client = $"{Properties.Settings.Default.lastFolder}/game_client/local/{Program.Local}/language/item_desc.csv";

            // Arquivo do servidor
            var path_item_server = $"{Properties.Settings.Default.lastFolder}/game_server/local/common/table/item.csv";
            var path_item_desc_server = $"{Properties.Settings.Default.lastFolder}/game_server/local/{Program.Local}/language/item_desc.csv";


            // Criar string com as informações que irão para o arquivo
            List<string> items = new List<string>()
                { this.itemLine1, this.itemLine2, this.itemLine3, this.itemLine4 };
            List<string> item_descs = new List<string>()
                { this.item_descLine1 };

            // Ler todos os trajes
            foreach (var item in Items)
            {
                items.Add(item.ToString());
                item_descs.Add(item.ToString_Desc());
            }

            File.WriteAllLines(path_item_server, items.ToArray()); // Salvar no servidor
            File.WriteAllLines(path_item_desc_server, item_descs.ToArray());

            File.WriteAllLines(path_item_client, items.ToArray()); // Salvar no cliente
            File.WriteAllLines(path_item_desc_client, item_descs.ToArray());
        }

        /// <summary>
        /// Ao alterar texto do icone, atualizar imagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIcon_TextChanged(object sender, EventArgs e)
        {
            // Definir local do arquivo
            string FilePath = $"{Properties.Settings.Default.lastFolder}\\game_client\\ui\\textures\\{txtIcon.Text}.dds";
            IImage image = Pfimage.FromFile("noicon.dds");

            if (File.Exists(FilePath)) // Verificar se o arquivo existe
                image = Pfimage.FromFile(FilePath);

            // Carregar imagem e atualizar a pre-visualização
            var data = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            picIcon.Image = new Bitmap(32, 32, image.Stride, PixelFormat.Format32bppArgb, data);
        }

        /// <summary>
        /// Ordenar items por ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuOrder_Click(object sender, EventArgs e)
        {
            Items.Sort(delegate (Item a, Item b)
            {
                return a.Id.CompareTo(b.Id);
            });

            UpdateList(); // Atualizar lista
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

        #region CheckedChanged

        /// <summary>
        /// Alterar valor bPath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPath_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Path = chkPath.Checked;
        }

        /// <summary>
        /// Alterar valor bNotCheckAttState
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNotCheckAttState_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].bNotCheckAttState = chkNotCheckAttState.Checked;
        }

        /// <summary>
        /// Alterar valor bCache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCache_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].bcache = chkCache.Checked;
        }

        /// <summary>
        /// Alterar valor do bPrePoseDrawWeap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPrePoseDrawWeap_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].bPrePoseDrawInWeapon = chkPrePoseDrawWeap.Checked;
        }

        /// <summary>
        /// Alterar valor bDungeonOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDungeonOnly_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].DungeonOnly = chkDungeonOnly.Checked;
        }

        /// <summary>
        /// Alterar valor isTestItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsTest_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].IsTestItem = chkIsTest.Checked;
        }

        /// <summary>
        /// Alterar valor bCountable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCountable_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Countable = chkCountable.Checked;
        }

        /// <summary>
        /// Alterar valor bConsume
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkConsume_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Consume = chkConsume.Checked;
        }

        /// <summary>
        /// Alterar valor bTaskItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTaskItem_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].TaskItem = chkTaskItem.Checked;
        }

        /// <summary>
        /// Alterar valor bSingle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSingle_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Single = chkSingle.Checked;
        }

        /// <summary>
        /// Alterar valor bBothHands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBothHands_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].BothHand = chkBothHands.Checked;
        }

        /// <summary>
        /// Alterar valor bNoDBID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNoBID_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].NoDBID = chkNoDBID.Checked;
        }

        /// <summary>
        /// Alterar valor m_bSuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSuit_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Suit = chkSuit.Checked;
        }

        /// <summary>
        /// Alterar valor PKDropLock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPKDropLock_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].PkDropLock = chkPKDropLock.Checked;
        }

        /// <summary>
        /// Alterar valor bCanTrade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanTrade_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanTrade = chkCanTrade.Checked;
        }

        /// <summary>
        /// Alterar valor bPointShop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPointShop_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].PointShop = chkPointShop.Checked;
        }

        /// <summary>
        /// Alterar valor bCanBank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanBank_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanBank = chkCanBank.Checked;
        }

        /// <summary>
        /// Alterar valor bCanShop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanShop_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanShop = chkCanShop.Checked;
        }

        /// <summary>
        /// Alterar valor bCanDrop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanDrop_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanDrop = chkCanDrop.Checked;
        }

        /// <summary>
        /// Alterar valor bCanDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanDelete_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanDelete = chkCanDelete.Checked;
        }

        /// <summary>
        /// Alterar valor do bCanSmithing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCanSmithing_CheckedChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CanSmithing = chkCanSmithing.Checked;
        }

        #endregion

        /// <summary>
        /// Validar ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtID_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtID.Text, out int Result))
            {
                // Verificar se o ID é válido
                foreach (var item in Items)
                {
                    if (item.Id == Result && item != Items[index])
                    {
                        MessageBox.Show("ID inválido, não é possível ter um traje com o mesmo ID.");
                        txtID.Text = Items[index].Id.ToString();
                        return;
                    }
                }

                Items[index].Id = Result;

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
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Name = txtName.Text;
            UpdateList(); // Atualizar lista
        }

        /// <summary>
        /// Validar Icone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIcon_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Icon = txtIcon.Text;
        }

        /// <summary>
        /// Alterar valor da região
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRegion_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Region = txtRegion.Text;
        }

        /// <summary>
        /// Alterar valor item_desc/remark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRemark_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Remark = txtRemark.Text;
        }

        /// <summary>
        /// Validar descrição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDesc_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Description = txtDesc.Text;
        }

        /// <summary>
        /// Validar level máximo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaxLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtMaxLevel.Text, out int value))
                Items[index].MaxLevel = value;
        }

        /// <summary>
        /// Validar level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtLevel.Text, out int value))
                Items[index].Level = value;
        }

        /// <summary>
        /// Validar level requerido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReqLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtReqLevel.Text, out int value))
                Items[index].ReqLevel = value;
        }

        /// <summary>
        /// Validar privilégio requerido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGodLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (byte.TryParse(txtGodLevel.Text, out byte value))
                if (value < 10)
                    Items[index].ReqPrivilege = value;
                else{
                    MessageBox.Show("Maximum privilege limit is 9, do not exceed the limit!");
                    txtGodLevel.Text = Items[index].ReqPrivilege.ToString();
                }      
        }

        /// <summary>
        /// Validar level de dano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtDamageLevel.Text, out int value))
                Items[index].DamageLevel = value;
        }
    }
}

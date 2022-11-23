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
                txtLoadingTmpSelf.Text = Items[Index].LoadingTmpSelf;
                txtLoadingTmpTarget.Text= Items[Index].LoadingTmpTarget;
                txtLoadingSelf.Text = Items[Index].LoadingSelf;
                txtLoadingTarget.Text = Items[Index].LoadingTarget;
                txtCDTime.Text = Items[Index].CDTime.ToString();
                txtPublicCDTime.Text = Items[Index].PublicCDTime.ToString();
                txtTypeCDId.Text = Items[Index].TypeCDId.ToString();
                txtForbiddenTime.Text = Items[Index].ForbiddenTime.ToString();
                txtPoseTime.Text = Items[Index].PoseTime.ToString();
                txtCriticalRate.Text = Items[Index].CriticalRate;
                txtAdditionShowParam.Text = Items[Index].AddtionShowParam;
                txtEffRate.Text = Items[Index].EffRate;
                txtNoDamage.Text = Items[Index].NoDamage;
                txtDmgType.Text = Items[Index].DmgType;
                txtRealDmgTime.Text = Items[Index].RealDmgTime;
                txtDmgMinMax.Text = Items[Index].DamageMinMax;
                txtGraphParam.Text = Items[Index].graph_Param2;
                txtIcon.Text = Items[Index].Icon;
                txtModel.Text = Items[Index].Model;
                txtMode2.Text = Items[Index].Model_Blue;
                txtModel3.Text = Items[Index].Model_Golden;
                txtModel4.Text = Items[Index].Model_10;
                txtScale.Text = Items[Index].Scale_Value;
                txtModel_2.Text = Items[Index].Model2;
                txtModel2_2.Text = Items[Index].Model_Blue2;
                txtModel3_2.Text = Items[Index].Model_Golden2;
                txtModel4_2.Text = Items[Index].Model2_10;
                txtWeaponLink.Text = Items[Index].weaponLink;
                txtItemRideEffect.Text = Items[Index].ItemRiderEffect;
                txtItemRideEffectLink.Text = Items[Index].ItemRiderEffectLink;
                txtWeapon1DrawInLink.Text = Items[Index].weapon1DrawInLink;
                txtWeaponLink2.Text = Items[Index].weaponLink2;
                txtWeapon1DrawInLink2.Text = Items[Index].weapon1DrawInLink2;
                txtSound.Text = Items[Index].Sound;
                txtGraph_HitEff.Text = Items[Index].graph_HitEff;
                txtGraph_HitEffLink.Text = Items[Index].graph_HitEffLink;
                txtGraph_SceneEff.Text = Items[Index].graph_SceneEff;

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

        #region Validated

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

        /// <summary>
        /// Validar atributos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDefaultAttrib_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtDefaultAttrib.Text, out int value))
                Items[index].DefaultAttrib = value;
        }

        /// <summary>
        /// Validar numero de atributos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDefaultAttribNum_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtDefaultAttribNum.Text, out int value))
                Items[index].DefaultAttibNum = value;
        }

        /// <summary>
        /// Validar alteração do Factor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFactor_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Factor = txtFactor.Text;
        }

        /// <summary>
        /// Validar DecomposeID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDecomposeId_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtDecomposeId.Text, out int value))
                Items[index].DecomposeId = value;
        }

        /// <summary>
        /// Validar quantidade inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInitCount_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtInitCount.Text, out int value))
                Items[index].InitCount = value;
        }

        /// <summary>
        /// Validar quantidade máxima
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaxCount_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtMaxCount.Text, out int value))
                Items[index].MaxCount = value;
        }

        /// <summary>
        /// Validar limite máximo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLimitTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtLimitTime.Text, out int value))
                Items[index].TimeLimit = value;
        }

        /// <summary>
        /// Validar valor do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtValue.Text, out int value))
                Items[index].Value = value;
        }

        /// <summary>
        /// Validar valor do item em cardpoints
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardPoints_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtCardPoints.Text, out int value))
                Items[index].CardPoints = value;
        }

        /// <summary>
        /// Validar resistência máxima
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaxHardiness_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtMaxHardiness.Text, out int value))
                Items[index].MaxHardiness = value;
        }

        /// <summary>
        /// Validar condição de uso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUseCondition_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].UseCondition = txtUseCondition.Text;
        }

        /// <summary>
        /// Validar condição para equipar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipCondition_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipCondition = txtEquipCondition.Text;
        }

        /// <summary>
        /// Validar ação ao equipar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPickAction_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].PickAction = txtPickAction.Text;
        }

        /// <summary>
        /// Validar Level_TableUse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLevel_TableUse_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtLevel_TableUse.Text, out int value))
                Items[index].Level_TableUse = value;
        }

        /// <summary>
        /// Validar Ação ao equipar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipAction_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipAction = txtEquipAction.Text;
        }

        /// <summary>
        /// Validar buff do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBindBuff_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtBindBuff.Text, out int value))
                Items[index].BindBuff = value;
        }

        /// <summary>
        /// Validar defesa fisica do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipPhyArmor_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipPhyArmor = txtEquipPhyArmor.Text;
        }

        /// <summary>
        /// Validar defesa ao fogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipArm_Fire_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipeArm_Fire = txtEquipArm_Fire.Text;
        }

        /// <summary>
        /// Validar defesa a água
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipArm_Wator_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipArm_Wator = txtEquipArm_Wator.Text;
        }

        /// <summary>
        /// Validar defesa ao veneno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipArm_Poison_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipArm_Poison = txtEquipArm_Poison.Text;
        }

        /// <summary>
        /// Validar defesa a luz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipArm_Light_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipArm_Light = txtEquipArm_Light.Text;
        }

        /// <summary>
        /// Validar defesa ao buddha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipArm_Fo_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipArm_Fo = txtEquipArm_Fo.Text;
        }

        /// <summary>
        /// Validar absorção de dano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipDamageAbsorb_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipDamageAbsorb = txtEquipDamageAbsorb.Text;
        }

        /// <summary>
        /// Validar dando por segundo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipDps_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipDps = txtEquipDps.Text;
        }

        /// <summary>
        /// Validar chance de desvio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipParry_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EquipParry = txtEquipParry.Text;
        }

        /// <summary>
        /// Validar valor inicial do efeito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInitValue_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].InitValue = txtInitValue.Text;
        }

        /// <summary>
        /// Validar GemmyLevel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGemmyLevel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtGemmyLevel.Text, out int value))
                Items[index].GemmyLevel = value;
        }

        /// <summary>
        /// Validar GemmyType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGemmyType_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].GemmyType = txtGemmyType.Text;
        }

        /// <summary>
        /// Validar pose ao iniciar uso do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPreStartPose_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].PreStartPose = txtPreStartPose.Text;
        }

        /// <summary>
        /// Validar pose enquanto carrega o item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrePose_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].PrePose = txtPrePose.Text;
        }

        /// <summary>
        /// Validar pose ao finalizar carregamento do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEndPose_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EndPose = txtEndPose.Text;
        }

        /// <summary>
        /// Validar outros paramentros 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOtherParam_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].OtherParam = txtOtherParam.Text;
        }

        /// <summary>
        /// Validar outros paramentros 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOtherParam2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].OtherParam2 = txtOtherParam2.Text;
        }

        /// <summary>
        /// Validar outros parametros 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOtherParam3_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].OtherParam3 = txtOtherParam3.Text;
        }

        /// <summary>
        /// Validar mensagem ao usar item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemUseMessage_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].ItemUseMessage = txtItemUseMessage.Text;
        }

        /// <summary>
        /// Validar habilidades vinculadas ao item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTrumpSkills_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].TrumpSkills = txtTrumpSkills.Text;
        }

        /// <summary>
        /// Validar habilidades finais vinculadas ao item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTrumpFinalSkills_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].TrumpFinalSkills = txtTrumpFinalSkills.Text;
        }

        /// <summary>
        /// Validar talentos do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTrumpTalents_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].TrumpTalents = txtTrumpTalents.Text;
        }

        /// <summary>
        /// Validar Attack Skill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTrumpAttackSkill_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtTrumpAttackSkill.Text, out int value))
                Items[index].TrumpAttackSkill = value;
        }

        /// <summary>
        /// Validar Benefit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBenefit_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (byte.TryParse(txtBenefit.Text, out byte value))
                Items[index].Benefit = value;
        }

        /// <summary>
        /// Validar Special
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSpecial_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtSpecial.Text, out int value))
                Items[index].Special = value;
        }

        /// <summary>
        /// Validar tipo de alvo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetType_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (byte.TryParse(txtTargetType.Text, out byte value))
                Items[index].TargetType = value;
        }

        /// <summary>
        /// Validar numero de alvos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetCount_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (byte.TryParse(txtTargetCount.Text, out byte value))
                Items[index].TargetCount = value;
        }

        /// <summary>
        /// Validar Alcance do alvo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetRange_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtTargetRange.Text, out int value))
                Items[index].TargetRange = value;
        }

        /// <summary>
        /// Validar condição do alvo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetCondition_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].TargetCondition = txtTargetCondition.Text;
        }

        /// <summary>
        /// Validar raio de alcance do efeito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetRadius_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtTargetRadius.Text, out int value))
                Items[index].TargetRadius = value;
        }

        /// <summary>
        /// Validar preTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPreTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtPreTime.Text, out int value))
                Items[index].PreTime = value;
        }

        /// <summary>
        /// Validar BeforeLoadingTmpSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBeforeLoadingTmpSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].BeforeLoadingTmpSelf = txtBeforeLoadingTmpSelf.Text;
        }

        /// <summary>
        /// Validar BeforeLoadingTmpTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBeforeLoadingTmpTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].BeforeLoadingTmpTarget = txtBeforeLoadingTmpTarget.Text;
        }

        /// <summary>
        /// Validar BeforeLoadingSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBeforeLoadingSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].BeforeLoadingSelf = txtBeforeLoadingSelf.Text;
        }

        /// <summary>
        /// Validar BeforeLoadingTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBeforeLoadingTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].BeforeLoadingTarget = txtBeforeLoadingTarget.Text;
        }

        /// <summary>
        /// Validar tempo de carregamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtLoadingTime.Text, out int value))
                Items[index].LoadingTime = value;
        }

        /// <summary>
        /// Validar quantidade de carregamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingCount_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtLoadingCount.Text, out int value))
                Items[index].LoadingCount = value;
        }

        /// <summary>
        /// Validar LoadingTmpSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingTmpSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LoadingTmpSelf = txtLoadingTmpSelf.Text;
        }

        /// <summary>
        /// Validar LoadingTmpTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingTmpTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LoadingTmpTarget = txtLoadingTmpTarget.Text;
        }

        /// <summary>
        /// Validar LoadingSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LoadingSelf = txtLoadingSelf.Text;
        }

        /// <summary>
        /// Validar LoadingTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadingTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LoadingTarget = txtLoadingTarget.Text;
        }

        /// <summary>
        /// Validar tempo de resfriamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCDTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtCDTime.Text, out int value))
                Items[index].CDTime = value;
        }

        /// <summary>
        /// Validar tempo de resfriamento publico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPublicCDTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtPublicCDTime.Text, out int value))
                Items[index].PublicCDTime = value;
        }

        /// <summary>
        /// Validar Tipo de resfriamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTypeCDId_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (byte.TryParse(txtTypeCDId.Text, out byte value))
                Items[index].TypeCDId = value;
        }

        /// <summary>
        /// Validar forbiddenTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtForbiddenTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtForbiddenTime.Text, out int value))
                Items[index].ForbiddenTime = value;
        }

        /// <summary>
        /// Validar PoseTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPoseTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            if (int.TryParse(txtPoseTime.Text, out int value))
                Items[index].PoseTime = value;
        }

        /// <summary>
        /// Validar Chance de Critico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCriticalRate_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].CriticalRate = txtCriticalRate.Text;
        }

        /// <summary>
        /// Validar AdditionShowParam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAdditionShowParam_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].AddtionShowParam = txtAdditionShowParam.Text;
        }

        /// <summary>
        /// Validar EffRate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEffRate_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].EffRate = txtEffRate.Text;
        }

        /// <summary>
        /// Validar NoDamage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoDamage_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].NoDamage = txtNoDamage.Text;
        }

        /// <summary>
        /// Validar DmgType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDmgType_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].DmgType = txtDmgType.Text;
        }

        /// <summary>
        /// Validar RealDmgTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRealDmgTime_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].RealDmgTime = txtRealDmgTime.Text;
        }

        /// <summary>
        /// Validar DmgMinMax
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDmgMinMax_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].DamageMinMax = txtDmgMinMax.Text;
        }

        /// <summary>
        /// Validar WeaponLink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeaponLink_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].weaponLink = txtWeaponLink.Text;
        }

        /// <summary>
        /// Validar ItemRideEffect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemRideEffect_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].ItemRiderEffect = txtItemRideEffect.Text;
        }

        /// <summary>
        /// Validar ItemRideEffectLink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItemRideEffectLink_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].ItemRiderEffectLink = txtItemRideEffectLink.Text;
        }

        /// <summary>
        /// Validar Weapon1DrawInLink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeapon1DrawInLink_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].weapon1DrawInLink = txtWeapon1DrawInLink.Text;
        }

        /// <summary>
        /// Validar WeaponLink2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeaponLink2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].weaponLink2 = txtWeaponLink2.Text;
        }

        /// <summary>
        /// Validar Weapon1DrawInLink2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeapon1DrawInLink2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].weapon1DrawInLink2 = txtWeapon1DrawInLink2.Text;
        }

        /// <summary>
        /// Validar LaunchTmpSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLaunchTmpSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LaunchTmpSelf = txtLaunchTmpSelf.Text;
        }

        /// <summary>
        /// Validar LaunchTmpTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLaunchTmpTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LaunchTmpTarget = txtLaunchTmpTarget.Text;
        }

        /// <summary>
        /// Validar LaunchSelf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLaunchSelf_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LaunchSelf = txtLaunchSelf.Text;
        }

        /// <summary>
        /// Validar LaunchTarget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLaunchTarget_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].LaunchTarget = txtLaunchTarget.Text;
        }

        /// <summary>
        /// Validar Parametro Grafico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGraphParam_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].graph_Param2 = txtGraphParam.Text;
        }

        /// <summary>
        /// Validar Escala do modelo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScale_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Scale_Value = txtScale.Text;
        }

        /// <summary>
        /// Validar modelo 3d padrão
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model = txtModel.Text;
        }

        /// <summary>
        /// Validar modelo 3d +4++
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMode2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model_Blue = txtMode2.Text;
        }

        /// <summary>
        /// Validar modelo 3d +7++
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel3_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model_Golden = txtModel3.Text;
        }

        /// <summary>
        /// Validar modelo 3d +10++
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel4_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model_10 = txtModel4.Text;
        }

        /// <summary>
        /// Validar modelo 3d padrão v2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel_2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model2 = txtModel_2.Text;
        }

        /// <summary>
        /// Validar modelo 3d +4++ v2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel2_2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model_Blue2 = txtModel2_2.Text;
        }

        /// <summary>
        /// Validar modelo 3d +7++ v2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel3_2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model_Golden2 = txtModel3_2.Text;
        }

        /// <summary>
        /// Validar modelo 3d +10++ v2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModel4_2_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Model2_10 = txtModel4_2.Text;
        }

        /// <summary>
        /// Validar som
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSound_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Sound = txtSound.Text;
        }

        /// <summary>
        /// Validar Graph_SceneEff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGraph_SceneEff_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].graph_SceneEff = txtGraph_SceneEff.Text;
        }

        /// <summary>
        /// Validar Graph_HitEff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGraph_HitEff_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].graph_HitEff = txtGraph_HitEff.Text;
        }

        /// <summary>
        /// Validar Graph_HitEffLink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGraph_HitEffLink_Validated(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].graph_HitEffLink = txtGraph_HitEffLink.Text;
        }



        #endregion

        /// <summary>
        /// Alterar releaseType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbReleaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].ReleaseType = (byte)cmbReleaseType.SelectedIndex;
        }

        /// <summary>
        /// Alterar tipo do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            var type = byte.Parse(cmbType.SelectedItem.ToString().Split(':')[0]);
            Items[index].Type = type;
        }

        /// <summary>
        /// Alterar cor padrão
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].DefaultColor = (byte)cmbColor.SelectedIndex;
        }

        /// <summary>
        /// Alterar classe permitida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].Party = (byte)cmbParty.SelectedIndex;
        }

        /// <summary>
        /// Alterar tipo de uso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].UseType = (byte)cmbUseType.SelectedIndex;
        }

        /// <summary>
        /// Alterar Tipo de Arma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeaponType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tratativa de erro
            var index = SelectedIndex();
            if (index < 0 || index >= Items.Count)
                return;

            Items[index].WeaponType = (byte)cmbWeaponType.SelectedIndex;
        }

        /// <summary>
        /// Organizar por ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items.Sort(delegate (Item a, Item b)
            {
                return a.Id.CompareTo(b.Id);
            });

            UpdateList(); // Atualizar lista
        }

        /// <summary>
        /// Organizar por tipo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items.Sort(delegate (Item a, Item b)
            {
                return a.Type.CompareTo(b.Type);
            });

            UpdateList(); // Atualizar lista
        }
    }
}

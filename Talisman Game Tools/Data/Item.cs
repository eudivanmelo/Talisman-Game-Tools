using System;
using System.Text;

namespace Talisman_Game_Tools.Data
{
    internal class Item
    {
        /// <summary>
        /// Onde ficará armezando os dados do item
        /// </summary>
        private readonly string[] Data;

        /// <summary>
        /// Identificador do item dentro do jogo
        /// [0]
        /// </summary>
        public int Id {
            get
            {
                return int.Parse(Data[0]);
            }
            set
            {
                Data[0] = value.ToString();
            }
        }

        /// <summary>
        /// Nome do item
        /// [1]
        /// </summary>
        public string Name
        {
            get{ return Data[1]; }
            set { Data[1] = value; }
        }

        /// <summary>
        /// Descrição do item
        /// [item_desc.csv]
        /// </summary>
        public string Description;

        /// <summary>
        /// Remark
        /// [item_desc.csv]
        /// </summary>
        public string Remark;

        /// <summary>
        /// Região ao qual o item foi criado
        /// [2]
        /// </summary>
        public string Region
        {
            get { 
                if (Data[2] == null)
                    return string.Empty;
                else
                    return Data[2]; 
            }
            set { 
                if (value == null)
                    Data[2] = string.Empty;
                else
                    Data[2] = value; 
            }
        }

        /// <summary>
        /// Este é um item de teste?
        /// [3]
        /// </summary>
        public bool IsTestItem
        {
            get
            {
                if (string.IsNullOrEmpty(Data[3]))
                    return false;
                else if (Data[3].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[3] = "1";
                else
                    Data[3] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo do item
        /// [4]
        /// </summary>
        public byte Type
        {
            get
            {
                if (string.IsNullOrEmpty(Data[4]))
                    return 0;
                else if (byte.TryParse(Data[4], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[4] = value.ToString();
                else
                    Data[4] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo de arma
        /// [5]
        /// </summary>
        public byte WeaponType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[5]))
                    return 0;
                else if (byte.TryParse(Data[5], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[5] = value.ToString();
                else
                    Data[5] = string.Empty;
            }
        }

        /// <summary>
        /// Classe ao qual o item pertence
        /// [6]
        /// </summary>
        public byte Party
        {
            get
            {
                if (string.IsNullOrEmpty(Data[6]))
                    return 5;
                else if (byte.TryParse(Data[6], out byte result))
                    return result;
                else
                    return 5;
            }
            set
            {
                Data[6] = value.ToString();
            }
        }

        /// <summary>
        /// Level inicial do item
        /// [7]
        /// </summary>
        public int Level
        {
            get
            {
                if (string.IsNullOrEmpty(Data[7]))
                    return 0;
                else if (int.TryParse(Data[7], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                Data[7] = value.ToString();
            }
        }

        /// <summary>
        /// Level máximo do item
        /// [8]
        /// </summary>
        public int MaxLevel
        {
            get
            {
                if (string.IsNullOrEmpty(Data[8]))
                    return 0;
                else if (int.TryParse(Data[8], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[8] = value.ToString();
                else
                    Data[8] = string.Empty;
            }
        }

        /// <summary>
        /// Level necessário para usar
        /// [9]
        /// </summary>
        public int ReqLevel
        {
            get
            {
                if (string.IsNullOrEmpty(Data[9]))
                    return 0;
                else if (int.TryParse(Data[9], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[9] = value.ToString();
                else
                    Data[9] = string.Empty;
            }
        }

        /// <summary>
        /// Privilégio necessário para usar
        /// [10]
        /// </summary>
        public byte ReqPrivilege
        {
            get
            {
                if (string.IsNullOrEmpty(Data[10]))
                    return 0;
                else if (byte.TryParse(Data[10], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[10] = value.ToString();
                else
                    Data[10] = string.Empty;
            }
        }

        /// <summary>
        /// Dano por level
        /// [11]
        /// </summary>
        public int DamageLevel
        {
            get
            {
                if (string.IsNullOrEmpty(Data[11]))
                    return 0;
                else if (int.TryParse(Data[11], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[11] = value.ToString();
                else
                    Data[11] = string.Empty;
            }
        }

        /// <summary>
        /// Cor padrão do item
        /// [12]
        /// </summary>
        public byte DefaultColor
        {
            get
            {
                if (string.IsNullOrEmpty(Data[12]))
                    return 0;
                else if (byte.TryParse(Data[12], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[12] = value.ToString();
                else
                    Data[12] = string.Empty;
            }
        }

        /// <summary>
        /// Atributo padrão com base na planilha de atributos
        /// [13]
        /// </summary>
        public int DefaultAttrib
        {
            get
            {
                if (string.IsNullOrEmpty(Data[13]))
                    return 0;
                else if (int.TryParse(Data[13], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[13] = value.ToString();
                else
                    Data[13] = string.Empty;
            }
        }

        /// <summary>
        /// Numeros de atributos padrão
        /// [14]
        /// </summary>
        public int DefaultAttibNum
        {
            get
            {
                if (string.IsNullOrEmpty(Data[14]))
                    return 0;
                else if (int.TryParse(Data[14], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[14] = value.ToString();
                else
                    Data[14] = string.Empty;
            }
        }

        /// <summary>
        /// Fator
        /// [15]
        /// </summary>
        public string Factor
        {
            get
            {
                if (Data[15] == null)
                    return string.Empty;
                else
                    return Data[15];
            }
            set
            {
                if (value == null)
                    Data[15] = string.Empty;
                else
                    Data[15] = value;
            }
        }

        /// <summary>
        /// Identificador da decomposição
        /// [16]
        /// </summary>
        public int DecomposeId
        {
            get
            {
                if (string.IsNullOrEmpty(Data[16]))
                    return 0;
                else if (int.TryParse(Data[16], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[16] = value.ToString();
                else
                    Data[16] = string.Empty;
            }
        }

        /// <summary>
        /// NoDBID
        /// [17]
        /// </summary>
        public bool NoDBID
        {
            get
            {
                if (string.IsNullOrEmpty(Data[17]))
                    return false;
                else if (Data[17].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[17] = "1";
                else
                    Data[17] = string.Empty;
            }
        }

        /// <summary>
        /// Identificador do traje ao qual o item pertence
        /// [18]
        /// </summary>
        public bool Suit
        {
            get
            {
                if (string.IsNullOrEmpty(Data[18]))
                    return false;
                else if (Data[18].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[18] = "1";
                else
                    Data[18] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser perdido caso o jogador morra estando como o PlayerKiller
        /// [19]
        /// </summary>
        public bool PkDropLock
        {
            get
            {
                if (string.IsNullOrEmpty(Data[19]))
                    return false;
                else if (Data[19].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[19] = "1";
                else
                    Data[19] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser negociado?
        /// [20]
        /// </summary>
        public bool CanTrade
        {
            get
            {
                if (string.IsNullOrEmpty(Data[20]))
                    return false;
                else if (Data[20].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[20] = "1";
                else
                    Data[20] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser guardado no banco?
        /// [21]
        /// </summary>
        public bool CanBank
        {
            get
            {
                if (string.IsNullOrEmpty(Data[21]))
                    return false;
                else if (Data[21].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[21] = "1";
                else
                    Data[21] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser vendido em lojas
        /// [22]
        /// </summary>
        public bool CanShop
        {
            get
            {
                if (string.IsNullOrEmpty(Data[22]))
                    return false;
                else if (Data[22].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[22] = "1";
                else
                    Data[22] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser dropado?
        /// [23]
        /// </summary>
        public bool CanDrop
        {
            get
            {
                if (string.IsNullOrEmpty(Data[23]))
                    return false;
                else if (Data[23].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[23] = "1";
                else
                    Data[23] = string.Empty;
            }
        }

        /// <summary>
        /// Point Shop
        /// [24]
        /// </summary>
        public bool PointShop
        {
            get
            {
                if (string.IsNullOrEmpty(Data[24]))
                    return false;
                else if (Data[24].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[24] = "1";
                else
                    Data[24] = string.Empty;
            }
        }

        /// <summary>
        /// Ganhar buff ao usar item
        /// [25]
        /// </summary>
        public int BindBuff
        {
            get
            {
                if (string.IsNullOrEmpty(Data[25]))
                    return 0;
                else if (int.TryParse(Data[25], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[25] = value.ToString();
                else
                    Data[25] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser deletado?
        /// [26]
        /// </summary>
        public bool CanDelete
        {
            get
            {
                if (string.IsNullOrEmpty(Data[26]))
                    return false;
                else if (Data[26].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[26] = "1";
                else
                    Data[26] = string.Empty;
            }
        }

        /// <summary>
        /// Item pode ser combinado?
        /// [27]
        /// </summary>
        public bool CanSmithing
        {
            get
            {
                if (string.IsNullOrEmpty(Data[27]))
                    return false;
                else if (Data[27].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[27] = "1";
                else
                    Data[27] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo de uso
        /// [28]
        /// </summary>
        public byte UseType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[28]))
                    return 0;
                else if (byte.TryParse(Data[28], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                Data[28] = value.ToString();
            }
        }

        /// <summary>
        /// Item contável?
        /// [29]
        /// </summary>
        public bool Countable
        {
            get
            {
                if (string.IsNullOrEmpty(Data[29]))
                    return false;
                else if (Data[29].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[29] = "1";
                else
                    Data[29] = string.Empty;
            }
        }

        /// <summary>
        /// Quantidade inicial do item
        /// [30]
        /// </summary>
        public int InitCount
        {
            get
            {
                if (string.IsNullOrEmpty(Data[30]))
                    return 0;
                else if (int.TryParse(Data[30], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[30] = value.ToString();
                else
                    Data[30] = string.Empty;
            }
        }

        /// <summary>
        /// Quantidade máxima do item por stack
        /// [31]
        /// </summary>
        public int MaxCount
        {
            get
            {
                if (string.IsNullOrEmpty(Data[31]))
                    return 0;
                else if (int.TryParse(Data[31], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[31] = value.ToString();
                else
                    Data[31] = string.Empty;
            }
        }

        /// <summary>
        /// Item consome ao utilizar?
        /// [32]
        /// </summary>
        public bool Consume
        {
            get
            {
                if (string.IsNullOrEmpty(Data[27]))
                    return false;
                else if (Data[27].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[27] = "1";
                else
                    Data[27] = string.Empty;
            }
        }

        /// <summary>
        /// Tempo limite do item
        /// [33]
        /// </summary>
        public long TimeLimit
        {
            get
            {
                if (string.IsNullOrEmpty(Data[33]))
                    return 0;
                else if (long.TryParse(Data[33], out long result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[33] = value.ToString();
                else
                    Data[33] = string.Empty;
            }
        }

        /// <summary>
        /// Item de Missão?
        /// [34]
        /// </summary>
        public bool TaskItem
        {
            get
            {
                if (string.IsNullOrEmpty(Data[34]))
                    return false;
                else if (Data[34].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[34] = "1";
                else
                    Data[34] = string.Empty;
            }
        }

        /// <summary>
        /// Item unico?
        /// [35]
        /// </summary>
        public bool Single
        {
            get
            {
                if (string.IsNullOrEmpty(Data[35]))
                    return false;
                else if (Data[35].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[35] = "1";
                else
                    Data[35] = string.Empty;
            }
        }

        /// <summary>
        /// Ambas as mãos
        /// [36]
        /// </summary>
        public bool BothHand
        {
            get
            {
                if (string.IsNullOrEmpty(Data[36]))
                    return false;
                else if (Data[36].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[36] = "1";
                else
                    Data[36] = string.Empty;
            }
        }

        /// <summary>
        /// Valor do item
        /// [37]
        /// </summary>
        public int Value
        {
            get
            {
                if (string.IsNullOrEmpty(Data[37]))
                    return 0;
                else if (int.TryParse(Data[37], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                Data[37] = value.ToString();
            }
        }

        /// <summary>
        /// CardPoints
        /// [38]
        /// </summary>
        public int CardPoints
        {
            get
            {
                if (string.IsNullOrEmpty(Data[38]))
                    return 0;
                else if (int.TryParse(Data[38], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[38] = value.ToString();
                else
                    Data[38] = string.Empty;
            }
        }

        /// <summary>
        /// MaxHardiness
        /// [39]
        /// </summary>
        public int MaxHardiness
        {
            get
            {
                if (string.IsNullOrEmpty(Data[39]))
                    return 0;
                else if (int.TryParse(Data[39], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[39] = value.ToString();
                else
                    Data[39] = string.Empty;
            }
        }

        /// <summary>
        /// Condição para uso
        /// [40]
        /// </summary>
        public string UseCondition
        {
            get
            {
                if (string.IsNullOrEmpty(Data[40]))
                    return string.Empty;
                else
                    return Data[40];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[40] = string.Empty;
                else
                    Data[40] = value;
            }
        }

        /// <summary>
        /// Condições para equipar
        /// [41]
        /// </summary>
        public string EquipCondition
        {
            get
            {
                if (string.IsNullOrEmpty(Data[41]))
                    return string.Empty;
                else
                    return Data[41];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[41] = string.Empty;
                else
                    Data[41] = value;
            }
        }

        /// <summary>
        /// Ações ao pegar
        /// [42]
        /// </summary>
        public string PickAction
        {
            get
            {
                if (string.IsNullOrEmpty(Data[42]))
                    return string.Empty;
                else
                    return Data[42];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[42] = string.Empty;
                else
                    Data[42] = value;
            }
        }

        /// <summary>
        /// Level Table Use
        /// [43]
        /// </summary>
        public int Level_TableUse
        {
            get
            {
                if (string.IsNullOrEmpty(Data[43]))
                    return 0;
                else if (int.TryParse(Data[43], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[43] = value.ToString();
                else
                    Data[43] = string.Empty;
            }
        }

        /// <summary>
        /// Ação ao equipar item
        /// [44]
        /// </summary>
        public string EquipAction
        {
            get
            {
                if (string.IsNullOrEmpty(Data[44]))
                    return string.Empty;
                else
                    return Data[44];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[44] = string.Empty;
                else
                    Data[44] = value;
            }
        }

        /// <summary>
        /// Defesa do que o item irá dar
        /// [45]
        /// </summary>
        public string EquipPhyArmor
        {
            get
            {
                if (string.IsNullOrEmpty(Data[46]))
                    return string.Empty;
                else
                    return Data[46];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[46] = string.Empty;
                else
                    Data[46] = value;
            }
        }

        /// <summary>
        /// Defesa do elemento Fogo
        /// [46]
        /// </summary>
        public string EquipeArm_Fire
        {
            get
            {
                if (string.IsNullOrEmpty(Data[46]))
                    return string.Empty;
                else
                    return Data[46];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[46] = string.Empty;
                else
                    Data[46] = value;
            }
        }

        /// <summary>
        /// Defesa do elemento Água
        /// [47]
        /// </summary>
        public string EquipArm_Wator
        {
            get
            {
                if (string.IsNullOrEmpty(Data[47]))
                    return string.Empty;
                else
                    return Data[47];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[47] = string.Empty;
                else
                    Data[47] = value;
            }
        }

        /// <summary>
        /// Defesa contra veneno
        /// [48]
        /// </summary>
        public string EquipArm_Poison
        {
            get
            {
                if (string.IsNullOrEmpty(Data[48]))
                    return string.Empty;
                else
                    return Data[48];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[48] = string.Empty;
                else
                    Data[48] = value;
            }
        }

        /// <summary>
        /// Defesa contra elemento de luz
        /// [49]
        /// </summary>
        public string EquipArm_Light
        {
            get
            {
                if (string.IsNullOrEmpty(Data[49]))
                    return string.Empty;
                else
                    return Data[49];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[49] = string.Empty;
                else
                    Data[49] = value;
            }
        }

        /// <summary>
        /// Defesa contra Buddha
        /// [50]
        /// </summary>
        public string EquipArm_Fo
        {
            get
            {
                if (string.IsNullOrEmpty(Data[50]))
                    return string.Empty;
                else
                    return Data[50];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[50] = string.Empty;
                else
                    Data[50] = value;
            }
        }

        /// <summary>
        /// Absorção de dano do equipamento
        /// [51]
        /// </summary>
        public string EquipDamageAbsorb
        {
            get
            {
                if (string.IsNullOrEmpty(Data[51]))
                    return string.Empty;
                else
                    return Data[51];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[51] = string.Empty;
                else
                    Data[51] = value;
            }
        }

        /// <summary>
        /// Dano por segundo da arma
        /// [52]
        /// </summary>
        public string EquipDps
        {
            get
            {
                if (string.IsNullOrEmpty(Data[52]))
                    return string.Empty;
                else
                    return Data[52];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[52] = string.Empty;
                else
                    Data[52] = value;
            }
        }

        /// <summary>
        /// Chance de desvio
        /// [53]
        /// </summary>
        public string EquipParry
        {
            get
            {
                if (string.IsNullOrEmpty(Data[53]))
                    return string.Empty;
                else
                    return Data[53];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[53] = string.Empty;
                else
                    Data[53] = value;
            }
        }

        /// <summary>
        /// InitValue
        /// [54]
        /// </summary>
        public string InitValue
        {
            get
            {
                if (string.IsNullOrEmpty(Data[54]))
                    return string.Empty;
                else
                    return Data[54];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[54] = string.Empty;
                else
                    Data[54] = value;
            }
        }

        /// <summary>
        /// Outros parametros 1
        /// [55]
        /// </summary>
        public string OtherParam
        {
            get
            {
                if (string.IsNullOrEmpty(Data[55]))
                    return string.Empty;
                else
                    return Data[55];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[55] = string.Empty;
                else
                    Data[55] = value;
            }
        }

        /// <summary>
        /// Outros paramentros 2
        /// [56]
        /// </summary>
        public string OtherParam2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[56]))
                    return string.Empty;
                else
                    return Data[56];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[56] = string.Empty;
                else
                    Data[56] = value;
            }
        }

        /// <summary>
        /// Outros parametros 3
        /// [57]
        /// </summary>
        public string OtherParam3
        {
            get
            {
                if (string.IsNullOrEmpty(Data[57]))
                    return string.Empty;
                else
                    return Data[57];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[57] = string.Empty;
                else
                    Data[57] = value;
            }
        }

        /// <summary>
        /// Level da gema
        /// [58]
        /// </summary>
        public int GemmyLevel
        {
            get
            {
                if (string.IsNullOrEmpty(Data[58]))
                    return 0;
                else if (int.TryParse(Data[58], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[58] = value.ToString();
                else
                    Data[58] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo de gema
        /// [59]
        /// </summary>
        public string GemmyType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[59]))
                    return string.Empty;
                else
                    return Data[59];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[59] = string.Empty;
                else
                    Data[59] = value;
            }
        }

        /// <summary>
        /// Mensagem ao usar item
        /// [60]
        /// </summary>
        public string ItemUseMessage
        {
            get
            {
                if (string.IsNullOrEmpty(Data[60]))
                    return string.Empty;
                else
                    return Data[60];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[60] = string.Empty;
                else
                    Data[60] = value;
            }
        }

        /// <summary>
        /// Habilidades do item
        /// [61]
        /// </summary>
        public string TrumpSkills
        {
            get
            {
                if (string.IsNullOrEmpty(Data[61]))
                    return string.Empty;
                else
                    return Data[61];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[61] = string.Empty;
                else
                    Data[61] = value;
            }
        }

        /// <summary>
        /// Habilidades finais do item
        /// [62]
        /// </summary>
        public string TrumpFinalSkills
        {
            get
            {
                if (string.IsNullOrEmpty(Data[62]))
                    return string.Empty;
                else
                    return Data[62];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[62] = string.Empty;
                else
                    Data[62] = value;
            }
        }

        /// <summary>
        /// Talentos do item
        /// [63]
        /// </summary>
        public string TrumpTalents
        {
            get
            {
                if (string.IsNullOrEmpty(Data[63]))
                    return string.Empty;
                else
                    return Data[63];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[63] = string.Empty;
                else
                    Data[63] = value;
            }
        }

        /// <summary>
        /// Ataque normal
        /// [64]
        /// </summary>
        public int TrumpAttackSkill
        {
            get
            {
                if (string.IsNullOrEmpty(Data[64]))
                    return 0;
                else if (int.TryParse(Data[64], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[64] = value.ToString();
                else
                    Data[64] = string.Empty;
            }
        }

        /// <summary>
        /// Pose ao iniciar o uso de um item
        /// [65]
        /// </summary>
        public string PreStartPose
        {
            get
            {
                if (string.IsNullOrEmpty(Data[65]))
                    return string.Empty;
                else
                    return Data[65];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[65] = string.Empty;
                else
                    Data[65] = value;
            }
        }

        /// <summary>
        /// Pose durante o uso do item
        /// [66]
        /// </summary>
        public string PrePose
        {
            get
            {
                if (string.IsNullOrEmpty(Data[66]))
                    return string.Empty;
                else
                    return Data[66];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[66] = string.Empty;
                else
                    Data[66] = value;
            }
        }

        /// <summary>
        /// Pose após usar o item
        /// [67]
        /// </summary>
        public string EndPose
        {
            get
            {
                if (string.IsNullOrEmpty(Data[67]))
                    return string.Empty;
                else
                    return Data[67];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[67] = string.Empty;
                else
                    Data[67] = value;
            }
        }

        /// <summary>
        /// Só pode usar em Dungeon?
        /// [68]
        /// </summary>
        public bool DungeonOnly
        {
            get
            {
                if (string.IsNullOrEmpty(Data[68]))
                    return false;
                else if (Data[68].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[68] = "1";
                else
                    Data[68] = string.Empty;
            }
        }

        /// <summary>
        /// ReleaseType
        /// [69]
        /// </summary>
        public byte ReleaseType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[69]))
                    return 0;
                else if (byte.TryParse(Data[69], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[69] = value.ToString();
                else
                    Data[69] = string.Empty;
            }
        }

        /// <summary>
        /// Benefit
        /// [70]
        /// </summary>
        public byte Benefit 
        {
            get
            {
                if (string.IsNullOrEmpty(Data[70]))
                    return 0;
                else if (byte.TryParse(Data[70], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[70] = value.ToString();
                else
                    Data[70] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo de alvo
        /// [71]
        /// </summary>
        public byte TargetType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[71]))
                    return 0;
                else if (byte.TryParse(Data[71], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[71] = value.ToString();
                else
                    Data[71] = string.Empty;
            }
        }

        /// <summary>
        /// Condição do alvo
        /// [72]
        /// </summary>
        public string TargetCondition
        {
            get
            {
                if (string.IsNullOrEmpty(Data[72]))
                    return string.Empty;
                else
                    return Data[72];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[72] = string.Empty;
                else
                    Data[72] = value;
            }
        }

        /// <summary>
        /// Especial
        /// [73]
        /// </summary>
        public int Special
        {
            get
            {
                if (string.IsNullOrEmpty(Data[73]))
                    return 0;
                else if (int.TryParse(Data[73], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[73] = value.ToString();
                else
                    Data[73] = string.Empty;
            }
        }

        /// <summary>
        /// Numero de alvos
        /// [74]
        /// </summary>
        public byte TargetCount
        {
            get
            {
                if (string.IsNullOrEmpty(Data[74]))
                    return 0;
                else if (byte.TryParse(Data[74], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[74] = value.ToString();
                else
                    Data[74] = string.Empty;
            }
        }

        /// <summary>
        /// Alcance do alvo
        /// [75]
        /// </summary>
        public int TargetRange
        {
            get
            {
                if (string.IsNullOrEmpty(Data[75]))
                    return 0;
                else if (int.TryParse(Data[75], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[75] = value.ToString();
                else
                    Data[75] = string.Empty;
            }
        }

        /// <summary>
        /// Raio o alvo
        /// [76]
        /// </summary>
        public int TargetRadius
        {
            get
            {
                if (string.IsNullOrEmpty(Data[76]))
                    return 0;
                else if (int.TryParse(Data[76], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[76] = value.ToString();
                else
                    Data[76] = string.Empty;
            }
        }

        /// <summary>
        /// Tempo de carregamento do item em milisegundos
        /// [77]
        /// </summary>
        public int PreTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[77]))
                    return 0;
                else if (int.TryParse(Data[77], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[77] = value.ToString();
                else
                    Data[77] = string.Empty;
            }
        }

        /// <summary>
        /// Antes de carregar
        /// [78]
        /// </summary>
        public string BeforeLoadingTmpSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[78]))
                    return string.Empty;
                else
                    return Data[78];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[78] = string.Empty;
                else
                    Data[78] = value;
            }
        }

        /// <summary>
        /// Antes de carregar no alvo
        /// [79]
        /// </summary>
        public string BeforeLoadingTmpTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[79]))
                    return string.Empty;
                else
                    return Data[79];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[79] = string.Empty;
                else
                    Data[79] = value;
            }
        }

        /// <summary>
        /// Antes de carregar
        /// [80]
        /// </summary>
        public string BeforeLoadingSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[80]))
                    return string.Empty;
                else
                    return Data[80];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[80] = string.Empty;
                else
                    Data[80] = value;
            }
        }

        /// <summary>
        /// Antes de carregar no alvo
        /// [81]
        /// </summary>
        public string BeforeLoadingTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[81]))
                    return string.Empty;
                else
                    return Data[81];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[81] = string.Empty;
                else
                    Data[81] = value;
            }
        }

        /// <summary>
        /// Tempo de uso do item
        /// [82]
        /// </summary>
        public int LoadingTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[82]))
                    return 0;
                else if (int.TryParse(Data[82], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[82] = value.ToString();
                else
                    Data[82] = string.Empty;
            }
        }

        /// <summary>
        /// Numero de usos durante o carregamento
        /// [83]
        /// </summary>
        public int LoadingCount
        {
            get
            {
                if (string.IsNullOrEmpty(Data[83]))
                    return 0;
                else if (int.TryParse(Data[83], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[83] = value.ToString();
                else
                    Data[83] = string.Empty;
            }
        }

        /// <summary>
        /// Carregamento temporário em sí
        /// [84]
        /// </summary>
        public string LoadingTmpSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[84]))
                    return string.Empty;
                else
                    return Data[84];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[84] = string.Empty;
                else
                    Data[84] = value;
            }
        }

        /// <summary>
        /// Carregamento temporário no alvo
        /// [85]
        /// </summary>
        public string LoadingTmpTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[85]))
                    return string.Empty;
                else
                    return Data[85];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[85] = string.Empty;
                else
                    Data[85] = value;
            }
        }

        /// <summary>
        /// Carregamento em sí
        /// [86]
        /// </summary>
        public string LoadingSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[86]))
                    return string.Empty;
                else
                    return Data[86];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[86] = string.Empty;
                else
                    Data[86] = value;
            }
        }

        /// <summary>
        /// Carregamento no alvo
        /// [87]
        /// </summary>
        public string LoadingTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[87]))
                    return string.Empty;
                else
                    return Data[87];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[87] = string.Empty;
                else
                    Data[87] = value;
            }
        }

        /// <summary>
        /// Tempo de resfriamento
        /// [88]
        /// </summary>
        public int CDTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[88]))
                    return 0;
                else if (int.TryParse(Data[88], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[88] = value.ToString();
                else
                    Data[88] = string.Empty;
            }
        }

        /// <summary>
        /// Tempo de resfriamento publico
        /// [89]
        /// </summary>
        public int PublicCDTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[89]))
                    return 0;
                else if (int.TryParse(Data[89], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[89] = value.ToString();
                else
                    Data[89] = string.Empty;
            }
        }

        /// <summary>
        /// Tipo de resfriamento
        /// [90]
        /// </summary>
        public byte TypeCDId
        {
            get
            {
                if (string.IsNullOrEmpty(Data[90]))
                    return 0;
                else if (byte.TryParse(Data[90], out byte result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[90] = value.ToString();
                else
                    Data[90] = string.Empty;
            }
        }

        /// <summary>
        /// ForbiddenTime
        /// [91]
        /// </summary>
        public int ForbiddenTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[91]))
                    return 0;
                else if (int.TryParse(Data[91], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[91] = value.ToString();
                else
                    Data[91] = string.Empty;
            }
        }

        /// <summary>
        /// Tempo de pose
        /// [92]
        /// </summary>
        public int PoseTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[92]))
                    return 0;
                else if (int.TryParse(Data[92], out int result))
                    return result;
                else
                    return 0;
            }
            set
            {
                if (value > 0)
                    Data[92] = value.ToString();
                else
                    Data[92] = string.Empty;
            }
        }

        /// <summary>
        /// Chance de critico
        /// [93]
        /// </summary>
        public string CriticalRate
        {
            get
            {
                if (string.IsNullOrEmpty(Data[93]))
                    return string.Empty;
                else
                    return Data[93];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[93] = string.Empty;
                else
                    Data[93] = value;
            }
        }

        /// <summary>
        /// Chance de efeito
        /// [94]
        /// </summary>
        public string EffRate
        {
            get
            {
                if (string.IsNullOrEmpty(Data[94]))
                    return string.Empty;
                else
                    return Data[94];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[94] = string.Empty;
                else
                    Data[94] = value;
            }
        }

        /// <summary>
        /// Parametro adicional
        /// [95]
        /// </summary>
        public string AddtionShowParam
        {
            get
            {
                if (string.IsNullOrEmpty(Data[95]))
                    return string.Empty;
                else
                    return Data[95];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[95] = string.Empty;
                else
                    Data[95] = value;
            }
        }

        /// <summary>
        /// Tipo de dano
        /// [96]
        /// </summary>
        public string DmgType
        {
            get
            {
                if (string.IsNullOrEmpty(Data[96]))
                    return string.Empty;
                else
                    return Data[96];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[96] = string.Empty;
                else
                    Data[96] = value;
            }
        }

        /// <summary>
        /// LaunchTmpSelf
        /// [97]
        /// </summary>
        public string LaunchTmpSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[97]))
                    return string.Empty;
                else
                    return Data[97];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[97] = string.Empty;
                else
                    Data[97] = value;
            }
        }

        /// <summary>
        /// LaunchTmpTarget
        /// [98]
        /// </summary>
        public string LaunchTmpTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[98]))
                    return string.Empty;
                else
                    return Data[98];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[98] = string.Empty;
                else
                    Data[98] = value;
            }
        }

        /// <summary>
        /// LaunchSelf
        /// [99]
        /// </summary>
        public string LaunchSelf
        {
            get
            {
                if (string.IsNullOrEmpty(Data[99]))
                    return string.Empty;
                else
                    return Data[99];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[99] = string.Empty;
                else
                    Data[99] = value;
            }
        }

        /// <summary>
        /// LaunchTarget
        /// [100]
        /// </summary>
        public string LaunchTarget
        {
            get
            {
                if (string.IsNullOrEmpty(Data[100]))
                    return string.Empty;
                else
                    return Data[100];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[100] = string.Empty;
                else
                    Data[100] = value;
            }
        }

        /// <summary>
        /// NoDamage
        /// [101]
        /// </summary>
        public string NoDamage
        {
            get
            {
                if (string.IsNullOrEmpty(Data[101]))
                    return string.Empty;
                else
                    return Data[101];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[101] = string.Empty;
                else
                    Data[101] = value;
            }
        }

        /// <summary>
        /// RealDmgTime
        /// [102]
        /// </summary>
        public string RealDmgTime
        {
            get
            {
                if (string.IsNullOrEmpty(Data[102]))
                    return string.Empty;
                else
                    return Data[102];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[102] = string.Empty;
                else
                    Data[102] = value;
            }
        }

        /// <summary>
        /// DamageMinMax
        /// [103]
        /// </summary>
        public string DamageMinMax
        {
            get
            {
                if (string.IsNullOrEmpty(Data[103]))
                    return string.Empty;
                else
                    return Data[103];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[103] = string.Empty;
                else
                    Data[103] = value;
            }
        }

        /// <summary>
        /// Parametro gráfico
        /// [104]
        /// </summary>
        public string graph_Param2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[104]))
                    return string.Empty;
                else
                    return Data[104];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[104] = string.Empty;
                else
                    Data[104] = value;
            }
        }

        /// <summary>
        /// Icone do item
        /// [105]
        /// </summary>
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(Data[105]))
                    return string.Empty;
                else
                    return Data[105];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[105] = string.Empty;
                else
                    Data[105] = value;
            }
        }

        /// <summary>
        /// Modelo 3d do item
        /// [106]
        /// </summary>
        public string Model
        {
            get
            {
                if (string.IsNullOrEmpty(Data[106]))
                    return string.Empty;
                else
                    return Data[106];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[106] = string.Empty;
                else
                    Data[106] = value;
            }
        }

        /// <summary>
        /// Modelo 3d do item +4 a +6
        /// [107]
        /// </summary>
        public string Model_Blue
        {
            get
            {
                if (string.IsNullOrEmpty(Data[107]))
                    return string.Empty;
                else
                    return Data[107];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[107] = string.Empty;
                else
                    Data[107] = value;
            }
        }

        /// <summary>
        /// Modelo 3d do item +7 a +8
        /// [108]
        /// </summary>
        public string Model_Golden
        {
            get
            {
                if (string.IsNullOrEmpty(Data[108]))
                    return string.Empty;
                else
                    return Data[108];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[108] = string.Empty;
                else
                    Data[108] = value;
            }
        }

        /// <summary>
        /// Modelo 3d do item +10
        /// [109]
        /// </summary>
        public string Model_10
        {
            get
            {
                if (string.IsNullOrEmpty(Data[109]))
                    return string.Empty;
                else
                    return Data[109];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[109] = string.Empty;
                else
                    Data[109] = value;
            }
        }

        /// <summary>
        /// Valor da escala
        /// [110]
        /// </summary>
        public string Scale_Value
        {
            get
            {
                if (string.IsNullOrEmpty(Data[110]))
                    return string.Empty;
                else
                    return Data[110];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[110] = string.Empty;
                else
                    Data[110] = value;
            }
        }

        /// <summary>
        /// Segundo modelo 3d do item
        /// [111]
        /// </summary>
        public string Model2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[111]))
                    return string.Empty;
                else
                    return Data[111];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[111] = string.Empty;
                else
                    Data[111] = value;
            }
        }

        /// <summary>
        /// Segundo modelo 3d do item +4 a +6
        /// [112]
        /// </summary>
        public string Model_Blue2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[112]))
                    return string.Empty;
                else
                    return Data[112];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[112] = string.Empty;
                else
                    Data[112] = value;
            }
        }

        /// <summary>
        /// Segundo modelo 3d do item +7 a +8
        /// [113]
        /// </summary>
        public string Model_Golden2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[113]))
                    return string.Empty;
                else
                    return Data[113];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[113] = string.Empty;
                else
                    Data[113] = value;
            }
        }

        /// <summary>
        /// Segundo modelo 3d do item +10
        /// [114]
        /// </summary>
        public string Model2_10
        {
            get
            {
                if (string.IsNullOrEmpty(Data[114]))
                    return string.Empty;
                else
                    return Data[114];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[114] = string.Empty;
                else
                    Data[114] = value;
            }
        }

        /// <summary>
        /// Path
        /// [115]
        /// </summary>
        public bool Path
        {
            get
            {
                if (string.IsNullOrEmpty(Data[115]))
                    return false;
                else if (Data[115].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[115] = "1";
                else
                    Data[115] = string.Empty;
            }
        }

        /// <summary>
        /// Link da arma
        /// [116]
        /// </summary>
        public string weaponLink
        {
            get
            {
                if (string.IsNullOrEmpty(Data[116]))
                    return string.Empty;
                else
                    return Data[116];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[116] = string.Empty;
                else
                    Data[116] = value;
            }
        }

        /// <summary>
        /// ItemRiderEffect
        /// [117]
        /// </summary>
        public string ItemRiderEffect
        {
            get
            {
                if (string.IsNullOrEmpty(Data[117]))
                    return string.Empty;
                else
                    return Data[117];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[117] = string.Empty;
                else
                    Data[117] = value;
            }
        }

        /// <summary>
        /// 
        /// [118]
        /// </summary>
        public string ItemRiderEffectLink
        {
            get
            {
                if (string.IsNullOrEmpty(Data[118]))
                    return string.Empty;
                else
                    return Data[118];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[118] = string.Empty;
                else
                    Data[118] = value;
            }
        }

        /// <summary>
        /// 
        /// [119]
        /// </summary>
        public string weapon1DrawInLink
        {
            get
            {
                if (string.IsNullOrEmpty(Data[118]))
                    return string.Empty;
                else
                    return Data[118];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[118] = string.Empty;
                else
                    Data[118] = value;
            }
        }

        /// <summary>
        /// 
        /// [120]
        /// </summary>
        public string weaponLink2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[120]))
                    return string.Empty;
                else
                    return Data[120];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[120] = string.Empty;
                else
                    Data[120] = value;
            }
        }

        /// <summary>
        /// 
        /// [121]
        /// </summary>
        public string weapon1DrawInLink2
        {
            get
            {
                if (string.IsNullOrEmpty(Data[121]))
                    return string.Empty;
                else
                    return Data[121];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[121] = string.Empty;
                else
                    Data[121] = value;
            }
        }

        /// <summary>
        /// Som do item
        /// [122]
        /// </summary>
        public string Sound
        {
            get
            {
                if (string.IsNullOrEmpty(Data[122]))
                    return string.Empty;
                else
                    return Data[122];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[122] = string.Empty;
                else
                    Data[122] = value;
            }
        }

        /// <summary>
        /// 
        /// [123]
        /// </summary>
        public string graph_SceneEff
        {
            get
            {
                if (string.IsNullOrEmpty(Data[123]))
                    return string.Empty;
                else
                    return Data[123];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[123] = string.Empty;
                else
                    Data[123] = value;
            }
        }

        /// <summary>
        /// 
        /// [124]
        /// </summary>
        public string graph_HitEff
        {
            get
            {
                if (string.IsNullOrEmpty(Data[124]))
                    return string.Empty;
                else
                    return Data[124];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[124] = string.Empty;
                else
                    Data[124] = value;
            }
        }

        /// <summary>
        /// 
        /// [125]
        /// </summary>
        public string graph_HitEffLink
        {
            get
            {
                if (string.IsNullOrEmpty(Data[125]))
                    return string.Empty;
                else
                    return Data[125];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data[125] = string.Empty;
                else
                    Data[125] = value;
            }
        }

        /// <summary>
        /// 
        /// [126]
        /// </summary>
        public bool bNotCheckAttState
        {
            get
            {
                if (string.IsNullOrEmpty(Data[126]))
                    return false;
                else if (Data[126].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[126] = "1";
                else
                    Data[126] = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// [127]
        /// </summary>
        public bool bcache
        {
            get
            {
                if (string.IsNullOrEmpty(Data[127]))
                    return false;
                else if (Data[127].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[127] = "1";
                else
                    Data[127] = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// [128]
        /// </summary>
        public bool bPrePoseDrawInWeapon
        {
            get
            {
                if (string.IsNullOrEmpty(Data[128]))
                    return false;
                else if (Data[128].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[128] = "1";
                else
                    Data[128] = string.Empty;
            }
        }

        /// <summary>
        /// Construtor do item apenas pelo ID
        /// </summary>
        /// <param name="ID">ID do item criado</param>
        public Item(int ID)
        {
            Data = new string[134];
            Data[0] = ID.ToString();
            Data[1] = "New Item";
        }

        /// <summary>
        /// Construtor do item com dados
        /// </summary>
        /// <param name="item">Dados do item</param>
        /// <param name="item_desc">Dados do item_desc</param>
        public Item(string[] item, string[] item_desc)
        {
            Data = item;

            if (item_desc.Length > 1)
                Name = item_desc[1]; // Nome do item

            if (item_desc.Length > 2)
                Description = item_desc[2]; // Descrição do item
            else
                Description = string.Empty;

            if (item_desc.Length > 3)
                Remark = item_desc[3];
            else
                Remark = string.Empty;
        }

        /// <summary>
        /// Retornar uma string com dados separados por vírgula para o item.csv
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Id);
            for(var i = 1; i < Data.Length; i++)
                sb.Append($",{Data[i]}");

            return sb.ToString();
        }

        /// <summary>
        /// Retorna a stringo com dados separados por virgula para o item_desc.csv
        /// </summary>
        /// <returns></returns>
        public string ToString_Desc()
        {
            return $"{Id},{Name},{Description},{Remark}";
        }
    }
}
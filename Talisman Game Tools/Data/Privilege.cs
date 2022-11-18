using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talisman_Game_Tools.Data
{
    internal class Priv_Cmd
    {
        public string Cmd;
        public string Desc;
        public byte Priv;

        /// <summary>
        /// Construtor da classe privelege
        /// </summary>
        /// <param name="priv_cmd">Dados do arquivo priv_cmd.csv</param>
        /// <param name="priv_cfg">Dados do arquivo priv_cfg.csv</param>
        public Priv_Cmd(string[] priv_cmd)
        {
            Cmd = priv_cmd[0];
            Desc = priv_cmd[1];
            Priv = byte.Parse(priv_cmd[2]);
        }

        /// <summary>
        /// Construtor sem dados
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="desc"></param>
        /// <param name="priv"></param>
        public Priv_Cmd(string cmd, string desc, byte priv)
        {
            Cmd = cmd;
            Desc = desc;
            Priv = priv;
        }

        /// <summary>
        /// Retornar linha do priv_cmd.csv
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Cmd},{Desc},{Priv}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Priv_Cfg
    {
        private readonly string[] Data;

        /// <summary>
        /// Id do privilégio
        /// </summary>
        public byte Id
        {
            get
            {
                if (string.IsNullOrEmpty(Data[0]))
                    return 0;
                else
                    if (byte.TryParse(Data[0], out byte val))
                        return val;
                    else
                        return 0;
            }
            set
            {
                Data[0] = value.ToString().Trim();
            }
        }

        /// <summary>
        /// Prefixo ou TAG
        /// </summary>
        public string PrefixName
        {
            get { return Data[1]; }
            set { Data[1] = value; }
        }

        /// <summary>
        /// Possui limite ao falar no chat?
        /// </summary>
        public bool WorldChatLimit
        {
            get
            {
                if (string.IsNullOrEmpty(Data[2]))
                    return false;
                else if (Data[2].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[2] = "1";
                else
                    Data[2] = "0";
            }
        }

        /// <summary>
        /// A tag é mostrada no cliente?
        /// </summary>
        public bool ShowInClient
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
                    Data[3] = "0";
            }
        }

        /// <summary>
        /// é um conta GM?
        /// </summary>
        public bool IsGM
        {
            get
            {
                if (string.IsNullOrEmpty(Data[4]))
                    return false;
                else if (Data[4].Trim() == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    Data[4] = "1";
                else
                    Data[4] = "0";
            }
        }

        /// <summary>
        /// Construtor com dados
        /// </summary>
        /// <param name="Data"></param>
        public Priv_Cfg(string[] Data)
        {
            this.Data = Data;
        }

        /// <summary>
        /// Retorna uma string com todos os dados em um linha separada por virgula
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Data[0]);
            for (var i = 1; i < Data.Length; i++)
                sb.Append($",{Data[i]}");

            return sb.ToString();
        }
    }
}

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
}

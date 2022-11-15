using System.Text;

namespace Talisman_Game_Tools.Data
{
    internal class Suit
    {
        /// <summary>
        /// Identificado do traje dentro do jogo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do traje dentro do jogo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Items ao qual fazem parte do traje
        /// Identificados pelo seu ID e separado por ponto e virgula
        /// </summary>
        public string Items { get; set; }

        /// <summary>
        /// Ações que irão ocorrer quando equipar cada item do traje
        /// </summary>
        public string[] Actions  { get; set; }

        /// <summary>
        /// Descrição mostrada ao jogador de cada item equipado
        /// </summary>
        public string[] ActionsDesc { get; set; }

        /// <summary>
        /// Icone do traje dentro do jogo
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Aparencia do efeito ao equipar todo o traje
        /// </summary>
        public string AppearenceEffect { get; set; }

        /// <summary>
        /// Ligação do efeito quando equipar todo o traje
        /// </summary>
        public string EffectLink { get; set; }

        /// <summary>
        /// Efeito do traje em personagens femininas
        /// </summary>
        public string AppearenceEffectWoman { get; set; }

        /// <summary>
        /// Link do efeito do traje em personagens femininas
        /// </summary>
        public string EffectLinkWoman { get; set; }

        /// <summary>
        /// Criar um suit apenas com o ID
        /// </summary>
        /// <param name="Id"></param>
        public Suit(int Id)
        {
            this.Id = Id;
            this.Name = "New Suit";
            this.Actions = new string[10]; // Determinar tamanho do array
            this.ActionsDesc = new string[10]; // Determinar tamanho do array
        }

        /// <summary>
        /// Criar um suit por meio de uma linha do itemsuit.csv e o itemsuit_desc.csv
        /// </summary>
        /// <param name="itemSuit"></param>
        /// <param name="itemSuit_desc"></param>
        public Suit(string[] itemSuit, string[] itemSuit_desc)
        {
            this.Id = int.Parse(itemSuit[0]);
            this.Name = itemSuit_desc[1];
            this.Items = itemSuit[2];
            this.Actions = new string[10]; // Determinar tamanho do array
            this.ActionsDesc = new string[10]; // Determinar tamanho do array
            for (var i = 0; i < 10; i++)
            {
                this.Actions[i] = itemSuit[i + 3];
                this.ActionsDesc[i] = itemSuit_desc[i + 2];
            }

            this.Icon = itemSuit[13];
            this.AppearenceEffect = itemSuit[14];
            this.EffectLink = itemSuit[15];
            this.AppearenceEffectWoman = itemSuit[16];
            this.EffectLinkWoman = itemSuit[17];
        }

        /// <summary>
        /// Retornar uma linha em string com dados separados por virgula
        /// para ser salvo no itemsuit.csv
        /// </summary>
        /// <returns></returns>
        public string ToString_ItemSuit()
        {
            var sb = new StringBuilder();
            sb.Append(Id);
            sb.Append("," + Name);
            sb.Append("," + Items);
            foreach(var action in Actions) 
                sb.Append("," + action.ToString());
            sb.Append("," + Icon);
            sb.Append("," + AppearenceEffect);
            sb.Append("," + EffectLink);
            sb.Append("," + AppearenceEffectWoman);
            sb.Append("," + EffectLinkWoman);
            return sb.ToString();
        }

        /// <summary>
        /// Retornar uma linha em string com dados separados por virgula
        /// para ser salvo no itemsuit_desc.csv
        /// </summary>
        /// <returns></returns>
        public string ToString_ItemSuit_Desc()
        {
            var sb = new StringBuilder();
            sb.Append(Id);
            sb.Append("," + Name);
            foreach (var actionDesc in ActionsDesc)
                sb.Append("," + actionDesc.ToString());
            return sb.ToString();
        }
    }
}

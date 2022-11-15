using System;
using System.IO;
using System.Windows.Forms;
using Talisman_Game_Tools.Forms;

namespace Talisman_Game_Tools
{
    internal static class Program
    {
        /// <summary>
        /// Atributo local do arquivo local_and_language.ini
        /// </summary>
        public static string Local;

        /// <summary>
        /// Atributo path do arquivo local_and_language.ini
        /// </summary>
        public static string Path;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        /// <summary>
        /// Verificar se é uma pasta de projeto válida para o editor
        /// </summary>
        /// <returns></returns>
        public static bool IsValidFolder(string Path)
        {
            // Verificar se é uma pasta game_server está criada
            if (!Directory.Exists($"{Path}/game_server"))
                return false;

            // Verificar se é uma pasta game_client está criada
            if (!Directory.Exists($"{Path}/game_client"))
                return false;

            return true; // Retornar verdadeiro se todos os arquivos existirem
        }

        /// <summary>
        /// Função que split uma linha por meio de um delimitador, porém ignora o delimitador quando se está dentro de aspas
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Delimiter"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static string[] SplitString(string Data, char Delimiter, int Size)
        {
            var a = Data.Split(Delimiter);

            // Verificar se os dados divididos está maior que o esperado
            if (a.Length > Size)
            {
                var x = new string[Size];
                var addding = false;
                var y = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].StartsWith('"'.ToString()))
                    {
                        addding = true;
                    }

                    if (addding)
                    {
                        if (a[i].EndsWith('"'.ToString()))
                        {
                            if (y < x.Length && i < a.Length)
                            {
                                x[y] = $"{x[y]}{a[i]}";
                                addding = false;
                                y++;
                            }
                        }else
                            if (y < x.Length && i < a.Length)
                                x[y] = $"{x[y]}{a[i]}{Delimiter}";
                    }
                    else
                    {
                        if (y < x.Length && i < a.Length)
                        {
                            x[y] = a[i];
                            y++;
                        } 
                    }
                }

                return x;
            }
            else
                return a;
        }
    }
}

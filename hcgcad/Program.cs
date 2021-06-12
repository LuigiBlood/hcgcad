using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hcgcad
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormViewer());
        }

        //Handy stuff
        public static T[] Subarray<T>(T[] obj, int i, int len)
        {
            T[] output = new T[len];

            for (int j = 0; j < len; j++)
            {
                if ((i + j) < obj.Length)
                    output[j] = obj[i + j];
                else
                    output[j] = obj[j - i];
            }

            return output;
        }
    }
}

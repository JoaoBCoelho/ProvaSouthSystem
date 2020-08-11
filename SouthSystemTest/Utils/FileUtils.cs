using System;
using System.IO;
using System.Text;

namespace SouthSystemTest.Utils
{
    public class FileUtils
    {
        public static void ValidarDiretorio(string caminho)
        {
            if (!File.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }
        }

        public static void ValidarArquivo(string caminhoCompleto)
        {
            if (File.Exists(caminhoCompleto))
            {
                File.Delete(caminhoCompleto);
            }
        }

        public static void GravarTexto(string texto, string caminhoCompleto)
        {
            if (File.Exists(caminhoCompleto))
            {
                File.Delete(caminhoCompleto);
            }

            using (FileStream fs = File.Create(caminhoCompleto))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes(texto);
                fs.Write(title, 0, title.Length);
            }
        }
    }
}

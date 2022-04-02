using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoHotel.Helpers
{
    public class ImagemHelper
    {
        public static string pathString;

        private string imgPath;

        public ImagemHelper()
        {
            imgPath = Path.Combine(pathString, "img");
        }
        
        public string SalvarImagem(IFormFile file)
        {
            string fileName = "";

            if (file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                fileName = GerarNomeImagem(extension);

                string filePath = Path.Combine(imgPath, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        private string GerarNomeImagem(string extension)
        {
            string nome = Guid.NewGuid().ToString("n").Substring(0, 10);
            string data = DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "");

            return $"img_{nome}_{data}{extension}";
        }

        public List<string> SalvarImagem(IFormFileCollection files)
        {
            List<string> fileNames = new List<string>();

            foreach (IFormFile file in files)
            {
                fileNames.Add(SalvarImagem(file));
            }

            return fileNames;
        }

        public void DeletarImagemDoDiretorio(List<string> paths)
        {
            foreach(var path in paths)
            {
                DeletarImagemDoDiretorio(path);
            }
        }

        public void DeletarImagemDoDiretorio(string imageName)
        {
            string path = Path.Combine(imgPath, imageName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }       
    }
}

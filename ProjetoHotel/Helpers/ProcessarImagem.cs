using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace ProjetoHotel.Helpers
{
    public class ProcessarImagem
    {
        private string path_String;


        public ProcessarImagem(IWebHostEnvironment environment)
        {
            path_String = Path.Combine(environment.WebRootPath, "img");
        }
        
        public string SalvarImagem(IFormFile file)
        {
            string file_name = "";

            if (file.Length > 0)
            {
                string filePath = Path.Combine(path_String, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                file_name = file.FileName;
            }

            return file_name;
        }

        public List<string> SalvarImagem(IFormFileCollection files)
        {
            List<string> files_name = new List<string>();

            foreach (IFormFile file in files)
            {
                files_name.Add(SalvarImagem(file));
            }

            return files_name;
        }

    }
}

using Microsoft.Extensions.Configuration;
using ServicoImport.Dados;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoImport
{
    public class ImportaArquivo
    {
        private readonly IConfiguration _settings;
        public ImportaArquivo(IConfiguration configuration)
        {
            _settings = configuration;
        }

        public IConfiguration Configuration { get; }
        public void Importar()
        {
            string Entrada      = _settings.GetSection("Diretorios:Entrada").Value;
            string Saida        = _settings.GetSection("Diretorios:Saida").Value;
            string Delimitador  = _settings.GetSection("Diretorios:Delimitador").Value;
            string Extencao     = _settings.GetSection("Diretorios:Extencao").Value;


            if (!Directory.Exists(Entrada))
                Directory.CreateDirectory(Entrada);

            DirectoryInfo diretorioEntrada = new(Entrada);
            List<FileInfo> arquivos = new();
            arquivos.AddRange(diretorioEntrada.GetFiles(Extencao));

            if (arquivos.Count > 0)
                if (!Directory.Exists(Saida))
                    Directory.CreateDirectory(Saida);
            var nome = "";
            foreach (FileInfo arquivo in arquivos)
            {
                var lista = new List<Entities.Arquivo>();
                nome = arquivo.FullName;
                string[] linhas;

                using (var sr = new StreamReader(arquivo.FullName))
                {
                    linhas = sr.ReadToEnd().Split('\n');
                }

                foreach (var item in linhas)
                {
                    try
                    {
                        var a = new Entities.Arquivo();
                        var coluna = item.Split(Delimitador).ToList();
                        a.ID            = coluna[0];
                        a.LON           = coluna[1];
                        a.LAT           = coluna[2];
                        a.SETCENS       = coluna[3];
                        a.AREAP         = coluna[4];
                        a.CODDIST       = coluna[5];
                        a.DISTRITO      = coluna[6];
                        a.CODSUBPREF    = coluna[7];
                        a.SUBPREFE      = coluna[8];
                        a.REGIAO5       = coluna[9];
                        a.REGIAO8       = coluna[10];
                        a.NOME_FEIRA    = coluna[11];
                        a.REGISTRO      = coluna[12];
                        a.LOGRADOURO    = coluna[13];
                        a.NUMERO        = coluna[14];
                        a.BAIRRO        = coluna[15];
                        a.REFERENCIA    = coluna[16];

                        lista.Add(a);
                    }

                    catch (Exception ex)
                    {
                        var erro = ex;
                        continue;
                    }
                }


                var registrar = new Query().Salvar(lista);

                if (registrar.Count >0)
                {
                    File.Move(nome, $"{Saida}" + "\\" + arquivo.Name + DateTime.Now.Minute);
                }
            }
        }
    }
}

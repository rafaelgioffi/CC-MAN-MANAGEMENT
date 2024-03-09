using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CC_MAN_MANAGEMENT
{
    public class Program
    {
        static void Main(string[] args)
        {
            string LogFile = ConfigurationSettings.AppSettings["LogFile"];

            string SourceFolder = ConfigurationSettings.AppSettings["SourceFolder"];
            string ProcessFolder = ConfigurationSettings.AppSettings["ProcessFolder"];
            string SuccessFolder = ConfigurationSettings.AppSettings["SuccessFolder"];
            string FailFolder = ConfigurationSettings.AppSettings["FailFolder"];

            string StationFiles = ConfigurationSettings.AppSettings["StationFiles"];

            string AJUST_Folder = ConfigurationSettings.AppSettings["AJUST_Folder"];
            string AJUST_File = ConfigurationSettings.AppSettings["AJUST_File"];
            string CRDIF_Folder = ConfigurationSettings.AppSettings["CRDIF_Folder"];
            string CRDIF_File = ConfigurationSettings.AppSettings["CRDIF_File"];
            string NFPEN_Folder = ConfigurationSettings.AppSettings["NFPEN_Folder"];
            string NFPEN_File = ConfigurationSettings.AppSettings["NFPEN_File"];
            string BORDE_Folder = ConfigurationSettings.AppSettings["BORDE_Folder"];
            string BORDE_File = ConfigurationSettings.AppSettings["BORDE_File"];
            string SALLH_Folder = ConfigurationSettings.AppSettings["SALLH_Folder"];
            string SALLH_File = ConfigurationSettings.AppSettings["SALLH_File"];
            string SALCR_Folder = ConfigurationSettings.AppSettings["SALCR_Folder"];
            string SALCR_File = ConfigurationSettings.AppSettings["SALCR_File"];
            string IRMES_Folder = ConfigurationSettings.AppSettings["IRMES_Folder"];
            string IRMES_File = ConfigurationSettings.AppSettings["IRMES_File"];
            string NDNCS_Folder = ConfigurationSettings.AppSettings["NDNCS_Folder"];
            string NDNCS_File = ConfigurationSettings.AppSettings["NDNCS_File"];
            string LHDIF_Folder = ConfigurationSettings.AppSettings["LHDIF_Folder"];
            string LHDIF_File = ConfigurationSettings.AppSettings["LHDIF_File"];
            string ENCAC_Folder = ConfigurationSettings.AppSettings["ENCAC_Folder"];
            string ENCAC_File = ConfigurationSettings.AppSettings["ENCAC_File"];
            string NOTAS_Folder = ConfigurationSettings.AppSettings["NOTAS_Folder"];
            string NOTAS_File = ConfigurationSettings.AppSettings["NOTAS_File"];
            string HISTO_Folder = ConfigurationSettings.AppSettings["HISTO_Folder"];
            string HISTO_File = ConfigurationSettings.AppSettings["HISTO_File"];
            string CONTA_Folder = ConfigurationSettings.AppSettings["CONTA_Folder"];
            string CONTA_File = ConfigurationSettings.AppSettings["CONTA_File"];

            string[] filesInFolder;
            int filesQuant;
            
            
            Log("Iniciando o processamento...");            
            //analisa a pasta de entrada...
            try
            {
                filesInFolder = Directory.GetFiles(SourceFolder, "*");
                filesQuant = filesInFolder.Length;

            }
            catch (Exception ex)
            {
                Log($"Nenhum arquivo para processar em {SourceFolder}");
                return;
            }

            if ( filesQuant > 0 )
            {
                int counter = 1;
                Log($"Iniciando o processamento de {filesQuant} arquivos...");
                
                foreach (var file in filesInFolder)
                {
                    Log($"Processando o arquivo {file}... ({counter}/{filesQuant})");
                    
                    try //analisa o nome do arquivo para verificar a validade...
                    {
                        string[] pathFile = file.Split('\\');
                        string fileName = pathFile.Last();
                        string[] actualFile = fileName.Split('.');

                        if (actualFile[0] == StationFiles && actualFile[1] == AJUST_File && actualFile.Last().ToLower() == "txt")   //verifica se é um arquivo AJUST válido...
                        {
                            try  //tenta copiar o arquivo para o processamento...
                            {
                                File.Copy(file, $"{ProcessFolder}{actualFile[0]}.{actualFile[1]}.{actualFile.Last()}", true);
                                Log($"Arquivo {file} copiado para {ProcessFolder}{actualFile[0]}.{actualFile[1]}.{actualFile.Last()}...");
                            }
                            catch
                            {
                                Log($"Falha ao copiar o arquivo {file} para {ProcessFolder}{actualFile[0]}.{actualFile[1]}.{actualFile.Last()}...");
                                File.Move(file, $"{FailFolder}{actualFile[0]}.{actualFile[1]}.{actualFile.Last()}");
                                return;
                            }

                            try //tenta executar a bat...
                            { 
                                Log($"Executando o {ProcessFolder}{AJUST_Folder}.bat...");
                                Process.Start(ProcessFolder + AJUST_Folder + ".bat").WaitForExit();
                                Log($"Execução do {ProcessFolder}{AJUST_Folder}.bat finalizada com sucesso!");
                            }
                            catch (Exception ex )
                            {
                                Log($"Falha ao executar {ProcessFolder}{AJUST_Folder}.bat...\n{ex.Message}");
                                return;
                            }

                            try  //tenta mover o arquivo da origem para o destino com sucesso...
                            {
                                File.Move(file, $"{SuccessFolder}{fileName}");
                                Log($"Arquivo {file} movido para {SuccessFolder}{fileName}");
                            }
                            catch (Exception ex)
                            {
                                Log($"Falha ao mover {file} para {SuccessFolder}{fileName}...\n{ex.Message}");
                                return;
                            }
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        Log($"Falha ao processar o arquivo {file}...\n{ex.Message}");
                    }
                }
                Log("", true);
            }

            void Log(string msg, bool special = false)
            {
                using (StreamWriter swLog = new StreamWriter(LogFile, true))
                {
                    if (special)
                    {
                        swLog.WriteLine(msg);
                    }
                    else
                    {
                        swLog.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} => {msg}");
                    }
                }
            }

        }
    }
}

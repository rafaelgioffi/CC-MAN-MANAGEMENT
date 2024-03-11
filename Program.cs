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
            string TextFilesExtension = ConfigurationSettings.AppSettings["TextFilesExtension"];
            string BatFilesExtension = ConfigurationSettings.AppSettings["BatFilesExtension"];

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
            string validateDate = null;
            string validateTime = null;


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

            if (filesQuant > 0)
            {
                int counter = 1;
                Log($"Iniciando o processamento de {filesQuant} arquivos...");

                foreach (var file in filesInFolder)
                {   //processa cada arquivo da pasta...
                    Log($"Processando o arquivo {file}... ({counter}/{filesQuant})");

                    try //analisa o nome do arquivo para verificar a validade...
                    {
                        string[] pathFile = file.Split('\\');
                        string fileName = pathFile.Last();
                        string[] actualFile = fileName.Split('.');                                            

                        // Verifica se parte do formato do arquivo está válido...
                        bool partialValidFile = false;
                        if (actualFile[0] == StationFiles && actualFile[2] == TextFilesExtension)
                            partialValidFile = true;
                        else
                        {
                            partialValidFile = false;                            
                        }

                        // Verifica se possui uma data no nome do arquivo...
                        if (partialValidFile && actualFile.Length > 3 && actualFile.Length < 6) 
                        {
                            validateDate = actualFile[3];
                            validateTime = actualFile[4];
                        }
                        else if (partialValidFile && actualFile.Length < 4)
                        {
                            var fileInf = new FileInfo(file);
                            validateDate = $"D{fileInf.LastWriteTime.ToString("yyMMdd")}";
                            validateTime = $"T{fileInf.LastWriteTime.ToString("HHmmss")}";
                        }                        

                        if (partialValidFile && actualFile[1] == AJUST_File)   //verifica se é um arquivo AJUST válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], AJUST_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == CRDIF_File)  // Verifica se é um arquivo CRDIF válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], CRDIF_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == NFPEN_File)  // Verifica se é um arquivo NFPEN válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], NFPEN_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == BORDE_File)  // Verifica se é um arquivo BORDE válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], BORDE_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == SALLH_File)  // Verifica se é um arquivo SALLH válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], SALLH_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == SALCR_File)  // Verifica se é um arquivo SALCR válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], SALCR_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == IRMES_File)  // Verifica se é um arquivo IRMES válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], IRMES_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == NDNCS_File)  // Verifica se é um arquivo NDNCS válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], NDNCS_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == LHDIF_File)  // Verifica se é um arquivo LHDIF válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], LHDIF_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == ENCAC_File)  // Verifica se é um arquivo ENCAC válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], ENCAC_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == NOTAS_File)  // Verifica se é um arquivo NOTAS válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], NOTAS_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == HISTO_File)  // Verifica se é um arquivo HISTO válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], HISTO_Folder);
                        }
                        else if (partialValidFile && actualFile[1] == CONTA_File)  // Verifica se é um arquivo CONTA válido...
                        {
                            ProcessFile(file, actualFile[0], actualFile[1], actualFile[2], CONTA_Folder);
                        }
                        else  // Qualquer outro nome é inválido e ignora o processamento...
                        {
                            Log($"Arquivo {file} não é uma carga do Conta Corrente ou está em formato inválido.");                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Falha ao processar o arquivo {file}...\n{ex.Message}");
                    }
                    Log(" ", true);
                    counter++;
                }
                Log (" ", true);
                Log(" Fim do processamento.");
                Log (" ", true);
                Log (" ", true);
            }
            else
            {
                Log($"Nenhum arquivo para processar.");
            }

            void ProcessFile(string fileName, string fileStation, string chargeName, string extension, string processFolder)
            {
                try  //tenta copiar o arquivo para o processamento...
                {
                    File.Copy(fileName, $"{ProcessFolder}{fileStation}.{chargeName}.{extension}", true);
                    Log($"Arquivo {fileName} copiado para {ProcessFolder}{fileStation}.{chargeName}.{extension}...");
                }
                catch (Exception ex)
                {
                    Log($"Falha ao copiar o arquivo {fileName} para {ProcessFolder}{fileStation}.{chargeName}.{extension}...\n{ex.Message}");

                    try  //tenta mover para a pasta de processamentos falhados...
                    {
                        if (!Directory.Exists(FailFolder + processFolder))  // Verifica se a pasta da interface existe...
                            try
                            {
                                Directory.CreateDirectory(FailFolder + processFolder);
                                Log($"Diretório {FailFolder}\\{processFolder} criado.");
                            }
                            catch (Exception exc) 
                            {
                                Log($"Falha ao criar o diretório {FailFolder}\\{processFolder}...\n{exc.Message}");
                                return;
                            }

                        File.Move(fileName, $"{FailFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}");
                        Log($"Arquivo {fileName} movido para {FailFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}");
                    }
                    catch
                    {
                        Log($"Falha ao mover o arquivo {fileName} para {FailFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}");
                        return;
                    }
                    return;
                }

                try //tenta executar a bat...
                {
                    Log($"Executando o {ProcessFolder}{processFolder}.{BatFilesExtension}...");
                    Process.Start(ProcessFolder + processFolder + "." + BatFilesExtension).WaitForExit();
                    Log($"Execução do {ProcessFolder}{processFolder}.{BatFilesExtension} finalizada com sucesso!");
                }
                catch (Exception ex)
                {
                    Log($"Falha ao executar {ProcessFolder}{processFolder}.{BatFilesExtension} ...\n{ex.Message}");
                    return;
                }

                try  //verifica se a pasta de processados com sucesso/interface existe
                {
                    if (!Directory.Exists(SuccessFolder + "\\" + processFolder))
                    {
                        Directory.CreateDirectory(SuccessFolder + "\\" + processFolder);
                        Log($"Diretório {SuccessFolder}{processFolder} criado.");
                    }
                }
                catch (Exception ex)
                {
                    Log($"Falha ao criar o diretório {SuccessFolder}\\{processFolder}...\n{ex.Message}");
                    return;
                }

                try  //tenta mover o arquivo da origem para o destino com sucesso...
                {
                    File.Move(fileName, $"{SuccessFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}");
                    Log($"Arquivo {fileName} movido para {SuccessFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}");
                }
                catch (Exception ex)
                {
                    Log($"Falha ao mover {fileName} para {SuccessFolder}{processFolder}\\{fileStation}.{chargeName}.{extension}.{validateDate}.{validateTime}...\n{ex.Message}");
                    return;
                }
            }

            void Log(string msg, bool special = false)
            {
                using (StreamWriter swLog = new StreamWriter(LogFile, true))
                {
                    if (special)
                    {
                        swLog.WriteLine(msg);
                        Console.WriteLine(msg);
                    }
                    else
                    {
                        swLog.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} => {msg}");
                        Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} => {msg}");
                    }
                }
            }

        }
    }
}

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoColProfesionales.Models
{
    public class WordProcessor : IWordProcessor
    {
        public ResultFile ModelLetter(string path, string filePath, AttributeReplaceLetter attribute)
        {
            ResultFile resultFile = new ResultFile { HasError = false };
            try
            {
                string documentoOriginal = path + filePath;
                using (MemoryStream originalMs = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(documentoOriginal, FileMode.Open, FileAccess.ReadWrite))
                    {
                        fs.CopyTo(originalMs);
                    }
                    MemoryStream cloneMs = new MemoryStream(originalMs.ToArray());
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(cloneMs, true))
                    {
                        Body body = wordDoc.MainDocumentPart.Document.Body;
                        var old = body.InnerXml.ToString();
                        var textOld = body.InnerText;
                        body.InnerXml = body.InnerXml.Replace("«FechaCreacion»", attribute.FechaCarta);
                        body.InnerXml = body.InnerXml.Replace("«FechaPresentacion»", attribute.FechaPresentacion);
                        body.InnerXml = body.InnerXml.Replace("«NombrePostulante»", attribute.NombrePostulante);
                        body.InnerXml = body.InnerXml.Replace("«Carrera»", attribute.Carrera);
                        body.InnerXml = body.InnerXml.Replace("«Profesional»", attribute.Profesional);

                        //MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                        //foreach (var text in mainPart.Document.Body.Descendants<Text>())
                        //{
                        //    string data = text.Text;
                        //    string valores = "45";
                        //}

                        //var profesional = wordDoc.MainDocumentPart.RootElement.Descendants<Text>().FirstOrDefault(c => c.Text.Contains("«FechaCreacion»"));
                        //profesional.InnerText.Replace("«FechaCreacion»", attribute.FechaCarta);

                        //var fechaPresentacion = wordDoc.MainDocumentPart.RootElement.Descendants<Text>().FirstOrDefault(c => c.Text.Contains("«FechaPresentacion»"));
                        //fechaPresentacion.InnerText.Replace("«FechaPresentacion»", attribute.FechaPresentacion);

                        //var nombrePostulante = wordDoc.MainDocumentPart.RootElement.Descendants<Text>().FirstOrDefault(c => c.Text.Contains("«NombrePostulante»"));
                        //nombrePostulante.InnerText.Replace("«NombrePostulante»", attribute.NombrePostulante);

                        //var carrera = wordDoc.MainDocumentPart.RootElement.Descendants<Text>().FirstOrDefault(c => c.Text.Contains("«Carrera»"));
                        //carrera.InnerText.Replace("«Carrera»", attribute.Carrera);

                        //var fechaCarta = wordDoc.MainDocumentPart.RootElement.Descendants<Text>().FirstOrDefault(c => c.Text.Contains("«Profesional»"));
                        //fechaCarta.InnerText.Replace("«FechaCarta»", attribute.Profesional);

                        wordDoc.MainDocumentPart.Document.Save();
                        //wordDoc.Save();
                    }
                    cloneMs.Seek(0, SeekOrigin.Begin);

                    resultFile.memoryStream = cloneMs;
                    resultFile.NameFile = "CARTA_" + DateTime.UtcNow.ToString("yyyyMMdd_HHmmss") + ".docx";

                    return resultFile;
                }
            }
            catch (Exception ex)
            {
                resultFile.HasError = true;
                resultFile.Error = ex.Message;
                return resultFile;
            }
        }
    }
}

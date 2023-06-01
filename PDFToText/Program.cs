
using Tesseract;
using System.Drawing;
using System.Collections.Generic;
using Pdf2Image;
using System.IO;
using System;

namespace PDFToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var outputPdfPath = @"output.pdf";
            List<Image> images = PdfSplitter.GetImages(outputPdfPath, PdfSplitter.Scale.High);
            var resText = "";
            //Tesseract ile imageları text haline getiriyoruz.
            string tessDataDir = @"tessdata";
            using (var engine = new TesseractEngine(tessDataDir, "tur", EngineMode.Default))
        
                foreach (var image in images)
                {
                    using (var msImg = new MemoryStream())
                    {
                        msImg.Position = 0;
                        image.Save(msImg, image.RawFormat);
                        using (var imageByte = Pix.LoadFromMemory(msImg.GetBuffer()))
                        using (var page = engine.Process(imageByte))
                        {
                            resText += page.GetText();
                        }
                    }
                }

            Console.WriteLine(resText);
            Console.ReadLine();
        }
    }
}

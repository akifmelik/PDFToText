
using Tesseract;
using System.Drawing;
using System.Collections.Generic;
using Pdf2Image;
using System.IO;
using System;
using System.Text;

namespace PDFToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPdfPath = @"input.pdf";
            List<Image> images = PdfSplitter.GetImages(inputPdfPath, PdfSplitter.Scale.High);
            var resText = new StringBuilder();
          
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
                            resText.Append(page.GetText());
                        }
                    }
                }

            Console.WriteLine(resText);
            Console.ReadLine();
        }
    }
}

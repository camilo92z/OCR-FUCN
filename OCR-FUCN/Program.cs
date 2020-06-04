using System;
using Tesseract;
using System.Diagnostics;

namespace OCR_FUCN
{
    static class Program
    {
        static void Main(string[] args)
        {
            var imagenPath = @"./phototest.tif";
            if (args.Length > 0)
            {
                imagenPath = args[0];
            }

            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagenPath))
                    {
                        using (var page = engine.Process(img))
                        {
                            Console.WriteLine("Confianza media: {0}", page.GetMeanConfidence());
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();
                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                Console.Write(" ");
                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                Console.WriteLine();
                                            }

                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Error inesperado: " + e.Message);
                Console.WriteLine("Detalles: ");
                Console.WriteLine(e.ToString());
            }
            Console.Write("Presione cualquier tecla para seguir . . . ");
            Console.ReadKey(true);
        }
    }
}

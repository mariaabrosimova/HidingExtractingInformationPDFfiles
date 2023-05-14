using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf.parser;
using Dotnet = System.Drawing.Image;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Aspose.Pdf;
//using Aspose.PDF;
namespace CURS_PDF_Encoder
{
    
    public partial class PDF_Parsing : Form
    {
        //класс извлечения из pdf-файла bitmap-ов c их последующим сохранением
        public partial class PDF_ImgExtraction
        {   
            string imgPath= "C://Users//Мария//Desktop//Курсовая//";

            public void ExtractImage(string pdfFile)
            {
                int i = 0;
                //string filename = OPF.FileName;

                PdfReader pdfReader = new PdfReader(pdfFile);
                for (int pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
                {
                    PdfReader pdf = new PdfReader(pdfFile);
                    PdfDictionary pg = pdf.GetPageN(pageNumber);
                    PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                    PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
                    foreach (PdfName name in xobj.Keys)
                    {
                        i++;
                        PdfObject obj = xobj.Get(name);
                        if (obj.IsIndirect())
                        {
                            PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                            string width = tg.Get(PdfName.WIDTH).ToString();
                            string height = tg.Get(PdfName.HEIGHT).ToString();
                            //string x= tg.Get(PdfName.X).ToString();
                            //string y = tg.Get(PdfName.).ToString();
                            ImageRenderInfo imgRI = ImageRenderInfo.CreateForXObject(new iTextSharp.text.pdf.parser.Matrix(float.Parse(width), float.Parse(height)), (PRIndirectReference)obj, tg);
                            RenderImage(imgRI, i);
                            PdfReader.KillIndirect(obj);


                        }

                        //iTextSharp.text.Image compressedImage = iTextSharp.text.Image.GetInstance(newBytes);
                        ////Kill off the old image
                        //PdfReader.KillIndirect(obj);
                        ////Add our image in its place

                    }
                    
                }


                //using (FileStream fs = new FileStream(smallPDF, FileMode.Create, FileAccess.Write, FileShare.None))
                //{
                //    //Bind a stamper to the file and our reader
                //    using (PdfStamper stamper = new PdfStamper(reader, fs))
                //    {
                //        var oldImage = obj;
                //        var newImage = "X.bmp";
                //        PdfReader.KillIndirect(oldImage);
                //        //yourPdfWriter.AddDirectImageSimple(newImage, (PRIndirectReference)oldImage);
                //        pdfReader.Writer.AddDirectImageSimple(newImage, (PRIndirectReference)obj);
                //    }
                //}

            }
                    public void RenderImage(ImageRenderInfo renderInfo, int i)
            {
                PdfImageObject image = renderInfo.GetImage();
                using (Dotnet dotnetImg = image.GetDrawingImage())
                {
                    if (dotnetImg != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            dotnetImg.Save(ms, ImageFormat.Tiff);
                            Bitmap d = new Bitmap(dotnetImg);
                            //d.Save(imgPath); //
                            d.Save(imgPath+ i+ ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                }
            }

            
            
        }
        
        public PDF_Parsing()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PDF_ImgExtraction PDF1 = new PDF_ImgExtraction();
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "PDF files(*.pdf)|*.pdf";

            if (OPF.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = OPF.FileName;
            PDF1.ExtractImage(filename);//метод, который извлекает из pdf-файла bmp-файлы
            //зашифруем в первый из них текстовое сообщение

            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document("C://Users//Мария//Desktop//Курсовая//Курсовая.pdf");

            // Заменить конкретное изображение
            pdfDocument.Pages[1].Resources.Images.Replace(1, new FileStream("C://Users//Мария//Desktop//Курсовая//X.bmp", FileMode.Open));//;

            // Сохранить обновленный файл PDF
            pdfDocument.Save("C://Users//Мария//Desktop//Курсовая//Курсовая.pdf");

          
        }
    }


   


}




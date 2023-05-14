
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf.parser;
using Dotnet = System.Drawing.Image;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Aspose.Pdf;
using System.Collections;
using BitMiracle.Docotic.Pdf;



using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing.Imaging;
using Aspose.Pdf;
using BitMiracle.Docotic.Pdf;
using System.IO;
namespace CURS_PDF_Encoder

{
    public partial class Form1 : Form
    {
        string pdf_filename;
        string txt_filename;
        string directoryPath;
        string password;
        string key_str = "";
        bool is_shifred = false;
        string shifr_pdf_filename;
        bool flag_decode = false;
        string directoryPath_shifr_pdf;
        public Form1()
        {
            InitializeComponent();
        }

        //----------------------------------------------------методы--------------------------------------------
        //класс извлечения из pdf-файла bitmap-ов c их последующим сохранением
        public partial class PDF_ImgExtraction
        {
            //string imgPath = "C://Users//Мария//Desktop//Курсовая//";
            //извлекаем из PDF файла изображения
            public void ExtractImage(string pdfFile, string imgPath, bool flag_decode)
            {
                int i = 0;
                PdfReader pdfReader = new PdfReader(pdfFile);
                for (int pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
                {
                    PdfReader pdf = new PdfReader(pdfFile);
                    PdfDictionary pg = pdf.GetPageN(pageNumber);
                    PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
                    PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));

                    if (xobj != null)
                    {
                        foreach (PdfName name in xobj.Keys)
                        {
                            i++;
                            PdfObject obj = xobj.Get(name);
                            if (obj.IsIndirect())
                            {
                                PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                                string width = tg.Get(PdfName.WIDTH).ToString();
                                string height = tg.Get(PdfName.HEIGHT).ToString();
                                ImageRenderInfo imgRI = ImageRenderInfo.CreateForXObject(new iTextSharp.text.pdf.parser.Matrix(float.Parse(width), float.Parse(height)), (PRIndirectReference)obj, tg);
                                RenderImage(imgRI, i, imgPath, flag_decode);
                                PdfReader.KillIndirect(obj);


                            }
                        }
                    }
                    else { MessageBox.Show("Выбранный PDF - файл не содержит изображений. Пожалуйста, выберите PDF - файл, содержащий изображения", "Ошибка"); }
                }
            }
            //сохраняем изображения, полученные в ExtractImage в формате bmp
            public void RenderImage(ImageRenderInfo renderInfo, int i, string imgPath, bool flag_decode)
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
                            if (flag_decode)
                            { d.Save(imgPath + "\\" + "decode" + i + ".bmp"); } //, System.Drawing.Imaging.ImageFormat.Bmp
                            else { d.Save(imgPath + "\\" + i + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp); }
                        }
                    }
                }
            }



        }



        //----------------------------------------------------Least Significant Bit--------------------------------------------
        const int ENCRYP_PESENT_SIZE = 1;
        const int ENCRYP_TEXT_SIZE = 3;
        const int ENCRYP_TEXT_MAX_SIZE = 999;

        private BitArray ByteToBit(byte src)
        {
            BitArray bitArray = new BitArray(8);
            bool st = false;
            for (int i = 0; i < 8; i++)
            {
                if ((src >> i & 1) == 1)
                {
                    st = true;
                }
                else st = false;
                bitArray[i] = st;
            }
            return bitArray;
        }

        private byte BitToByte(BitArray scr)
        {
            byte num = 0;
            for (int i = 0; i < scr.Count; i++)
                if (scr[i] == true)
                    num += (byte)Math.Pow(2, i);
            return num;
        }

        /*Проверяет, зашифрован ли файл,  возвраещает true, если символ в первом пикслеле равен / иначе false */
        private bool isEncryption(Bitmap scr)
        {
            byte[] rez = new byte[1];
            System.Drawing.Color color = scr.GetPixel(0, 0);
            BitArray colorArray = ByteToBit(color.R); //получаем байт цвета и преобразуем в массив бит
            BitArray messageArray = ByteToBit(color.R); ;//инициализируем результирующий массив бит
            messageArray[0] = colorArray[0];
            messageArray[1] = colorArray[1];

            colorArray = ByteToBit(color.G);//получаем байт цвета и преобразуем в массив бит
            messageArray[2] = colorArray[0];
            messageArray[3] = colorArray[1];
            messageArray[4] = colorArray[2];

            colorArray = ByteToBit(color.B);//получаем байт цвета и преобразуем в массив бит
            messageArray[5] = colorArray[0];
            messageArray[6] = colorArray[1];
            messageArray[7] = colorArray[2];
            rez[0] = BitToByte(messageArray); //получаем байт символа, записанного в 1 пикселе
            string m = Encoding.GetEncoding(1251).GetString(rez);
            if (m == "/")
            {
                return true;
            }
            else return false;
        }

        /*Нормализует количество символов для шифрования,чтобы они всегда занимали ENCRYP_TEXT_SIZE байт*/
        private byte[] NormalizeWriteCount(byte[] CountSymbols)
        {
            int PaddingByte = ENCRYP_TEXT_SIZE - CountSymbols.Length;

            byte[] WriteCount = new byte[ENCRYP_TEXT_SIZE];

            for (int j = 0; j < PaddingByte; j++)
            {
                WriteCount[j] = 0x30;
            }

            for (int j = PaddingByte; j < ENCRYP_TEXT_SIZE; j++)
            {
                WriteCount[j] = CountSymbols[j - PaddingByte];
            }
            return WriteCount;
        }

        /*Записыает количество символов для шифрования в первые биты картинки */
        private void WriteCountText(int count, Bitmap src)
        {
            byte[] CountSymbols = Encoding.GetEncoding(1251).GetBytes(count.ToString());

            if (CountSymbols.Length < ENCRYP_TEXT_SIZE)
            {
                CountSymbols = NormalizeWriteCount(CountSymbols);
            }

            for (int i = 0; i < ENCRYP_TEXT_SIZE; i++)
            {
                BitArray bitCount = ByteToBit(CountSymbols[i]); //биты количества символов
                System.Drawing.Color pColor = src.GetPixel(0, i + 1);
                BitArray bitsCurColor = ByteToBit(pColor.R); //бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[0];
                bitsCurColor[1] = bitCount[1];
                byte nR = BitToByte(bitsCurColor); //новый бит цвета пиксея

                bitsCurColor = ByteToBit(pColor.G);//бит бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[2];
                bitsCurColor[1] = bitCount[3];
                bitsCurColor[2] = bitCount[4];
                byte nG = BitToByte(bitsCurColor);//новый цвет пиксея

                bitsCurColor = ByteToBit(pColor.B);//бит бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[5];
                bitsCurColor[1] = bitCount[6];
                bitsCurColor[2] = bitCount[7];
                byte nB = BitToByte(bitsCurColor);//новый цвет пиксея

                System.Drawing.Color nColor = System.Drawing.Color.FromArgb(nR, nG, nB); //новый цвет из полученных битов
                src.SetPixel(0, i + 1, nColor); //записали полученный цвет в картинку
            }
        }

        /*Читает количество символов для дешифрования из первых бит картинки*/
        private int ReadCountText(Bitmap src)
        {
            byte[] rez = new byte[ENCRYP_TEXT_SIZE];
            for (int i = 0; i < ENCRYP_TEXT_SIZE; i++)
            {
                System.Drawing.Color color = src.GetPixel(0, i + 1);
                BitArray colorArray = ByteToBit(color.R); //биты цвета
                BitArray bitCount = ByteToBit(color.R); ; //инициализация результирующего массива бит
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = ByteToBit(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = ByteToBit(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];
                rez[i] = BitToByte(bitCount);
            }
            string m = Encoding.GetEncoding(1251).GetString(rez);
            return Convert.ToInt32(m, 10);
        }
        //---------------------------------шифрование перестановкой на ключе, выводимом из парольной фразы;----------------------------
        public string preparing_key_pas(string pas, string pass_key, out string new_pass_key)
        { //подготовка сообщения на основе длины ключа
            string encrypted_password = string.Empty;
            int key_length = pass_key.Length;
            int pas_length = pas.Length;
            string new_pas = pas.Substring(0);

            if (key_length > pas_length)
            {
                new_pass_key = pass_key.Substring(0, pas_length); //Если длина ключа больше длины открытого текста, то ключ усекается до нужной длины.

            }
            else
            {
                new_pass_key = pass_key;
                int x = pas_length % key_length;
                if (x != 0)
                {
                    int dop = key_length - x; //сколько нулей нужно добавить в последний блок
                    new_pas += new string(' ', dop);
                }


            }
            return new_pas;
        }
        //генерация ключа перестановки на основе пароля
        public int[] Encrypt_perestanovka(string key, out string key_str)
        {
            string sort_alp = String.Concat(key.OrderBy(ch => ch)); //отсортированный ключ  //сортировка пароля по алфавиту

            int[] position = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                int m = sort_alp.IndexOf(key[i]);
                position[i] = m;    //позиция символа старой строки в строке новой 
                StringBuilder sb = new StringBuilder(sort_alp);
                sb[m] = '@';
                sort_alp = sb.ToString();
            }
            key_str = string.Join("", position); //пробел
            return position;
        }

        public string Code(string pas, int[] key)
        {
            int n = pas.Length / key.Length;
            string buf_block = "";
            string result = "";
            int begin = 0;

            for (int i = 0; i < n; i++)
            {
                buf_block = pas.Substring(begin, key.Length);
                for (int j = 0; j < key.Length; j++)
                    result += buf_block[key[j]];
                begin += key.Length;
            }

            return result;
        }


        public string Decode_perestanovka(string pas, string key)
        {
            string decoded_pas = "";

            int n = pas.Length / key.Length;
            string buf_block = "";
            string result = "";
            string res = "";
            int a;
            a = 0;
            char buf;
            for (int i = 0; i < n; i++)
            {
                Dictionary<int, string> elem = new Dictionary<int, string>();
                buf_block = pas.Substring(a, key.Length);
                for (int j = 0; j < key.Length; j++)
                {   //Преобразует строковое представление числа в эквивалентное ему 32-битовое целое число со знаком
                    elem.Add(Int32.Parse(char.ToString(key[j])), buf_block.Substring(j, 1));
                }
                foreach (var item in elem.OrderBy(x => x.Key))
                {
                    result += item.Value;
                }

                a += key.Length;



            }
            //result = result.Replace(" ", ""); //не удаляем пробелы

            return result;
        }

        //----------------------------------------------------реализация--------------------------------------------
        private void Search_pdf_Click(object sender, EventArgs e)
        {  //выбор pdf-файла
            flag_decode = false;
            PDF_ImgExtraction PDF1 = new PDF_ImgExtraction();

            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "PDF files(*.pdf)|*.pdf";

            if (OPF.ShowDialog() == DialogResult.Cancel)
                return;
            pdf_filename = OPF.FileName;
            directoryPath = System.IO.Path.GetDirectoryName(pdf_filename);//файл содержится в папке
            //MessageBox.Show(directoryPath);
            //извлечение из pdf-файла картинок и сохранение из в формате bmp
            PDF1.ExtractImage(pdf_filename, directoryPath, flag_decode);
        }

        private void RB1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB1.Checked)
            {
                OpenFileDialog dText = new OpenFileDialog();
                dText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                if (dText.ShowDialog() == DialogResult.OK)
                {
                    txt_filename = dText.FileName;
                    // MessageBox.Show(txt_filename);
                }
                else
                {
                    txt_filename = "";
                    return;
                }


            }
        }

        private void RB2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB2.Checked)
            {
                TB1.Visible = true;
            }
            else TB1.Visible = false;
        }


        private void Encrypt_Click(object sender, EventArgs e)
        {
            //string message = "";
            string preparing_message = "";
            if (RB2.Checked)
            {
                TB1.Visible = true;
                FileStream file3 = new FileStream(directoryPath + "//" + "input_message.txt", FileMode.Create);
                StreamWriter fnew3 = new StreamWriter(file3);
                fnew3.WriteLine(TB1.Text);
                fnew3.Close();
                file3.Close();
                txt_filename = directoryPath + "//" + "input_message.txt";
            }

           
            if ((checkBox1.Checked) &&  (Pas1.Text.Length == 0)) { MessageBox.Show("Введите пароль", "Ошибка"); }
            else if ((checkBox1.Checked) && (Pas1.Text != Pas2.Text))  { MessageBox.Show("Пароли не совпадают", "Ошибка"); }

            else
            { //try выбрать файл нужно обязательно

                if ((checkBox1.Checked) && (Pas1.Text.Length != 0) && (Pas1.Text == Pas2.Text))
                {
                    MessageBox.Show("Шифруем сообщение");
                    is_shifred = true;
                    StreamReader f = new StreamReader(txt_filename);
                    FileStream file2 = new FileStream(directoryPath + "//" + "Encrypted.txt", FileMode.Create);
                    StreamWriter fnew2 = new StreamWriter(file2);
                    while (!f.EndOfStream)
                    {
                        string message = f.ReadLine();
                        password = Pas1.Text;

                        preparing_message = preparing_key_pas(message/*сообщение*/, password  /*ключ шифррования*/, out preparing_message);//+
                        int[] position;
                        position = Encrypt_perestanovka(password, out key_str);//
                        string key_sort = string.Join("", preparing_message);//key;  
                        string encrypt_pass = Code(preparing_message, position);
                        //fnew2.WriteLine(encrypt_pass + "#preparing_message" + preparing_message + "#key_str " + key_str + "#pas" + password);
                        fnew2.WriteLine(encrypt_pass);
                    }
                    f.Close();
                    fnew2.Close();
                }
                string FilePic = directoryPath + "//1.bmp";
                string FileText = "";
                if (is_shifred) { FileText = directoryPath + "//" + "Encrypted.txt"; }
                else FileText = txt_filename; //directoryPath + "//" + "Encrypted.txt";//txt_filename; //зашифрованный файл скрываем в bmp
                FileStream rFile;
                try
                {
                    rFile = new FileStream(FilePic, FileMode.Open); //открываем поток
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия изображения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Bitmap bPic = new Bitmap(rFile);

                FileStream rText;
                try
                {
                    rText = new FileStream(FileText, FileMode.Open); //открываем поток
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия текстового файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




                BinaryReader bText = new BinaryReader(rText, Encoding.ASCII);

                List<byte> bList = new List<byte>();
                while (bText.PeekChar() != -1)
                { //считали весь текстовый файл для шифрования в лист байт
                    bList.Add(bText.ReadByte());
                }
                int CountText = bList.Count; // в CountText - количество в байтах текста, который нужно закодировать
                bText.Close();
                rFile.Close();

                //проверям, что размер не выходит за рамки максимального, поскольку для хранения размера используется
                //ограниченное количество байт
                if (CountText > (ENCRYP_TEXT_MAX_SIZE - ENCRYP_PESENT_SIZE - ENCRYP_TEXT_SIZE))
                {
                    MessageBox.Show("Размер текста велик для данного алгоритма, уменьшите размер", "Информация", MessageBoxButtons.OK);
                    return;
                }

                //проверяем, поместится ли исходный текст в картинке
                if (CountText > (bPic.Width * bPic.Height))
                {
                    MessageBox.Show("Выбранная картинка мала для размещения выбранного текста", "Информация", MessageBoxButtons.OK);
                    return;
                }

                //проверяем, может быть картинка уже зашифрована
                if (isEncryption(bPic))
                {
                    MessageBox.Show("Файл уже зашифрован", "Информация", MessageBoxButtons.OK);
                    return;
                }

                byte[] Symbol = Encoding.GetEncoding(1251).GetBytes("/");
                BitArray ArrBeginSymbol = ByteToBit(Symbol[0]);
                System.Drawing.Color curColor = bPic.GetPixel(0, 0);
                BitArray tempArray = ByteToBit(curColor.R);
                tempArray[0] = ArrBeginSymbol[0];
                tempArray[1] = ArrBeginSymbol[1];
                byte nR = BitToByte(tempArray);

                tempArray = ByteToBit(curColor.G);
                tempArray[0] = ArrBeginSymbol[2];
                tempArray[1] = ArrBeginSymbol[3];
                tempArray[2] = ArrBeginSymbol[4];
                byte nG = BitToByte(tempArray);

                tempArray = ByteToBit(curColor.B);
                tempArray[0] = ArrBeginSymbol[5];
                tempArray[1] = ArrBeginSymbol[6];
                tempArray[2] = ArrBeginSymbol[7];
                byte nB = BitToByte(tempArray);

                System.Drawing.Color nColor = System.Drawing.Color.FromArgb(nR, nG, nB);
                bPic.SetPixel(0, 0, nColor);
                //то есть в первом пикселе будет символ /, который говорит о том, что картинка зашифрована

                WriteCountText(CountText, bPic); //записываем количество символов для шифрования

                int index = 0;
                bool st = false;
                for (int i = ENCRYP_TEXT_SIZE + 1; i < bPic.Width; i++)
                {
                    for (int j = 0; j < bPic.Height; j++)
                    {
                        System.Drawing.Color pixelColor = bPic.GetPixel(i, j);
                        if (index == bList.Count)
                        {
                            st = true;
                            break;
                        }
                        BitArray colorArray = ByteToBit(pixelColor.R);
                        BitArray messageArray = ByteToBit(bList[index]);
                        colorArray[0] = messageArray[0]; //меняем
                        colorArray[1] = messageArray[1]; // в нашем цвете биты
                        byte newR = BitToByte(colorArray);

                        colorArray = ByteToBit(pixelColor.G);
                        colorArray[0] = messageArray[2];
                        colorArray[1] = messageArray[3];
                        colorArray[2] = messageArray[4];
                        byte newG = BitToByte(colorArray);

                        colorArray = ByteToBit(pixelColor.B);
                        colorArray[0] = messageArray[5];
                        colorArray[1] = messageArray[6];
                        colorArray[2] = messageArray[7];
                        byte newB = BitToByte(colorArray);

                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(newR, newG, newB);
                        bPic.SetPixel(i, j, newColor);
                        index++;
                    }
                    if (st)
                    {
                        break;
                    }
                }
                //pictureBox1.Image = bPic;



                //картинка со скрытым сообщением
                string sFilePic = directoryPath + "//bmpwithtext.bmp";
                FileStream wFile;
                try
                {
                    wFile = new FileStream(sFilePic, FileMode.Create); //открываем поток на запись результатов
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bPic.Save(wFile, System.Drawing.Imaging.ImageFormat.Bmp);

                wFile.Close(); //закрываем поток   -   -------------------



                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdf_filename);

                FileStream strim_pdf = new FileStream(sFilePic, FileMode.Open);
                try
                {
                    pdfDocument.Pages[1].Resources.Images.Replace(1, strim_pdf);//;
                    pdfDocument.Save(pdf_filename);
                    MessageBox.Show("Сообщение скрыто", "Информация");
                    //wFile = new FileStream(sFilePic, FileMode.Create); //открываем поток на запись результатов
                }
                catch (IOException)
                {
                    MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                strim_pdf.Close();
                Pas1.Text = "";
                Pas2.Text = "";


            }

        }


        private void Decrypt_Click(object sender, EventArgs e)
        {
            string FilePic = directoryPath + "//bmpwithtext.bmp"; 
            FileStream rFile;
            try
            {
                rFile = new FileStream(FilePic, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bPic = new Bitmap(rFile);
            if (!isEncryption(bPic))
            {
                MessageBox.Show("В файле нет зашифрованной информации", "Информация", MessageBoxButtons.OK);
                rFile.Close();
                return;
            }

            int countSymbol = ReadCountText(bPic); //считали количество зашифрованных символов
            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            for (int i = ENCRYP_TEXT_SIZE + 1; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    System.Drawing.Color pixelColor = bPic.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(pixelColor.R); ;
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = ByteToBit(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = ByteToBit(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];
                    message[index] = BitToByte(messageArray);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            string strMessage = Encoding.GetEncoding(1251).GetString(message);

           


            if (!is_shifred) { // если файл зашифрован
                FileStream wFile; string sFileText = directoryPath + "//" + "Извлеченная_информация.txt";
            try
            {
                wFile = new FileStream(sFileText, FileMode.Create); //открываем поток на запись результатов
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rFile.Close();
                return;
            }


            StreamWriter wText = new StreamWriter(wFile, Encoding.Default);
            //wText.Write(Decode_perestanovka(strMessage, key_str));
            wText.Write(strMessage);
            MessageBox.Show("Текст записан в файл Извлеченная_информация.txt", "Информация", MessageBoxButtons.OK);
            wText.Close();
            wFile.Close(); //закрываем поток

            }


            else
            {
                {
                    if ((password == P3.Text) && (is_shifred))
                    {
                        FileStream wFile; string sFileText = directoryPath + "//" + "Извлеченный_зашифрованный.txt";
                        try
                        {
                            wFile = new FileStream(sFileText, FileMode.Create); //открываем поток на запись результатов
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            rFile.Close();
                            return;
                        }


                        StreamWriter wText = new StreamWriter(wFile, Encoding.Default);
                        //wText.Write(Decode_perestanovka(strMessage, key_str));
                        wText.Write(strMessage);
                        MessageBox.Show("Текст записан в файл Извлеченная_информация.txt", "Информация", MessageBoxButtons.OK);
                        wText.Close();
                        wFile.Close(); //закрываем поток





                        StreamReader f3 = new StreamReader((directoryPath + "//" + "Извлеченный_зашифрованный.txt"));
                        FileStream file3 = new FileStream(directoryPath + "//" + "Извлеченная_информация.txt", FileMode.Create);
                        StreamWriter fnew3 = new StreamWriter(file3);
                        while (!f3.EndOfStream)
                        {


                            string encrypt_pass = f3.ReadLine();
                            string key_sort = string.Join("", password);//key; 
                            string decode = Decode_perestanovka(encrypt_pass, key_str); // string decoded_pass = Decode( encrypt_pass , key_sort); //подставляем сортированный ключ то есть логин
                            fnew3.WriteLine(decode);

                        }
                        f3.Close();
                        fnew3.Close();
                    }
                    else { MessageBox.Show("Неправильный пароль", "Ошибка"); }
                }



                rFile.Close();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            is_shifred = !is_shifred;
        }
    }





}

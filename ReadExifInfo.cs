using System;
using System.IO;
using System.Windows.Media.Imaging;


namespace FotoSort
{
    class ReadExifInfo
    {    //для чтения EXIF информации из файла
        private DateTime creationTime;
        private FileStream Foto;
        private BitmapMetadata TmpImgEXIF;

        public ReadExifInfo(string fileName)
        { //конструктор
            try
            {
                Foto = File.Open(fileName, FileMode.Open, FileAccess.Read); // открыли файл по адресу fileName для чтения
            }
            catch (Exception)
            {
                Console.WriteLine("Невозможно открыть файл");
            }
            try
            {
                JpegBitmapDecoder decoder = new JpegBitmapDecoder(Foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
                TmpImgEXIF = (BitmapMetadata)decoder.Frames[0].Metadata.Clone(); //считали и сохранили метаданные
                creationTime = Convert.ToDateTime(TmpImgEXIF.DateTaken);    

                Foto.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Невозможно прочитать EXIF информацию.");
                Foto.Close();
            }
        }

        public DateTime CreateTime
        { 
            get { return creationTime; }
        }
    }
}

using System;
using System.Text;
using System.IO;

namespace SizeDirectory
{
    class Program
    {
        /// посчитаем размер каталога на C#. Размер каталога — это сумма размеров всех файлов, 
        /// которые в нём находятся. По сути, нам нужно получить размер всех файлов в исходном каталоге,
        /// затем, если в данном каталоге есть ещё директории, рекурсивно вызвать для них тот же самый метод.
        static void Main(string[] args)
        {
            string pathToDirectory = "D:\\Catalog";
            double catalogSize = 0;
            catalogSize = sizeOfFolder(pathToDirectory, ref catalogSize); //Вызываем наш рекурсивный метод
            if (catalogSize != 0)
            {
                Console.WriteLine("Размер каталога {0} составляет {1} ГБ", pathToDirectory, catalogSize);
            }
            else
            {
                Console.WriteLine("Каталог {0} пуст.", pathToDirectory);
            }
            Console.ReadLine();
        }

        static double sizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                //В переменную catalogSize будем записывать размеры всех файлов, с каждым
                //новым файлом перезаписывая данную переменную
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                //В цикле пробегаемся по всем файлам директории di и складываем их размеры
                foreach (FileInfo f in fi)
                {
                    //Записываем размер файла в байтах
                    catalogSize = catalogSize + f.Length;
                }
                //В цикле пробегаемся по всем вложенным директориям директории di 
                foreach (DirectoryInfo df in diA)
                {
                    //рекурсивно вызываем наш метод
                    sizeOfFolder(df.FullName, ref catalogSize);
                }
                //1ГБ = 1024 Байта * 1024 КБайта * 1024 МБайта
                return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                return 0;
            }
        }
    }
}
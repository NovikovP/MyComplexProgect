using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Hаботу с файлами можно разделить на 2 группы: 
// 1) это работа с файлами, как с элементами файловой системы, например,
// найти файл, получить список файлов в директории, узнать дату изменения файла, 
// различные его атрибуты, расширение, скопировать или удалить файл, создать новый.
// 2) это работа с содержимым файла: прочитать файл или записать в него что-нибудь

namespace FileWorking
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToFile = "C:\\Temp\\cities.txt";
            // можно также указать вот так pathToFile = @"C:\Temp\cities.txt";
            // выведем содержимое файла целиком
            string readAllFile = File.ReadAllText(pathToFile);
            Console.WriteLine(readAllFile); // выйдет весь файл
            string[] readEveryLine = new string[5];
            // записываем в массив каждую строку файла
            readEveryLine = File.ReadAllLines(pathToFile);
            // выводим содержимое массива
            for (int i = 0; i < readEveryLine.Length; i++)
                Console.Write(readEveryLine[i] + " | ");
            // Попробуем добавить новый город в наш файл
            // Данная команда перезатрёт наш файл, и в нём будет только "Нижний Новгород"
            //File.WriteAllText(pathToFile, "Нижний Новгород");
            // Следующая команда допишет новый город в конец файла
            File.AppendAllText(pathToFile, Environment.NewLine); // Добавим новую строку в наш файл
            File.AppendAllText(pathToFile, "Нижний Новгород");
            Console.ReadLine();

            //----------------------

            string pathToDirectory = @"C:\Windows\System32";
            // Получаем пользовательскую дату
            // Будем вводить в формате 17.05.2019 без проверки на корректность ввода
            Console.Write("Введите дату: ");
            string userDate = Console.ReadLine();
            userDate += " 00:00:00";
            Console.WriteLine("Вы ввели дату: {0}", userDate);
            // сохраняем названия всех файлов в переменную allFiles (массив строк)
            DirectoryInfo dI = new DirectoryInfo(pathToDirectory);
            FileInfo[] allFiles = dI.GetFiles();
            try
            {
                // В цикле пройдём по всем файлам и проверим их дату изменения
                foreach (FileInfo fi in allFiles)
                {
                    // Если дата изменения файла старше нашей
                    if (fi.LastWriteTime > DateTime.Parse(userDate))
                    {
                        // выводим этот файл
                        Console.WriteLine("{0} | {1}", fi.Name, fi.LastWriteTime.ToString());
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Произошла ошибка: {0}", ex.Message);
            }

            Console.ReadLine();

            //----------------------

            string pathToFiles = @"C:\Temp\cities.txt";
            // Скопируем наш файл на локальный диск C
            // true говорит о том, что файл будет перезаписан
            File.Copy(pathToFiles, @"C:\Temp\temp\cities.txt", true);
            // Проверим существует ли файл с помощью тернарного оператора
            string message = (File.Exists(pathToFiles)) ? "Файл существует" : "Файл не существует";
            Console.WriteLine(message);
            // Удалим наш файл с диска C
            File.Delete(@"C:\Temp\temp\cities.txt");
            Console.ReadLine();

        }
    }
}

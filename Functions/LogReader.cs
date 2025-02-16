namespace WebApplication1.Functions
{
    static class LogReader
    {
        public static void ReadLogs()
        {
            string filePath = "path_to_your_log_file.log";

            // Создаем FileSystemWatcher для отслеживания изменений в файле
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = Path.GetDirectoryName(filePath); // Путь к папке с файлом
                watcher.Filter = Path.GetFileName(filePath);    // Имя файла
                watcher.NotifyFilter = NotifyFilters.LastWrite; // Отслеживаем изменения в файле

                // Обработчик события изменения файла
                watcher.Changed += (sender, e) =>
                {
                    ReadNewLines(filePath);
                };

                // Запускаем отслеживание
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Нажмите Enter для выхода...");
                Console.ReadLine(); // Ожидаем, пока пользователь не нажмет Enter
            }
        }

        static void ReadNewLines(string filePath)
        {
            try
            {
                // Открываем файл для чтения
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(fs))
                {
                    // Читаем все строки до конца файла
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line); // Выводим новую строку
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }
    }
}

using System;
using System.IO;
using Newtonsoft.Json;

namespace TestFlightUploader
{
    class Program
    {
        private static FileSystemWatcher watcher;

        static void Main(string[] args)
        {
            string watchPath = args[0];

            if (!Directory.Exists(watchPath))
                throw new DirectoryNotFoundException(watchPath + " does not exist");

            Console.WriteLine("Waiting for changes...");
            
            watcher = new FileSystemWatcher
                                       {
                                           Path = watchPath,
                                           NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite
                                       };
            watcher.Created += fs_Changed;
            watcher.Changed += fs_Changed;
            watcher.EnableRaisingEvents = true;
            Console.ReadKey();
        }

        private static void fs_Changed(object sender, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;

            Console.WriteLine("Change detected : " + e.Name + " (" + e.ChangeType + ")");

            var package = ReadPackageJsonFile();

            var filename = Path.GetFileName(package.File);

            System.Threading.Thread.Sleep(5000);

            if (e.Name == filename)
            {
                package.File = e.FullPath;

                Console.WriteLine("Uploading " + e.Name + " to TestFlight ...");
                try
                {
                    TestFlightApi.Upload(package);

                    Console.WriteLine("Upload Completed Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }                
            }

            watcher.EnableRaisingEvents = true;
        }

        private static TestFlightPackage ReadPackageJsonFile()
        {
            var re = new StreamReader(@"sample.json");
            var reader = new JsonTextReader(re);
            var se = new JsonSerializer();
            var package = se.Deserialize<TestFlightPackage>(reader);

            return package;
        }
    }
}

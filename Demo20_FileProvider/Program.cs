using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace Demo20_FileProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var physicalFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var fileInfos = physicalFileProvider.GetDirectoryContents("/");
            foreach (var fileInfo in fileInfos)
            {
                Console.WriteLine(fileInfo.Name);
            }
            var embeddedFileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var info = embeddedFileProvider.GetFileInfo("emb.html");
            Console.WriteLine(info.Name);

            var compositeFileProvider = new CompositeFileProvider(physicalFileProvider, embeddedFileProvider);
            var compositeFiles = compositeFileProvider.GetDirectoryContents("/");
            foreach (var file in compositeFiles)
            {
                Console.WriteLine($"composite:{file.Name}");
            }

            Console.Read();
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using SQLitePCL;

namespace TranslationManagement.Api.Services.JobFileService
{
    public class JobFileService : IJobFileService
    {
        private static readonly string[] SUPPORTED_FILE_TYPES = { ".txt", ".xml" };

        public async Task<(string content, string? customerName)> ReadFileContent(IFormFile file)
        {
            string content;
            string customerName = null;
            var fileExtension = Path.GetExtension(file.FileName);
            if (!SUPPORTED_FILE_TYPES.Contains(fileExtension))
            {
                throw new NotSupportedException($"Invalid file extension {fileExtension}");
            }

            Console.WriteLine($"Reading file with extension {fileExtension}");
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                if (fileExtension == ".txt")
                {
                    content = await ReadTextFile(stream);
                }
                else
                {
                    (content, customerName) = ReadXmlFile(stream);
                }
            }

            return (content, customerName);
        }

        private Task<string> ReadTextFile(TextReader streamReader)
        {
            return streamReader.ReadToEndAsync();
        }

        private (string content, string customerName) ReadXmlFile(TextReader streamReader)
        {
            var xdoc = XDocument.Parse(streamReader.ReadToEnd());
            return (xdoc.Root.Element("Content").Value, xdoc.Root.Element("Customer").Value);
        }
    }
}
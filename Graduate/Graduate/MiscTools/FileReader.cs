using System;
using System.IO;

namespace Graduate.Core.MiscTools
{
    public class FileReader
    {
        Stream fileStream = null;

        public FileReader(Stream fileStream)
        {
            this.fileStream = fileStream;
        }


        public String readFile()
        {
            String fileContent;
            try
            {

                using (StreamReader fileReader = new StreamReader(fileStream))
                {
                    fileContent = fileReader.ReadToEnd();
                    fileReader.Dispose();
                    return fileContent;
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new System.IO.FileNotFoundException("File not Found", e);
            }

        }
    }
}

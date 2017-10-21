using System;
using System.Collections.Generic;
using System.IO;

namespace Agent.Connector.PSA.Citroen
{
    public class LexiaAdditionalFilePathSearcher
    {
        private readonly string filepathArgName;
        private readonly string csv;
        private readonly string asterTxt;

        private string initialXmlFilePath;

        public LexiaAdditionalFilePathSearcher(string initialXmlFilePath)
        {
            if (String.IsNullOrEmpty(initialXmlFilePath))
            {
                throw new ArgumentNullException(filepathArgName);
            }
            this.initialXmlFilePath = initialXmlFilePath;

            filepathArgName = "initialXmlFilePath";
            csv = "csv"; // csv
            asterTxt = "*.txt"; // *.txt
        }

        public IList<string> Search()
        {
            IList<string> result = new List<string>();

            // try to get csv if it's graph data
            string csvFilePath = 
                String.Format("{0}{1}",
                initialXmlFilePath.Substring(0, 
                initialXmlFilePath.Length - 3), csv);
            if (File.Exists(csvFilePath))
            {
                result.Add(csvFilePath);
            }

            // try to get all localized files - for trace
            FileInfo info = new FileInfo(initialXmlFilePath);
            DirectoryInfo dataDirectory = info.Directory;
            string pureName =
                    Path.GetFileNameWithoutExtension(initialXmlFilePath);
            foreach (FileInfo txtFileInfo in 
                dataDirectory.EnumerateFiles(asterTxt))
            {
                if (!String.IsNullOrEmpty(pureName) &&
                    txtFileInfo.Name.Contains(pureName))
                {
                    result.Add(txtFileInfo.FullName);
                }
            }
            return result;
        }
    }
}

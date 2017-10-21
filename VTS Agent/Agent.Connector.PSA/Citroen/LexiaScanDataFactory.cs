using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Agent.Common.Data;
using Agent.Connector.PSA.GraphTypeData;

namespace Agent.Connector.PSA.Citroen
{
    /// <summary>
    /// Creates LexiaScanDataobjects from xml and csv files.
    /// </summary>
    public static class LexiaScanDataFactory
    {
        private static readonly char[] LabelsSeparator = new[] { ';' };

        /// <summary>
        /// Creates from XML and loads labels from csv.
        /// </summary>
        public static LexiaGraphSessionRawData CreateWithCsv(
            string xmlFilePath, string csvFilePath)
        {
            LexiaGraphSessionRawData result = Create(xmlFilePath);
            UpdateWithCsvData(result, csvFilePath);
            return result;
        }

        private static LexiaGraphSessionRawData Create(string xmlFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlFilePath);
            string[] split = fileName.Split('&');
            int year = Int32.Parse(split[0].Substring(0, 4));
            int month = Int32.Parse(split[0].Substring(4, 2));
            int day = Int32.Parse(split[0].Substring(6, 2));
            int mileage = Int32.Parse(split[1]);
            string vehicleModelName = split[3];
            DateTime date = new DateTime(year, month, day);
            if (!File.Exists(xmlFilePath))
            {
                throw new ArgumentException("file does not exist!");
            }
            XmlSerializer srl = new XmlSerializer(
                typeof(LexiaGraphSessionRawData));
            LexiaGraphSessionRawData data = null;
            using (FileStream stream = new FileStream(xmlFilePath, FileMode.Open))
            {
                data = (LexiaGraphSessionRawData)srl.Deserialize(stream);
                if (data != null)
                {
                    data.SourceFileName = xmlFilePath;
                }
            }
            if (String.IsNullOrEmpty(data.SessionInformation.Date))
            {
                data.SessionInformation.Date = date.ToString();
            }
            if (data.SessionInformation.Vehicle.Contains("@"))
            {
                data.SessionInformation.Vehicle = vehicleModelName;
            }
            if (data.Mileage == 0)
            {
                data.Mileage = mileage;
            }
            return data;
        }

        private static void UpdateWithCsvData(
            LexiaGraphSessionRawData dataToUpdate, string sourceCsvFilePath)
        {
            if (!File.Exists(sourceCsvFilePath))
            {
                throw new ArgumentNullException(
                    Cipher.Decrypt("D4crmV7R0TDlcW3CamkthnTEKVqyEr1DejV2Afcmqhk=", "Int32"));
                //"sourceCsvFilePath");
            }
            if (dataToUpdate == null)
            {
                string s = Cipher.Decrypt("QhMahI5v+7DMVfkhkAh+CA==", "Int32");
                s.ToString();
                return;
                //throw new ArgumentNullException(s);
            }
            using (StreamReader reader = new StreamReader(sourceCsvFilePath))
            {
                if (!reader.EndOfStream)
                {
                    // we need the second line with headers information;
                    reader.ReadLine();
                    string headersLine = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(headersLine))
                    {
                        string s = Cipher.Decrypt(
                            "UgUtkt2sAZwTUywVUewpRHbSEWoeMnd77CFCuMVhN6jAgSM7DvGzinNPIIvu1i0k",
                            "Int32");
                        throw new ArgumentException(
                            String.Format(s, sourceCsvFilePath));
                        // Error! Cannot read headers line from {0} file!
                    }
                    IList<string> labelsList =
                        new List<string>(headersLine.TrimEnd(
                            LabelsSeparator).Split(LabelsSeparator));
                    labelsList.RemoveAt(0); // remove milliseconds, 
                    //we need Y axis labels only.
                    if (labelsList.Count != dataToUpdate.Channels.Count)
                    {
                        string s = Cipher.Decrypt(
                            "5ZVSoAIlslPoknadYKzy/ryzJm1Dj08UBVOMGcGigVwcjfOGM4Z9QKtvbox0/tIXvM9LqxayJIAyPVJo0UQXsA==", "Int32");
                        throw new ArgumentException(s, sourceCsvFilePath);
                    }
                    for (int i = 0; i < dataToUpdate.Channels.Count; i++)
                    {
                        dataToUpdate.Channels[i].Header.Label = labelsList[i];
                    }
                }
            }
        }
    }
}

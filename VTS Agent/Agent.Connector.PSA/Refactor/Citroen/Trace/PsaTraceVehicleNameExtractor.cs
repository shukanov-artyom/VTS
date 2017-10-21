using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Agent.Connector.PSA.Refactor.Common;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    public class PsaTraceVehicleNameExtractor
    {
        private readonly FileInfo traceFile;
        private readonly string chapitre;
        private readonly string code;
        private readonly string oo2;
        private readonly string vehicule;
        private readonly string phrase;
        //.txt
        private readonly string txt = ".txt";
        //phrasethesau
        private readonly string phrasethesau = "phrasethesau";
        //vehicle model
        private readonly string vehicleModel = "vehicle model";
        //Famille
        private readonly string famille = "Famille";
        //hicule
        private readonly string hicule = "hicule";

        public PsaTraceVehicleNameExtractor(string filePath)
        {
            traceFile = new FileInfo(filePath);

            chapitre = "chapitre"; //"chapitre"
            code = "code"; //"code"
            oo2 = "002"; // 002
            vehicule = "vehicule"; //vehicule
            phrase = "phrase"; //phrase
        }

        public string Get()
        {
            // Plan A: search the xml
            using (FileStream stream = new FileStream(
                traceFile.FullName, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                IEnumerable<XElement> chapter1s = xDoc.Root.Elements(chapitre)
                    .Where(e => e.Attribute(code).Value == oo2);
                foreach (XElement ch1 in chapter1s)
                {
                    XElement veh = ch1.Element(vehicule);
                    if (veh != null)
                    {
                        XElement phr = veh.Element(phrase);
                        if (phr != null)
                        {
                            //XElement pra = phr.Element(phrasethesau);
                            string res = phr.Element(phrasethesau).Value;
                            if (!String.IsNullOrEmpty(res))
                            {
                                if (KnownVehicleNames.Knows(res))
                                {
                                    return KnownVehicleNames.Get(res);
                                }
                            }
                        }
                    }
                }
            }

            // Plan B: no data in xml, let's search the txt
            DirectoryInfo dir = traceFile.Directory;
            string fileMask = traceFile.Name.
                Remove(traceFile.Name.Length - 4);
            string fileMask2 = String.Format("{0}*{1}", fileMask, txt);
            foreach (FileInfo file in dir.EnumerateFiles(fileMask2))
            {
                string s = String.Empty;
                using (StreamReader rdr = new StreamReader(file.FullName))
                {
                    while (s != null &&
                        !s.Contains(vehicleModel) &&
                        !(s.Contains(famille) && s.Contains(hicule)) &&
                        !rdr.EndOfStream)
                    {

                        s = rdr.ReadLine();
                    }
                    string vehLine = rdr.ReadLine();
                    if (!String.IsNullOrEmpty(vehLine))
                    {
                        string[] arr = vehLine.Trim(' ').Split(' ');
                        if (arr != null && arr.Length == 2)
                        {
                            if (!String.IsNullOrEmpty(arr[1]))
                            {
                                return arr[1];
                            }
                        }
                    }
                }
            }
            return String.Empty;
        }
    }
}

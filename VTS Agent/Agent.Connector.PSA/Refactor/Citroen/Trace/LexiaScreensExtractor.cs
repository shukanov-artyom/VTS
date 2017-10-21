using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal static class LexiaScreensExtractor
    {
        //chapitre
        private static readonly string chapitre = "chapitre";
        //009
        private static readonly string oo9 = "009";
        //code
        private static readonly string code = "code";
        //mpm
        private static readonly string mpm = "mpm";
        //IdEcran
        private static readonly string idEcran = "IdEcran";
        //identligne
        private static readonly string identligne = "identligne";
        //lignetitre
        private static readonly string lignetitre = "lignetitre";
        //phrase
        private static readonly string phrase = "phrase";
        //phrasethesau
        private static readonly string phrasethesau = "phrasethesau";
        //lignevaleur
        private static readonly string lignevaleur = "lignevaleur";
        //ligneunite
        private static readonly string ligneunite = "ligneunite";
        //textlibre
        private static readonly string textlibre = "textlibre";

        public static IList<LexiaScreen> GetScreens(string filePath)
        {
            IList<LexiaScreen> result = new List<LexiaScreen>();
            using (FileStream stream = 
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XDocument xDoc = XDocument.Load(stream);
                IList<XElement> chapter9s = xDoc.Root.Elements(chapitre)
                    .Where(e => e.Attribute(code).Value == oo9).ToList();
                foreach (XElement element in chapter9s) // screens
                {
                    LexiaScreen screen = new LexiaScreen();
                    screen.Name = 
                        element.Element(mpm).Attribute(idEcran).Value;

                    foreach (XElement xParameter in 
                        element.Elements(identligne))
                    {
                        LexiaRawParameterPoint point =
                            new LexiaRawParameterPoint();
                        point.ParameterName = 
                            xParameter.Element(lignetitre)
                            .Element(phrase).Element(phrasethesau).Value;
                        point.Value =
                            xParameter.Element(lignevaleur)
                                .Element(phrase).Element(textlibre).Value;
                        point.Units =
                            xParameter.Element(ligneunite)
                                .Element(phrase).Element(textlibre).Value;
                        screen.Points.Add(point);
                    }
                    result.Add(screen);
                }
            }
            return result;
        }
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using VTS.Site.DomainObjects.VendorData;

namespace VTS.Site.VehicleData.Connectors.Opel
{
    /// <summary>
    /// Throws OldVehicleNotSupportedException and VinDecodeException.
    /// </summary>
    public class ElcatsRuWebConnector : IManufacturerWebConnector
    {
        private const string SiteAddress = "http://www.elcats.ru/opel/Default.aspx";
        private const string InputElementName = "ctl00_cphMasterPage_txbVIN";
        private const string ButtonElementName = "ctl00_cphMasterPage_btnFindByVIN";

        private string vin;
        private HtmlDocument document;
        private Thread th;
        private Exception error;
        private VehicleCharacteristics result;

        public void GetCharacteristics(string vin)
        {
            this.vin = vin;
            RunBrowserThread(new Uri(SiteAddress));
        }

        private void RunBrowserThread(Uri url)
        {
            th = new Thread(() =>
            {
                var br = new WebBrowser();
                br.DocumentCompleted += OnDocumentCompleted;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser br = sender as WebBrowser;
            document = br.Document;
            HtmlElement testbox = document.GetElementById(InputElementName);
            testbox.SetAttribute("Value", vin);
            HtmlElement button = document.GetElementById(ButtonElementName);
            br.DocumentCompleted -= OnDocumentCompleted;
            br.DocumentCompleted += OnDocumentCompleted2;
            button.InvokeMember("click");
            Application.Run();
        }

        void OnDocumentCompleted2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser br = sender as WebBrowser;
            document = br.Document;
            try
            {
                VehicleCharacteristics characteristics =
                ParseDocumentAndGetCharacteristics(document);
                if (characteristics != null)
                {
                    result = characteristics;
                }
                else
                {
                    error = new Exception("No chars retrieved.");
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }
            Application.ExitThread();
        }


        public void Disconnect()
        {
            // nothing here yet
        }

        private VehicleCharacteristics
            ParseDocumentAndGetCharacteristics(HtmlDocument doc)
        {
            HtmlElementCollection tables = doc.GetElementsByTagName("table");
            if (tables.Count < 4)
            {
                throw new OldVehicleNotSupportedException(vin);
            }
            HtmlElement table4 = tables[4];
            HtmlElement table6 = tables[6];
            VehicleCharacteristicsItemsGroup basicCharacteristicsGroup =
                ParseBasicCharacteristics(table4);
            VehicleCharacteristicsItemsGroup extendedCharacteristicsGroup =
                ParseExtendedCharacteristicsGroup(table6);
            if (basicCharacteristicsGroup.Items.Count == 1 ||
                extendedCharacteristicsGroup.Items.Count == 1)
            {
                throw new VinDecodeException("No data for this vin");
            }
            VehicleCharacteristics chars =
                new VehicleCharacteristics(vin);
            chars.Language = "ru_RU";
            chars.GeneralVehicleInfo = String.Format("OPEL {0}, Engine {1}",
                basicCharacteristicsGroup.Items[0].Value,
                basicCharacteristicsGroup.Items[3].Value);
            chars.ItemsGroups.Add(basicCharacteristicsGroup);
            chars.ItemsGroups.Add(extendedCharacteristicsGroup);
            return chars;
        }

        private VehicleCharacteristicsItemsGroup
            ParseBasicCharacteristics(HtmlElement table4)
        {
            HtmlElementCollection rows = table4.GetElementsByTagName("tr");
            VehicleCharacteristicsItemsGroup result = new VehicleCharacteristicsItemsGroup();
            result.Name = "Basic characteristics";
            int detailedDataRowsCount = rows[0].GetElementsByTagName("tr").Count;
            VehicleCharacteristicsItem firstItem = new VehicleCharacteristicsItem();
            firstItem.Name = rows[0].GetElementsByTagName("td")[0].InnerText;
            firstItem.Value = rows[0].GetElementsByTagName("td")[2].InnerText;
            result.Items.Add(firstItem);
            for (int i = detailedDataRowsCount + 1; i < rows.Count; i++)
            {
                HtmlElement row = rows[i];
                VehicleCharacteristicsItem item = new VehicleCharacteristicsItem();
                item.Name = row.GetElementsByTagName("td")[0].InnerText;
                item.Value = row.GetElementsByTagName("td")[2].InnerText;
                result.Items.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Looks like it's currently accessible only in russian.
        /// Will look for other languages.
        /// </summary>
        /// <param name="table6"></param>
        /// <returns></returns>
        private VehicleCharacteristicsItemsGroup
            ParseExtendedCharacteristicsGroup(HtmlElement table6)
        {
            HtmlElementCollection rows = table6.GetElementsByTagName("tr");
            VehicleCharacteristicsItemsGroup result = new VehicleCharacteristicsItemsGroup();
            result.Name = "Extended characteristics";
            foreach (HtmlElement row in rows)
            {
                VehicleCharacteristicsItem item = new VehicleCharacteristicsItem();
                HtmlElement td1 = row.GetElementsByTagName("td")[0];
                item.Name = td1.InnerText;
                HtmlElement td2 = row.GetElementsByTagName("td")[1];
                item.Value = td2.InnerText;
                result.Items.Add(item);
            }
            return result;
        }

        public void Connect()
        {
            // nothing here yet
        }

        public VehicleCharacteristics Retrieve(string vin)
        {
            GetCharacteristics(vin);
            th.Join();
            if (error == null)
            {
                return result;
            }
            throw error;
        }
    }
}

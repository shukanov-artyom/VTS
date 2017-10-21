using System;
using HtmlAgilityPack;
using VTS.Site.DomainObjects.VendorData;

namespace VTS.Site.VehicleData
{
    public class VehicleCharacteristicsPageParser
    {
        private const string InfoZonePath = @"//div[@id='InfoZone']";
        private readonly string vin;
        private readonly string rawPage;
        private readonly string lang;

        public VehicleCharacteristicsPageParser(string vin, string rawPage, string lang)
        {
            this.vin = vin;
            this.rawPage = rawPage;
            this.lang = lang;
        }

        public VehicleCharacteristics Parse()
        {
            VehicleCharacteristics result = new VehicleCharacteristics(vin);
            result.Language = lang;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawPage);
            result.GeneralVehicleInfo = GetGeneralVehicleInfo(doc);
            ProcessInfoZone(doc.DocumentNode.
                SelectSingleNode(InfoZonePath), result);
            return result;
        }

        private string GetGeneralVehicleInfo(HtmlDocument doc)
        {
            HtmlNodeCollection infoNodes = doc.DocumentNode.SelectNodes("//p[@class]");
            return Strip(infoNodes[0].InnerText);
        }

        private void ProcessInfoZone(HtmlNode infoZone, VehicleCharacteristics result)
        {
            if (infoZone == null)
            {
                return;
                // most likely incorrect vin!
                // let's check it!
                /*if (rawPage.Contains(@"<p class=""alert"">"))
                {
                    return;
                }*/
            }
            HtmlNodeCollection tables = infoZone.SelectNodes("./table");
            foreach (HtmlNode tableNode in tables)
            {
                ParseTable(tableNode, result);
            }
        }

        private void ParseTable(HtmlNode tableNode,
            VehicleCharacteristics result)
        {
            if (tableNode.Attributes.Contains("class") &&
                tableNode.Attributes["class"].Value.Contains("table_lst_doc"))
            {
                ParseGeneralInfoTable(tableNode, result);
            }
            else
            {
                try
                {
                    ParseUsualTable(tableNode, result);
                }
                catch (NullReferenceException)
                {
                    // Table could not be parsed
                    // TODO : log it?
                }
            }
        }

        private void ParseGeneralInfoTable(HtmlNode tableNode,
            VehicleCharacteristics result)
        {
            //HtmlNode tbody = tableNode.SelectSingleNode("./tbody");
            VehicleCharacteristicsItemsGroup itemsGroup =
                new VehicleCharacteristicsItemsGroup();
            itemsGroup.Name = result.GeneralVehicleInfo;
            // TODO : Get translation for "general info"
            foreach (HtmlNode node in tableNode.SelectNodes("./tr"))
            {
                ParseGeneralInfoItemItemNode(node, itemsGroup);
            }
            result.ItemsGroups.Add(itemsGroup);
        }

        private void ParseUsualTable(HtmlNode tableNode,
            VehicleCharacteristics result)
        {
            VehicleCharacteristicsItemsGroup group =
                new VehicleCharacteristicsItemsGroup();
            HtmlNodeCollection trNodes = tableNode.SelectNodes("./tr");
            HtmlNodeCollection tds = trNodes[0].SelectNodes("./td");
            group.Name = Strip(tds[0].InnerText);
            HtmlNode subTable = trNodes[1].SelectSingleNode("./td/table");
            HtmlNodeCollection itemTrNodes = subTable.SelectNodes("./tr");
            for (int i = 1; i < itemTrNodes.Count; i++)
            {
                ParseUsualItemNode(itemTrNodes[i], group);
            }
            result.ItemsGroups.Add(group);
        }

        private void ParseUsualItemNode(HtmlNode trNode,
            VehicleCharacteristicsItemsGroup group)
        {
            VehicleCharacteristicsItem item =
                new VehicleCharacteristicsItem();
            item.Name = Strip(trNode.SelectSingleNode("./td[1]").InnerText);
            item.Value = Strip(trNode.SelectSingleNode("./td[2]").InnerText);
            // TODO : Parse Code
            //item.Code = 
            group.Items.Add(item);
        }

        private void ParseGeneralInfoItemItemNode(HtmlNode node,
            VehicleCharacteristicsItemsGroup itemsGroup)
        {
            VehicleCharacteristicsItem item =
                new VehicleCharacteristicsItem();
            HtmlNode name = node.SelectSingleNode(@"./td[1]");
            HtmlNode value = node.SelectSingleNode(@"./td[2]");
            item.Name = Strip(name.InnerText);
            item.Value = Strip(value.InnerText);
            itemsGroup.Items.Add(item);
        }

        private static string Strip(string value)
        {
            return value.Replace("\n", String.Empty).Replace("&#8217;", "'");
        }
    }
}

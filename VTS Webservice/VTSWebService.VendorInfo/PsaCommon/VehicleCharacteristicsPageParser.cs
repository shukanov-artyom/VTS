using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using VTSWebService.DataContracts;

namespace VTSWebService.VendorInfo.PsaCommon
{
    public class VehicleCharacteristicsPageParser
    {
        private const string InfoZonePath = @"//div[@id='InfoZone']";
        private string rawPage;
        private string lang;

        public VehicleCharacteristicsPageParser(string rawPage, string lang)
        {
            if (rawPage == null)
            {
                throw new ArgumentNullException("rawPage");
            }
            this.rawPage = rawPage;
            this.lang = lang;
        }

        public VehicleCharacteristicsDto Parse()
        {
            VehicleCharacteristicsDto result = new VehicleCharacteristicsDto();
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

        private void ProcessInfoZone(HtmlNode infoZone, VehicleCharacteristicsDto result)
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
            VehicleCharacteristicsDto result)
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
                catch (Exception)
                {
                    // TODO: report failure for some vin
                    //throw;
                }
            }
        }

        private void ParseGeneralInfoTable(HtmlNode tableNode,
            VehicleCharacteristicsDto result)
        {
            //HtmlNode tbody = tableNode.SelectSingleNode("./tbody");
            VehicleCharacteristicsItemsGroupDto itemsGroup =
                new VehicleCharacteristicsItemsGroupDto();
            if (result.ItemGroups == null)
            {
                result.ItemGroups = new List<VehicleCharacteristicsItemsGroupDto>();
            }
            itemsGroup.Name = result.GeneralVehicleInfo;
            // TODO : Get translation for "general info"
            foreach (HtmlNode node in tableNode.SelectNodes("./tr"))
            {
                ParseGeneralInfoItemItemNode(node, itemsGroup);
            }
            result.ItemGroups.Add(itemsGroup);
        }

        private void ParseUsualTable(HtmlNode tableNode,
            VehicleCharacteristicsDto result)
        {
            VehicleCharacteristicsItemsGroupDto group =
                new VehicleCharacteristicsItemsGroupDto();
            HtmlNodeCollection trNodes = tableNode.SelectNodes("./tr");
            HtmlNodeCollection tds = trNodes[0].SelectNodes("./td");
            group.Name = Strip(tds[0].InnerText);
            HtmlNode subTable = trNodes[1].SelectSingleNode("./td/table");
            HtmlNodeCollection itemTrNodes = subTable.SelectNodes("./tr");
            for (int i = 1; i < itemTrNodes.Count; i++)
            {
                ParseUsualItemNode(itemTrNodes[i], group);
            }
            result.ItemGroups.Add(group);
        }

        private void ParseUsualItemNode(HtmlNode trNode,
            VehicleCharacteristicsItemsGroupDto group)
        {
            VehicleCharacteristicsItemDto item =
                new VehicleCharacteristicsItemDto();
            item.Name = Strip(trNode.SelectSingleNode("./td[1]").InnerText);
            item.Value = Strip(trNode.SelectSingleNode("./td[2]").InnerText);
            // TODO : Parse Code
            //item.Code = 
            group.Items.Add(item);
        }

        private void ParseGeneralInfoItemItemNode(HtmlNode node,
            VehicleCharacteristicsItemsGroupDto itemsGroup)
        {
            VehicleCharacteristicsItemDto item =
                new VehicleCharacteristicsItemDto();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AMDTreat.Models
{
    public class XmlHelper
    {

        /// <summary>
        /// Return a list of item names.
        /// </summary>
        /// <param name="data">An xml document created by the XDocument class in System.Xml.Linq</param>
        /// <param name="itemSection">The name of the section within the xml file</param>
        /// <returns></returns>
        public static List<string> GetElementNames(XDocument data, string itemSection)
        {
            List<string> xmlNameList = new List<string>();

            IEnumerable<XElement> element =
            from el in data.Descendants(itemSection).Elements()
            select el;

            foreach (XElement e in element)
            {
                xmlNameList.Add(e.Element("name").Value);
            }

            return xmlNameList;
        }

        /// <summary>
        /// Return a dictionary of an item in an xml file with the following keys:
        ///     name, value, units, and description
        /// </summary>
        /// <param name="data">An xml document created by the XDocument class in System.Xml.Linq</param>
        /// <param name="itemName">The name of the item within the name tag in the xml file</param>
        /// <returns>A dictionary of an item with keys name, value, units, and description</returns>
        public static Dictionary<string, string> GetElementValuesByName(XDocument data, string itemSection, string itemName)
        {

            Dictionary<string, string> item = new Dictionary<string, string>();

            IEnumerable<XElement> element =
                from el in data.Descendants(itemSection).Elements()
                where (string)el.Element("name") == itemName
                select el;

            foreach (XElement e in element)
            {
                item.Add("name", e.Element("name").Value);
                item.Add("value", e.Element("value").Value);
                item.Add("units", e.Element("units").Value);
                item.Add("description", e.Element("description").Value);
            }

            return item;
        }

        /// <summary>
        /// Return a dictionary of an item in an xml file with the following keys:
        ///     name, value, units, and description
        /// </summary>
        /// <param name="data">An xml document created by the XDocument class in System.Xml.Linq</param>
        /// <param name="itemName">The name of the item within the name tag in the xml file</param>
        /// <returns>A dictionary of an item with keys name, value, units, and description</returns>
        public static Dictionary<string, string> GetElementValuesByNameSampling(XDocument data, string itemSection, string itemName)
        {

            Dictionary<string, string> item = new Dictionary<string, string>();

            IEnumerable<XElement> element =
                from el in data.Descendants(itemSection).Elements()
                where (string)el.Element("name") == itemName
                select el;

            foreach (XElement e in element)
            {
                item.Add("name", e.Element("name").Value);
                item.Add("value", e.Element("value").Value);
            }

            return item;
        }
        /// <summary>
        /// Saves an xml file of the water quality data.
        /// </summary>
        /// <param name="dataItems">A dictionary of dictionary items</param>
        /// <param name="filename">An output filename</param>
        public static void SaveXmlData(Dictionary<string, Dictionary<string, string>> dataItems, string filename)
        {
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            xmlDoc.Add(new XElement("water_quality",
                dataItems.Select(dict => new XElement("item",
                    new XElement("name", dict.Value["name"]),
                    new XElement("value", dict.Value["value"]),
                    new XElement("units", dict.Value["units"]),
                    new XElement("description", dict.Value["description"])
                    ))
                ));

            xmlDoc.Save(filename);
        }

    }
}

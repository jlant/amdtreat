using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AMDTreat.Models
{
    public class DataHandler
    {
        /// <summary>
        /// Dynamically assign values from an xml file to the view model's properties.
        /// Property name syntax is very important for finding and assigning correct values.
        /// </summary>
        /// <param name="viewModelProperties"></param>
        /// <param name="xmlNames"></param>
        /// <param name="data"></param>
        /// <param name="xmlSectionName"></param>
        private void AssignPropertyValues(Array viewModelProperties, List<string> xmlPropertyNames, XDocument data, string xmlSectionName)
        {
            foreach (System.Reflection.PropertyInfo viewModelProperty in viewModelProperties)
            {
                foreach (string xmlPropertyName in xmlPropertyNames)
                {
                    string viewModelPropertyName = viewModelProperty.Name;

                    // handle property names with underscore delimieter ("_"), i.e. Wq_DesignFlow becomes DesignFlow
                    if (viewModelProperty.Name.Contains("_"))
                    {
                        char[] delimiterChars = { '_' };
                        viewModelPropertyName = viewModelProperty.Name.Split(delimiterChars)[1];
                    }

                    // handle matching propery names between viewmodel xml file - comparing by lowercasing and removing any spaces in property names
                    // the string.Equals is faster (more optimized) for .NET than comparing lowercase like 
                    //        if (viewModelPropertyName.ToLower() == xmlPropertyName.Replace(" ", "").ToLower())
                    if (viewModelPropertyName.Equals(xmlPropertyName.Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
                    {
                        var xmlPropertyName1 = XmlHelper.GetElementValuesByNameSampling(data, xmlSectionName, xmlPropertyName)["name"];
                        var xmlPropertyValue = XmlHelper.GetElementValuesByNameSampling(data, xmlSectionName, xmlPropertyName)["value"];

                        if (viewModelProperty.PropertyType == typeof(double))
                        {
                            viewModelProperty.SetValue(this, Convert.ToDouble(xmlPropertyValue));
                        }

                        if (viewModelProperty.PropertyType == typeof(decimal))
                        {
                            viewModelProperty.SetValue(this, Convert.ToDecimal(xmlPropertyValue));
                        }

                        if (viewModelProperty.PropertyType == typeof(string))
                        {
                            viewModelProperty.SetValue(this, xmlPropertyValue);
                        }

                        if (viewModelProperty.PropertyType == typeof(bool))
                        {
                            viewModelProperty.SetValue(this, Convert.ToBoolean(xmlPropertyValue));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Helper/wrapper to open xml file and assign all property values.
        /// </summary>
        /// <param name="xmlFileName"></param>
        private void OpenXmlAndAssignValues(string xmlFileName)
        {
            XDocument data = XDocument.Load(xmlFileName);
            List<string> xmlNameListItems = XmlHelper.GetElementNames(data, "items");

            // Assign all the properties            
            AssignPropertyValues(this.GetType().GetProperties(), xmlNameListItems, data, "items");
        }

        /// <summary>
        /// Open an XML file using a dialog window.
        /// </summary>
        private void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                OpenXmlAndAssignValues(openFileDialog.FileName);
            }
        }

        private void SaveFile(string moduleType)
        {
            // Lists to hold dictionaries of data
            List<Item> propertyItems = new List<Item>();

            // Get all properties based on type
            foreach (var prop in this.GetType().GetProperties())
            {
                // property items
                propertyItems.Add(new Item(prop.Name, prop.GetValue(this)));
            }

            // Open save dialog and write data
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlDoc.Add(
                    new XElement("module",
                    new XAttribute("type", moduleType),
                        new XElement("items",
                            propertyItems.Select(item => new XElement("item",
                                new XElement("name", item.Name),
                                new XElement("value", item.Value)
                            )))
                   )
                );
                xmlDoc.Save(saveFileDialog.FileName);
            }
        }

    }
}

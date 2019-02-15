using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    public static class Helpers
    {
        #region Retrieving viewmodel properties

        /// <summary>
        /// Return a list of strings of property names for a class.
        /// </summary>
        /// <param name="aObject">An object with properties; i.e. a view model class</param>
        /// <returns>A list of strings of property names for a class</returns>
        public static List<string> GetNamesOfClassProperties(object aObject)
        {
            List<string> propertyList = new List<string>();
            if (aObject != null)
            {
                foreach (var prop in aObject.GetType().GetProperties())
                {
                    propertyList.Add(prop.Name);
                }
            }

            return propertyList;
        }

        /// <summary>
        /// Return a list of property information for a specific class.
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetPropertiesOfClass(object pObject)
        {
            List<PropertyInfo> propertyList = new List<PropertyInfo>();
            if (pObject != null)
            {
                foreach (var prop in pObject.GetType().GetProperties())
                {
                    propertyList.Add(prop);
                }
            }

            return propertyList;
        }

        /// <summary>
        /// Filter a list of property names filterd by a string that the property name starts with and return a
        /// string array.
        /// </summary>
        /// <param name="propertiesStringList">List of string property names</param>
        /// <param name="startsWithString">String that common property names start with</param>
        /// <returns>A string array of property names filterd by a string that the property name starts with</returns>
        public static string[] FilterPropertiesList(List<string> propertiesStringList, string startsWithString)
        {
            List<string> filteredPropertiesStringList = new List<string>();
            if (propertiesStringList != null)
            {
                foreach (var prop in propertiesStringList)
                {
                    if (prop.StartsWith(startsWithString))
                    {
                        filteredPropertiesStringList.Add(prop);
                    }
                }
            }

            // Convert string list to string array for the additionalNames parameter in ChangeAndNotify() 
            string[] filteredPropertiesStringArray = filteredPropertiesStringList.ToArray();

            return filteredPropertiesStringArray;
        }


        /// <summary>
        /// Filter a list of property names filterd by a string that the property name starts with and return a
        /// string array.
        /// </summary>
        /// <param name="propertiesStringList">List of string property names</param>
        /// <param name="startsWithString">String that common property names start with</param>
        /// <returns>A string array of property names filterd by a string that the property name starts with</returns>
        public static string[] FilterPropertiesListNot(List<string> propertiesStringList, string startsWithString)
        {
            List<string> filteredPropertiesStringList = new List<string>();
            if (propertiesStringList != null)
            {
                foreach (var prop in propertiesStringList)
                {
                    if (!prop.StartsWith(startsWithString))
                    {
                        filteredPropertiesStringList.Add(prop);
                    }
                }
            }

            // Convert string list to string array for the additionalNames parameter in ChangeAndNotify() 
            string[] filteredPropertiesStringArray = filteredPropertiesStringList.ToArray();

            return filteredPropertiesStringArray;
        }

        /// <summary>
        /// Return a filtered array of property info.
        /// </summary>
        /// <param name="propertiesStringList"></param>
        /// <param name="endsWithString"></param>
        /// <returns></returns>
        public static PropertyInfo[] FilterPropertiesListEndsWith(List<PropertyInfo> propertiesStringList, string endsWithString)
        {
            List<PropertyInfo> filteredPropertiesStringList = new List<PropertyInfo>();
            if (propertiesStringList != null)
            {
                foreach (var prop in propertiesStringList)
                {
                    if (prop.Name.EndsWith(endsWithString))
                    {
                        filteredPropertiesStringList.Add(prop);
                    }
                }
            }

            // Convert string list to string array for the additionalNames parameter in ChangeAndNotify() 
            PropertyInfo[] calcPropertiesStringArray = filteredPropertiesStringList.ToArray();

            return calcPropertiesStringArray;
        }




        #endregion

        #region Rounding like Excel

        public static double RoundUp(double number, int digits)
        {
            return Math.Ceiling(number * Math.Pow(10, digits)) / Math.Pow(10, digits);
        }

        public static double RoundExcel(double value, int decimals)
        {
            if (decimals < 0)
            {
                var factor = Math.Pow(10, -decimals);
                return RoundExcel(value / factor, 0) * factor;
            }
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
        #endregion
    }
}

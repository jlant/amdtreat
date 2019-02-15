using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace AMDTreat.Models
{
    public class PropertyChangedBase : INotifyPropertyChanged, IObservable<SharedData>
    {

        #region Properties

        public string ModuleName { get; set; }
        public string ModuleTreatmentType { get; set; }
        public int ModuleId { get; set; }
        public Random random { get; set; } = new Random();

        #endregion

        #region Utility methods

        /// <summary>
        /// Notify of a property change and optional additional dependencies.
        /// </summary>
        public void Notify([CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            OnPropertyChanged(propertyName);
            foreach (var name in additionalNames)
            {
                OnPropertyChanged(name);
            }
        }

        /// <summary>
        /// Makes a change to the supplied reference if different.
        /// If different, notify of a property change and optional additional dependencies.
        /// <example>
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value);}
        /// }
        /// 
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value, "PropertyName", "AdditionalRelatedPropertyName"); }
        /// }
        /// </example>
        /// </summary>
        public bool ChangeAndNotify<T>(ref T toChange, T newValue, [CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            var cmp = EqualityComparer<T>.Default;
            if (cmp.Equals(toChange, newValue) == false)
            {
                toChange = newValue;
                OnPropertyChanged(propertyName);
                foreach (var name in additionalNames)
                {
                    OnPropertyChanged(name);
                }
                return true;
            }

            // Notify observers 
            NotifyObservers(observers, propertyName);

            return false;
        }

        /// <summary>
        /// Makes a change to the supplied reference if different.
        /// If different, notify of a property change and optional additional dependencies then call action.
        /// <example>
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value, () => SomeActionOnSuccess()); }
        /// }
        /// </example>
        /// </summary>
        public bool ChangeAndNotifyWithAction<T>(ref T toChange, T newValue, Action action, [CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            var doAction = ChangeAndNotify(ref toChange, newValue, propertyName, additionalNames);
            if (doAction)
            {
                action();
            }
            return doAction;
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

            // Notify observers that a property
            NotifyObservers(observers, propertyName);
        }

        #endregion

        #region IObserver implementation
        public string costPropertyEnding = "CostData";
        public string clearAndGrubPropertyEnding = "GrubAreaData";
        public string foundationAreaPropertyEnding = "FoundationAreaData";
        public string foundationDepthPropertyEnding = "FoundationAreaTimesDepthData";
        public string sharedDataEnding = "_SharedData";

        public List<IObserver<SharedData>> observers = new List<IObserver<SharedData>>();

        /// <summary>
        /// Called by observers that wish to receive notifications from the provider.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<SharedData> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            observer.OnNext(new SharedData(ModuleId, ModuleTreatmentType, GetSharedData()));

            return new Unsubscriber(observers, observer, ModuleId, ModuleTreatmentType);
        }

        internal class Unsubscriber : IDisposable
        {
            private List<IObserver<SharedData>> _observers;
            private IObserver<SharedData> _observer;
            public int ModuleId { get; set; }
            public string ModuleTreatmentType { get; set; }

            public Unsubscriber(List<IObserver<SharedData>> observers, IObserver<SharedData> observer, int id, string moduleTreatmentType)
            {
                this._observers = observers;
                this._observer = observer;
                this.ModuleId = id;
                this.ModuleTreatmentType = moduleTreatmentType;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        /// <summary>
        /// Notify observers of any property changes.  
        /// Note: property names that contain costs of interest have the costPropertyEnding = "CostData"
        /// </summary>
        /// <param name="observers"></param>
        /// <param name="propertyName"></param>
        public void NotifyObservers(List<IObserver<SharedData>> observers, string propertyName)
        {
            if (propertyName.EndsWith(costPropertyEnding) || propertyName.EndsWith(sharedDataEnding) || propertyName.EndsWith(clearAndGrubPropertyEnding) || propertyName.EndsWith(foundationAreaPropertyEnding) || propertyName.EndsWith(foundationDepthPropertyEnding))
            {
                if (observers.Count > 0)
                {
                    Dictionary<string, object> data = GetSharedData();

                    foreach (var observer in observers)
                    {
                        observer.OnNext(new SharedData(ModuleId, ModuleTreatmentType, data));
                    }
                }
            }
        }

        /// <summary>
        /// Return a dictionary containing key, value pairs of property name, property value for a class where the 
        /// property names end with a certain string, i.e. a string to specify a certain cost.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetSharedData(params string[] propertyNames)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            var costProperties = Helpers.FilterPropertiesListEndsWith(Helpers.GetPropertiesOfClass(this), costPropertyEnding);
            var clearAndGrubDataProperties = Helpers.FilterPropertiesListEndsWith(Helpers.GetPropertiesOfClass(this), clearAndGrubPropertyEnding);
            var foundationAreaDataProperties = Helpers.FilterPropertiesListEndsWith(Helpers.GetPropertiesOfClass(this), foundationAreaPropertyEnding);
            var foundationDepthDataProperties = Helpers.FilterPropertiesListEndsWith(Helpers.GetPropertiesOfClass(this), foundationDepthPropertyEnding);

            var sharedDataProperties = Helpers.FilterPropertiesListEndsWith(Helpers.GetPropertiesOfClass(observers[0]), sharedDataEnding);

            foreach (var costProperty in costProperties)
            {
                data.Add(costProperty.Name, (decimal)costProperty.GetValue(this));
            }

            foreach (var clearAndGrubDataProperty in clearAndGrubDataProperties)
            {
                data.Add(clearAndGrubDataProperty.Name, (double)clearAndGrubDataProperty.GetValue(this));
            }

            foreach (var foundationAreaDataProperty in foundationAreaDataProperties)
            {
                data.Add(foundationAreaDataProperty.Name, (double)foundationAreaDataProperty.GetValue(this));
            }

            foreach (var foundationDepthDataProperty in foundationDepthDataProperties)
            {
                data.Add(foundationDepthDataProperty.Name, (double)foundationDepthDataProperty.GetValue(this));
            }

            foreach (var sharedDataProperty in sharedDataProperties)
            {
                if (sharedDataProperty.PropertyType == typeof(double))
                {
                    data.Add(sharedDataProperty.Name, (double)sharedDataProperty.GetValue(observers[0]));
                }

                if (sharedDataProperty.PropertyType == typeof(decimal))
                {
                    data.Add(sharedDataProperty.Name, (decimal)sharedDataProperty.GetValue(observers[0]));
                }
            }

            return data;
        }

        public ObservableCollection<SharedData> SharedDataCollection { get; set; } = new ObservableCollection<SharedData>();

        #endregion

        #region Data Reading, Assigning, and Writing Methods
        /// <summary>
        /// Dynamically assign values from an xml file to the view model's properties.
        /// Property name syntax is very important for finding and assigning correct values.
        /// </summary>
        /// <param name="viewModelProperties"></param>
        /// <param name="xmlNames"></param>
        /// <param name="data"></param>
        /// <param name="xmlSectionName"></param>
        public void AssignPropertyValues(Array viewModelProperties, List<string> xmlPropertyNames, XDocument data, string xmlSectionName)
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

                        // radio buttons
                        if (viewModelProperty.PropertyType.IsEnum)
                        {
                            viewModelProperty.SetValue(this, Enum.Parse(viewModelProperty.PropertyType, xmlPropertyValue.ToString(), true));
                        }

                        if (viewModelProperty.Name == "ModuleTiles")
                        {
                            viewModelProperty.SetValue(this, xmlPropertyValue);
                        }

                        //if (viewModelProperty.GetType().IsGenericType && viewModelProperty.GetType().GetGenericTypeDefinition() == typeof(ObservableCollection<ModuleItem>))
                        //{
                        //    viewModelProperty.SetValue(this, xmlPropertyValue);
                        //}

                        //// lists
                        //if (viewModelProperty.PropertyType == typeof(List<GeneralCostItem>))
                        //{
                        //    viewModelProperty.SetValue(this, xmlPropertyValue);
                        //}

                    }
                }
            }
        }


        /// <summary>
        /// Helper/wrapper to open xml file and assign all property values.
        /// </summary>
        /// <param name="xmlFileName"></param>
        public void OpenXmlAndAssignValues(string xmlFileName)
        {
            XDocument data = XDocument.Load(xmlFileName);
            List<string> xmlNameListItems = XmlHelper.GetElementNames(data, "items");

            // Assign all the properties            
            AssignPropertyValues(this.GetType().GetProperties(), xmlNameListItems, data, "items");
        }

        /// <summary>
        /// Open an XML file using a dialog window.
        /// </summary>
        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                OpenXmlAndAssignValues(openFileDialog.FileName);
            }
        }

        public void SaveFile()
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

        public void SaveMainUiFile()
        {
            // Lists to hold dictionaries of data
            List<Item> propertyItems = new List<Item>();
            ObservableCollection<ModuleItem> moduleItems = new ObservableCollection<ModuleItem>();
            List<List<Item>> viewModelPropertyItemsAll = new List<List<Item>>();

            // Get all properties based on type
            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.Name == "ModuleTiles")
                {
                    moduleItems = (ObservableCollection<ModuleItem>)prop.GetValue(this);
                    foreach (ModuleItem moduleItem in moduleItems)
                    {
                        List<Item> viewModelPropertyItems = new List<Item>();
                        foreach (var p in moduleItem.ViewModel.GetType().GetProperties())
                        {
                            // property items
                            viewModelPropertyItems.Add(new Item(p.Name, p.GetValue(moduleItem.ViewModel)));
                        }
                        viewModelPropertyItemsAll.Add(viewModelPropertyItems);
                    }
                }
                else
                {
                    // property items
                    propertyItems.Add(new Item(prop.Name, prop.GetValue(this)));
                }

            }

            // Open save dialog and write data
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                xmlDoc.Add(
                    new XElement("module",
                    new XElement("items",
                        propertyItems.Select(item => new XElement("item",
                            new XElement("name", item.Name),
                            new XElement("value", item.Value)
                        )),
                        new XElement("modules",
                        viewModelPropertyItemsAll.Select(module => new XElement("module",
                            new XElement("items",
                            module.Select(vmitem => new XElement("item",
                                new XElement("name", vmitem.Name),
                                new XElement("value", vmitem.Value)
                            ))
                        ))                      
                   ))
                )));
                xmlDoc.Save(saveFileDialog.FileName);
            }
        }
        #endregion

    }
}

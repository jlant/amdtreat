﻿#pragma checksum "..\..\..\Views\SamplingWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "29D258F26037C45CD1C07E91DD0A61FB223EAF99"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AMDTreat.Converters;
using AMDTreat.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AMDTreat.Views {
    
    
    /// <summary>
    /// SamplingWindow
    /// </summary>
    public partial class SamplingWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 82 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout flyoutModuleInformation;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout flyoutErrorInformation;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessageTextBlock;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkboxRoundTripTravelTime;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxRoundTripMileage;
        
        #line default
        #line hidden
        
        
        #line 321 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkboxSamplingEquipmentCost;
        
        #line default
        #line hidden
        
        
        #line 478 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioButtonAnnualCostCalculated;
        
        #line default
        #line hidden
        
        
        #line 505 "..\..\..\Views\SamplingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioButtonAnnualCostUserSpecified;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AMDTreat;component/views/samplingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\SamplingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.flyoutModuleInformation = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 2:
            
            #line 123 "..\..\..\Views\SamplingWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.TextBox_KeyEnterUpdate);
            
            #line default
            #line hidden
            return;
            case 3:
            this.flyoutErrorInformation = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 4:
            this.ErrorMessageTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.checkboxRoundTripTravelTime = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.checkBoxRoundTripMileage = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            this.checkboxSamplingEquipmentCost = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.radioButtonAnnualCostCalculated = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.radioButtonAnnualCostUserSpecified = ((System.Windows.Controls.RadioButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "C:\Users\ecneb\Documents\Udalosti\Udalosti\Udalosti\UI\ZoznamUdalosti.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "923E69858644838624228E4F82FEF128"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Udalosti.Udalosti.UI
{
    partial class ZoznamUdalosti : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Udalosti\UI\ZoznamUdalosti.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.nacitajZoznam;
                }
                break;
            case 2: // Udalosti\UI\ZoznamUdalosti.xaml line 18
                {
                    this.moznostiObsahy = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                }
                break;
            case 3: // Udalosti\UI\ZoznamUdalosti.xaml line 158
                {
                    this.miesto = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Udalosti\UI\ZoznamUdalosti.xaml line 27
                {
                    this.zoznamUdalosti = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


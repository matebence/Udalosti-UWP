﻿#pragma checksum "C:\Users\ecneb\Downloads\Windows\Udalosti\Udalosti\Udalosti\UI\Podrobnosti.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "85EA2C531C17A266BD111043E0517853"
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
    partial class Podrobnosti : 
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
            case 2: // Udalosti\UI\Podrobnosti.xaml line 9
                {
                    this.podrobnosti = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Udalosti\UI\Podrobnosti.xaml line 18
                {
                    this.nacitavanie = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 4: // Udalosti\UI\Podrobnosti.xaml line 33
                {
                    this.obsah = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // Udalosti\UI\Podrobnosti.xaml line 225
                {
                    this.tlacidloZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.tlacidloZvolenejUdalosti).Click += this.zaujem;
                }
                break;
            case 6: // Udalosti\UI\Podrobnosti.xaml line 49
                {
                    this.obrazokZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 7: // Udalosti\UI\Podrobnosti.xaml line 208
                {
                    this.casZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Udalosti\UI\Podrobnosti.xaml line 175
                {
                    this.vstupenkaZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // Udalosti\UI\Podrobnosti.xaml line 142
                {
                    this.pocetZaujemcovZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Udalosti\UI\Podrobnosti.xaml line 92
                {
                    this.nazovZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // Udalosti\UI\Podrobnosti.xaml line 101
                {
                    this.miestoZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Udalosti\UI\Podrobnosti.xaml line 65
                {
                    this.denZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Udalosti\UI\Podrobnosti.xaml line 75
                {
                    this.mesiacZvolenejUdalosti = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // Udalosti\UI\Podrobnosti.xaml line 24
                {
                    this.nacitavanieUdalosti = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
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


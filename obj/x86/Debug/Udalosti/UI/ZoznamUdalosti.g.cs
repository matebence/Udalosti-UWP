﻿#pragma checksum "C:\Users\ecneb\Downloads\Windows\Udalosti\Udalosti\Udalosti\UI\ZoznamUdalosti.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2DB9C3AE4A0B627F1065DA25F84B676"
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
            case 2: // Udalosti\UI\ZoznamUdalosti.xaml line 9
                {
                    this.udalosti = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Udalosti\UI\ZoznamUdalosti.xaml line 26
                {
                    this.moznostiObsahu = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                    ((global::Windows.UI.Xaml.Controls.Pivot)this.moznostiObsahu).SelectionChanged += this.aktualnyPivot;
                }
                break;
            case 4: // Udalosti\UI\ZoznamUdalosti.xaml line 327
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element4 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element4).Click += this.odhlasitSa;
                }
                break;
            case 5: // Udalosti\UI\ZoznamUdalosti.xaml line 338
                {
                    this.miesto = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Udalosti\UI\ZoznamUdalosti.xaml line 35
                {
                    global::Windows.UI.Xaml.Controls.PivotItem element6 = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    ((global::Windows.UI.Xaml.Controls.PivotItem)element6).Loaded += this.nacitajZoznam;
                }
                break;
            case 7: // Udalosti\UI\ZoznamUdalosti.xaml line 178
                {
                    global::Windows.UI.Xaml.Controls.PivotItem element7 = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    ((global::Windows.UI.Xaml.Controls.PivotItem)element7).Loaded += this.nacitajZoznamPodlaPozicii;
                }
                break;
            case 8: // Udalosti\UI\ZoznamUdalosti.xaml line 185
                {
                    this.ziadneUdalostiPodlaPozicie = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 9: // Udalosti\UI\ZoznamUdalosti.xaml line 193
                {
                    this.nacitavaniePodlaPozicie = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 10: // Udalosti\UI\ZoznamUdalosti.xaml line 200
                {
                    this.zoznamUdalostiPodlaPozicie = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 12: // Udalosti\UI\ZoznamUdalosti.xaml line 42
                {
                    this.ziadneUdalosti = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 13: // Udalosti\UI\ZoznamUdalosti.xaml line 50
                {
                    this.nacitavanieUdalosti = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 14: // Udalosti\UI\ZoznamUdalosti.xaml line 57
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


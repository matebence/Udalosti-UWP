﻿#pragma checksum "C:\Users\ecneb\Downloads\Windows\Udalosti\Udalosti\Udalosti\UI\Aplikacia.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DD316BD36766BBBB572A222A05DACAE7"
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
    partial class Aplikacia : 
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
            case 2: // Udalosti\UI\Aplikacia.xaml line 11
                {
                    this.udalosti = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Udalosti\UI\Aplikacia.xaml line 28
                {
                    this.moznostiObsahu = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                    ((global::Windows.UI.Xaml.Controls.Pivot)this.moznostiObsahu).SelectionChanged += this.aktualnyPivot;
                }
                break;
            case 4: // Udalosti\UI\Aplikacia.xaml line 495
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element4 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element4).Click += this.odhlasitSa;
                }
                break;
            case 5: // Udalosti\UI\Aplikacia.xaml line 506
                {
                    this.titul = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Udalosti\UI\Aplikacia.xaml line 37
                {
                    global::Windows.UI.Xaml.Controls.PivotItem element6 = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    ((global::Windows.UI.Xaml.Controls.PivotItem)element6).Loaded += this.nacitajUdalosti;
                }
                break;
            case 7: // Udalosti\UI\Aplikacia.xaml line 195
                {
                    global::Windows.UI.Xaml.Controls.PivotItem element7 = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    ((global::Windows.UI.Xaml.Controls.PivotItem)element7).Loaded += this.nacitajUdalosti;
                }
                break;
            case 8: // Udalosti\UI\Aplikacia.xaml line 353
                {
                    global::Windows.UI.Xaml.Controls.PivotItem element8 = (global::Windows.UI.Xaml.Controls.PivotItem)(target);
                    ((global::Windows.UI.Xaml.Controls.PivotItem)element8).Loaded += this.nacitajUdalosti;
                }
                break;
            case 9: // Udalosti\UI\Aplikacia.xaml line 360
                {
                    this.chybaZaujmov = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 10: // Udalosti\UI\Aplikacia.xaml line 368
                {
                    this.nacitavanieZaujmov = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 11: // Udalosti\UI\Aplikacia.xaml line 375
                {
                    this.zoznamZaujmov = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.zoznamZaujmov).ItemClick += this.zvolenaUdalost;
                    ((global::Windows.UI.Xaml.Controls.ListView)this.zoznamZaujmov).RightTapped += this.spustiMoznostiZoznamu;
                }
                break;
            case 13: // Udalosti\UI\Aplikacia.xaml line 475
                {
                    this.moznostiZoznamu = (global::Windows.UI.Xaml.Controls.MenuFlyout)(target);
                }
                break;
            case 14: // Udalosti\UI\Aplikacia.xaml line 478
                {
                    this.odstranit = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.odstranit).Click += this.odstranitZaujem;
                }
                break;
            case 15: // Udalosti\UI\Aplikacia.xaml line 202
                {
                    this.chybaUdalostiPodlaPozicie = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 16: // Udalosti\UI\Aplikacia.xaml line 210
                {
                    this.nacitavaniePodlaPozicie = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 17: // Udalosti\UI\Aplikacia.xaml line 217
                {
                    global::PullToRefresh.UWP.PullToRefreshBox element17 = (global::PullToRefresh.UWP.PullToRefreshBox)(target);
                    ((global::PullToRefresh.UWP.PullToRefreshBox)element17).RefreshInvoked += this.aktualizujUdalostiPodlaPozicie;
                }
                break;
            case 18: // Udalosti\UI\Aplikacia.xaml line 220
                {
                    this.zoznamUdalostiPodlaPozicie = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.zoznamUdalostiPodlaPozicie).ItemClick += this.zvolenaUdalost;
                }
                break;
            case 21: // Udalosti\UI\Aplikacia.xaml line 44
                {
                    this.chybaUdalosti = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 22: // Udalosti\UI\Aplikacia.xaml line 52
                {
                    this.nacitavanieUdalosti = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 23: // Udalosti\UI\Aplikacia.xaml line 59
                {
                    global::PullToRefresh.UWP.PullToRefreshBox element23 = (global::PullToRefresh.UWP.PullToRefreshBox)(target);
                    ((global::PullToRefresh.UWP.PullToRefreshBox)element23).RefreshInvoked += this.aktualizujUdalosti;
                }
                break;
            case 24: // Udalosti\UI\Aplikacia.xaml line 62
                {
                    this.zoznamUdalosti = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.zoznamUdalosti).ItemClick += this.zvolenaUdalost;
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


﻿#pragma checksum "C:\Users\ecneb\Documents\Udalosti\Udalosti\Autentifikacia\UI\Prihlasenie.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB5E43B7644164567F30E76D6B7EE75C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Udalosti
{
    partial class Prihlasenie : 
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
            case 2: // Autentifikacia\UI\Prihlasenie.xaml line 9
                {
                    this.prihlasenie = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Autentifikacia\UI\Prihlasenie.xaml line 100
                {
                    this.tlacidloRegistrovat = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.tlacidloRegistrovat).Click += this.tlacidloRegistrovatSa;
                }
                break;
            case 4: // Autentifikacia\UI\Prihlasenie.xaml line 54
                {
                    this.email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Autentifikacia\UI\Prihlasenie.xaml line 61
                {
                    this.heslo = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 6: // Autentifikacia\UI\Prihlasenie.xaml line 74
                {
                    this.prihlasitSa = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.prihlasitSa).Click += this.prihlasit;
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

﻿#pragma checksum "C:\Users\ecneb\Downloads\Udalosti\Udalosti\Autentifikacia\UI\Registracia.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8778529AA0AAEAFA3D40360F9C51AE63"
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
    partial class Registracia : 
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
            case 2: // Autentifikacia\UI\Registracia.xaml line 9
                {
                    this.registracia = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Autentifikacia\UI\Registracia.xaml line 53
                {
                    this.meno = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Autentifikacia\UI\Registracia.xaml line 61
                {
                    this.email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Autentifikacia\UI\Registracia.xaml line 69
                {
                    this.heslo = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 6: // Autentifikacia\UI\Registracia.xaml line 77
                {
                    this.potvrd = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 7: // Autentifikacia\UI\Registracia.xaml line 105
                {
                    this.nacitavanie = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 8: // Autentifikacia\UI\Registracia.xaml line 92
                {
                    this.registrovatSa = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.registrovatSa).Click += this.registrovatAsync;
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


﻿#pragma checksum "C:\Users\ecneb\Documents\Udalosti\Udalosti\Autentifikacia\UI\Registracia.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "281E9A84FA2D7063666B36C5A0B7D680"
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
            case 3: // Autentifikacia\UI\Registracia.xaml line 46
                {
                    this.meno = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Autentifikacia\UI\Registracia.xaml line 53
                {
                    this.email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Autentifikacia\UI\Registracia.xaml line 60
                {
                    this.heslo = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 6: // Autentifikacia\UI\Registracia.xaml line 67
                {
                    this.potvrd = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 7: // Autentifikacia\UI\Registracia.xaml line 80
                {
                    this.registrovatSa = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.registrovatSa).Click += this.registrovat;
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


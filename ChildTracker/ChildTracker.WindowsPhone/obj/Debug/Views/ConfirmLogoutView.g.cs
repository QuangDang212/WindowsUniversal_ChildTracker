﻿

#pragma checksum "C:\Users\M.Tonkov\Desktop\ChildTracker\WindowsUniversal_ChildTracker\ChildTracker\ChildTracker.Shared\Views\ConfirmLogoutView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C2D91EC7D1ACA7030D87B75DE21D9D08"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChildTracker.Views
{
    partial class ConfirmLogoutView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 40 "..\..\Views\ConfirmLogoutView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.OnLogoutButtonClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 46 "..\..\Views\ConfirmLogoutView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.OnCancelButtonClick;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



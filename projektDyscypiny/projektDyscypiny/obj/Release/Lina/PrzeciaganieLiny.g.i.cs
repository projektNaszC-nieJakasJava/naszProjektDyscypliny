﻿#pragma checksum "..\..\..\Lina\PrzeciaganieLiny.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CCE98693D4C75B29328C45004CF50355BC981F3BAB70FE9C805757B8AF30D1D5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using projektDyscypiny.Lina;


namespace projektDyscypiny.Lina {
    
    
    /// <summary>
    /// PrzeciaganieLiny
    /// </summary>
    public partial class PrzeciaganieLiny : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SedziaImie;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SedziaNazwisko;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsuwanieSedziegoTextBox;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sedziowieStackPanel;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SedziaIDTextBox;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DruzynaNazwa;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WycofanieDruzynyTextBox;
        
        #line default
        #line hidden
        
        
        #line 303 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel druzynyStackPanel;
        
        #line default
        #line hidden
        
        
        #line 332 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DruzynaIDTextBox;
        
        #line default
        #line hidden
        
        
        #line 378 "..\..\..\Lina\PrzeciaganieLiny.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RozpocznijTurniejButton;
        
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
            System.Uri resourceLocater = new System.Uri("/projektDyscypiny;component/lina/przeciaganieliny.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Lina\PrzeciaganieLiny.xaml"
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
            this.SedziaImie = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SedziaNazwisko = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 75 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DodajSedziegoClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.UsuwanieSedziegoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 121 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UsunSedziegoClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sedziowieStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.SedziaIDTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 179 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SzukajSedziegoClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DruzynaNazwa = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 239 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DodajDruzyneClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.WycofanieDruzynyTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            
            #line 285 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.WycofajDruzyneClick);
            
            #line default
            #line hidden
            return;
            case 13:
            this.druzynyStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 14:
            this.DruzynaIDTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            
            #line 371 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Powrot);
            
            #line default
            #line hidden
            return;
            case 16:
            this.RozpocznijTurniejButton = ((System.Windows.Controls.Button)(target));
            
            #line 378 "..\..\..\Lina\PrzeciaganieLiny.xaml"
            this.RozpocznijTurniejButton.Click += new System.Windows.RoutedEventHandler(this.RozpocznijTurniejClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


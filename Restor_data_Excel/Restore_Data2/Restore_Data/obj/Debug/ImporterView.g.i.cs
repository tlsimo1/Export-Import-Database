﻿#pragma checksum "..\..\ImporterView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "545D5BBB688EF6D4D8655949917FCAF74B3A02806CD23F8441E99EA2E0FA72F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Restore_Data.UC;
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


namespace Restore_Data {
    
    
    /// <summary>
    /// ImporterView
    /// </summary>
    public partial class ImporterView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tab;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtfile;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Openfile;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnImport;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNameDB;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblVersionSql;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblVersionBD;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid dg;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewTableName;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTeminer1;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\ImporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTeminer2;
        
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
            System.Uri resourceLocater = new System.Uri("/Restore_Data;component/importerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ImporterView.xaml"
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
            this.tab = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.txtfile = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Openfile = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ImporterView.xaml"
            this.Openfile.Click += new System.Windows.RoutedEventHandler(this.Openfile_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnImport = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\ImporterView.xaml"
            this.btnImport.Click += new System.Windows.RoutedEventHandler(this.BtnImporter_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblNameDB = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblVersionSql = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblVersionBD = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.scroll = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 9:
            this.dg = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.ListViewTableName = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.btnTeminer1 = ((System.Windows.Controls.Button)(target));
            
            #line 114 "..\..\ImporterView.xaml"
            this.btnTeminer1.Click += new System.Windows.RoutedEventHandler(this.BtnTerminer_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnTeminer2 = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\ImporterView.xaml"
            this.btnTeminer2.Click += new System.Windows.RoutedEventHandler(this.BtnTerminer_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

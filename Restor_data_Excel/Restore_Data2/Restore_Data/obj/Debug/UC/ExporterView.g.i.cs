﻿#pragma checksum "..\..\..\UC\ExporterView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "428377FD2C58F07138160822DAA6B83C1C36E5C0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Restore_Data;
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
    /// ExporterView
    /// </summary>
    public partial class ExporterView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tab;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgImporter;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtfile;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Openfile;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnImport;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNameDB;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblVersionSql;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblVersionBD;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\UC\ExporterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewTableName;
        
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
            System.Uri resourceLocater = new System.Uri("/Restore_Data;component/uc/exporterview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UC\ExporterView.xaml"
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
            this.dgImporter = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.txtfile = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Openfile = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\UC\ExporterView.xaml"
            this.Openfile.Click += new System.Windows.RoutedEventHandler(this.Openfile_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnImport = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\UC\ExporterView.xaml"
            this.btnImport.Click += new System.Windows.RoutedEventHandler(this.BtnExporter_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblNameDB = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblVersionSql = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lblVersionBD = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.ListViewTableName = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

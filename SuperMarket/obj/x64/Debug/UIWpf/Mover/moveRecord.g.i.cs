﻿#pragma checksum "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "78B2F235A3205202EA5706EBBC97EEA5"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace SuperMarket.UIWpf.Mover {
    
    
    /// <summary>
    /// moveRecord
    /// </summary>
    public partial class moveRecord : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SuperMarket.UIWpf.Mover.moveRecord UserControl;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox removeId_TextBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox employeeId_TextBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid removeRecord_datagrid;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid record_detail_datagrid;
        
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
            System.Uri resourceLocater = new System.Uri("/SuperMarket;component/uiwpf/mover/moverecord.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
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
            this.UserControl = ((SuperMarket.UIWpf.Mover.moveRecord)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.removeId_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.removeId_TextBox.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.textBox1_Pasting));
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.removeId_TextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.textBox1_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.removeId_TextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textBox1_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textBox1_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.employeeId_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.employeeId_TextBox.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.textBox1_Pasting));
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.employeeId_TextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.textBox1_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.removeRecord_datagrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 20 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            this.removeRecord_datagrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.datagrid_selected);
            
            #line default
            #line hidden
            return;
            case 7:
            this.record_detail_datagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            
            #line 23 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_removeID);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 25 "..\..\..\..\..\UIWpf\Mover\moveRecord.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_employeeID);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


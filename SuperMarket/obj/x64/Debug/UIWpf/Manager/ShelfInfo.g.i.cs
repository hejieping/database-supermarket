﻿#pragma checksum "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AB90C9F747141F7BA27B7F74D4947559"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using SuperMarket.UIWpf.Manager;
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


namespace SuperMarket.UIWpf.Manager {
    
    
    /// <summary>
    /// ShelfInfo
    /// </summary>
    public partial class ShelfInfo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy2;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Search_Copy;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border btncopy_Copy;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox shelfEmployeeID;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border btncopy_Copy1;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox shelfID;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Search_Copy1;
        
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
            System.Uri resourceLocater = new System.Uri("/SuperMarket;component/uiwpf/manager/shelfinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
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
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.label_Copy2 = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Search_Copy = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
            this.Search_Copy.Click += new System.Windows.RoutedEventHandler(this.Search_Copy_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btncopy_Copy = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.shelfEmployeeID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.label_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btncopy_Copy1 = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.shelfID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Search_Copy1 = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\..\UIWpf\Manager\ShelfInfo.xaml"
            this.Search_Copy1.Click += new System.Windows.RoutedEventHandler(this.Search_Copy1_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


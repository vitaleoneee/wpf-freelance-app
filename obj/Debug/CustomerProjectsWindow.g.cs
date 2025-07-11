﻿#pragma checksum "..\..\CustomerProjectsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "83046BE5459DE3396157B26F0204896CEACEE7068341F90DA59FDE4D6A313558"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace FreelanceAplication {
    
    
    /// <summary>
    /// CustomerProjectsWindow
    /// </summary>
    public partial class CustomerProjectsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock WelcomeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ProfileTextBlock;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FreelancersTextBlock;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LogoutTextBlock;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EmailTextBlock;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\CustomerProjectsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ProjectsListBox;
        
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
            System.Uri resourceLocater = new System.Uri("/FreelanceAplication;component/customerprojectswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CustomerProjectsWindow.xaml"
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
            this.WelcomeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ProfileTextBlock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 11 "..\..\CustomerProjectsWindow.xaml"
            this.ProfileTextBlock.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ProfileTextBlock_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FreelancersTextBlock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 13 "..\..\CustomerProjectsWindow.xaml"
            this.FreelancersTextBlock.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.FreelancersTextBlock_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LogoutTextBlock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 14 "..\..\CustomerProjectsWindow.xaml"
            this.LogoutTextBlock.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.LogoutTextBlock_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.EmailTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ProjectsListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            
            #line 34 "..\..\CustomerProjectsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateProjectButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


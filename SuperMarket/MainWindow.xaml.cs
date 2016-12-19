﻿using SuperMarket.UIWpf;
using SuperMarket.UIWpf.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMarket
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("new URI is opened!");
            //Uri window =new System.Uri("window.xaml", System.UriKind.Relative);
            contentControl.Content = new WpfTest();
            //contentControl.Content = new MV();
        }
    }
}

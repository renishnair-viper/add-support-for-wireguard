﻿// <copyright file="MainWindow.xaml.cs" company="NetProtect, LLC">
// Copyright (c) NetProtect, LLC. All Rights Reserved.
// </copyright>

using System;
using System.Windows;
using Example.ViewModel;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Handles the on Closed event for the window.  And tells the View model about it.
        /// </summary>
        /// <param name="e">the normal OnClosed event</param>
        protected override void OnClosed(EventArgs e)
        {
            ((MainViewModel)DataContext).Disconnect.Execute(null);
            base.OnClosed(e);
        }
    }
}

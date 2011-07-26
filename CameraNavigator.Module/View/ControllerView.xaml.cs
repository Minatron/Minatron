﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Band.CameraNavigator.Module.Presenter;

namespace Band.CameraNavigator.Module.View
{
    /// <summary>
    /// Interaction logic for ControllerView.xaml
    /// </summary>
    public partial class ControllerView : UserControl
    {
        public ControllerView(ControllerPresenter presenter)
        {
            InitializeComponent();
            DataContext = presenter;
        }
    }
}
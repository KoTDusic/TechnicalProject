﻿using System;
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
using System.Windows.Shapes;

namespace StydentEditor
{
    public partial class MyDialog : Window
    {
        public bool cancelled;
        public MyDialog(string headerText, bool editable, string inputText = "", int width = 400, int height = 200)
        {
            InitializeComponent();
            HeaderText.Text = headerText;
            if (!editable)
            {
                InputTextBox.IsReadOnly = true;
                if (inputText == "") InputTextBox.Visibility = System.Windows.Visibility.Hidden;
            }

            else InputTextBox.Focus();
            InputTextBox.Text = inputText;
            this.Width = width;
            this.Height = height;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelled = true;
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            cancelled = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background =new SolidColorBrush(Colors.CornflowerBlue);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = null;
        }
    }
}

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

namespace Guitar.View3 {
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window {
        public bool accepted = false;

        public InputWindow (string title) {
            InitializeComponent ();
            this.Title = title;
            this.customLabel.Text = title;
            this.inputTextbox.Focus ();
        }

        private void okButton_Click (object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty (inputTextbox.Text.Trim ())) {
                MessageBox.Show ("Prazan tekst");
                return;
            }
            this.Close ();
            accepted = true;
        }

        private void cancelButton_Click (object sender, RoutedEventArgs e) {
            this.Close ();
            accepted = false;
        }
    }
}

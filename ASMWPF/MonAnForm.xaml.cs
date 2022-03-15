﻿using ASMLibrary.Management.IService;
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
using System.Windows.Shapes;

namespace ASMWPF
{
    /// <summary>
    /// Interaction logic for MonAnForm.xaml
    /// </summary>
    public partial class MonAnForm : Window
    {
        IMonAnService monAnService;
        public MonAnForm(IMonAnService monAn)
        {
            InitializeComponent();
            monAnService = monAn;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // lv.ItemsSource = monAnService.GetMonAns();
            Uri fileUri = new Uri(monAnService.GetMonAns().FirstOrDefault(a => a.Idmon.Equals("M0001")).Hinh);
            img.Source = new BitmapImage(fileUri);
            txt.Text = monAnService.GetIDCuoi();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoaiForm loai = new LoaiForm();
            loai.Show();
        }
    }
}
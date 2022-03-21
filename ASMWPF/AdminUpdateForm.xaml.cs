using ASMLibrary.DataAccess;
using ASMLibrary.Management.Sevice;
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
    /// Interaction logic for AdminUpdateForm.xaml
    /// </summary>
    public partial class AdminUpdateForm : Window
    {
        KhachHang admin;
        MonAn monAn;
        MonAnService monAnService = new MonAnService();
        LoaiService loaiService = new LoaiService();

        List<Loai> loais;
        int i = 0;
        public AdminUpdateForm(KhachHang khach, MonAn mon)
        {
            InitializeComponent();
            admin = khach;
            monAn = mon;
            loadData();
        }
        private void loadData()
        {
            int y=0;
            tbFoodName.Text = monAn.TenMon;
            tbPrice.Text = (int)monAn.DonGia+"";
            tbNote.Text = monAn.ChuThich;
            lbImg.Content = monAn.Hinh;

            loais = loaiService.GetLoais().ToList();
            foreach (Loai loai in loais)
            {
                cbbloai.Items.Add(loai.TenLoai);
                if(loai.Idloai.Equals(monAn.Idloai))i=y;
                y++;
            }
            cbbloai.SelectedIndex = i;

        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Files|*.jpg;*.jpeg;*.png;";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                lbImg.Content = filename;
            }
        }
        bool CheckCreate()
        {
            bool check = true;

           
            if (tbFoodName.Text.Equals(""))
            {
                lbNameError.Content = "FoodName is Null";
                check = false;
            }
            if (tbPrice.Text.Equals(""))
            {
                lbPriceError.Content = "Price is Null";
                check = false;
            }
            if (tbNote.Text.Equals(""))
            {
                lbNoteError.Content = "Note is Null";
                check = false;
            }
            return check;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckCreate())
            {


                monAn.TenMon = tbFoodName.Text;
                monAn.Hinh = lbImg.Content.ToString();
                monAn.Idloai = loais[cbbloai.SelectedIndex].Idloai;
                monAn.DonGia = Convert.ToDecimal(tbPrice.Text);
                monAn.ChuThich = tbNote.Text;
                    
               
                monAnService.UpdateMonAn(monAn);

                MessageBox.Show("Update food Successfully", "Update food information");
                AdminHomePageForm adminHome = new AdminHomePageForm(admin,0);
                adminHome.Show();
                this.Close();
            }
           
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            AdminHomePageForm adminHome = new AdminHomePageForm(admin,0);
            adminHome.Show();
            this.Close();
        }
    }
}

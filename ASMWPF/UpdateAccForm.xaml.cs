using ASMLibrary.DataAccess;
using ASMLibrary.Management.Sevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UpdateAccForm.xaml
    /// </summary>
    public partial class UpdateAccForm : Window
    {
        private KhachHangSevice khachHangSevice = new KhachHangSevice();
        public KhachHang khachHang;
        public KhachHang admin;

        public UpdateAccForm(KhachHang khach, KhachHang admin)
        {
            InitializeComponent();
            khachHang = khach;
            loaddata();
            this.admin = admin;
        }
        public void loaddata()
        {
            lbUserName.Content = khachHang.Username;
            txtFullName.Text = khachHang.HotenKh;
            txtMail.Text = khachHang.MailKh;
            txtPhone.Text = khachHang.Sdtkh;
            txtAddress.Text = khachHang.DiachiKh;
        }
        private bool Checktext()
        {
            bool check = true;
            lbFullNameError.Content = "";
            lbMailError.Content = "";
            lbPhoneError.Content = "";
            lbAddressError.Content = "";
            if (txtFullName.Text.Equals(""))
            {
                lbFullNameError.Content = "FullName is null";
                check = false;
            }
            if (!Regex.IsMatch(txtMail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                lbMailError.Content = "Email is Wrong format";
                check = false;
            }
            if (!Regex.IsMatch(txtPhone.Text, @"^(\+84|0[3|5|7|8|9])+([0-9]{8})")) // [\+]?[0-9]{2}?[0-9]{9,10}
            {
                lbPhoneError.Content = "Phone is Wrong format";
                check = false;
            }
            if (txtAddress.Text.Equals(""))
            {
                lbAddressError.Content = "Address is null";
                check = false;
            }

            return check;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Checktext())
            {
                khachHang.HotenKh = txtFullName.Text.ToString();
                khachHang.MailKh = txtMail.Text.ToString();
                khachHang.Sdtkh = txtPhone.Text.ToString();
                khachHang.DiachiKh = txtAddress.Text.ToString();

                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Update Info Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        khachHangSevice.UpdateKhachHang(khachHang);
                        MessageBox.Show("Update Info Success!!", "Change Info Confirmation");
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close(); 
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            AdminHomePageForm adminHome = new AdminHomePageForm(admin,2);
            adminHome.Show();
            this.Close();
        }
    }
}

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
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        private KhachHangSevice khachHangSevice = new KhachHangSevice();
        public KhachHang khachHang;
        public UserForm(KhachHang khach)
        {
            InitializeComponent();
            khachHang = khach;
            loaddata();
        }

        public void loaddata()
        {
            lbUserName.Content = khachHang.Username;
            txtFullName.Text = khachHang.HotenKh;
            txtMail.Text = khachHang.MailKh;
            txtPhone.Text = khachHang.Sdtkh;
            txtAddress.Text = khachHang.DiachiKh;
        }


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePageForm home = new HomePageForm(khachHang);
            home.Show();
            this.Close();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            CartForm cart = new CartForm(khachHang);
            cart.Show();
            this.Close();
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

        private bool CheckChangePass()
        {
            bool check = true;
            lbPasswordError.Content = "";
            lbNewPasswordError.Content = "";
            lbComfirmPassError.Content = "";
            if (!pbPassword.Password.ToString().Equals(khachHang.Password))
            {
                lbPasswordError.Content = "Password is Wrong!";
                check = false;
            }
            if (!Regex.IsMatch(pbNewPassword.Password.ToString(), @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                lbNewPasswordError.Content = "pass is >8,have char and number";
                check = false;
            }
             if (pbNewPassword.Password.ToString() != pbConfirmPassword.Password.ToString())
            {
                lbComfirmPassError.Content = "Confirm pass not same";
                check = false;
            }

            return check;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {

            if (CheckChangePass())
            {
                khachHang.Password = pbNewPassword.Password.ToString();

                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Change Password Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        khachHangSevice.UpdateKhachHang(khachHang);
                        MessageBox.Show("Change Password Success!!", "Change Password Confirmation");
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
    }
}
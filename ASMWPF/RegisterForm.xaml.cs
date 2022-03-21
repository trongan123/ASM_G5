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
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        KhachHang khach = new KhachHang();
        KhachHangSevice khachHangSevice = new KhachHangSevice();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private KhachHang khachHang()
        {
            if (Checktext() == true)
            {
                khach.Idkh = khachHangSevice.GetIDCuoi();
                khach.Username = txtUsername.Text;
                khach.Password = txtPassword.Password.ToString();
                khach.HotenKh = txtFullname.Text;
                khach.MailKh = txtEmail.Text;
                khach.Sdtkh = txtPhone.Text;
                khach.DiachiKh = txtAddress.Text;
                khach.Role = "User";
                khach.Tt = 1;

                return khach;
            }
            return null;
        }

        private bool Checktext()
        {
            if (txtUsername.Text.Equals(""))
            {
                lbRegisterNotify.Content = "UserName is null";
                return false;
            }
            else if (khachHangSevice.CheckUserName(txtUsername.Text) != null)
            {
                lbRegisterNotify.Content = "UserName is Exist";

                return false;
            }
            else if (!Regex.IsMatch(txtPassword.Password.ToString(), @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                lbRegisterNotify.Content = "pass is >8 and have char and number";
                return false;
            }
            else if (txtPassword.Password.ToString() != txtConfirmPass.Password.ToString())
            {
                lbRegisterNotify.Content = "Confirm pass not same";
                return false;
            }
            else if (txtFullname.Text.Equals(""))
            {
                lbRegisterNotify.Content = "FullName is null";
                return false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                lbRegisterNotify.Content = "Email is Wrong format";
                return false;
            }
            else if (!Regex.IsMatch(txtPhone.Text, @"^(\+84|0[3|5|7|8|9])+([0-9]{8})")) // [\+]?[0-9]{2}?[0-9]{9,10}
            {
                lbRegisterNotify.Content = "Phone is Wrong format";
                return false;
            }
            else if (txtAddress.Text.Equals(""))
            {
                lbRegisterNotify.Content = "Address is null";
                return false;
            }

            return true;
        }



        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            if (Checktext())
            {
                try
                {
                    KhachHang khach = khachHang();
                    khachHangSevice.AddKhachHang(khach);

                    MessageBox.Show("Register Successfully","Register infor");
                    HomePageForm Home = new HomePageForm(khach);
                    Home.Show();
                    this.Close();
                }
                catch (Exception)
                {

                }

            }

        }

        private void btnBackLogin_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtConfirmPass.Password = "";
            txtFullname.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";

            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}

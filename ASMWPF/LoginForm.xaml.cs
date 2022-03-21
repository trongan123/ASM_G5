using ASMLibrary.DataAccess;
using ASMLibrary.Management.IService;
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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private KhachHangSevice khachHangSevice = new KhachHangSevice();
        private KhachHang khachHang;
      //  IMonAnService monAn;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();   
        }

        private void lbRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterForm  register = new RegisterForm();
            register.Show();
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        void Login()
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password.ToString();

            khachHang = khachHangSevice.CheckLogin(username, password);

            if (khachHang != null)
            {
                if (khachHang.Role.Equals("User") && khachHang.Tt.Equals(1))
                {
                    HomePageForm Home = new HomePageForm(khachHang);

                    Home.Show();
                    this.Close();
                }

                if (khachHang.Role.Equals("Admin") && khachHang.Tt.Equals(1))
                {
                    AdminHomePageForm admin = new AdminHomePageForm(khachHang,0);                
                    MessageBox.Show("Welcome admin: " + khachHang.HotenKh);
                    admin.Show();
                    this.Close();
                }

                if ((khachHang.Role.Equals("User") || khachHang.Role.Equals("Admin")) && khachHang.Tt.Equals(0))
                {
                    MessageBox.Show("Sorry but your account has been blocked");
                }
            }
            else if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Null kìa");
            }
            else if(khachHang == null)
            {
                MessageBox.Show("Sai òi nha");
            }
        }
    }
}

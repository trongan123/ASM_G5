using ASMLibrary.DataAccess;
using ASMLibrary.Management.Sevice;
using Ganss.Excel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
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
using Button = System.Windows.Controls.Button;

namespace ASMWPF
{
    /// <summary>
    /// Interaction logic for AdminHomePageForm.xaml
    /// </summary>
    public partial class AdminHomePageForm : System.Windows.Window
    {
        public KhachHang admin;
        MonAnService monAnService = new MonAnService();
        LoaiService loaiService = new LoaiService();
        KhachHangSevice khachHangSevice = new KhachHangSevice();
        DonHangService donHangService = new DonHangService(); 
        List<MonAn> monAns;
        List<Loai> loais;
        List<KhachHang> khachHangs;
        List<DonHang> donHangs;
        public AdminHomePageForm(KhachHang ad)
        {
            InitializeComponent();
            admin = ad;
            ClearErrorLabel();
            loaddataMenu();
            loaddataCreate();
            loaddataACC();
            loaddataDonHang();
        }

        void ClearErrorLabel()
        {
            lbNameError.Content = "";
            lbNoteError.Content = "";
            lbPriceError.Content = "";
            lbImg.Content = "";
        }
        private void loaddataMenu()
        {
            monAns = monAnService.GetMonAns().ToList();
            List<MenuItem> foods = new List<MenuItem>();
            

            foreach (MonAn mon in monAns)
            {
                foods.Add(new MenuItem { Id = mon.Idmon, Price = "Giá :" + mon.DonGia, Name = mon.TenMon, ImageData = LoadImage(mon.Hinh) });
            }
            
            
            lvMenu.ItemsSource = foods;

        }

        private void loaddataCreate()
        {
            loais = loaiService.GetLoais().ToList();
            foreach (Loai loai in loais)
            {
                cbbLoai.Items.Add(loai.TenLoai);
            }
        }
        private void loaddataACC()
        {
           khachHangs = khachHangSevice.GetKhachHangs().ToList();
            lvAccount.ItemsSource = khachHangs;
        }
        private void loaddataDonHang()
        {
            donHangs = donHangService.GetDonHangList().ToList();
            List<dataDonHang> datas = new List<dataDonHang>();
            foreach (var item in donHangs)
            {  
                datas.Add(new dataDonHang {IddonHang = item.IddonHang, Name = khachHangSevice.GetKhachHangByID(item.Idkh).HotenKh, Diachi = item.Diachi, Sdt= item.Sdt, 
                    ThoiGian = String.Format("{0:yyyy/MM/dd}", item.ThoiGian), TongTien= item.TongTien});
            }
            lvOrder.ItemsSource = datas;
        }
        private BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(filename));
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


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as MenuItem;
            AdminUpdateForm update = new AdminUpdateForm(admin, monAnService.GetMonAnByID(rowItem.Id));
            update.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete food Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var rowItem = (sender as Button).DataContext as MenuItem;
                    monAnService.DeleteMonAn(monAnService.GetMonAnByID(rowItem.Id));
                    loaddataMenu();
                    MessageBox.Show("Delete Food Success!!");
                }
                catch (Exception)
                {
                    throw;
                }

            }
          
        }
        bool CheckCreate()
        {
            bool check = true;

            if (tbFoodName.Equals("")){
                lbNameError.Content = "FoodName is Null";
                check = false;
            }
            if (tbPrice.Equals("")){
                lbNameError.Content = "Price is Null";
                check = false;
            }
            if (tbNote.Equals("")){
                lbNameError.Content = "Note is Null";
                check = false;
            }
            if (cbbLoai.SelectedValue.Equals(""))
            {
               // lbNameError.Content = "Note is Null";
                check = false;
            }
            return check;
        }

        private void tbPrice_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckCreate())
            {
                MonAn monAn = new MonAn
                {
                    Idmon = monAnService.GetIDCuoi(),
                    TenMon = tbFoodName.Text,
                    Hinh = lbImg.Content.ToString(),
                    Idloai = loais[cbbLoai.SelectedIndex].Idloai,
                    DonGia = Convert.ToDecimal(tbPrice.Text),
                    ChuThich = tbNote.Text,
                    Rate = 1,
                    Tt = 1
                };
                monAnService.AddMonAn(monAn);
                MessageBox.Show("thanh cong");
                loaddataMenu();
            }
        }

        private void btnUpdateAcc_Click(object sender, RoutedEventArgs e)
        {
            loaddataACC();
        }

        private void btndeleteAcc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete food Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    var rowItem = (sender as Button).DataContext as KhachHang;
                    KhachHang khach = khachHangSevice.GetKhachHangByID(rowItem.Idkh);
                    if (khach != null)
                    {
                        khachHangSevice.DeleteKhachHang(khach);
                    }
                    loaddataACC();
                    MessageBox.Show("Delete Info Success!!");
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private void btnExportOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelMapper mapper = new ExcelMapper();
                var location = @"OrderFile.xlsx";
                mapper.Save(location, donHangs, "SheetName", true);
                MessageBox.Show("Exported");
            }
            catch (Exception ex) { MessageBox.Show("Failed"); }
        }

        private void btnExportAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelMapper mapper = new ExcelMapper();
                var location = @"AccountFile.xlsx";
                mapper.Save(location, khachHangs, "SheetName", true);
                MessageBox.Show("Exported");
            }
            catch (Exception ex) { MessageBox.Show("Failed"); }
        }
    }
    public class dataDonHang
    {
      
        public string IddonHang { get; set; } = null!;
        public string? Name { get; set; }
        public string? Diachi { get; set; }
        public string? Sdt { get; set; }
        public string? ThoiGian { get; set; }
        public decimal? TongTien { get; set; }
        public int? Tt { get; set; }

    }
}

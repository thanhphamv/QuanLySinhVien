using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo form đăng nhập
            using (DangNhap dangNhap = new DangNhap())
            {
                // Nếu đăng nhập thành công
                if (dangNhap.ShowDialog() == DialogResult.OK)
                {
                    // Mở form TrangChu
                    Application.Run(new TrangChu());
                }
                else
                {
                    // Thoát ứng dụng nếu đăng nhập không thành công
                    Application.Exit();
                }
            }
        }
    }
}

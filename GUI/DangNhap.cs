using QuanLySinhVien.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.GUI
{
    public partial class DangNhap : Form
    {
        BLL_DangNhap bllDangNhap = new BLL_DangNhap();
        public DangNhap()
        {
            InitializeComponent();
        }
        int dem = 0;
        void newDangNhap()
        {
            txtTDN.Clear();
            txtMK.Clear();
            txtTDN.Focus();
        }
        public void reDangNhap()
        {
            txtMK.Clear();
            txtMK.Focus();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát khỏi chương trình", "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DataTable dtDN = bllDangNhap.getData($"SELECT tenDangNhap, matKhau,VaiTro  FROM TaiKhoan WHERE tenDangNhap = '{txtTDN.Text.Trim()}' AND matKhau = '{txtMK.Text.Trim()}'");

            if (dtDN.Rows.Count > 0)
            {
                string VaiTro = dtDN.Rows[0]["VaiTro"].ToString();
                string selectedRole = cbbvaitro.SelectedItem?.ToString();
                string maSV = txtTDN.Text.Trim(); // Giả sử `maSV` cũng là tên đăng nhập

                if (VaiTro != selectedRole)
                {
                    MessageBox.Show("Sai vai trò người dùng. Vui lòng chọn vai trò đúng.");
                    return;
                }

                if (VaiTro == "ADMIN")
                {
                    MessageBox.Show("Đăng nhập thành công với quyền admin");
                    // Điều hướng đến giao diện quản lý chính
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (VaiTro == "SINHVIEN")
                {
                    MessageBox.Show("Đăng nhập thành công với quyền user.");
                    // Mở form BangDiemChiTiet chỉ cho phép xem bảng điểm
                    BangDiemChiTiet formBangDiem = new BangDiemChiTiet(maSV);
                    formBangDiem.ShowDialog();
                    this.Close();
                }
                else if (VaiTro == "GV")
                {
                    MessageBox.Show("Đăng nhập thành công với quyền teacher.");
                    TrangChu trangChu = new TrangChu(); // Tạo đối tượng TrangChu nếu cần
                    DiemThi formDiemThi = new DiemThi(trangChu, VaiTro);  // Truyền role vào DiemThi
                    formDiemThi.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                newDangNhap();
                dem++;
            }

            if (dem == 3)
            {
                MessageBox.Show("Bạn đã nhập sai 3 lần liên tiếp, đóng ứng dụng");
                Application.Exit();
            }
        }




        private void DangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
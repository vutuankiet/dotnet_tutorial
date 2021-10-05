using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2_T3H
{
    public partial class frmTableManager : Form
    {
        string flag;
        DataTable dtPhong;
        public frmTableManager()
        {
            InitializeComponent();
        }

        private void cbbManager_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void phongToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        public DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Phong");
            dt.Columns.Add("TinhTrangPhong");
            dt.Columns.Add("LoaiPhong");
            dt.Columns.Add("ViTri");
            dt.Columns.Add("GiaThanh");
            return dt;
        }

        public void LookControl()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtPhong.ReadOnly = true;
            txtTinhTrangPhong.ReadOnly = true;
            txtLoaiPhong.ReadOnly = true;
            txtViTri.ReadOnly = true;
            txtGiaThanh.ReadOnly = true;

            btnThem.Focus();
        }

        public void UnlookControl()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtPhong.ReadOnly = false;
            txtTinhTrangPhong.ReadOnly = false;
            txtLoaiPhong.ReadOnly = false;
            txtViTri.ReadOnly = false;
            txtGiaThanh.ReadOnly = false;

            txtPhong.Focus();
        }

        private void frmTableManager_Load(object sender, EventArgs e)
        {
            LookControl();
            dtPhong = createTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            UnlookControl();
            flag = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            UnlookControl();
            flag = "edit";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            UnlookControl();
            flag = "delete";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
         if(flag == "add")
            {
                if(checkData())
                {
                    dtPhong.Rows.Add(txtPhong.Text, txtTinhTrangPhong.Text, txtLoaiPhong.Text, txtViTri.Text, txtGiaThanh.Text);
                    dataGridPhong.DataSource = dtPhong;
                    dataGridPhong.RefreshEdit();
                }
            }
        }

        public bool checkData()
        {
            if(string.IsNullOrWhiteSpace(txtPhong.Text))
            {
                MessageBox.Show("Ban chua nhap phong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTinhTrangPhong.Text))
            {
                MessageBox.Show("Ban chua nhap phong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTinhTrangPhong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLoaiPhong.Text))
            {
                MessageBox.Show("Ban chua nhap phong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoaiPhong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtViTri.Text))
            {
                MessageBox.Show("Ban chua nhap phong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtViTri.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGiaThanh.Text))
            {
                MessageBox.Show("Ban chua nhap phong", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiaThanh.Focus();
                return false;
            }
            return true;
        }

    }
}

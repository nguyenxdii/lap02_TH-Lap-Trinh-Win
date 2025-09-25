using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap02_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // 2.1
            cmbFaculty.SelectedIndex = 0; // da chinh thanh dropdownlist trong properties
            optFemale.Checked = true;

#if DEBUG
            LoadSampleData();   //load data khi debug
#endif

            dgvStudent.CellClick += dgvStudent_CellClick; // gan event click vao bang thong tin
            UpdateGenderCount(); // tinh so luong nam nu (ban dau la 0)
        }

        private void LoadSampleData()
        {
            // du lieu mau
            dgvStudent.Rows.Add("11", "Nguyễn Văn A", "Nam", 7.5, "CNTT");
            dgvStudent.Rows.Add("12", "Trần Thị B", "Nữ", 8.0, "QTKD");
            dgvStudent.Rows.Add("13", "Lê Văn C", "Nam", 6.2, "NNA");
        }

        // kiem tra mssv da co trong bang hay chua
        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value != null &&
                !string.IsNullOrEmpty(dgvStudent.Rows[i].Cells[0].Value.ToString()))
                // dong thu i (cot thu 0, cells[0] tuc la mssv) k rong
                // && chuoi k rong -> co du lieu,
                // IsNullOrEmpty tra ve true neu null hoac rong ""
                // tra ve false neu co ky tu
                // !IsNullOrEmpty tra ve true neu co ky tu
                {
                    if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)  // dong i cot mssv co giong studentID k, co return i, k return -1
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void InsertUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nữ" : "Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = float.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = cmbFaculty.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentID.Text == "" || txtFullName.Text == "" || txtAverageScore.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên!");

                int selectedRow = GetSelectedRow(txtStudentID.Text); // kiem tra co sv trong bang chua
                if (selectedRow == -1)  // k tim thay sv
                {
                    selectedRow = dgvStudent.Rows.Add();    // them 1 dong ms
                    InsertUpdate(selectedRow);              // chen du lieu tu nho nhap qua bang
                    UpdateGenderCount();                    // cap nhat lai so luong nam nu
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                else // da co sv trong bang
                {
                    InsertUpdate(selectedRow);  // cap nhat lai du lieu dong do
                    UpdateGenderCount();        // cap nhat lai so luong nam nu
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtStudentID.Text);    // goi ham kiem tra mssv
                if (selectedRow == -1)  // k co nem ra ex k tim thay mssv
                {
                    throw new Exception("Không tìm thấy MSSV cần xóa!");
                }
                else // tim thay
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        UpdateGenderCount(); // cap nhat lai so luong nam nu
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2.4 tra lai ket qua ve thong tin sv
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // dam bao nhap vao du lieu k phai tieu de 
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];  // lay dong dc click
                txtStudentID.Text = row.Cells[0].Value?.ToString();
                txtFullName.Text = row.Cells[1].Value?.ToString();

                string gender = row.Cells[2].Value?.ToString();

                if (gender == "Nam")
                {
                    optMale.Checked = true;
                }
                else
                {
                    optFemale.Checked = true;
                }

                txtAverageScore.Text = row.Cells[3].Value?.ToString();
                cmbFaculty.Text = row.Cells[4].Value?.ToString();
            }
        }

        // 2.5 tinh so luong nam nu
        private void UpdateGenderCount()
        {
            int male = 0, female = 0; // khoi tao ban dau nam nu = 0
            foreach (DataGridViewRow row in dgvStudent.Rows)    // duyet tung dong trong dgv
            {
                if (row.Cells[2].Value != null)     // kiem tra xem cot gioi tinh co du lieu hay k, null == bo qua
                {
                    string gender = row.Cells[2].Value.ToString();      // chuyen du lieu trong cot gioi tinh thanh chuoi
                    if (gender == "Nam")
                    {
                        male++;
                    }
                    else if (gender == "Nữ")
                    {
                        female++;
                    }
                }
            }

            txtTotalMale.Text = male.ToString();
            txtTotalFemale.Text = female.ToString();
        }
    }
}

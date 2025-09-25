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
            cmbFaculty.SelectedIndex = 0;
            optFemale.Checked = true;

#if DEBUG
            LoadSampleData();
#endif

            dgvStudent.CellClick += dgvStudent_CellClick; // event clickcell
            UpdateGenderCount(); // so luong nam nu ban dau
        }

        private void LoadSampleData()
        {
            // du lieu mau
            dgvStudent.Rows.Add("11", "Nguyễn Văn A", "Nam", 7.5, "CNTT");
            dgvStudent.Rows.Add("12", "Trần Thị B", "Nữ", 8.0, "QTKD");
            dgvStudent.Rows.Add("13", "Lê Văn C", "Nam", 6.2, "NNA");
        }

        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value != null &&
                !string.IsNullOrEmpty(dgvStudent.Rows[i].Cells[0].Value.ToString()))
                {
                    if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
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

                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1)
                {
                    selectedRow = dgvStudent.Rows.Add();
                    InsertUpdate(selectedRow);
                    UpdateGenderCount();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    InsertUpdate(selectedRow);
                    UpdateGenderCount();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy MSSV cần xóa!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        UpdateGenderCount();
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2.4 tra lai ket qua ve thong tin sv
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // dam bao rang se k chon header
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
                txtStudentID.Text = row.Cells[0].Value?.ToString();
                txtFullName.Text = row.Cells[1].Value?.ToString();
                string gender = row.Cells[2].Value?.ToString();
                if (gender == "Nam")
                    optMale.Checked = true;
                else
                    optFemale.Checked = true;
                txtAverageScore.Text = row.Cells[3].Value?.ToString();
                cmbFaculty.Text = row.Cells[4].Value?.ToString();
            }
        }

        // 2.5 tinh so luong nam nu
        private void UpdateGenderCount()
        {
            int male = 0, female = 0; // khoi tao ban dau nam nu = 0
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    string gender = row.Cells[2].Value.ToString();
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAB04_01.Model;
using LAB04_01.Controller;
using System.Text.RegularExpressions;

namespace LAB04_01
{
    public partial class frmFacultyManagement : Form
    {
        private DataTable data;
        public frmFacultyManagement()
        {
            InitializeComponent();
            data = new DataTable();
            data.Columns.Add("Mã Khoa", typeof(int));
            data.Columns.Add("Tên Khoa", typeof(string));
            data.Columns.Add("Tổng số GS", typeof(int));
            dgvFaculty.DataSource = data;
        }

        private void frmFacultyManagement_Load(object sender, EventArgs e)
        {
            CenterToParent();
            FacultyItemLoad();
        }

        private void FacultyItemLoad()
        {
            List<Faculty> faculties = FacultyController.GetFacultyList();
            foreach (var item in faculties)
            {
                AddFacultyToDataTable(item);
            }
        }

        private void AddFacultyToDataTable(Faculty faculty)
        {
            DataRow row = data.NewRow();
            row["Mã Khoa"] = faculty.FacultyID;
            row["Tên Khoa"] = faculty.FacultyName;
            row["Tổng số GS"] = faculty.TotalProfessor;
            data.Rows.Add(row);
        }

        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvFaculty.CurrentCell.RowIndex;
            txtFacultyID.Text = dgvFaculty.Rows[index].Cells[0].Value.ToString();
            txtFacultyName.Text = dgvFaculty.Rows[index].Cells[1].Value.ToString();
            txtTotalProfessor.Text = dgvFaculty.Rows[index].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckFacultyName() && CheckTotalProfessor())
            {
                Faculty faculty = GetFacultyFromForm();
                string Error = string.Empty;
                if (indexID(faculty.FacultyID) == -1)
                {
                    if (FacultyController.AddFaculty(faculty, out Error))
                    {
                        AddFacultyToDataTable(faculty);
                        MessageBox.Show("Thêm khoa thành công", "Success", MessageBoxButtons.OK);
                        SetDefault();
                    }
                    else
                    {
                        MessageBox.Show(Error, "Failure", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    int index = indexID(faculty.FacultyID);
                    if (FacultyController.UpdateFaculty(faculty.FacultyID, faculty, out Error))
                    {
                        data.Rows[index]["Tên Khoa"] = faculty.FacultyName;
                        data.Rows[index]["Tổng số GS"] = faculty.TotalProfessor;
                        MessageBox.Show("Cập nhật thành công", "Success", MessageBoxButtons.OK);
                        SetDefault();
                    }
                    else
                    {
                        MessageBox.Show(Error, "Failure", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckFacultyName() && CheckTotalProfessor())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Faculty faculty = GetFacultyFromForm();
                    string Error = string.Empty;
                    if (FacultyController.DeleteFaculty(faculty.FacultyID, out Error))
                    {
                        int index = indexID(faculty.FacultyID);
                        data.Rows.RemoveAt(index);
                        MessageBox.Show("Xóa khoa thành công", "Success", MessageBoxButtons.OK);
                        SetDefault();
                    }
                    else
                    {
                        MessageBox.Show(Error, "Failure", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private Faculty GetFacultyFromForm()
        {
            return new Faculty()
            {
                FacultyID = string.IsNullOrEmpty(txtFacultyID.Text) ? -1 : int.Parse(txtFacultyID.Text),
                FacultyName = txtFacultyName.Text,
                TotalProfessor = int.Parse(txtTotalProfessor.Text)
            };
        }

        private int indexID(int FacultyID)
        {
            for (int i = 0; i < data.Rows.Count; ++i)
            {
                if (data.Rows[i]["Mã Khoa"].Equals(FacultyID))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool CheckFacultyName()
        {
            if (string.IsNullOrEmpty(txtFacultyName.Text) || string.IsNullOrWhiteSpace(txtFacultyName.Text))
            {
                txtFacultyName.Focus();
                MessageBox.Show("Tên khoa không được để trống", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (!Regex.IsMatch(UtilityMethod.UnicodeToAscii(txtFacultyName.Text), @"^[A-Za-z ]+$"))
            {
                txtFacultyName.Focus();
                MessageBox.Show("Tên sinh viên không chứa số hoặc ký tự đặc biệt", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private bool CheckTotalProfessor()
        {
            int number;
            bool check;
            check = int.TryParse(txtTotalProfessor.Text, out number);
            if (string.IsNullOrWhiteSpace(txtTotalProfessor.Text) || string.IsNullOrEmpty(txtTotalProfessor.Text))
            {
                txtTotalProfessor.Focus();
                MessageBox.Show("Điểm trung bình không được để trống", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (!check)
            {
                txtTotalProfessor.Focus();
                MessageBox.Show("Phải nhập dữ liệu là số nguyên", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (number < 0)
            {
                txtTotalProfessor.Focus();
                MessageBox.Show("Số lượng phải lớn hơn 0", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void SetDefault()
        {
            txtFacultyID.Text = "";
            txtFacultyName.Text = "";
            txtTotalProfessor.Text = "";
        }
    }
}

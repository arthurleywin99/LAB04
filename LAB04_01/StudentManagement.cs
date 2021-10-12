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
    public partial class frmStudentManagement : Form
    {
        private DataTable data;

        public frmStudentManagement()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            data = new DataTable();
            data.Columns.Add("Mã Số SV", typeof(string));
            data.Columns.Add("Họ Tên", typeof(string));
            data.Columns.Add("Tên Khoa", typeof(string));
            data.Columns.Add("Điểm TB", typeof(float));
            dgvStudent.DataSource = data;
        }

        private void StudentManagement_Load(object sender, EventArgs e)
        {
            StudentDataLoad();
            ComboBoxFacultyItemLoad();
        }

        private void StudentDataLoad()
        {
            List<Student> students = StudentController.GetStudent();
            foreach (var item in students)
            {
                AddStudentToDataTable(item);
            }
        }

        private void AddStudentToDataTable(Student student)
        {
            DataRow row = data.NewRow();
            row["Mã Số SV"] = student.StudentID;
            row["Họ Tên"] = student.FullName;
            row["Tên Khoa"] = FacultyController.GetFacultyName(Convert.ToInt32(student.FacultyID));
            row["Điểm TB"] = student.AverageScore;
            data.Rows.Add(row);
        }

        private void ComboBoxFacultyItemLoad()
        {
            List<Faculty> faculties = FacultyController.GetFacultyList();
            cboFaculty.DataSource = faculties;
            cboFaculty.DisplayMember = "FacultyName";
            cboFaculty.ValueMember = "FacultyID";
            cboFaculty.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckStudentID() && CheckStudentName() && CheckScore())
            {
                Student student = GetStudentFromForm();
                if (indexID(student.StudentID) != -1)
                {
                    MessageBox.Show("Đã tồn tại sinh viên với mã này", "Failure", MessageBoxButtons.OK);
                }
                else
                {
                    string Error = string.Empty;
                    if (StudentController.AddStudent(student, out Error))
                    {
                        AddStudentToDataTable(student);
                        MessageBox.Show("Thêm sinh viên thành công", "Success", MessageBoxButtons.OK);
                        SetDefault();
                    }
                    else
                    {
                        MessageBox.Show(Error, "Failure", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private Student GetStudentFromForm()
        {
            return new Student()
            {
                StudentID = txtStudentID.Text,
                FullName = txtFullName.Text,
                AverageScore = float.Parse(txtAverageScore.Text),
                FacultyID = Convert.ToInt32(cboFaculty.SelectedValue)
            };
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CheckStudentID() && CheckStudentName() && CheckScore())
            {
                Student newStudent = GetStudentFromForm();
                int index = indexID(newStudent.StudentID);
                if (index != -1)
                {
                    string Error = string.Empty;
                    if (StudentController.UpdateStudent(newStudent.StudentID, newStudent, out Error))
                    {
                        data.Rows[index]["Họ Tên"] = newStudent.FullName;
                        data.Rows[index]["Tên Khoa"] = FacultyController.GetFacultyName(Convert.ToInt32(newStudent.FacultyID));
                        data.Rows[index]["Điểm TB"] = newStudent.AverageScore;
                        MessageBox.Show("Chỉnh sửa sinh viên thành công", "Success", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show(Error, "Failure", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Không tồn tại sinh viên với mã này", "Failure", MessageBoxButtons.OK);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckStudentID() && CheckStudentName() && CheckScore())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Student student = GetStudentFromForm();
                    string Error = string.Empty;
                    if (StudentController.DeleteStudent(student.StudentID, out Error))
                    {
                        int index = indexID(student.StudentID);
                        data.Rows.RemoveAt(index);
                        MessageBox.Show("Xóa sinh viên thành công", "Success", MessageBoxButtons.OK);
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

        private int indexID(string StudentID)
        {
            for (int i = 0; i < data.Rows.Count; ++i)
            {
                if (data.Rows[i]["Mã Số SV"].Equals(StudentID))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool CheckStudentID()
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrEmpty(txtStudentID.Text))
            {
                txtStudentID.Focus();
                MessageBox.Show("Mã sinh viên không được để trống", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (txtStudentID.Text.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                txtStudentID.Focus();
                MessageBox.Show("Mã sinh viên không chứa ký tự đặc biệt", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (txtStudentID.Text.Length != 10)
            {
                txtStudentID.Focus();
                MessageBox.Show("Mã sinh viên là một chuỗi 10 ký tự", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private bool CheckStudentName()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrEmpty(txtFullName.Text))
            {
                txtFullName.Focus();
                MessageBox.Show("Tên sinh viên không được để trống", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (!Regex.IsMatch(UtilityMethod.UnicodeToAscii(txtFullName.Text), @"^[A-Za-z ]+$"))
            {
                txtFullName.Focus();
                MessageBox.Show("Tên sinh viên không chứa số hoặc ký tự đặc biệt", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private bool CheckScore()
        {
            float number;
            bool check;
            check = float.TryParse(txtAverageScore.Text, out number);
            if (string.IsNullOrWhiteSpace(txtAverageScore.Text) || string.IsNullOrEmpty(txtAverageScore.Text))
            {
                txtAverageScore.Focus();
                MessageBox.Show("Điểm trung bình không được để trống", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (!check)
            {
                txtAverageScore.Focus();
                MessageBox.Show("Phải nhập dữ liệu là số thực", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (number < 0 || number > 10)
            {
                txtAverageScore.Focus();
                MessageBox.Show("Điểm phải nằm trong khoảng từ 0 - 10", "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void SetDefault()
        {
            txtStudentID.Text = "";
            txtFullName.Text = "";
            cboFaculty.SelectedIndex = 0;
            txtAverageScore.Text = "";
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvStudent.CurrentCell.RowIndex;
            txtStudentID.Text = dgvStudent.Rows[index].Cells[0].Value.ToString();
            txtFullName.Text = dgvStudent.Rows[index].Cells[1].Value.ToString();
            for (int i = 0; i < cboFaculty.Items.Count; ++i)
            {
                if (cboFaculty.Items[i].ToString().Equals(dgvStudent.Rows[index].Cells[2].Value.ToString()))
                {
                    cboFaculty.SelectedIndex = i;
                    break;
                }
            }
            txtAverageScore.Text = dgvStudent.Rows[index].Cells[3].Value.ToString();
        }

        private void tsmFaculty_Click(object sender, EventArgs e)
        {
            new frmFacultyManagement().ShowDialog();
            Close();
        }

        private void tsmSearch_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmSearchForm Form = new frmSearchForm();
            Form.sender(data);
            Form.ShowDialog();
            Visible = true;
        }
    }
}
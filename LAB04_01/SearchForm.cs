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

namespace LAB04_01
{
    public partial class frmSearchForm : Form
    {
        public delegate void SendData(DataTable dataTable);
        public SendData sender;
        private DataTable data;

        public frmSearchForm()
        {
            InitializeComponent();
            sender = new SendData(GetData);
            CenterToParent();
            ComboBoxFacultyItemLoad();
        }

        private void ComboBoxFacultyItemLoad()
        {
            List<Faculty> faculties = FacultyController.GetFacultyList();
            cboFaculty.DataSource = faculties;
            cboFaculty.DisplayMember = "FacultyName";
            cboFaculty.ValueMember = "FacultyID";
            cboFaculty.SelectedIndex = 0;
        }

        private void GetData(DataTable Data)
        {
            data = Data;
        }

        private void frmSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvStudent.DataSource = null;
            txtResult.Text = "0";
            List<Student> result = data.AsEnumerable().Select(m => new Student()
            {
                StudentID = m.Field<string>("Mã Số SV"),
                FullName = m.Field<string>("Họ Tên"),
                FacultyID = FacultyController.GetFacultyID(m.Field<string>("Tên Khoa")),
                AverageScore = m.Field<float>("Điểm TB")
            }).ToList().Where(p => p.StudentID.Equals(txtStudentID.Text) && p.FullName.Equals(txtFullName.Text) && p.FacultyID == Convert.ToInt32(cboFaculty.SelectedValue)).ToList();
            if (result.Count > 0)
            {
                DataTable resultTable = new DataTable();
                resultTable.Columns.Add("Mã Số SV", typeof(string));
                resultTable.Columns.Add("Họ Tên", typeof(string));
                resultTable.Columns.Add("Tên Khoa", typeof(string));
                resultTable.Columns.Add("Điểm TB", typeof(float));
                foreach (var item in result)
                {
                    AddStudentToDataTable(resultTable, item);
                }
                dgvStudent.DataSource = resultTable;
                txtResult.Text = result.Count.ToString();
            }
        }

        private void AddStudentToDataTable(DataTable data, Student student)
        {
            DataRow row = data.NewRow();
            row["Mã Số SV"] = student.StudentID;
            row["Họ Tên"] = student.FullName;
            row["Tên Khoa"] = FacultyController.GetFacultyName(Convert.ToInt32(student.FacultyID));
            row["Điểm TB"] = student.AverageScore;
            data.Rows.Add(row);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtStudentID.Text = "";
            txtResult.Text = "0";
            cboFaculty.SelectedIndex = 0;
            dgvStudent.DataSource = null;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

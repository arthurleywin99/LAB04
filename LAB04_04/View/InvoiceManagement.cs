using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAB04_04.Model;
using LAB04_04.Controller;

namespace LAB04_04.View
{
    public partial class fInvoiceManagement : Form
    {
        private DataTable data;
        private DateTime FromDate, ToDate;

        public fInvoiceManagement()
        {
            InitializeComponent();
            data = new DataTable();
            InitDataTable(data);
            dgvResult.DataSource = data;
        }

        private void InitDataTable(DataTable data)
        {
            data.Columns.Add("STT", typeof(int));
            data.Columns.Add("Số HĐ", typeof(string));
            data.Columns.Add("Ngày Đặt Hàng", typeof(string));
            data.Columns.Add("Ngày Giao Hàng", typeof(string));
            data.Columns.Add("Thành Tiền", typeof(long));
        }

        private void fInvoiceManagement_Load(object sender, EventArgs e)
        {
            List<Invoice> invoices = InvoiceController.GetInvoice();
            AddDataToDataTable(invoices);

            DataTable table = new DataTable();
            InitDataTable(table);
            foreach (DataRow item in data.Rows)
            {
                if (item["Ngày Giao Hàng"].Equals(DateTime.Now.ToString("MM/dd/yyyy"))) {
                    table.ImportRow(item);
                }
            }
            txtTotal.Text = table.Rows.Count.ToString();
            dgvResult.DataSource = table;
            FromDate = dtpFrom.Value;
            ToDate = dtpFrom.Value;
        }

        private void AddDataToDataTable(List<Invoice> invoices)
        {
            int index = 0;
            data.Rows.Clear();
            foreach (var item in invoices)
            {
                DataRow row = data.NewRow();
                row["STT"] = index + 1;
                row["Số HĐ"] = item.InvoiceNo;
                row["Ngày Đặt Hàng"] = item.OrderDate.ToShortDateString();
                row["Ngày Giao Hàng"] = item.DeliveryDate.ToShortDateString();
                row["Thành Tiền"] = InvoiceController.GetTotalByID(item.InvoiceNo);
                data.Rows.Add(row);
                index++;
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpFrom.Value, dtpTo.Value) > 0)
            {
                MessageBox.Show("Ngày trước phải nhỏ hơn ngày sau", "Error", MessageBoxButtons.OK);
                dtpFrom.Value = FromDate;
                data.Rows.Clear();
            }
            else
            {
                DataTable table = new DataTable();
                InitDataTable(table);
                foreach (DataRow item in data.Rows)
                {
                    if (DateTime.Compare(dtpFrom.Value.Date, DateTime.Parse(item["Ngày Giao Hàng"].ToString()).Date) <= 0 && DateTime.Compare(DateTime.Parse(item["Ngày Giao Hàng"].ToString()).Date, dtpTo.Value.Date) <= 0)
                    {
                        table.ImportRow(item);
                    }
                }
                txtTotal.Text = table.Rows.Count.ToString();
                dgvResult.DataSource = table;
                FromDate = dtpFrom.Value;
            }
        }

        private void ckbMonth_CheckedChanged(object sender, EventArgs e)
        {
            int lastDayInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDayInMonth);
            dtpTo_ValueChanged(sender, e);
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpTo.Value, dtpFrom.Value) < 0)
            {
                MessageBox.Show("Ngày sau phải lớn hơn ngày trước", "Error", MessageBoxButtons.OK);
                dtpTo.Value = ToDate;
                data.Rows.Clear();
            }
            else
            {
                DataTable table = new DataTable();
                InitDataTable(table);
                foreach (DataRow item in data.Rows)
                {
                    if (DateTime.Compare(dtpFrom.Value.Date, DateTime.Parse(item["Ngày Giao Hàng"].ToString()).Date) <= 0 && DateTime.Compare(DateTime.Parse(item["Ngày Giao Hàng"].ToString()).Date, dtpTo.Value.Date) <= 0)
                    {
                        table.ImportRow(item);
                    }
                }
                txtTotal.Text = table.Rows.Count.ToString();
                dgvResult.DataSource = table;
                ToDate = dtpTo.Value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML;
using ClosedXML.Excel;

namespace CompuScan_MES_Main
{
    public partial class Reports : Form
    {
        private int comboNum = 0;
        private int formHeight = 133;
        private int formWidth = 397;
        private Dictionary<string,object> selectedFilters = new Dictionary<string, object>();
        private List<string> reportItems = new List<string> { "Production Data", "Users" };
        private List<string> filterItems = new List<string> { "Time", "Operator" };
        private List<Control> controls = new List<Control>();
        private bool btnAddFilter = false;
        private bool btnRemoveFilter = false;
        private bool btnExport = false;

        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            foreach (string item in reportItems)
            {
                cb_ReportSubject.Items.Add(item);
            }
        }

        #region [Add Filter]
        private void Add_Filter_Click(object sender, EventArgs e)
        {
            ComboBox filterBox = new ComboBox();
            filterBox.Font = new Font("Microsoft Sans Serif", 12);
            filterBox.Size = new Size(242, 28);
            filterBox.Name = "filterBox" + comboNum;
            Label filterLabel = new Label();
            filterLabel.Text = "Option " + (comboNum+1) + ":";
            filterLabel.Font = new Font("Microsoft Sans Serif", 12);
            filterLabel.Size = new Size(80, 28);
            filterLabel.Name = "filterLabel" + comboNum;
            switch (comboNum)
            {
                case 0:
                    formHeight += 50;
                    this.Size = new Size(formWidth, formHeight);
                    filterBox.Location = new Point(cb_ReportSubject.Location.X, cb_ReportSubject.Location.Y + 80);
                    filterLabel.Location = new Point(label1.Location.X, label1.Location.Y + 80);
                    AddFilterItems(filterBox);
                    break;
                case 1:
                    formHeight += 50;
                    this.Size = new Size(formWidth, formHeight);
                    filterBox.Location = new Point(cb_ReportSubject.Location.X, cb_ReportSubject.Location.Y + 130);
                    filterLabel.Location = new Point(label1.Location.X, label1.Location.Y + 130);
                    AddFilterItems(filterBox);
                    break;
                case 2:
                    formHeight += 50;
                    this.Size = new Size(formWidth, formHeight);
                    filterBox.Location = new Point(cb_ReportSubject.Location.X, cb_ReportSubject.Location.Y + 180);
                    filterLabel.Location = new Point(label1.Location.X, label1.Location.Y + 180);
                    AddFilterItems(filterBox);
                    break;
                case 3:
                    formHeight += 50;
                    this.Size = new Size(formWidth, formHeight);
                    filterBox.Location = new Point(cb_ReportSubject.Location.X, cb_ReportSubject.Location.Y + 230);
                    filterLabel.Location = new Point(label1.Location.X, label1.Location.Y + 230);
                    AddFilterItems(filterBox);
                    break;
                case 4:
                    formHeight += 50;
                    this.Size = new Size(formWidth, formHeight);
                    filterBox.Location = new Point(cb_ReportSubject.Location.X, cb_ReportSubject.Location.Y + 280);
                    filterLabel.Location = new Point(label1.Location.X, label1.Location.Y + 280);
                    AddFilterItems(filterBox);
                    break;
                default:
                    break;
            }
            filterBox.SelectedValueChanged += FilterBox_SelectedValueChanged;
            this.Controls.Add(filterLabel);
            this.Controls.Add(filterBox);
            comboNum++;
            selectedFilters.Add(filterLabel.Name, filterLabel);
            selectedFilters.Add(filterBox.Name, filterBox);
            //controls.Add(filterLabel);
            //controls.Add(filterBox);
            add_Filter.Enabled = false;
        }

        private void AddFilterItems(ComboBox comboBox)
        {
            foreach (string item in filterItems)
            {
                comboBox.Items.Add(item);
            }
        }

        private void FilterBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox curBox = (ComboBox)sender;
            switch (curBox.SelectedItem)
            {
                case "Time":
                    DateTimePicker fromDP = new DateTimePicker();
                    fromDP.Size = new Size(98, 20);
                    fromDP.Format = DateTimePickerFormat.Short;
                    fromDP.Location = new Point(curBox.Location.X + 290, curBox.Location.Y + 5);
                    fromDP.Name = "fromDP";
                    Label fromLbl = new Label();
                    fromLbl.Text = "From:";
                    fromLbl.Location = new Point(curBox.Location.X + 255, curBox.Location.Y + 8);
                    this.Controls.Add(fromDP);
                    this.Controls.Add(fromLbl);
                    DateTimePicker toDP = new DateTimePicker();
                    toDP.Size = new Size(98, 20);
                    toDP.Format = DateTimePickerFormat.Short;
                    toDP.Location = new Point(curBox.Location.X + 425, curBox.Location.Y + 5);
                    toDP.Name = "toDP";
                    Label toLbl = new Label();
                    toLbl.Text = "To:";
                    toLbl.Location = new Point(curBox.Location.X + 400, curBox.Location.Y + 8);
                    this.Controls.Add(toDP);
                    this.Controls.Add(toLbl);
                    filterItems.Remove("Time");
                    formWidth += 270;
                    separator1.Size = new Size(formWidth, 5);
                    this.Size = new Size(formWidth,formHeight);
                    break;
                case "Operator":
                    filterItems.Remove("Operator");
                    break;
                default:
                    break;
            }
            Console.WriteLine(((ComboBox)sender).SelectedItem);
            add_Filter.Enabled = true;
        }

        private void Add_Filter_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddFilter == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }

        private void Add_Filter_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddFilter = true;
            (sender as Button).Invalidate();
        }

        private void Add_Filter_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddFilter = false;
            (sender as Button).Invalidate();
        }
        #endregion

        #region [Remove Filter]
        private void Remove_Filter_Click(object sender, EventArgs e)
        {
            if (controls.Count != 0)
            {
                controls[controls.Count - 1].Dispose();
                controls[controls.Count - 2].Dispose();
                controls.RemoveAt(controls.Count - 1);
                controls.RemoveAt(controls.Count - 1);
                formHeight -= 50;
                this.Size = new Size(397, formHeight);
                comboNum--;
            }
        }

        private void Remove_Filter_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemoveFilter = true;
            (sender as Button).Invalidate();
        }

        private void Remove_Filter_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemoveFilter = false;
            (sender as Button).Invalidate();
        }

        private void Remove_Filter_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemoveFilter == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [Export Button]
        private void Btn_Export_Click(object sender, EventArgs e)
        {
            string path = "c:\\CompuScan_Reports";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = string.Format("{0}_Report.xlsx", cb_ReportSubject.SelectedItem.ToString());
            path = Path.Combine(path, fileName);

            XLWorkbook wb = new XLWorkbook();

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                try
                {
                    switch (cb_ReportSubject.SelectedItem)
                    {
                        case "Production Data":
                            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Station10", conn);
                            DataTable dt1 = new DataTable();
                            da1.Fill(dt1);
                            wb.Worksheets.Add(dt1, fileName);
                            wb.SaveAs(path);

                            MessageBox.Show("Data successfully exported to " + path, "Export Complete", MessageBoxButtons.OK);
                            break;
                        case "Users":
                            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM UserDetails", conn);
                            DataTable dt2 = new DataTable();
                            da2.Fill(dt2);
                            wb.Worksheets.Add(dt2, fileName);
                            wb.SaveAs(path);

                            MessageBox.Show("Data successfully exported to " + path, "Export Complete", MessageBoxButtons.OK);
                            break;
                        default:
                            break;
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void Btn_Export_Paint(object sender, PaintEventArgs e)
        {
            if (btnExport == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }

        private void Btn_Export_MouseUp(object sender, MouseEventArgs e)
        {
            btnExport = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Export_MouseDown(object sender, MouseEventArgs e)
        {
            btnExport = true;
            (sender as Button).Invalidate();
        }
        #endregion

        private void Cb_ReportSubject_SelectedValueChanged(object sender, EventArgs e)
        {
            add_Filter.Enabled = true;
        }
    }
}

using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FormCustom
{
    /*
        |================================================================================================|
        |        |          Formulas     |                         statement                             |
        |        |=======================|===============================================================|
        |        |    Name 1   |  Name2  | Prop1 | Prop2 | Prop3 | Prop4 | List<falseitem>               |
        |================================================================================================|
        |    1   |      -      |  true   | false | true  | false |   _   |                               |
        |================================================================================================|
        |    2   |    defg     |  false  | false |   -   | false |   -   |                               |
        |================================================================================================|
        |    3   |    defg     |  false  | false |   -   | false |   -   |                               |
        |================================================================================================|
    */

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // There will be a list item of Formulas and falseitem, will be send to intialize header table
        string[] headersDefault = new string[] { "isused", "iseditable", "isenabled", "isstorable", "issendable", "defaultValue", "basicvalue", "value" };

        // get the number of conditions/formulas
        List<string> lstFormulas = new List<string>() { "PhoBo", "MiGoi", "ComChienTrung", "LapXuongNuongThanDa" };

        /// <summary>
        /// Load Main Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 2;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            int numFalseItems = 5; // get the amount of falseitem in sequenceconfig file.
            this.CreateHeaderTableHeader(lstFormulas, numFalseItems);
            //this.AutoResizeAndFillColorHeaderDGV(dataGridView1, Color.SkyBlue, true);
        }

        /// <summary>
        /// Paint the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // for statement
            Rectangle rDraw = gridView.GetCellDisplayRectangle(1, -1, true);
            rDraw.Y++;
            rDraw.Height /= 2;
            for (int j = 1; j < 1 + lstFormulas.Count(); j++)
            {
                rDraw.Width += ((DataGridView)sender).Columns[j].Width;
            }
            rDraw.Inflate(new Size(1, 1));
            e.Graphics.FillRectangle(new SolidBrush(Color.Pink), rDraw);
            ControlPaint.DrawBorder(e.Graphics, rDraw, Color.Gray, ButtonBorderStyle.Solid);
            e.Graphics.DrawString("Fomulas", gridView.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, rDraw, format);

            // for statement
            rDraw = gridView.GetCellDisplayRectangle(1 + lstFormulas.Count(), -1, true);
            rDraw.Y++;
            rDraw.Height /= 2;
            for (int j = 1 + lstFormulas.Count(); j < gridView.ColumnCount; j++)
            {
                rDraw.Width += ((DataGridView)sender).Columns[j].Width;
            }
            rDraw.Inflate(new Size(1, 1));
            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rDraw);
            ControlPaint.DrawBorder(e.Graphics, rDraw, Color.Gray, ButtonBorderStyle.Solid);
            e.Graphics.DrawString("statements", gridView.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, rDraw, format);
        }

        /// <summary>
        /// Resize table, however there is a bug need to fix
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="colorHeader"></param>
        /// <param name="isAutoFit"></param>
        private void AutoResizeAndFillColorHeaderDGV(DataGridView dgv, Color colorHeader, bool isAutoFit = false)
        {
            if (isAutoFit)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) * (dgv.Rows.Count - 1);
            var totalWidth = 0;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                totalWidth += column.Width;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (dgv.MaximumSize.Height != 0 && totalHeight > dgv.MaximumSize.Height) // for the scrollbar
            {
                totalWidth += 20;
                dgv.ScrollBars = ScrollBars.Vertical;
                totalHeight = dgv.MaximumSize.Height;
            }
            dgv.ClientSize = new Size(totalWidth, totalHeight);
            // for header
            if (dgv.Rows.Count > 0 && dgv.Name != "drgvConstraintParam")
            {
                dgv.Rows[0].Frozen = true;
                foreach (DataGridViewCell cell in dgv.Rows[0].Cells)
                {
                    cell.ReadOnly = true;
                    cell.Selected = false;
                    cell.Style.BackColor = colorHeader;
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            //for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
            //{
            //    this.dataGridView1.Columns[j].Width = 45;
            //    this.dataGridView1.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    this.dataGridView1.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
        }

        /// <summary>
        /// Create header
        /// </summary>
        /// <param name="colFormulas"></param>
        /// <param name="colFalseItem"></param>
        private void CreateHeaderTableHeader(List<string> lstFormulasName, int colFalseItem = 1)
        {
            this.dataGridView1.Columns.Add("colNo", "");
            foreach (var formulas in lstFormulasName)
            {
                this.dataGridView1.Columns.Add("colFormula_" + formulas, formulas);
            }
            foreach (var header in headersDefault)
            {
                this.dataGridView1.Columns.Add("colProperty_" + header, header);
            }
            for (int i = 0; i < colFalseItem; i++)
            {
                this.dataGridView1.Columns.Add("colFalseitem_" + i, "falseitem" + i);
            }
        }
    }
}

using System.Reflection;

namespace FormCustom
{
    /*
        |================================================================================================|
        |        |             |         |                         statement                             |
        |        |NameCondition|  Type   |===============================================================|
        |        |             |         | Prop1 | Prop2 | Prop3 | Prop4 | List<falseitem>               |
        |================================================================================================|
        |        |    abcd     |  true   |       |       |       |       |                               |
        |    1   |=============|=========| false | true  | false |   _   |                               |
        |        |    defg     |  false  |       |       |       |       |                               |
        |================================================================================================|
        |        |    abcd     |  true   |       |       |       |       |                               |
        |        |=============|=========|       |       |       |       |                               |
        |    2   |    defg     |  false  | false |   -   | false |   -   |                               |
        |        |=============|=========|       |       |       |       |                               |
        |        |    defg     |  false  |       |       |       |       |                               |
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

        string[] headers = new string[] { };

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 2;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Columns.Add("", "");
            this.dataGridView1.Columns.Add("", "Condition Name");
            this.dataGridView1.Columns.Add("", "Win");
            this.dataGridView1.Columns.Add("", "Loss");
            this.dataGridView1.Columns.Add("", "Win");
            this.dataGridView1.Columns.Add("", "Loss");
            this.dataGridView1.Columns.Add("", "Win");
            this.dataGridView1.Columns.Add("", "Loss");
            this.dataGridView1.Columns.Add("", "Win");
            this.dataGridView1.Columns.Add("", "Loss");
            for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
            {
                this.dataGridView1.Columns[j].Width = 45;
                this.dataGridView1.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.AutoResizeAndFillColorHeaderDGV(dataGridView1, Color.SkyBlue, true);
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            Rectangle rDraw = gridView.GetCellDisplayRectangle(3, -1, true);
            rDraw.Y++;
            rDraw.Height /= 2;
            for (int j = 3; j < gridView.ColumnCount; j++)
            {
                rDraw.Width += ((DataGridView)sender).Columns[j].Width;
            }
            rDraw.Inflate(new Size(1, 1));
            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), rDraw);
            ControlPaint.DrawBorder(e.Graphics, rDraw, Color.Gray, ButtonBorderStyle.Solid);
            e.Graphics.DrawString("statements", gridView.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, rDraw, format);
        }

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
        }
    }
}

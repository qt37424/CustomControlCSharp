using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCustom
{
    class CustomDataGridView: DataGridView
    {
        public CustomDataGridView() { }
    }

    public class MergedCell : DataGridViewTextBoxCell
    {
        public Rectangle MergedBounds { get; set; }
    }

    public class MergedColumn : DataGridViewColumn
    {
        public MergedColumn() : base(new MergedCell()) { }

        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(MergedCell)))
                    throw new InvalidCastException("Must be a MergedCell");
                base.CellTemplate = value;
            }
        }
    }
}

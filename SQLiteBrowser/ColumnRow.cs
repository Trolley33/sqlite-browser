using System;
using Gtk;

namespace SQLiteBrowser
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ColumnRow : Gtk.Bin
    {
        public ColumnRow()
        {
            this.Build();

            foreach (var type in Enum.GetValues(typeof(Column.DataType)))
            {
                DataTypeCombo.AppendText(type.ToString());
            }
        }
    }
}

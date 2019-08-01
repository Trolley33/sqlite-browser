using System;
using System.Collections.Generic;
using Gtk;

namespace SQLiteBrowser
{
    public partial class NewTableDialog : Dialog
    {
        private string tableName;
        private List<Column> columns;

        public NewTableDialog()
        {
            tableName = "";
            columns = new List<Column>();

            Build();

            // Setup 'table' headers.
            HBox newRow = new HBox
            {
                new Label() { Name = "ColumnName", Text = "Column Name"},
                new Label() { Name = "Type", Text = "DataType"},
                new Label() { Name = "Delete", Text = "Delete"}
            };
            column_table.NRows = 2;

            column_table.Attach(
                    newRow,
                    0,
                    3,
                    0,
                    1,
                    Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                    Gtk.AttachOptions.Fill,
                    5,
                    5
                );

            newRow.Show();
            foreach (var child in newRow.Children)
            {
                child.Show();
            }

        }

        protected void CreateClicked(object sender, EventArgs e)
        {
            tableName = TableNameEntry.Text;

            bool acceptInfo = true;
            string message = "";
            if (tableName.Equals(""))
            {
                message = "No table name provided!";
                acceptInfo = false;
            }
            else if (columns.Count == 0)
            {
                message = "No columns provided!";
                acceptInfo = false;
            }

            if (!acceptInfo)
            {
                using (var md = new MessageDialog(null, DialogFlags.Modal, 
                    MessageType.Info, ButtonsType.Ok,
                    message))
                {
                    md.Title = "Information";
                    md.Run();
                    md.Destroy();
                }

                return;
            }

            Respond(ResponseType.Accept);
        }

        public string GetTableName()
        {
            return this.tableName;
        }

        public List<Column> GetColumns()
        {
            return this.columns;
        }

        protected void AddColumnRow(object sender, EventArgs e)
        {
            ColumnUI column = new ColumnUI();
            column.AddTo(column_table, column_table.NRows);
        }
    }
}

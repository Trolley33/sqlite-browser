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

            Label columnNameHeader = new Label()
            {
                Name = "ColumnNameHeader",
                Text = "Name"
            };
            Label dataTypeHeader = new Label()
            {
                Name = "DataTypeHeader",
                Text = "Data Type"
            };

            Label notNullHeader = new Label()
            {
                Name = "NNHeader",
                Text = "Not Null?"
            };

            Label pkHeader = new Label()
            {
                Name = "PKHeader",
                Text = "PK?"
            };

            Label uniqueHeader = new Label()
            {
                Name = "UniqueHeader",
                Text = "Unique?"
            };

            Label defaultHeader = new Label()
            {
                Name = "DefaultHeader",
                Text = "Default Value"
            };

            Label deleteHeader = new Label()
            {
                Name = "DeleteHeader",
                Text = "Delete"
            };

            /*
            column_column_table.Attach(
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
            */

            // Add column name entry.
            column_table.Attach(
                columnNameHeader,
                0, 1,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );
            // Add data type dropdown.
            column_table.Attach(
                dataTypeHeader,
                1, 2,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );

            column_table.Attach(
                notNullHeader,
                2, 3,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );

            column_table.Attach(
                pkHeader,
                3, 4,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );


            column_table.Attach(
                uniqueHeader,
                4, 5,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );

            // Add column name entry.
            column_table.Attach(
                defaultHeader,
                5, 6,
                0, 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                5,
                5
                );

            columnNameHeader.Show();
            dataTypeHeader.Show();
            notNullHeader.Show();
            pkHeader.Show();
            uniqueHeader.Show();
            defaultHeader.Show();

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

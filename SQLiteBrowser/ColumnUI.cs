using System;
using Gtk;

namespace SQLiteBrowser
{
    public class ColumnUI
    {
        private Entry columnNameEntry;
        private ComboBox dataTypeCombo;
        private CheckButton notNullCheckbox;
        private CheckButton primaryKeyCheckbox;
        private CheckButton uniqueCheckbox;
        private Entry defaultEntry;

        private static uint padding = 0;

        public ColumnUI()
        {
            columnNameEntry = new Entry();
            // Create data type dropdown menu, add appropriate entries to it.


            Array types = Enum.GetValues(typeof(Column.DataType));

            String[] dataTypes = new String[
                types.Length
            ];

            foreach (var type in Enum.GetValues(typeof(Column.DataType)))
            for (int i = 0; i < types.Length; i++)
            {
                    dataTypes[i] = types.GetValue(i).ToString();
            }

            dataTypeCombo = new ComboBox(dataTypes)
            {
                Name = "DataTypeComboBox"
            };

            notNullCheckbox = new CheckButton();
            primaryKeyCheckbox = new CheckButton();
            uniqueCheckbox = new CheckButton();
            defaultEntry = new Entry();
        }

        public void AddTo(Table table, uint row)
        {
            // Add column name entry.
            table.Attach(
                this.columnNameEntry,
                0,1,
                row,row+1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );
            // Add data type dropdown.
            table.Attach(
                this.dataTypeCombo,
                1,2,
                row,row + 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );

            table.Attach(
                this.notNullCheckbox,
                2,3,
                row,row + 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );

            table.Attach(
                this.primaryKeyCheckbox,
                3,4,
                row,row + 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );


            table.Attach(
                this.uniqueCheckbox,
                4,5,
                row,row + 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );

            // Add column name entry.
            table.Attach(
                this.defaultEntry,
                5,6,
                row,row + 1,
                Gtk.AttachOptions.Fill | Gtk.AttachOptions.Expand,
                Gtk.AttachOptions.Fill,
                padding,padding
                );

            this.columnNameEntry.Show();
            this.dataTypeCombo.Show();
            this.notNullCheckbox.Show();
            this.primaryKeyCheckbox.Show();
            this.uniqueCheckbox.Show();
            this.defaultEntry.Show();

        }
    }
}

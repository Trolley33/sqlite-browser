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
            PlaceWidgetAt(columnNameEntry, table, 0, 1);
            PlaceWidgetAt(dataTypeCombo, table, 1, 1);
            PlaceWidgetAt(notNullCheckbox, table, 2, 1);
            PlaceWidgetAt(primaryKeyCheckbox, table, 3, 1);
            PlaceWidgetAt(uniqueCheckbox, table, 4, 1);
            PlaceWidgetAt(defaultEntry, table, 5, 1);
        }

        private void PlaceWidgetAt(Widget w, Table t, uint column, uint row)
        {
            t.Attach(
                w,
                column, column+1,
                row, row+1,
                Gtk.AttachOptions.Fill,
                Gtk.AttachOptions.Fill,
                padding, padding
                );

            w.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Gtk;
using Mono.Data.Sqlite;
using SQLiteBrowser;

public partial class MainWindow : Gtk.Window
{
    SqliteConnection DBConnection;

    public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        if (DBConnection != null) DBConnection.Close();

        Application.Quit();
        a.RetVal = true;
    }

    /**
     * Get location for new SQLite file to
     * be created.
     */
    protected void NewFile(object sender, EventArgs e)
    {
        // Create a file choosing dialog.
        FileChooserDialog chooser = new FileChooserDialog(
           "Select New SQL File Location",
           this,
           FileChooserAction.Save,
           "Cancel", ResponseType.Cancel,
           "Save", ResponseType.Accept);

        // Set filetype filter to SQL files.
        FileFilter filter = new FileFilter
        {
            Name = "SQLite DB File"
        };
        filter.AddPattern("*.db");
        chooser.AddFilter(filter);

        // If result of file chooser is accepted.
        if (chooser.Run() == (int)ResponseType.Accept)
        {
            // Create SQLite file at specified location, 
            // then run connect function on new SQLite file.
            SqliteConnection.CreateFile(chooser.Filename + ".db");
        }
        // Cleanup file chooser.
        chooser.Destroy();
    }

    /**
     * Get location of SQLite file to be loaded.
     */
    protected void OpenFile(object sender, EventArgs e)
    {
        // Create a file choosing dialog.
        FileChooserDialog chooser = new FileChooserDialog(
           "Select SQLite File",
           this,
           FileChooserAction.Open,
           "Cancel", ResponseType.Cancel,
           "Open", ResponseType.Accept);

        // Set filetype filter to SQL files.
        FileFilter filter = new FileFilter
        {
            Name = "SQLite DB File"
        };
        filter.AddPattern("*.db");
        chooser.AddFilter(filter);

        // If result of file chooser is accepted.
        if (chooser.Run() == (int)ResponseType.Accept)
        {
            SQLiteConnect(chooser.Filename);
        }
        // Cleanup file chooser.
        chooser.Destroy();
    }

    /**
     * Write changes to open SQLite file   .
     */
    protected void SaveFile(object sender, EventArgs e)
    {

    }

    /**
     * Connect to SQLite database at given path.
     */
    private bool SQLiteConnect(String path)
    {
        Console.WriteLine($"Data Source={path};Version=3;");
        if (DBConnection != null) DBConnection.Close();

        DBConnection = new SqliteConnection($"Data Source={path};Version=3;");
        DBConnection.Open();

        EnableDatabaseButtons();

        return true;
    }

    private void EnableDatabaseButtons ()
    {
        CreateTableAction.Sensitive = true;
        RunQueryAction.Sensitive = true;
    }

    private void DisableDatabaseButtons ()
    {
        CreateTableAction.Sensitive = false;
        RunQueryAction.Sensitive = false;
    }

    /**
     * Fill in widgets with new database information.
     */
    private void PopulateInfo()
    {
        if (DBConnection == null) return;

    }

    /**
     * Create SQL table using info provided by dialog.   
     */    
    protected void CreateTable(object sender, EventArgs e)
    {
        NewTableDialog dialog = new NewTableDialog();

        ResponseType r;
        while (true)
        {
            r = (ResponseType) dialog.Run();
            if (r == ResponseType.Accept) break;
            if (r == ResponseType.DeleteEvent) break;
            if (r == ResponseType.Cancel) break;
        }

        dialog.Destroy();
        if (r == ResponseType.Accept)
        {
            string tableName = dialog.GetTableName();
            List<Column> columns = dialog.GetColumns();

            StringBuilder query = new StringBuilder();
            query.Append($"CREATE TABLE `{tableName}` (\n");
            for (int i = 0; i < columns.Count; i++)
            {
                var col = columns[i];

                query.Append($"`{col.name}` {col.dataType.ToString()} ");
                if (col.notNull) query.Append("NOTNULL ");
                if (col.primaryKey) query.Append("PRIMARY KEY ");
                if (col.unique) query.Append("UNIQUE ");
                if (!(col.def == null)) query.Append($"DEFAULT {col.def} ");

                if (i != columns.Count - 1) query.Append(",\n");

                if (i == columns.Count - 1) query.Append("\n");
            }
            query.Append(");");

            SqliteCommand command = new SqliteCommand(query.ToString(), 
                                                      DBConnection);
            command.ExecuteNonQuery();
        }
    }
}

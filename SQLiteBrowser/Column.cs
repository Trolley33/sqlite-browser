using System;
namespace SQLiteBrowser
{
    public struct Column
    {
        public enum DataType
        {
            NULL,
            INTEGER,
            REAL,
            TEXT,
            BLOB,
        }

        public string name;
        public DataType dataType;
        public bool notNull;
        public bool primaryKey;
        public bool unique;
        public string def;

    }
}

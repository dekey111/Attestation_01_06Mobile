using SQLite;
using System;


namespace Notes.dataFiles
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int IDNote { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }

    }
}

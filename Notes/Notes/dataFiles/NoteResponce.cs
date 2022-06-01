using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.dataFiles
{
    public class NoteResponce
    {
        readonly SQLiteAsyncConnection db;
        public NoteResponce(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()
        {
            return db.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return db.Table<Note>().FirstOrDefaultAsync(x => x.IDNote == id);
        }

        public Task<int> SaveNotesAsync(Note notes)
        {
            if (notes.IDNote == 0)
                return db.InsertAsync(notes);
            else
                return db.UpdateAsync(notes);
        }

        public Task<int> DeleteNoteAsync(Note notes)
        {
            return db.DeleteAsync(notes);
        }
    }
}

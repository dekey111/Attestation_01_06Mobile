using Notes.dataFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Pages
{
    public partial class PageWordNotes : ContentPage
    {
        public string imagingPath;

        public PageWordNotes()
        {
            InitializeComponent();
            BindingContext = new Note();
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            if (note.Image == null)
                note.Image = imagingPath;

            if (String.IsNullOrWhiteSpace(note.Description))
                eText.Placeholder = "Введите описание заметки!";
            else
            {
                await App.NoteResponce.SaveNotesAsync(note);
                await Navigation.PopAsync();
            }    
        }

        private async void btnDel_Clicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (note.IDNote == 0)
                return;
            await App.NoteResponce.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }

        private async void btnGalery_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                iImage.Source = ImageSource.FromFile(photo.FullPath);
                imagingPath = photo.FullPath;
            }
            catch (Exception ex)
            { await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");}
        }

        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.png"
                });

                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                iImage.Source = ImageSource.FromFile(photo.FullPath);
                imagingPath = photo.FullPath;
            }
            catch (Exception ex)
            { await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");}
        }
    }
}
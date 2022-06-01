using Notes.dataFiles;
using Notes.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        private Note selectedNote = null;
        public  MainPage()
        {
            InitializeComponent(); 
        }
        protected override async void OnAppearing()
        {
            Notes.ItemsSource = await App.NoteResponce.GetNotesAsync();
            //if (App.NoteResponce.)
            //{
            //    bool result = await DisplayAlert("Пустой список", "Добавить запись?", "Да", "Нет");
            //    if (result.ToString() == "Да")
            //        await Navigation.PushAsync(new PageWordNotes());
            //}
            base.OnAppearing();
        }

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageWordNotes());
        }

        private async void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            selectedNote = Notes.SelectedItem as Note;
            if (selectedNote != null)
            {
                await App.NoteResponce.DeleteNoteAsync(selectedNote);
                Notes.ItemsSource = await App.NoteResponce.GetNotesAsync();
            }
            else
                await DisplayAlert("Уведомление", "Запись не выбрана", "Ок");
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            selectedNote = Notes.SelectedItem as Note;
            if (selectedNote != null)
            {
                PageWordNotes notePage = new PageWordNotes
                {
                    BindingContext = selectedNote
                };
                await Navigation.PushAsync(notePage);
            }
            else
                await DisplayAlert("Уведомление", "Запись не выбрана", "Ок");
        }
    }
}

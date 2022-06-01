using Notes.dataFiles;
using System;
using System.IO;
using Xamarin.Forms;

namespace Notes
{
    public partial class App : Application
    {
        static NoteResponce noteResponce;
        public static NoteResponce NoteResponce
        {
            get
            {
                if (noteResponce == null)
                {
                    noteResponce = new NoteResponce(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Attestation.db"));
                }
                return noteResponce;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

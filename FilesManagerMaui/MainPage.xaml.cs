using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FilesManagerMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            FilesManager.CurrentDirectoryChanged += (s, e) =>
            {
                CurrentDirectoryEntry.Text = e.NewDirectory.FullName;
            };
        }

        private void CurrentDirectoryEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Path = ((Entry)sender).Text ?? @"";
            DirectoryInfo DirectoryPath = new(Path);

            if (!Directory.Exists(Path))
                return;

            FilesManager.CurrentDirectory = new(Path);
            Debug.WriteLine($"New Directory Open: {Path}");
        }

        private void PreviousDirectory_ButtonClicked(object sender, EventArgs e)
        {
            if (FilesManager.CurrentDirectory.Parent == null)
                return;

            FilesManager.CurrentDirectory = FilesManager.CurrentDirectory.Parent;
        }
    }
}

/*
public partial class FilesManagerViewModel : ObservableObject
{
    public DirectoryInfo CurrentDirectory => FilesManager.CurrentDirectory;
    public ObservableCollection<FileInfo> Files => FilesManager.FilesInFolder;
    public ObservableCollection<DirectoryInfo> Directories => FilesManager.DirectoriesInFolder;
}
    */
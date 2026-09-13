using CommunityToolkit.Mvvm.ComponentModel;
using FilesManagerMaui.Classes;
using FilesManagerMaui.CustomViews;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FilesManagerMaui
{
    public partial class MainPage : ContentPage
    {
        private double RootFilesManagerView_StartWidth = 0;

        public FilesManager RootFilesManager { get; set; } = new();
        public FilesManager InternalFilesManager { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            
            if (RootFilesManager.CurrentDirectory is not null)
                RootFilesManager.CurrentDirectory.SetFilesAndDirectoriesInDirectory();

            FilesManagerEventBus.OnNewDirectoryOpened += (object? sender, DirectoryItem NewDirectoryItem) =>
            {
                OpenNewDirectoryOnInternalFilesManager(NewDirectoryItem);
            };
        }

        private void RootFilesManagerView_DirectoryTapped(object sender, DirectoryTappedEventArgs e)
        {
            InternalFilesManager.OpenDirectory(e.PathOpened ?? "");
        }

        private void OpenNewDirectoryOnInternalFilesManager(DirectoryItem NewDirectoryItem)
        {
            InternalFilesManager.OpenDirectory(NewDirectoryItem.DirectoryInfo.FullName);
        }

        private void RootFilesManager_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
                RootFilesManagerView_StartWidth = RootFilesManagerView.Width;

            else if (e.StatusType == GestureStatus.Running)
            {
                double NewWidth = RootFilesManagerView_StartWidth + e.TotalX;
                RootFilesManagerView.WidthRequest = NewWidth;
            }
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
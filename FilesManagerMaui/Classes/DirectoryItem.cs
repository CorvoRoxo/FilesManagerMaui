using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace FilesManagerMaui.Classes
{
    public partial class DirectoryItem : ObservableObject
    {
        [ObservableProperty]
        public partial DirectoryInfo DirectoryInfo { get; set; }

        [ObservableProperty]
        public partial string CustomTitle { get; set; } = string.Empty;

        [ObservableProperty]
        public partial FilesManager FilesManager { get; set; }

        public ObservableCollection<FileInfo> CurrentFilesInside { get; private set; } = [];
        public ObservableCollection<DirectoryItem> CurrentDirectoriesInside { get; private set; } = [];

        public ObservableCollection<object> CurrentDirectoriesAndFilesInside { get; private set; } = [];

        public DirectoryItem(FilesManager FilesManager, string DirectoryPath, string CustomTitle = "")
        {
            this.FilesManager = FilesManager;
            this.DirectoryInfo = new(DirectoryPath);
            this.CustomTitle = (string.IsNullOrEmpty(CustomTitle)) ? DirectoryInfo.Name : CustomTitle;
        }

        public DirectoryItem(FilesManager FilesManager, DirectoryInfo DirectoryInfo, string CustomTitle = "")
        {
            this.FilesManager = FilesManager;
            this.DirectoryInfo = DirectoryInfo;
            this.CustomTitle = (string.IsNullOrEmpty(CustomTitle)) ? DirectoryInfo.Name : CustomTitle;
        }

        public void SetFilesAndDirectoriesInDirectory()
        {
            foreach (DirectoryInfo DirectoryInfo in this.DirectoryInfo.EnumerateDirectories())
            {
                DirectoryItem NewDirectoryItem = new(this.FilesManager, DirectoryInfo);

                if (this.CurrentDirectoriesAndFilesInside.Contains(NewDirectoryItem))
                    continue;

                this.CurrentDirectoriesInside.Add(NewDirectoryItem);
                this.CurrentDirectoriesAndFilesInside.Add(NewDirectoryItem);
            }

            foreach (FileInfo FileInfo in this.DirectoryInfo.EnumerateFiles())
            {
                if (this.CurrentDirectoriesAndFilesInside.Contains(FileInfo))
                    continue;

                this.CurrentFilesInside.Add(FileInfo);
                this.CurrentDirectoriesAndFilesInside.Add(FileInfo);
            }
        }
    }
}

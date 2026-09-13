using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FilesManagerMaui.Classes
{
    public partial class FilesManager : ObservableObject
    {
        [ObservableProperty]
        public partial DirectoryItem? CurrentDirectory { get; set; }

        public FilesManager()
        {
            this.CurrentDirectory = new(this, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        public void OpenDirectory(string NewDirectory)
        {
            if (!Path.Exists(NewDirectory))
                return;

            if (NewDirectory.EndsWith('/') || NewDirectory.EndsWith('\\'))
                NewDirectory = NewDirectory.Remove(NewDirectory.Length - 1, 1);

            try
            {
                DirectoryItem NewDirectoryItem = new(this, NewDirectory);
                string CurrentDirectoryPath = this.CurrentDirectory?.DirectoryInfo.FullName ?? string.Empty;

                bool CurrentDirectoryIsTheSame = string.Equals(NewDirectoryItem.DirectoryInfo.FullName, CurrentDirectoryPath, StringComparison.OrdinalIgnoreCase);
                if (CurrentDirectoryIsTheSame)
                    return;

                this.CurrentDirectory = NewDirectoryItem;
                this.CurrentDirectory.SetFilesAndDirectoriesInDirectory();
                Debug.WriteLine($"Changing directory from {CurrentDirectoryPath} to {NewDirectoryItem.DirectoryInfo.FullName}");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void OpenDirectorysParent()
        {
            if (this.CurrentDirectory is null)
                return;

            if (this.CurrentDirectory.DirectoryInfo.Parent is null)
                return;

            this.OpenDirectory(this.CurrentDirectory.DirectoryInfo.Parent.FullName);
        }
    }
}

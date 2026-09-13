using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FAKE.FilesManagerMaui
{
    public class CurrentDirectoryEventArgs : EventArgs
    {
        public DirectoryInfo OldDirectory { get; }
        public DirectoryInfo NewDirectory { get; }

        public CurrentDirectoryEventArgs(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
        {
            this.OldDirectory = OldDirectory;
            this.NewDirectory = NewDirectory;
        }
    }

    public static class FilesManager
    { 
        public static event EventHandler<CurrentDirectoryEventArgs> CurrentDirectoryChanged;

        private static DirectoryInfo _CurrentDirectory = new(@"C:\Users\win10\Desktop");
        public static DirectoryInfo CurrentDirectory
        {
            get => _CurrentDirectory;
            set
            {
                DirectoryInfo OldDirectory = _CurrentDirectory;
                if (_CurrentDirectory == value)
                    return;

                _CurrentDirectory = value;
                OnCurrentDirectoryChanged(new(OldDirectory, value));
            }
        }

        public static ObservableCollection<FileInfo> FilesInFolder { get; private set; } = [];
        public static ObservableCollection<DirectoryInfo> DirectoriesInFolder { get; private set; } = [];

        private static void OnCurrentDirectoryChanged(CurrentDirectoryEventArgs e)
        {
            AddItemsToListFromList(FilesInFolder, CurrentDirectory.EnumerateFiles().ToList());
            AddItemsToListFromList(DirectoriesInFolder, CurrentDirectory.EnumerateDirectories().ToList());

            CurrentDirectoryChanged?.Invoke(null, e);
        }

        private static void AddItemsToListFromList<T>(IList<T> ListA, IList<T> ListB)
        {
            ListA.Clear();
            foreach (var Item in ListB)
                ListA.Add(Item);
        }
    }
}

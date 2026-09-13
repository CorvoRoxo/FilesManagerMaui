namespace FilesManagerMaui.Classes
{
    public class DirectoryTappedEventArgs : EventArgs
    {
        public DirectoryItem? DirectoryItem { get; private set; }
        public string? PathOpened => this.DirectoryItem?.DirectoryInfo.FullName;

        public DirectoryTappedEventArgs(DirectoryItem? DirectoryItem)
        {
            this.DirectoryItem = DirectoryItem;
        }
    }
}

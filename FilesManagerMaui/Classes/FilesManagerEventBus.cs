namespace FilesManagerMaui.Classes
{
    public static class FilesManagerEventBus
    {
        public static EventHandler<DirectoryItem>? OnNewDirectoryOpened;

        public static void OpenNewDirectory(DirectoryItem directoryItem)
            => OnNewDirectoryOpened?.Invoke(null, directoryItem);
    }
}

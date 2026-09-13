using FilesManagerMaui.Classes;

namespace FilesManagerMaui.CustomViews;

public partial class FilesManagerView : ContentView
{
	public FilesManagerView()
	{
		InitializeComponent();
	}

    private void Directory_Tapped(object sender, TappedEventArgs e)
    {
        if (this.HierarchicalMode)
            return;

		if (sender is not DirectoryView DirectoryViewSender)
			return;

		if (DirectoryViewSender.BindingContext is not DirectoryItem DirectoryItemClicked)
			return;

		this.FilesManager?.OpenDirectory(DirectoryItemClicked.DirectoryInfo.FullName);
        this.DirectoryTapped?.Invoke(this, new(this.FilesManager?.CurrentDirectory));
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not Entry EntrySender)
            return;

        string Path = EntrySender.Text;

        this.FilesManager?.OpenDirectory(Path);
		this.DirectoryTapped?.Invoke(this, new(this.FilesManager?.CurrentDirectory));
    }

	public event EventHandler<DirectoryTappedEventArgs>? DirectoryTapped;

    public static readonly BindableProperty FilesManagerProperty = BindableProperty.Create(
		nameof(FilesManager),
		typeof(FilesManager),
		typeof(FilesManagerView),
		null);

	public FilesManager? FilesManager
	{
		get => (FilesManager)GetValue(FilesManagerProperty);
		set => SetValue(FilesManagerProperty, value);
	}

    public static readonly BindableProperty HierarchicalModeProperty = BindableProperty.Create(
       nameof(HierarchicalMode),
       typeof(bool),
       typeof(FilesManagerView),
       false);

    public bool HierarchicalMode
    {
        get => (bool)GetValue(HierarchicalModeProperty);
        set => SetValue(HierarchicalModeProperty, value);
    }

    public static readonly BindableProperty TapsRequiredToOpenDirectoryProperty = BindableProperty.Create(
        nameof(TapsRequiredToOpenDirectory),
        typeof(int),
        typeof(FilesManagerView),
        1);

    public int TapsRequiredToOpenDirectory
    {
        get => (int)GetValue(TapsRequiredToOpenDirectoryProperty);
        set => SetValue(TapsRequiredToOpenDirectoryProperty, value);
    }

    private void Back_ButtonClicked(object sender, EventArgs e)
    {
        this.FilesManager?.OpenDirectorysParent();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
using FilesManagerMaui.Classes;
using System.Diagnostics;

namespace FilesManagerMaui.CustomViews;

public partial class DirectoryView : ContentView
{
	public DirectoryView()
	{
		InitializeComponent();

	}

    private void HierarchicalCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        => FilesManagerEventBus.OpenNewDirectory(this.DirectoryItem);

    private void Directory_Tapped(object sender, TappedEventArgs e)
        => FilesManagerEventBus.OpenNewDirectory(this.DirectoryItem);

    private void ShowHierarchicList_ButtonClicked(object sender, EventArgs e)
    {
        if (!this.HierarchicalMode)
            return;

        this.HierarchicalCollectionView.IsVisible = !this.HierarchicalCollectionView.IsVisible;
        this.ShowHierarchicListButton.Text = this.HierarchicalCollectionView.IsVisible ? "-" : "+";
    }

    public static readonly BindableProperty DirectoryItemProperty = BindableProperty.Create(
        nameof(DirectoryItem),
        typeof(DirectoryItem),
        typeof(DirectoryView),
        null);

    public DirectoryItem DirectoryItem
    {
        get => (DirectoryItem)GetValue(DirectoryItemProperty);
        set => SetValue(DirectoryItemProperty, value);
    }

    public static readonly BindableProperty HierarchicalModeProperty = BindableProperty.Create(
        nameof(HierarchicalMode),
        typeof(bool),
        typeof(DirectoryView),
        false,
        propertyChanged: (BindableObject BindableObjectSender, object OldValue, object NewValue) =>
        {
            if (BindableObjectSender is not DirectoryView DirectoryViewSender)
                return;

            if (NewValue is not bool NewValueBool)
                return;

            if (NewValueBool)
                DirectoryViewSender.DirectoryItem.SetFilesAndDirectoriesInDirectory();

            DirectoryViewSender.ShowHierarchicListButton.IsVisible = NewValueBool;
        });

	public bool HierarchicalMode
    {
        get => (bool)GetValue(HierarchicalModeProperty);
        set => SetValue(HierarchicalModeProperty, value);
    }

    public static readonly BindableProperty TapsRequiredToBeOpenProperty = BindableProperty.Create(
        nameof(TapsRequiredToBeOpen),
        typeof(int),
        typeof(DirectoryView),
        1);

    public int TapsRequiredToBeOpen
    {
        get => (int)GetValue(TapsRequiredToBeOpenProperty);
        set => SetValue(TapsRequiredToBeOpenProperty, value);
    }
}
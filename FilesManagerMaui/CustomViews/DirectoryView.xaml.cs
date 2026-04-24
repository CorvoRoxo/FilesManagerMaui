namespace FilesManagerMaui.CustomViews;

public partial class DirectoryView : ContentView
{
	public DirectoryView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		FilesManager.CurrentDirectory = ((DirectoryInfo)this.BindingContext);
    }
}
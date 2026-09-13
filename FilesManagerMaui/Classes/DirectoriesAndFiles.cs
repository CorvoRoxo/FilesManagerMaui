namespace FilesManagerMaui.Classes
{
    public class DirectoriesAndFilesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? DirectoryTemplate { get; set; }
        public DataTemplate? FileTemplate { get; set; }

        protected override DataTemplate? OnSelectTemplate(object Item, BindableObject Container)
           => (Item is DirectoryItem || Item is DirectoryInfo) ? DirectoryTemplate : FileTemplate;
    }
}

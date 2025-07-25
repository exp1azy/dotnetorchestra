namespace DotNetOrchestra.Shared
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Version { get; set; }
        public string? Description { get; set; }
        public byte[]? AppData { get; set; }
        public int AppSdkType { get; set; }
    }
}

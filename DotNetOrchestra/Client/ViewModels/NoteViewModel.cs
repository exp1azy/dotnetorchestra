using DotNetOrchestra.Shared;

namespace DotNetOrchestra.Client.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Version { get; set; }
        public string? Description { get; set; }
        public int AppSdkType { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 100;

        public static NoteViewModel FromModel(NoteModel noteModel)
        {
            return new NoteViewModel
            {
                Id = noteModel.Id,
                Name = noteModel.Name,
                Version = noteModel.Version,
                Description = noteModel.Description,
                AppSdkType = noteModel.AppSdkType
            };
        }
    }
}

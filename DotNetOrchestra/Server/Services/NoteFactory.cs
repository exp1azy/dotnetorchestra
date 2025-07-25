using DotNetOrchestra.Server.Data;
using DotNetOrchestra.Shared;

namespace DotNetOrchestra.Server.Services
{
    public static class NoteFactory
    {
        public static NoteModel CreateNoteModel(Note note) => new()
        {
            Id = note.Id,
            Name = note.Name,
            Version = note.Version,
            Description = note.Description,
            AppData = note.AppData,
            AppSdkType = note.AppSdkType
        };
    }
}

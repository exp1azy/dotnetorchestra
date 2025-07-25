using DotNetOrchestra.Server.Data;
using DotNetOrchestra.Server.Exceptions;
using DotNetOrchestra.Server.Repositories;
using DotNetOrchestra.Server.Resources;
using DotNetOrchestra.Shared;
using DynamicDI;

namespace DotNetOrchestra.Server.Services
{
    [RegisterService]
    public class NoteService
    {
        private readonly NoteRepository _noteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteModel[]> GetNotesAsync(CancellationToken cancellationToken = default)
        {
            var notes = await _noteRepository.GetNotesAsync(cancellationToken);

            if (notes == null || notes.Length == 0)
                return Array.Empty<NoteModel>();

            return notes.Select(NoteFactory.CreateNoteModel).ToArray();
        }

        public async Task<NoteModel> GetNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            var note = await _noteRepository.GetNoteAsync(id, cancellationToken)
                ?? throw new NotFoundException(Error.NoteNotFound);

            return NoteFactory.CreateNoteModel(note);
        }

        public async Task<NoteModel> CreateNoteAsync(NoteModel noteModel, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(noteModel.Name))
                throw new BadRequestException(Error.NameRequired);

            if (noteModel.AppData == null || noteModel.AppData.Length == 0)
                throw new BadRequestException(Error.AppDataRequired);

            bool exist = await _noteRepository.ExistsAsync(noteModel.Name, cancellationToken);

            if (exist)
                throw new BadRequestException(Error.NoteExists);

            string sdkType = await DotNetSdkHelper.GetSdkTypeAsync(noteModel.AppData);
            int sdkTypeAsInt = DotNetSdkHelper.GetSdkTypeAsInt(sdkType);

            var note = new Note
            {
                Name = noteModel.Name,
                Version = noteModel.Version,
                Description = noteModel.Description,
                AppData = noteModel.AppData!,
                AppSdkType = sdkTypeAsInt
            };

            var result = await _noteRepository.CreateNoteAsync(note, cancellationToken);

            return NoteFactory.CreateNoteModel(result);
        }

        public async Task<NoteModel> UpdateNoteAsync(NoteModel noteModel, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(noteModel.Name))
                throw new BadRequestException(Error.NameRequired);

            var noteToUpdate = await _noteRepository.GetNoteAsync(noteModel.Id, cancellationToken)
                ?? throw new NotFoundException(Error.NoteNotFound);

            noteToUpdate.Name = noteModel.Name;
            noteToUpdate.Version = noteModel.Version;
            noteToUpdate.Description = noteModel.Description;

            var result = await _noteRepository.UpdateNoteAsync(noteToUpdate, cancellationToken);

            return NoteFactory.CreateNoteModel(result);
        }

        public async Task RemoveNoteAsync(int id, CancellationToken cancellationToken = default)
        {
            bool deleted = await _noteRepository.RemoveNoteAsync(id, cancellationToken);

            if (!deleted)
                throw new NotFoundException(Error.NoteNotFound);
        }
    }
}

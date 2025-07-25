using DotNetOrchestra.Shared;
using System.Net.Http.Json;

namespace DotNetOrchestra.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<NoteModel> GetNoteAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<NoteModel>($"/api/note/{id}/") ?? new NoteModel();
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось получить приложение (ID={id}): {ex.Message}");
            }
        }

        public async Task<List<NoteModel>> GetNotesAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<NoteModel>>("/api/note/") ?? new List<NoteModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка при получении приложений: {ex.Message}");
            }
        }

        public async Task<NoteModel?> CreateNoteAsync(NoteModel model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/note/", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<NoteModel>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Не удалось добавить приложение: {ex.Message}");
            }
        }

        public async Task<NoteModel?> UpdateNoteAsync(NoteModel model)
        {
            try
            {
                var response = await _http.PutAsJsonAsync("/api/note/", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<NoteModel>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Не удалось обновить данные о приложении: {ex.Message}");
            }
        }

        public async Task RemoveNoteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"/api/note/{id}/");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Произошла ошибка при удалении приложения: {ex.Message}");
            }
        }
    }
}

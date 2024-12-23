﻿namespace MDB.Common.Services;

public class MembershipService : IMembershipService
{
    public readonly MembershipHttpClient _http;
    public MembershipService(MembershipHttpClient http)
    {
        _http = http;
    }
    public async Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly = false)
    {
        try
        {
            using HttpResponseMessage response =
                await _http.Client.GetAsync($"films?freeOnly={freeOnly}");

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmDTO>>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch (Exception)
        {
            throw;
        }
    }
   
    public async Task<FilmInfoDTO> GetFilmAsync(int? id)
    {
        try
        {
            if(id is null) throw new ArgumentNullException("id");

            using HttpResponseMessage response =
                await _http.Client.GetAsync($"films/{id}");

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmInfoDTO>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<List<GenreDTO>> GetGenresAsync()
    {
        try
        {
            using HttpResponseMessage response =
                await _http.Client.GetAsync($"genres");

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<GenreDTO>>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

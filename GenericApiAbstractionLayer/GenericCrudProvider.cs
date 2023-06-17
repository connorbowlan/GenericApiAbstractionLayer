using System.Text;
using GenericApiAbstractionLayer.Extensions;
using GenericApiAbstractionLayer.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GenericApiAbstractionLayer;

/// <summary>
/// Provides generic CRUD operations for a specified type of object.
/// </summary>
/// <typeparam name="TObj">The type of the object.</typeparam>
/// <typeparam name="TKey">The type of the object's key.</typeparam>
public class GenericCrudProvider<TObj, TKey> : ICrudProvider<TObj, TKey>
{
    #region Private Fields

    private readonly HttpClient _client;
    private readonly string _entity;
    private readonly ILogger<GenericCrudProvider<TObj, TKey>> _logger;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericCrudProvider{TObj, TKey}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging messages.</param>
    /// <param name="client">The HTTP client used for making API requests.</param>
    public GenericCrudProvider(ILogger<GenericCrudProvider<TObj, TKey>> logger, HttpClient client)
    {
        _logger = logger;
        _client = client;
        _entity = typeof(TObj).Name.Pluralize().ToLower();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Deletes an object asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object to delete.</param>
    /// <returns>The deleted object if successful; otherwise, null.</returns>
    public async Task<TObj?> DeleteAsync(TKey id)
    {
        try
        {
            var response = await _client.DeleteAsync($"{_entity}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync();
            var deletedObj = JsonConvert.DeserializeObject<TObj>(json);

            return deletedObj;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return default;
        }
    }

    /// <summary>
    /// Gets a collection of objects asynchronously.
    /// </summary>
    /// <param name="includeRelationships">Determines whether to include relationships in the result.</param>
    /// <returns>A collection of objects if successful; otherwise, null.</returns>
    public async Task<IEnumerable<TObj>?> GetAsync(bool includeRelationships = false)
    {
        try
        {
            var includeRelationShipsParam = includeRelationships ? "?includeRelationships=true" : string.Empty;

            var response = await _client.GetAsync($"{_entity}/{includeRelationShipsParam}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync();
            var objs = JsonConvert.DeserializeObject<IEnumerable<TObj>>(json);

            return objs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return null;
        }
    }

    /// <summary>
    /// Gets an object by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object.</param>
    /// <param name="includeRelationships">Determines whether to include relationships in the result.</param>
    /// <returns>The object if found; otherwise, null.</returns>
    public async Task<TObj?> GetAsync(TKey id, bool includeRelationships = false)
    {
        try
        {
            var includeRelationShipsParam = includeRelationships ? "?includeRelationships=true" : string.Empty;
            var response = await _client.GetAsync($"{_entity}/{id}{includeRelationShipsParam}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<TObj>(json);

            return obj;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return default;
        }
    }

    /// <summary>
    /// Creates a new object asynchronously.
    /// </summary>
    /// <param name="obj">The object to create.</param>
    /// <returns>The created object if successful; otherwise, null.</returns>
    public async Task<TObj?> PostAsync(TObj obj)
    {
        try
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_entity}", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync();
            var createdObj = JsonConvert.DeserializeObject<TObj>(json);

            return createdObj;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return default;
        }
    }

    /// <summary>
    /// Updates an object asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object to update.</param>
    /// <param name="obj">The updated object.</param>
    /// <returns>The updated object if successful; otherwise, null.</returns>
    public async Task<TObj?> PutAsync(TKey id, TObj obj)
    {
        try
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"{_entity}/{id}", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            var json = await response.Content.ReadAsStringAsync();

            var updatedObj = JsonConvert.DeserializeObject<TObj>(json);

            return updatedObj;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return default;
        }
    }

    #endregion Public Methods
}
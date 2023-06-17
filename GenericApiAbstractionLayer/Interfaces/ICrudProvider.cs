namespace GenericApiAbstractionLayer.Interfaces;

/// <summary>
/// Represents a generic CRUD provider interface for a specified type of object.
/// </summary>
/// <typeparam name="TObj">The type of the object.</typeparam>
/// <typeparam name="TKey">The type of the object's key.</typeparam>
public interface ICrudProvider<TObj, in TKey>
{
    #region Public Methods

    /// <summary>
    /// Deletes an object asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object to delete.</param>
    /// <returns>The deleted object if successful; otherwise, null.</returns>
    Task<TObj?> DeleteAsync(TKey id);

    /// <summary>
    /// Gets a collection of objects asynchronously.
    /// </summary>
    /// <param name="includeRelationships">Determines whether to include relationships in the result.</param>
    /// <returns>A collection of objects if successful; otherwise, null.</returns>
    Task<IEnumerable<TObj>?> GetAsync(bool includeRelationships = false);

    /// <summary>
    /// Gets an object by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object.</param>
    /// <param name="includeRelationships">Determines whether to include relationships in the result.</param>
    /// <returns>The object if found; otherwise, null.</returns>
    Task<TObj?> GetAsync(TKey id, bool includeRelationships = false);

    /// <summary>
    /// Creates a new object asynchronously.
    /// </summary>
    /// <param name="obj">The object to create.</param>
    /// <returns>The created object if successful; otherwise, null.</returns>
    Task<TObj?> PostAsync(TObj obj);

    /// <summary>
    /// Updates an object asynchronously.
    /// </summary>
    /// <param name="id">The ID of the object to update.</param>
    /// <param name="obj">The updated object.</param>
    /// <returns>The updated object if successful; otherwise, null.</returns>
    Task<TObj?> PutAsync(TKey id, TObj obj);

    #endregion Public Methods
}
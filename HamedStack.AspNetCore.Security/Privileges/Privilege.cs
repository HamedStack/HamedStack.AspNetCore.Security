// ReSharper disable UnusedMember.Global

namespace HamedStack.AspNetCore.Security.Privileges;

/// <summary>
/// Represents a set of privilege actions that can be performed by a user on a given item.
/// This is an abstract base class that should be implemented to define custom privilege rules for different items and users.
/// </summary>
/// <typeparam name="TItem">The type of the item on which the action is to be performed.</typeparam>
/// <typeparam name="TUser">The type of the user performing the action.</typeparam>
public abstract class Privilege<TItem, TUser>
{
    /// <summary>
    /// Determines if the specified user has the privilege to create the specified item.
    /// </summary>
    /// <param name="item">The item to be created.</param>
    /// <param name="user">The user attempting to create the item.</param>
    /// <returns><c>true</c> if the user has the privilege; otherwise, <c>false</c>.</returns>
    public abstract bool CanCreate(TItem item, TUser user);

    /// <summary>
    /// Determines if the specified user has the privilege to delete the specified item.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    /// <param name="user">The user attempting to delete the item.</param>
    /// <returns><c>true</c> if the user has the privilege; otherwise, <c>false</c>.</returns>
    public abstract bool CanDelete(TItem item, TUser user);

    /// <summary>
    /// Determines if the specified user has the privilege to edit the specified item.
    /// </summary>
    /// <param name="item">The item to be edited.</param>
    /// <param name="user">The user attempting to edit the item.</param>
    /// <returns><c>true</c> if the user has the privilege; otherwise, <c>false</c>.</returns>
    public abstract bool CanEdit(TItem item, TUser user);

    /// <summary>
    /// Determines if the specified user has the privilege to view the specified item.
    /// </summary>
    /// <param name="item">The item to be viewed.</param>
    /// <param name="user">The user attempting to view the item.</param>
    /// <returns><c>true</c> if the user has the privilege; otherwise, <c>false</c>.</returns>
    public abstract bool CanView(TItem item, TUser user);
}
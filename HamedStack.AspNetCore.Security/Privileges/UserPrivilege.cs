// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using Microsoft.AspNetCore.Identity;

namespace HamedStack.AspNetCore.Security.Privileges;

/// <summary>
/// Represents a set of privilege actions specific to an Identity user that can be performed on a given item.
/// This is an abstract class derived from the general <see cref="Privilege{TItem, TUser}"/> but specifically tailored for IdentityUser.
/// </summary>
/// <typeparam name="TItem">The type of the item on which the action is to be performed.</typeparam>
/// <typeparam name="TUser">The type of the Identity user performing the action. This type is constrained to IdentityUser or its derived types.</typeparam>
public abstract class UserPrivilege<TItem, TUser> : Privilege<TItem, TUser>
    where TUser : IdentityUser
{

}
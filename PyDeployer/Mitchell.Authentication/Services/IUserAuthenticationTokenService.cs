using System;
using System.Collections.Generic;
using System.Text;
using Mitchell.Authentication.Entities;
using Mitchell.Common.ViewModels;

namespace Mitchell.Authentication.Services
{
    public interface IUserAuthenticationTokenService
    {
        /// <summary>
        /// Fetches the User Authentication Token with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserAuthenticationToken Get(long id);

        UserAuthenticationToken Get(long userId, string token);
        
        /// <summary>
        /// Fetches Active tokens for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        PagedViewModel<UserAuthenticationToken> GetActiveForUser(long userId, int skip, int take,
            SortParam sort);

        /// <summary>
        /// Creates a token for the given user with the given description
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        string CreateToken(long userId, string description);

        /// <summary>
        /// Updates the given User Authentication Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserAuthenticationToken Update(UserAuthenticationToken token);

        /// <summary>
        /// Records a log in using the given authentication token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserAuthenticationToken RecordUserLogin(UserAuthenticationToken token);
    }
}

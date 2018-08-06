﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Mitchell.Authentication.PasswordHasher;
using Mitchell.Authentication.Services;

namespace Mitchell.Authentication.Managers
{
    public class UserAuthenticationManager
    {

        private readonly IUserService _userService;
        private readonly IUserAuthenticationTokenService _userAuthenticationTokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly SessionTokenManager _sessionTokenManager;

        public UserAuthenticationManager(IUserService userService,
            IUserAuthenticationTokenService userAuthenticationTokenService,
            IPasswordHasher passwordHasher,
            SessionTokenManager sessionTokenManager)
        {
            this._userService = userService;
            this._userAuthenticationTokenService = userAuthenticationTokenService;
            this._passwordHasher = passwordHasher;
            this._sessionTokenManager = sessionTokenManager;
        }

        private bool LoginPassword(string username, string password)
        {
            var user = _userService.Get(username);
            if (null != user)
            {
                return _passwordHasher.VerifyPassword(user.Password, password);
            }
            //If no user with the given username exists, still run the password hasher 
            // to avoid timing attacks
            _passwordHasher.VerifyPassword("", password);
            return false;
        }

        public bool LoginToken(string username, string token)
        {
            var user = _userService.Get(username);
            if (null != user)
            {
                var authenticationTokens =
                    _userAuthenticationTokenService.GetActive(user.UserId);
                
                foreach (var authenticationToken in authenticationTokens)
                {
                    if (_passwordHasher.VerifyPassword(authenticationToken.Token, token))
                    {
                        _userAuthenticationTokenService.RecordUserLogin(authenticationToken);
                        return true;
                    }
                }
                
            }

            _passwordHasher.VerifyPassword("", token);
            return false;
        }

        public ClaimsPrincipal LoginPasswordForPrincipal(string username, string password)
        {
            if (!LoginPassword(username, password))
            {
                return null;
            }
            return GetPrincipalForUser(username);
        }

        public ISessionToken LoginPasswordForSessionToken(string username, string password)
        {
            if (!LoginPassword(username, password))
            {
                return null;
            }
            //login was sucessful
            return GetTokenForUser(username);
        }

        public ISessionToken LoginTokenForSessionToken(string username,string token)
        {
            if (!LoginToken(username, token))
            {
                return null;
            }
            return GetTokenForUser(username);
        }

        /// <summary>
        /// Gets a session token for a user
        /// 
        /// Assumes the user is already authenticated / logged in sucessfully
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private ISessionToken GetTokenForUser(string username)
        {
            var token = _sessionTokenManager.GetTokenForUser(username);
            if (null == token)
            {
                token = _sessionTokenManager.CreateToken(username, GetPrincipalForUser(username));
            }
            return token;
        }

        private ClaimsPrincipal GetPrincipalForUser(string username)
        {
            var user = _userService.Get(username);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Sid, user.UserId.ToString())

            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            return new ClaimsPrincipal(userIdentity);
        }
    }
}

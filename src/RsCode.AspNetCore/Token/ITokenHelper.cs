using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Token
{
  public  interface ITokenHelper
    {
        ///// <summary>
        ///// Creates an identity token.
        ///// </summary>
        ///// <param name="request">The token creation request.</param>
        ///// <returns>An identity token</returns>
        //Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request);

        ///// <summary>
        ///// Creates an access token.
        ///// </summary>
        ///// <param name="request">The token creation request.</param>
        ///// <returns>An access token</returns>
        //Task<Token> CreateAccessTokenAsync(TokenCreationRequest request);

        ///// <summary>
        ///// Creates a serialized and protected security token.
        ///// </summary>
        ///// <param name="token">The token.</param>
        ///// <returns>A security token in serialized form</returns>
        //Task<string> CreateSecurityTokenAsync(Token token);

        ///// <summary>
        ///// Creates the refresh token.
        ///// </summary>
        ///// <param name="subject">The subject.</param>
        ///// <param name="accessToken">The access token.</param>
        ///// <param name="client">The client.</param>
        ///// <returns>
        ///// The refresh token handle
        ///// </returns>
        //Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client);

        ///// <summary>
        ///// Updates the refresh token.
        ///// </summary>
        ///// <param name="handle">The handle.</param>
        ///// <param name="refreshToken">The refresh token.</param>
        ///// <param name="client">The client.</param>
        ///// <returns>
        ///// The refresh token handle
        ///// </returns>
        //Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client);

    }
}

//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Internal;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace Rs.Token
//{
//  public  class TokenHelper:ITokenHelper
//    {
      
//        /// <summary>
//        /// The HTTP context accessor
//        /// </summary>
//        protected readonly IHttpContextAccessor Context;

//        /// <summary>
//        /// The clock
//        /// </summary>
//        protected readonly ISystemClock Clock;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="TokenHelper" /> class. This overloaded constructor is deprecated and will be removed in 3.0.0.
//        /// </summary>
//        /// <param name="claimsProvider">The claims provider.</param>
//        /// <param name="referenceTokenStore">The reference token store.</param>
//        /// <param name="creationService">The signing service.</param>
//        /// <param name="contextAccessor">The HTTP context accessor.</param>
//        /// <param name="clock">The clock.</param>
//        /// <param name="logger">The logger.</param>
//        public TokenHelper(   
//            IHttpContextAccessor contextAccessor,
//            ISystemClock clock)
//        {
//            Context = contextAccessor;          
//            Clock = clock;          
//        }

//        /// <summary>
//        /// Creates an identity token.
//        /// </summary>
//        /// <param name="request">The token creation request.</param>
//        /// <returns>
//        /// An identity token
//        /// </returns>
//        public virtual async Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
//        {            
//            request.Validate();

//            // host provided claims
//            var claims = new List<Claim>();

//            // if nonce was sent, must be mirrored in id token
//            if (request.Nonce.IsPresent())
//            {
//                claims.Add(new Claim(JwtClaimTypes.Nonce, request.Nonce));
//            }

//            // add iat claim
//            claims.Add(new Claim(JwtClaimTypes.IssuedAt, Clock.UtcNow.ToEpochTime().ToString(), ClaimValueTypes.Integer));

//            // add at_hash claim
//            if (request.AccessTokenToHash.IsPresent())
//            {
//                claims.Add(new Claim(JwtClaimTypes.AccessTokenHash, HashAdditionalData(request.AccessTokenToHash)));
//            }

//            // add c_hash claim
//            if (request.AuthorizationCodeToHash.IsPresent())
//            {
//                claims.Add(new Claim(JwtClaimTypes.AuthorizationCodeHash, HashAdditionalData(request.AuthorizationCodeToHash)));
//            }

//            // add sid if present
//            if (request.ValidatedRequest.SessionId.IsPresent())
//            {
//                claims.Add(new Claim(JwtClaimTypes.SessionId, request.ValidatedRequest.SessionId));
//            }

//            claims.AddRange(await ClaimsProvider.GetIdentityTokenClaimsAsync(
//                request.Subject,
//                request.Resources,
//                request.IncludeAllIdentityClaims,
//                request.ValidatedRequest));

//            var issuer = Context.HttpContext.GetIdentityServerIssuerUri();

//            var token = new Token(OidcConstants.TokenTypes.IdentityToken)
//            {
//                CreationTime = Clock.UtcNow.UtcDateTime,
//                Audiences = { request.ValidatedRequest.Client.ClientId },
//                Issuer = issuer,
//                Lifetime = request.ValidatedRequest.Client.IdentityTokenLifetime,
//                Claims = claims.Distinct(new ClaimComparer()).ToList(),
//                ClientId = request.ValidatedRequest.Client.ClientId,
//                AccessTokenType = request.ValidatedRequest.AccessTokenType
//            };

//            return token;
//        }

//        /// <summary>
//        /// Creates an access token.
//        /// </summary>
//        /// <param name="request">The token creation request.</param>
//        /// <returns>
//        /// An access token
//        /// </returns>
//        public virtual async Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
//        {
        
//            request.Validate();

//            var claims = new List<Claim>();
//            claims.AddRange(await ClaimsProvider.GetAccessTokenClaimsAsync(
//                request.Subject,
//                request.Resources,
//                request.ValidatedRequest));

//            if (request.ValidatedRequest.Client.IncludeJwtId)
//            {
//                claims.Add(new Claim(JwtClaimTypes.JwtId, CryptoRandom.CreateUniqueId(16)));
//            }

//            var issuer = Context.HttpContext.GetIdentityServerIssuerUri();
//            var token = new Token(OidcConstants.TokenTypes.AccessToken)
//            {
//                CreationTime = Clock.UtcNow.UtcDateTime,
//                Audiences = { string.Format(IdentityServerConstants.AccessTokenAudience, issuer.EnsureTrailingSlash()) },
//                Issuer = issuer,
//                Lifetime = request.ValidatedRequest.AccessTokenLifetime,
//                Claims = claims,
//                ClientId = request.ValidatedRequest.Client.ClientId,
//                AccessTokenType = request.ValidatedRequest.AccessTokenType
//            };

//            foreach (var api in request.Resources.ApiResources)
//            {
//                if (api.Name.IsPresent())
//                {
//                    token.Audiences.Add(api.Name);
//                }
//            }

//            return token;
//        }

//        /// <summary>
//        /// Creates a serialized and protected security token.
//        /// </summary>
//        /// <param name="token">The token.</param>
//        /// <returns>
//        /// A security token in serialized form
//        /// </returns>
//        /// <exception cref="System.InvalidOperationException">Invalid token type.</exception>
//        public virtual async Task<string> CreateSecurityTokenAsync(Token token)
//        {
//            string tokenResult;

//            if (token.Type == OidcConstants.TokenTypes.AccessToken)
//            {
//                if (token.AccessTokenType == AccessTokenType.Jwt)
//                {
//                   // Logger.LogTrace("Creating JWT access token");

//                    tokenResult = await CreationService.CreateTokenAsync(token);
//                }
//                else
//                {
//                    //Logger.LogTrace("Creating reference access token");

//                    var handle = await ReferenceTokenStore.StoreReferenceTokenAsync(token);

//                    tokenResult = handle;
//                }
//            }
//            else if (token.Type == OidcConstants.TokenTypes.IdentityToken)
//            {
//                //Logger.LogTrace("Creating JWT identity token");

//                tokenResult = await CreationService.CreateTokenAsync(token);
//            }
//            else
//            {
//                throw new InvalidOperationException("Invalid token type.");
//            }

//            return tokenResult;
//        }

//        /// <summary>
//        /// Hashes an additional data (e.g. for c_hash or at_hash).
//        /// </summary>
//        /// <param name="tokenToHash">The token to hash.</param>
//        /// <returns></returns>
//        protected virtual string HashAdditionalData(string tokenToHash)
//        {
//            using (var sha = SHA256.Create())
//            {
//                var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(tokenToHash));

//                var leftPart = new byte[16];
//                Array.Copy(hash, leftPart, 16);

//                return Base64Url.Encode(leftPart);
//            }
//        }

//        public Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    /// <summary>
//    /// Default token creation service
//    /// </summary>
//    public class TokenCreationService : ITokenCreationService
//    {
//        /// <summary>
//        /// The key service
//        /// </summary>
//        protected readonly IKeyMaterialService Keys;

        

//        /// <summary>
//        ///  The clock
//        /// </summary>
//        protected readonly ISystemClock Clock;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="TokenCreationService"/> class.
//        /// </summary>
//        /// <param name="clock">The options.</param>
//        /// <param name="keys">The keys.</param>
//        /// <param name="logger">The logger.</param>
//        public TokenCreationService(ISystemClock clock, IKeyMaterialService keys, ILogger<TokenCreationService> logger)
//        {
//            Clock = clock;
//            Keys = keys; 
//        }

//        /// <summary>
//        /// Creates the token.
//        /// </summary>
//        /// <param name="token">The token.</param>
//        /// <returns>
//        /// A protected and serialized security token
//        /// </returns>
//        public virtual async Task<string> CreateTokenAsync(Token token)
//        {
//            var header = await CreateHeaderAsync(token);
//            var payload = await CreatePayloadAsync(token);

//            return await CreateJwtAsync(new JwtSecurityToken(header, payload));
//        }

//        /// <summary>
//        /// Creates the JWT header
//        /// </summary>
//        /// <param name="token">The token.</param>
//        /// <returns>The JWT header</returns>
//        protected virtual async Task<JwtHeader> CreateHeaderAsync(Token token)
//        {
//            var credential = await Keys.GetSigningCredentialsAsync();

//            if (credential == null)
//            {
//                throw new InvalidOperationException("No signing credential is configured. Can't create JWT token");
//            }

//            var header = new JwtHeader(credential);

//            // emit x5t claim for backwards compatibility with v4 of MS JWT library
//            if (credential.Key is X509SecurityKey x509key)
//            {
//                var cert = x509key.Certificate;
//                if (Clock.UtcNow.UtcDateTime > cert.NotAfter)
//                {
//                    //Logger.LogWarning("Certificate {subjectName} has expired on {expiration}", cert.Subject, cert.NotAfter.ToString(CultureInfo.InvariantCulture));
//                }

//                header["x5t"] = Base64Url.Encode(cert.GetCertHash());
//            }

//            return header;
//        }

//        /// <summary>
//        /// Creates the JWT payload
//        /// </summary>
//        /// <param name="token">The token.</param>
//        /// <returns>The JWT payload</returns>
//        protected virtual Task<JwtPayload> CreatePayloadAsync(Token token)
//        {
//            var payload = token.CreateJwtPayload(Clock, Logger);
//            return Task.FromResult(payload);
//        }

//        /// <summary>
//        /// Applies the signature to the JWT
//        /// </summary>
//        /// <param name="jwt">The JWT object.</param>
//        /// <returns>The signed JWT</returns>
//        protected virtual Task<string> CreateJwtAsync(JwtSecurityToken jwt)
//        {
//            var handler = new JwtSecurityTokenHandler();
//            return Task.FromResult(handler.WriteToken(jwt));
//        }
//    }


//    public interface IRefreshTokenService
//    {
//        /// <summary>
//        /// Creates the refresh token.
//        /// </summary>
//        /// <param name="subject">The subject.</param>
//        /// <param name="accessToken">The access token.</param>
//        /// <param name="client">The client.</param>
//        /// <returns>
//        /// The refresh token handle
//        /// </returns>
//        Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client);

//        /// <summary>
//        /// Updates the refresh token.
//        /// </summary>
//        /// <param name="handle">The handle.</param>
//        /// <param name="refreshToken">The refresh token.</param>
//        /// <param name="client">The client.</param>
//        /// <returns>
//        /// The refresh token handle
//        /// </returns>
//        Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client);
//    }

//    /// <summary>
//    /// Default refresh token service
//    /// </summary>
//    public class DefaultRefreshTokenService : IRefreshTokenService
//    { 
//        /// <summary>
//        /// The refresh token store
//        /// </summary>
//        protected readonly IRefreshTokenStore RefreshTokenStore;

//        /// <summary>
//        /// The clock
//        /// </summary>
//        protected readonly ISystemClock Clock;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DefaultRefreshTokenService" /> class.
//        /// </summary>
//        /// <param name="clock">The clock</param>
//        /// <param name="refreshTokenStore">The refresh token store</param>
//        /// <param name="logger">The logger</param>
//        public DefaultRefreshTokenService(ISystemClock clock, IRefreshTokenStore refreshTokenStore, ILogger<DefaultRefreshTokenService> logger)
//        {
//            Clock = clock; 
//            RefreshTokenStore = refreshTokenStore;
//        }

//        /// <summary>
//        /// Creates the refresh token.
//        /// </summary>
//        /// <param name="subject">The subject.</param>
//        /// <param name="accessToken">The access token.</param>
//        /// <param name="client">The client.</param>
//        /// <returns>
//        /// The refresh token handle
//        /// </returns>
//        public virtual async Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client)
//        {
            

//            int lifetime;
//            if (client.RefreshTokenExpiration == TokenExpiration.Absolute)
//            {
           
//                lifetime = client.AbsoluteRefreshTokenLifetime;
//            }
//            else
//            {
//               // _logger.LogDebug("Setting a sliding lifetime: " + client.SlidingRefreshTokenLifetime);
//                lifetime = client.SlidingRefreshTokenLifetime;
//            }

//            var refreshToken = new RefreshToken
//            {
//                CreationTime = Clock.UtcNow.UtcDateTime,
//                Lifetime = lifetime,
//                AccessToken = accessToken
//            };

//            var handle = await RefreshTokenStore.StoreRefreshTokenAsync(refreshToken);
//            return handle;
//        }

//        /// <summary>
//        /// Updates the refresh token.
//        /// </summary>
//        /// <param name="handle">The handle.</param>
//        /// <param name="refreshToken">The refresh token.</param>
//        /// <param name="client">The client.</param>
//        /// <returns>
//        /// The refresh token handle
//        /// </returns>
//        public virtual async Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client)
//        {
//            _logger.LogDebug("Updating refresh token");

//            bool needsCreate = false;
//            bool needsUpdate = false;

//            if (client.RefreshTokenUsage == TokenUsage.OneTimeOnly)
//            {
//                _logger.LogDebug("Token usage is one-time only. Generating new handle");

//                // delete old one
//                await RefreshTokenStore.RemoveRefreshTokenAsync(handle);

//                // create new one
//                needsCreate = true;
//            }

//            if (client.RefreshTokenExpiration == TokenExpiration.Sliding)
//            {
//                _logger.LogDebug("Refresh token expiration is sliding - extending lifetime");

//                // if absolute exp > 0, make sure we don't exceed absolute exp
//                // if absolute exp = 0, allow indefinite slide
//                var currentLifetime = refreshToken.CreationTime.GetLifetimeInSeconds(Clock.UtcNow.UtcDateTime);
//                //_logger.LogDebug("Current lifetime: " + currentLifetime.ToString());

//                var newLifetime = currentLifetime + client.SlidingRefreshTokenLifetime;
//                //_logger.LogDebug("New lifetime: " + newLifetime.ToString());

//                // zero absolute refresh token lifetime represents unbounded absolute lifetime
//                // if absolute lifetime > 0, cap at absolute lifetime
//                if (client.AbsoluteRefreshTokenLifetime > 0 && newLifetime > client.AbsoluteRefreshTokenLifetime)
//                {
//                    newLifetime = client.AbsoluteRefreshTokenLifetime;
//                    //_logger.LogDebug("New lifetime exceeds absolute lifetime, capping it to " + newLifetime.ToString());
//                }

//                refreshToken.Lifetime = newLifetime;
//                needsUpdate = true;
//            }

//            if (needsCreate)
//            {
//                handle = await RefreshTokenStore.StoreRefreshTokenAsync(refreshToken);
//                _logger.LogDebug("Created refresh token in store");
//            }
//            else if (needsUpdate)
//            {
//                await RefreshTokenStore.UpdateRefreshTokenAsync(handle, refreshToken);
//                _logger.LogDebug("Updated refresh token in store");
//            }
//            else
//            {
//                _logger.LogDebug("No updates to refresh token done");
//            }

//            return handle;
//        }
//    }
//}

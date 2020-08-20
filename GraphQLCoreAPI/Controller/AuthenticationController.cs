using GraphQL;
using GraphQLCoreAPI.Authentication.Interfaces;
using GraphQLCoreAPI.Controller.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.Controller
{

    public class GoogleResponseData
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {

        private IJwtAuthenticationHandler _jwtAuthenticationHandler;
        public AuthenticationController(IJwtAuthenticationHandler jwtAuthenticationHandler)
        {
            _jwtAuthenticationHandler = jwtAuthenticationHandler;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> DoLogin([FromBody] GoogleAuthModel googleAuthModel)
        {
            if (googleAuthModel.GoogleAuthToken.IsEmpty()) return Unauthorized();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(@"https://oauth2.googleapis.com/tokeninfo?id_token=" + googleAuthModel.GoogleAuthToken.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadAsStringAsync();
                    GoogleResponseData data = JsonConvert.DeserializeObject<GoogleResponseData>(info);

                    var claims = new []
                        {
                            new Claim(ClaimTypes.Name, data.name),
                            new Claim(ClaimTypes.Email, data.email),
                            new Claim("Role", "Admin")
                        };

                    Tuple<string, DateTime> infoTuple = _jwtAuthenticationHandler.GenerateJwtToken(claims);
                    string refresh_token = _jwtAuthenticationHandler.GenerateRefreshToken();

                    if (infoTuple.Item1 == null)
                    {
                        return Unauthorized();
                    }

                    return Ok(new LoginDataModel { Token = infoTuple.Item1, Refresh = refresh_token, Expiry = infoTuple.Item2 });
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [HttpPost]
        [Route("Refresh")]
        [AllowAnonymous]
        public IActionResult DoRefresh([FromBody] RefreshDataModel refresh_info)
        {
            if (refresh_info.Refresh.IsEmpty() || refresh_info.Token.IsEmpty()) return Unauthorized();

            try {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(refresh_info.Token) as JwtSecurityToken;
                string username = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;

                if (!ValidateRefreshToken(refresh_info.Refresh, username))
                {
                    return Unauthorized();
                }

                var claims = jwtToken.Claims as Claim[];

                Tuple<string, DateTime> infoTuple = _jwtAuthenticationHandler.GenerateJwtToken(claims);
                string refresh_token = _jwtAuthenticationHandler.GenerateRefreshToken();

                if (infoTuple.Item1 == null)
                {
                    return Unauthorized();
                }

                return Ok(new LoginDataModel { Token = infoTuple.Item1, Refresh = refresh_token, Expiry = infoTuple.Item2 });
            
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        private bool ValidateRefreshToken(string refresh, string username)
        {
            return true;
        }
    }
}

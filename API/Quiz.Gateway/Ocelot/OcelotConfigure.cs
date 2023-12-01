using Microsoft.IdentityModel.Tokens;
using Ocelot.Middleware;
using Quiz.Gateway.Services;
using Quiz.Gateway.SwaggerExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using Ocelot.Authorization;

namespace Quiz.Gateway.Ocelot
{
    public static class OcelotConfigure
    {
        public static OcelotPipelineConfiguration Create(ISwaggerService swaggerService)
        {
            var listOpperate = swaggerService.MergeSwagger();
            var ocelotConfigs = new OcelotPipelineConfiguration
            {
                PreAuthenticationMiddleware = async (ctx, next) =>
                {
                    var match = MatchPathWithPattern(ctx.Request.Path, listOpperate, ctx.Request.Method);
                    if (!match)
                    {
                        return;
                    }
                    await next.Invoke();
                },
                AuthenticationMiddleware = async (ctx, next) =>
                {
                    //var configuration = ctx.RequestServices.GetRequiredService<IConfiguration>();
                    //var route = GetRoutePath(ctx.Request.Path, listOpperate, ctx.Request.Method);

                    //var isCheckSecurity = CheckSecurity(route, ctx.Request.Method); // check required security in swagger document is null or not
                    //if (isCheckSecurity != null) // Isn't Required
                    //{
                    //    var token = GetToken(ctx); //Get token from Header 
                    //    var isValidateToken = ValidateTokenJWT(token, configuration); //validate token
                    //    Console.WriteLine(">>> Validate: " + isValidateToken.ToString());
                    //    if (isValidateToken == false) //token invalid
                    //    {
                    //        ctx.Items.SetError(new UnauthenticatedError("Invalid token."));
                    //        return;
                    //    }
                    //    var claimsPrincipal = GetClaims(token, configuration); // get role claim user
                    //    var mergeListRoles = string.Join(",", isCheckSecurity); // get list roles of route
                    //    if (CompareRole(claimsPrincipal, mergeListRoles) == false)  // check roles
                    //    {
                    //        ctx.Items.SetError(new UnauthorizedError("You do not have a role to access"));
                    //        return;
                    //    }
                    //    //UpdateDownstreamRequest(ctx, claimsPrincipal, token);
                    //}
                    //else
                    //    Console.WriteLine(">>> None Authorize");
                    await next.Invoke();
                }
            };
            return ocelotConfigs;
        }
        public static string GetToken(HttpContext ctx)
        {
            var token = ctx.Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Trim();
            return token;
        }
        public static bool CompareRole(ClaimsPrincipal claimsPrincipal, string mergeListRoles)
        {
            Console.WriteLine(">>>>>>>>> mergeListRoles:" + mergeListRoles);
            var isRoleUser = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
            var listRoles = mergeListRoles.Split(',');
            foreach (var role in listRoles) //compare role user with roles of route
            {
                Console.WriteLine(">>>>>>>>> Role Security: " + role);
                if (role == "")
                {
                    Console.WriteLine($">>>>>>>>> Compare roles: {role.Trim()} - {isRoleUser}");
                    return true;
                }
                if (role.Trim() == isRoleUser) // Role is valid
                {
                    Console.WriteLine($">>> Compare roles: {role.Trim()} - {isRoleUser}");
                    return true;
                }
            }
            return false;
        }

        public static List<string> CheckSecurity(SwaggerPath routePath, string method)
        {
            switch (method)
            {
                case "GET":
                    try
                    {
                        Console.WriteLine($"{routePath.Get.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault()}");
                        if (routePath.Get.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault() != null)
                        {
                            var role = routePath.Get.Security.FirstOrDefault(x => x.ContainsKey("bearer")).Values.FirstOrDefault();
                            return role;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
                case "POST":
                    try
                    {
                        Console.WriteLine($"{routePath.Post.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault()}");
                        if (routePath.Post.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault() != null)
                        {
                            var role = routePath.Post.Security.FirstOrDefault(x => x.ContainsKey("bearer")).Values.FirstOrDefault();
                            return role;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
                case "PUT":
                    try
                    {
                        Console.WriteLine($"{routePath.Put.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault()}");
                        if (routePath.Put.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault() != null)
                        {
                            var role = routePath.Put.Security.FirstOrDefault(x => x.ContainsKey("bearer")).Values.FirstOrDefault();
                            return role;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
                case "DELETE":
                    try
                    {
                        Console.WriteLine($"{routePath.Delete.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault()}");
                        if (routePath.Delete.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault() != null)
                        {
                            var role = routePath.Delete.Security.FirstOrDefault(x => x.ContainsKey("bearer")).Values.FirstOrDefault();
                            return role;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
                case "PATCH":
                    try
                    {
                        Console.WriteLine($"{routePath.Patch.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault()}");
                        if (routePath.Patch.Security?.FirstOrDefault()?.Values?.FirstOrDefault()?.FirstOrDefault() != null)
                        {
                            var role = routePath.Patch.Security.FirstOrDefault(x => x.ContainsKey("bearer")).Values.FirstOrDefault();
                            return role;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
            }
            return null;
        }

        public static bool MatchPathWithPattern(string path, SwaggerDocument ListOpperate, string method)
        {
            Console.WriteLine(">>> Path url: " + path);
            foreach (var routePath in ListOpperate.Paths)
            {
                try
                {
                    var pattern = "^" + routePath.Key + "$";
                    if (routePath.Key.Contains("{") && routePath.Key.Contains("}"))
                        pattern = GenerateRoutePatternRegex(pattern);
                    Match matchPathWithPattern = Regex.Match(path, pattern);
                    if (matchPathWithPattern.Success)
                    {
                        var methodExist = false;
                        switch (method)
                        {
                            case "GET":
                                if (routePath.Value.Get != null)
                                    methodExist = true;
                                break;
                            case "POST":
                                if (routePath.Value.Post != null)
                                    methodExist = true;
                                break;
                            case "PUT":
                                if (routePath.Value.Put != null)
                                    methodExist = true;
                                break;
                            case "DELETE":
                                if (routePath.Value.Delete != null)
                                    methodExist = true;
                                break;
                            case "PATCH":
                                if (routePath.Value.Patch != null)
                                    methodExist = true;
                                break;
                            default:
                                methodExist = false;
                                break;
                        }
                        if (methodExist)
                        {
                            Console.WriteLine($">>> Match found with route: {routePath.Key} --- pattern: {pattern}");
                            return true;
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            return false;
        }
        public static SwaggerPath GetRoutePath(string path, SwaggerDocument ListOpperate, string method)
        {
            Console.WriteLine(">>> Path url: " + path);
            foreach (var routePath in ListOpperate.Paths)
            {
                try
                {
                    var pattern = "^" + routePath.Key + "$";
                    if (routePath.Key.Contains("{") && routePath.Key.Contains("}"))
                        pattern = GenerateRoutePatternRegex(pattern);
                    Match matchPathWithPattern = Regex.Match(path, pattern);
                    if (matchPathWithPattern.Success)
                    {
                        var methodExist = false;
                        switch (method)
                        {
                            case "GET":
                                if (routePath.Value.Get != null)
                                    methodExist = true;
                                break;
                            case "POST":
                                if (routePath.Value.Post != null)
                                    methodExist = true;
                                break;
                            case "PUT":
                                if (routePath.Value.Put != null)
                                    methodExist = true;
                                break;
                            case "DELETE":
                                if (routePath.Value.Delete != null)
                                    methodExist = true;
                                break;
                            case "PATCH":
                                if (routePath.Value.Patch != null)
                                    methodExist = true;
                                break;
                            default:
                                methodExist = false;
                                break;
                        }
                        if (methodExist)
                        {
                            return routePath.Value;
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            return null;
        }
        public static string GenerateRoutePatternRegex(string path)
        {
            string pattern = @"\{[^}]+\}";
            string RegPath = Regex.Replace(path, pattern, @"((\w|\-)+)");
            return RegPath;
        }
        public static ClaimsPrincipal GetClaims(string token, IConfiguration configuration)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecurityKey"])),
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var _);
                //var rolesClaim = claimsPrincipal.FindFirst(ClaimTypes.Role);
                if (claimsPrincipal != null)
                {
                    return claimsPrincipal;
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public static bool ValidateTokenJWT(string token, IConfiguration configuration)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecurityKey"])),
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out var _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetHeaderValue(this List<Header> headers, string key, string value)
        {
            var header = headers.FirstOrDefault(p => p.Key == key);
            if (header != null)
            {
                headers.Remove(header);
            }
            headers.Add(new Header(key, new string[] { value }));
        }
        //private static void UpdateDownstreamRequest(HttpContext ctx, ClaimsPrincipal claims, string token)
        //{
        //    var headers = ctx.Items.DownstreamRequest().Headers;
        //    var Roles = string.Join(", ", claims.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList());
        //    try
        //    {
        //        headers.Add("X-Auth-Username", claims.FindFirst(ClaimTypes.GivenName).Value);
        //        headers.Add("X-Auth-Email", claims.FindFirst(ClaimTypes.GivenName).Value);
        //        headers.Add("X-Auth-Token", token);
        //        headers.Add("X-Auth-UserId", claims.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        headers.Add("X-Auth-Role", Roles);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Interface;
using System.Globalization;
using System.Security.Claims;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly IAdmin _admin;
    public static string idUser = "";
    public static string user_Id { get { return idUser; } set { idUser = value; } }
    public static string accessToken = "";
    public static string access_Token { get { return accessToken; } set { accessToken = value; } }
    private static string userRole = "";

    private readonly ILogger<HomeController> _logger;
    private static string _domain ="";
    public static string _ClientId = "";
    public static string _ClientSecret = "";

    public HomeController(IAdmin admin, IConfiguration configuration)
    {
        _admin = admin;
        _domain = configuration["Auth0:Domain"];
        _ClientId = configuration["Auth0:ClientId"];
        _ClientSecret = configuration["Auth0:ClientSecret"];
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            string idToken = await HttpContext.GetTokenAsync("id_token");
            accessToken = await HttpContext.GetTokenAsync("access_token");
            DateTime accessTokenExpiresAt = DateTime.Parse(await HttpContext.GetTokenAsync("expires_at"),
                CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var identity = User.Identity as ClaimsIdentity;
            userRole = identity.Claims.FirstOrDefault().Value; //Admin
            idUser = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
        }
        if (userRole != "Admin" && userRole != "" && userRole != "User_LocationDeVoiture")
        {
            return RedirectToAction("PostClient", "Client");
        }
        return RedirectToAction("Index","Client");
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }

    public async Task Login(string returnUrl = "/")
    {
        await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties()
        {
            RedirectUri = Url.Action("Index", "Home")
        });   
    }

    [Authorize]
    public async Task LogOut()
    {
        userRole = "";
        await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
        {
            
            RedirectUri = Url.Action("Index", "Home")
        });
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }


    public async Task AssignRoleToUser(string accessToken)
    {
        if(userRole != "" && accessToken != null)
        {
            if (userRole != "Admin" && userRole != "User_LocationDeVoiture")
            {
                await _admin.AssignRoleToUser(idUser, accessToken);
            }
        }
    }

    [HttpGet]
    public ActionResult GetConfigValue()
    {
        return Json(new {clientId = _ClientId, clientSecret = _ClientSecret, audience = _domain });
    }

}

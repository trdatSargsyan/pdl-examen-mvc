using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MVC.Service;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
    .AddCookie()
    .AddOpenIdConnect("Auth0", option =>
    {
        //Define the autority
        //Set the authority to Auth0 domain
        option.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        //configure the auth0 Client ID and Client Secret
        option.ClientId = builder.Configuration["Auth0:ClientId"];
        option.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
        //Authorization Code
        //Set response the code
        option.ResponseType = OpenIdConnectResponseType.Code;
        //Configure the scope
        option.Scope.Clear();
        option.Scope.Add("openid");
        option.Scope.Add("profile");
        option.Scope.Add("read:messages");
        //define callback 
        option.CallbackPath = new PathString("/callback");
        option.SaveTokens = true;
        option.ClaimsIssuer = "Auth0";

        //option.ClaimActions.MapJsonKey(claimType: ClaimTypes.Email, jsonKey: "emails");

        //Add events for logout (and maybe more ;-))

        option.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProvider = context =>
            {
                // Set the audience query parameter to the API identifier to ensure the returned Access Tokens 
                //can be used to call protected endpoints on the corresponding API.
                context.ProtocolMessage.SetParameter("audience", builder.Configuration["Auth0:Audience"]);
                return Task.FromResult(0);
            },
            OnMessageReceived = context =>
            {
                if (context.ProtocolMessage.Error == "access_denied")
                {
                    context.HandleResponse();
                    context.Response.Redirect("/Account/AccessDenied");
                }
                return Task.FromResult(0);
            },
            OnRedirectToIdentityProviderForSignOut = (context) =>
            {
                var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Auth0:ClientId"]}";

                var postLogoutUri = context.Properties.RedirectUri;
                if (!string.IsNullOrEmpty(postLogoutUri))
                {
                    if (postLogoutUri.StartsWith("/"))
                    {
                        var request = context.Request;
                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                    }
                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                }
                context.Response.Redirect(logoutUri);
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


ServiceInjection.AdminService(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//Errors
app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;

    await context.Response.WriteAsJsonAsync(new { error = exception.Message });
})); ;

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(); //
app.UseRouting();
app.UseAuthentication();//
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

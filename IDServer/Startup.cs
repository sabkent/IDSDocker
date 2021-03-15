using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IDServer
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                options.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies(o =>
            {                
                o.ApplicationCookie.Configure(opts =>
                {
                    opts.LoginPath = "/Account/Login";
                    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    opts.LogoutPath = "/Account/Logout";
                });
                o.ExternalCookie.Configure(opts =>
                {
                    opts.LoginPath = "/Account/Login";
                    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    opts.LogoutPath = "/Account/Logout";
                });
            });

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.AccessTokenJwtType = string.Empty;
                options.EmitStaticAudienceClaim = true;
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";
            })
            .AddInMemoryApiResources(InitialConfiguration.GetApis())
            .AddInMemoryApiScopes(InitialConfiguration.GetApiScopes())
            .AddInMemoryIdentityResources(InitialConfiguration.GetIdentityResources())
            .AddInMemoryClients(InitialConfiguration.GetClients())
            .AddTestUsers(InitialConfiguration.GetUsers())
            .AddDeveloperSigningCredential();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}

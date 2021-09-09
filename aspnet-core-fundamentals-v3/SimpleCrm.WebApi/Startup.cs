using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCrm.SqlDbServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Authentication.Google;
using SimpleCrm.WebApi.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SimpleCrm.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    // JWT Auth stuff
    private const string secretKey = "5f4a3ec3f9e8c539d33a92fb";
    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleCrmDbContext>(options => {
                var cs = Configuration.GetConnectionString("SimpleCrmConnection");
                options.UseNpgsql(cs);
            });

      var jwtOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
      });

            services.AddSpaStaticFiles(config =>
            {
              config.RootPath = Configuration["SpaRoot"];
            });

            services.AddDbContext<CrmIdentityDbContext>(options =>{
                var cs = Configuration.GetConnectionString("SimpleCrmConnection");
                options.UseNpgsql(cs);
            });

            var googleOptions = Configuration.GetSection(nameof(GoogleAuthSettings));
            services.Configure<GoogleAuthSettings>(options =>
            {
              options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
              options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
            });


            var msOptions = Configuration.GetSection(nameof(MSAuthSettings));
            services.Configure<MSAuthSettings>(options =>
            {
              options.ClientId = msOptions[nameof(MSAuthSettings.ClientId)];
              options.ClientSecret = msOptions[nameof(MSAuthSettings.ClientSecret)];
            });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer( configureOptions =>
      {
        // TODO
      });


            services.AddDefaultIdentity<CrmUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<CrmIdentityDbContext>();

            services.AddControllersWithViews();
           services.AddRazorPages();

           services.AddScoped<ICustomerData, SqlCustomerData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api"),
                appBuilder => appBuilder.UseSpa(spa =>
                {
                  if (env.IsDevelopment())
                  {
                    spa.Options.SourcePath = "../simple-crm-cli";
                    spa.Options.StartupTimeout = new TimeSpan(0, 0, 300); //300 seconds
                    spa.UseAngularCliServer(npmScript: "start");
                  }
                }));
        }
    }
}

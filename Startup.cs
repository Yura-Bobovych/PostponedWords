using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostponedWords.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PostponedWords.Models;
using PostponedWords.Models.Db;
using Microsoft.EntityFrameworkCore;
using PostponedWords.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PostponedWords
{
    public class Startup
    {
		public IConfigurationRoot Configuration { get; set; }
		public Startup(IHostingEnvironment env)		
        {
			var builder = new ConfigurationBuilder()
			 .SetBasePath(env.ContentRootPath)
			 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			Configuration = builder.Build();
		}        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			var connection = @"Server=PC;Initial Catalog=PostroneWords_v2;Integrated Security=True;";
			services.AddDbContext<PostroneWords_v2Context>(options => options.UseSqlServer(connection),ServiceLifetime.Singleton);

			services.Configure<AppSettings>(Configuration);
			services.AddSingleton<IJwtFactory, JwtFactory>();
			services.AddSingleton<IJwtFactory, JwtFactory>();
			services.AddSingleton<IDbUserMiddleware, DbUserMiddleware>();
			services.AddSingleton<IDbWordsMiddleware, DbWordsMiddleware>();
			services.AddSingleton<IWordsApiMiddleware,WordsApiMiddleware>();
			services.AddAuthorization(options =>
			{
				options.AddPolicy("BaseUser", policy => policy.RequireClaim(Configuration["JwtClaimIdentifiers:Rol"], Configuration["JwtClaims:BaseUser"]));
			});
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]));
			var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
			services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			});

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,

				RequireExpirationTime = false,
				ValidateLifetime = false,
				ClockSkew = TimeSpan.Zero
			};

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options => {
			options.RequireHttpsMetadata = false;			
			options.TokenValidationParameters = tokenValidationParameters;
		});
			services.AddLogging();
			services.AddMvc();
			
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]));
			var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
			app.UseAuthentication();
			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();
			
			app.UseMvc(routes =>
            {
				routes.MapRoute(
					name: "api",
					template: "api/{controller}/{action}");
				routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");			

				routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });			
			
			
			
		}

		
	}
}

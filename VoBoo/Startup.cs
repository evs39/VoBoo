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
using VoBoo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using VoBoo.Services;
using VoBoo.Models;

namespace VoBoo
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(
					Configuration.GetSection("VoBooConnection_Core3").Value));

			services.AddScoped<IEmailSender, EmailSender>();

			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			
			services.AddRazorPages();

			services.Configure<EmailSettings>(es =>
			{
				es.MailPort = int.Parse(Configuration.GetSection("EmailSettings:MailPort").Value);
				es.MailServer = Configuration.GetSection("EmailSettings:MailServer").Value;
				es.SenderName = Configuration.GetSection("EmailSettings:SenderName").Value;
				es.Sender = Configuration.GetSection("EmailSettings:Sender").Value;
				es.Password = Configuration.GetSection("EmailSettings:Password").Value;
			});
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}

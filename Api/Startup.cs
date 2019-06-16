using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Email;
using Api.Helpers;
using Application;
using Application.Commands;
using Application.Interfaces;
using EfCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<moviesContext>();
            services.AddTransient<IGetMoviesCommand, EfGetMoviesCommand>();
            services.AddTransient<IGetMovieCommand, EfGetMovieCommand>();
            services.AddTransient<IAddMovieCommand, EfAddMovieCommand>();
            services.AddTransient<IAddDirectorCommand, EfAddDirectorCommand>();
            services.AddTransient<IDeleteMovieCommand, EfDeleteMovieCommand>();
            services.AddTransient<IGetDirectorsCommand, EfGetDirectorsCommand>();
            services.AddTransient<IGetDirectorCommand, EfGetDirectorCommand>();
            services.AddTransient<IUpdateDirectorCommand, EfUpdateDirectorCommand>();
            services.AddTransient<IDeleteDirectorCommand, EfDeleteDirectorCommand>();
            services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IAddUserCommand, EfAddUserCommand>();
            services.AddTransient<IUpdateMovieCommand, EfUpdateMovieCommand>();
            services.AddTransient<IAddReservationCommand, EfAddReservationCommand>();
            services.AddTransient<IGetReservationsCommand, EfGetReservationsCommand>();
            services.AddTransient<IGetReservationCommand, EfGetReservationCommand>();
            services.AddTransient<IUpdateReservationCommand, EfUpdateReservationCommand>();
            services.AddTransient<IDeleteReservationCommand, EfDeleteReservationCommand>();

            //services.AddTransient<IHttpContextAccessor, IHttpContextAccessor>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var section = Configuration.GetSection("Email");

            var sender = new SmtpEmailSender(section["host"],Int32.Parse(section["port"]),section["fromaddress"],section["password"]);

            services.AddSingleton<IEmailSender>(sender);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var key = Configuration.GetSection("Encryption")["key"];

            var enc = new Encryption(key);

            services.AddSingleton(enc);

            services.AddTransient(s =>
            {
                var http = s.GetRequiredService<IHttpContextAccessor>();
                var value = http.HttpContext.Request.Headers["Authorization"].ToString();
                var encryption = s.GetRequiredService<Encryption>();

                try
                {
                    var decodedString = encryption.DecryptString(value);
                    decodedString = decodedString.Replace("\f", "");
                    var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
                    user.IsLogged = true;
                    return user;
                }
                catch (Exception)
                {

                    return new LoggedUser
                    {
                        IsLogged = false
                    };                }
            }
            );

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
              
            });

            
        }
    }
}

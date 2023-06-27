using AutoMapper;
using Hotel.API.Data;
using Hotel.API.Entities;
using Hotel.API.Helpers;
using Hotel.API.Interfaces;
using Hotel.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Hotel.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel.API", Version = "v1" });
            });
            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetValue<string>("DbSettings:HotelDbConString")));
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("AdminPolicy", policy =>
                //    policy.RequireClaim("roles", "Admin"));
                //options.AddPolicy("UserPolicy", policy =>
                //    policy.RequireClaim("roles", "User"));
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var roles = context.User.FindAll("roles").Select(r => r.Value);
                        return roles.Any(r => r == "Admin");
                    });
                });

                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var roles = context.User.FindAll("roles").Select(r => r.Value);
                        return roles.Any(r => r == "User");
                    });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

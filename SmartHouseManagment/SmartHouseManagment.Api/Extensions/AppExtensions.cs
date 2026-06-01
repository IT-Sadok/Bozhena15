using Microsoft.AspNetCore.Identity;
using SmartHouseManagment.Api.Middleware;
using SmartHouseManagment.AppCore.Extensions;
using SmartHouseManagment.AppCore.Models.User;

namespace SmartHouseManagment.Api.Extensions;

public static class AppExtensions
{
    public static async Task AddAppServices(this WebApplication app)
    {
        await app.AddIdentityRoles();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }

    private static async Task AddIdentityRoles(
        this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            
            var roles = EnumExtensions.GetAllDescriptions<UserRole>();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
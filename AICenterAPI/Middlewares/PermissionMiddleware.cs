﻿using Microsoft.EntityFrameworkCore;
using AICenterAPI.Attributes;
using AICenterAPI.Datas;
using AICenterAPI.Models;
using AICenterAPI.Repositories;

namespace AICenterAPI.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var permissionAttribute = endpoint.Metadata.GetMetadata<PermissionAttribute>();
                if (permissionAttribute != null)
                {
                    var userClaimPermissions = context.User.Claims
                        .Where(c => c.Type == "Permission")
                        .Select(c => c.Value);
                    if (
                        userClaimPermissions.Contains(RoleDefaultTypes.SuperAdmin.KeyName) ||
                        userClaimPermissions.Contains(permissionAttribute.Permission)
                    )
                    {
                        await _next(context);
                        return;
                    }
                    var _myDbContext = context.RequestServices.GetService<MyDBContext>();
                    if (_myDbContext != null)
                    {
                        var userId = int.Parse(context.User.FindFirst("Id").Value);
                        var user = await _myDbContext.Users.FindAsync(userId);
                        if (user != null)
                        {
                            if (user.RoleId != null)
                            {
                                var permissionIds = await _myDbContext.RolePermissions.Where(rp => rp.RoleId == user.RoleId).Select(rp => rp.PermissionId).ToListAsync();

                                var permissions = await _myDbContext.Permissions
                                    .Where(p => permissionIds.Contains(p.Id))
                                    .Select(p => p.KeyName)
                                    .ToListAsync();

                                if (permissions.Contains(permissionAttribute.Permission))
                                {
                                    await _next(context);
                                    return;
                                }
                            }

                        }
                        
                    }
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new ApiResponse()
                    {
                        Message = "You don't have permission to access this resource",
                        Success = false
                    });
                    return;
                }
            }
            await _next(context);
        }
    }   
}

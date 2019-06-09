﻿// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAuthorize
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// This is called in the overridden SaveChanges in the application's DbContext
        /// Its job is to see if a entity has a IUserId and set the appropriate key 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        public static void MarkWithUserIdIfNeeded(this DbContext context, string userId)
        {
            foreach (var entityEntry in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entityEntry is IUserId hasUserId)
                    hasUserId.SetDataKey(userId);
            }
        }
    }
}
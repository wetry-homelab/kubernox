﻿using Application.Interfaces;
using Application.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public TemplateRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Task<Template[]> ReadsAsync()
        {
            return serviceDbContext.Template.Where(s => !s.DeleteAt.HasValue).ToArrayAsync();
        }

        public Task<Template> ReadAsync(Expression<Func<Template, bool>> predicate)
        {
            return serviceDbContext.Template.FirstOrDefaultAsync(predicate);
        }

        public Task<int> InsertAsync(Template entity)
        {
            serviceDbContext.Template.Add(entity);
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(Template entity)
        {
            serviceDbContext.Entry(entity).State = EntityState.Modified;
            return serviceDbContext.SaveChangesAsync();
        }

        public Task<Template[]> ReadsAsync(Expression<Func<Template, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertsAsync(Template[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatesAsync(Template[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Template entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletesAsync(Template[] entities)
        {
            throw new NotImplementedException();
        }
    }
}

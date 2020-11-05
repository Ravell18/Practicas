﻿using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpawn.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSpawn.Infraestructure.Repositories
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AnimalSpawnContext _context;
        private readonly DbSet<T> _entity;
        public SQLRepository(AnimalSpawnContext context)
        {
            this._context = context;
            this._entity = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity");
            _entity.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            if (id <= 0) throw new ArgumentNullException("Entity");
            var entity = await GetById(id);
            _entity.Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity");
            if (entity.Id <= 0) throw new ArgumentNullException("Entity");
            _entity.Update(entity);
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _entity.Where(expression).AsNoTracking().AsEnumerable();
        }
        public IEnumerable<T> GetAll()
        {
            return _entity.AsNoTracking().AsEnumerable();
        }
        public async Task<T> GetById(int id)
        {
            return await _entity.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id);
        }
       
    }

}

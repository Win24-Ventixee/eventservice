﻿using System.Linq.Expressions;
using DataLayer.Models;

namespace DataLayer.Repositories
{
  public interface IBaseRepository<TEntity> where TEntity : class
  {
    Task<RepositoryResult> AddAsync(TEntity entity);
    Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<RepositoryResult> DeleteAsync(TEntity entity);
    Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<RepositoryResult> UpdateAsync(TEntity entity);
  }
}
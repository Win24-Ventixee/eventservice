﻿using System.Linq.Expressions;
using DataLayer.Contexts;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
  protected readonly DataContext _context;
  protected readonly DbSet<TEntity> _table;

  protected BaseRepository(DataContext context)
  {
    _context = context;
    _table = _context.Set<TEntity>();
  }

  public async Task<RepositoryResult> AddAsync(TEntity entity)
  {
    try
    {
      _table.Add(entity);
      await _context.SaveChangesAsync();
      return new RepositoryResult { Success = true };
    }
    catch (Exception ex)
    {
      return new RepositoryResult
      {
        Success = false,
        Error = ex.Message
      };
    }
  }

  public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
  {
    try
    {
      var entities = await _table.ToListAsync();
      return new RepositoryResult<IEnumerable<TEntity>> { Success = true, Result = entities };
    }
    catch (Exception ex)
    {
      return new RepositoryResult<IEnumerable<TEntity>>
      {
        Success = false,
        Error = ex.Message
      };
    }
  }

  public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression)
  {
    try
    {
      var entity = await _table.FirstOrDefaultAsync(expression) ?? throw new Exception("Not Found");
      return new RepositoryResult<TEntity?> { Success = true, Result = entity };
    }
    catch (Exception ex)
    {
      return new RepositoryResult<TEntity?>
      {
        Success = false,
        Error = ex.Message
      };
    }
  }

  public async Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
  {
    var exists = await _table.AnyAsync(expression);
    return exists
      ? new RepositoryResult { Success = true }
      : new RepositoryResult { Success = false, Error = "Not Found" };
  }

  public async Task<RepositoryResult> UpdateAsync(TEntity entity)
  {
    try
    {
      _table.Update(entity);
      await _context.SaveChangesAsync();
      return new RepositoryResult { Success = true };
    }
    catch (Exception ex)
    {
      return new RepositoryResult
      {
        Success = false,
        Error = ex.Message
      };
    }
  }

  public async Task<RepositoryResult> DeleteAsync(TEntity entity)
  {
    try
    {
      _table.Remove(entity);
      await _context.SaveChangesAsync();
      return new RepositoryResult { Success = true };
    }
    catch (Exception ex)
    {
      return new RepositoryResult
      {
        Success = false,
        Error = ex.Message
      };
    }
  }
}

﻿using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IBaseRepository<TEntity, TId>
    where TEntity : class
{
    Task<ResponseHandler<IEnumerable<TEntity>>> Get();
    Task<ResponseHandler<TEntity>> Get(TId id);
    Task<ResponseHandler<TEntity>> Post(TEntity entity);
    Task<ResponseHandler<TEntity>> Put(TId id, TEntity entity);
    Task<ResponseHandler<TEntity>> Delete(TId id);
}

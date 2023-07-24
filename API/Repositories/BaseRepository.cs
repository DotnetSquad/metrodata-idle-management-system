using API.Contracts;
using API.Data;

namespace API.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext Context;

    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public ICollection<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public TEntity? GetByGuid(Guid guid)
    {
        var entity = Context.Set<TEntity>().Find(guid);
        Context.ChangeTracker.Clear();
        return entity;
    }

    public TEntity Create(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null!;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool IsExist(Guid guid)
    {
        return Context.Set<TEntity>().Find(guid) != null;
    }
}

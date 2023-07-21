using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context) { }
}

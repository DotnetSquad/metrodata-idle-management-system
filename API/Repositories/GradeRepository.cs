using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class GradeRepository : BaseRepository<Grade>, IGradeRepository
{
    public GradeRepository(ApplicationDbContext Context) : base(Context) { }
}

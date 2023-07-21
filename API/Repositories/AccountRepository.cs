using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRepository : BaseRepository<Account>
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}

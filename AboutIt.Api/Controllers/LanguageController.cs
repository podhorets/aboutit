using AboutIt.Mongo;
using AboutIt.Postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace AboutIt.Controllers;

[ApiController]
[Route("[controller]")]
public class LanguageController : ControllerBase
{
    private readonly PostgresDbContext _postgresDbContext;

    public LanguageController(PostgresDbContext postgresDbContext)
    {
        _postgresDbContext = postgresDbContext;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(string name, CancellationToken cancellationToken)
    {
        var language = new Language { Name = name };
        await _postgresDbContext.Languages.AddAsync(language, cancellationToken);
        await _postgresDbContext.SaveChangesAsync(cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get(string name)
    {
        var language = await _postgresDbContext.Languages.FirstOrDefaultAsync(x => x.Name.Equals(name));
        return Ok(language);
    }
}
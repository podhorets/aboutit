using AboutIt.Mongo;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AboutIt.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly MongoDbContext _mongoDbContext;

    public LessonController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(string name, CancellationToken cancellationToken)
    {
        var lesson = new Lesson { Name = name };
        await _mongoDbContext.LessonCollection.InsertOneAsync(lesson, null, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get(string name, CancellationToken cancellationToken)
    {
        var filter = Builders<Lesson>.Filter.Eq(r => r.Name, name);
        var lesson = await _mongoDbContext.LessonCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return Ok(lesson);
    }
}
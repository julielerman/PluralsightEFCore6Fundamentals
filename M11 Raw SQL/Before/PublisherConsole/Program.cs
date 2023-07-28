using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext(); //existing database
_context.Database.EnsureDeleted();
_context.Database.Migrate(); //ensurecreated won't pick up the non-schema changes!

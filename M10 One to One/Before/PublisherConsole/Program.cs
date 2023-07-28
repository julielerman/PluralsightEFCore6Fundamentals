using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();


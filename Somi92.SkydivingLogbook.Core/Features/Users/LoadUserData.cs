using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Somi92.SkydivingLogbook.Core.Data;

namespace Somi92.SkydivingLogbook.Core.Features.Users
{
    public class LoadUserData
    {
        public class Result
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Query : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly SkydivingLogbookContext _context;

            public Handler(SkydivingLogbookContext context) => _context = context;

            // TODO: Check case when no match found
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken) =>
                await _context.Users
                    .Select(e => new Result { Id = e.Id, Name = e.Name })
                    .SingleOrDefaultAsync(e => e.Id == request.Id);
        }
    }
}

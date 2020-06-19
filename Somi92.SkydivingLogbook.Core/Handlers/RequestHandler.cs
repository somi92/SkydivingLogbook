using System;
using System.Threading.Tasks;

namespace Somi92.SkydivingLogbook.Core.Handlers
{
    public interface IRequest { }

    public abstract class RequestHandler<I, O>
    {
        public abstract Task<O> HandleAsync(I request);
    }
}

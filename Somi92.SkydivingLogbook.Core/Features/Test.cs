using System.Threading.Tasks;
using Somi92.SkydivingLogbook.Core.Handlers;

namespace Somi92.SkydivingLogbook.Core.Features
{
    public class Test : RequestHandler<string, int>
    {
        public override Task<int> HandleAsync(string request)
        {
            return Task.FromResult(0);
        }
    }
}

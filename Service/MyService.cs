using System.Threading.Tasks;
using Grpc.Core;
using Shared;

namespace Service
{
    public class MyService : Svc.SvcBase
    {
        public override Task<CalculateReply> Calculate(CalculateRequest request, ServerCallContext context)
        {
            long result = request.Op switch
            {
                "+" => request.X + request.Y,
                "-" => request.X - request.Y,
                "*" => request.X * request.Y,
                "/" => (long)request.X / request.Y,
                _ => -1
            };

            return Task.FromResult(new CalculateReply {Result = result});
        }
    }
}
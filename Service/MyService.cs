using System;
using System.Collections.Generic;
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

        public override async Task Median(IAsyncStreamReader<Temperature> requestStream, IServerStreamWriter<Temperature> responseStream, ServerCallContext context)
        {
            Console.WriteLine("Median");
            var vals = new List<double>();
            while (await requestStream.MoveNext())
            {
                var temp = requestStream.Current;
                vals.Add(temp.Value);
                double med = 0;
                if (vals.Count == 10)
                {
                    var arr = vals.ToArray();
                    Array.Sort(arr);
                    med = (arr[4] + arr[5]) / 2;
                    vals.Clear();
                    await responseStream.WriteAsync(new Temperature { Timestamp = temp.Timestamp, Value = med });
                }
            }
        }
    }
}
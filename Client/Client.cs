﻿using System;
using Grpc.Core;
using Shared;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = args[0];
            var port = int.Parse(args[1]);
            var x = long.Parse(args[2]);
            var op = args[3];
            var y = long.Parse(args[4]);

            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            
            var client = new Svc.SvcClient(channel);
            var reply = client.Calculate(new CalculateRequest
            {
                X = x,
                Op = op,
                Y = y
            });
            
            Console.WriteLine($"The calculated result is: {reply.Result}");
        }
    }
}
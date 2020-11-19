using System;
using System.IO;
using Grpc.Core;
using Shared;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            
            var pair = new KeyCertificatePair(
                File.ReadAllText("cert/service.pem"),
                File.ReadAllText("cert/service-key.pem")
            );
            var creds = new SslServerCredentials(new[] { pair });
            
            var server = new Server
            {
                Services = {Svc.BindService(new MyService())},
                Ports = {new ServerPort("0.0.0.0", port, creds)}
            };
            server.Start();

            Console.WriteLine($"Server listening at port {port}. Press any key to terminate");

            Console.ReadKey();
        }
    }
}
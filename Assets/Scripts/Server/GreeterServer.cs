using Grpc.Core;
using Helloworld;
using System.Threading.Tasks;
using UnityEngine;

public class GreeterServer : MonoBehaviour
{
    class GreeterImpl : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }

    const int Port = 50051;

    void Start()
    {
        Server server = new Server
        {
            Services = { Greeter.BindService(new GreeterImpl()) },
            Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
        };

        server.Start();
    }
}

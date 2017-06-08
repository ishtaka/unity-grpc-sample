using Grpc.Core;
using Helloworld;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GreeterServer : MonoBehaviour
{
    [SerializeField]
    Text output;

    [SerializeField]
    Button button;

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
        button.OnClickAsObservable()
            .Subscribe(_ => StartServer())
            .AddTo(this);
    }

    void StartServer()
    {
        try
        {
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }
            };

            server.Start();
            output.text = "Start Server";
        }
        catch (System.Exception e)
        {
            output.text = e.Message;
        }
    }
}

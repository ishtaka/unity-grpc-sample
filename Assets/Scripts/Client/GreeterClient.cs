using Grpc.Core;
using Helloworld;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GreeterClient : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    InputField input;

    [SerializeField]
    Text output;

    void Start()
    {
        button.OnClickAsObservable()
            .Subscribe(_ => Send())
            .AddTo(this);
    }

    void Send()
    {
        Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);

        var client = new Greeter.GreeterClient(channel);
        String user = input.text;

        HelloReply reply = client.SayHello(new HelloRequest { Name = user });
        output.text = reply.Message;

        channel.ShutdownAsync();
    }
}

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

    [SerializeField]
    Button createChButton;

    [SerializeField]
    InputField serverIp;

    Channel channel;

    void Start()
    {
        button.OnClickAsObservable()
            .Subscribe(_ => Send())
            .AddTo(this);

        createChButton.OnClickAsObservable()
            .Subscribe(_ => CreateChannel())
            .AddTo(this);
    }

    void CreateChannel()
    {
        var ip = serverIp.text + ":50051";
        channel = new Channel(ip, ChannelCredentials.Insecure);
    }

    void Send()
    {
        if (channel == null)
            return;

        try
        {
            var client = new Greeter.GreeterClient(channel);
            String user = input.text;

            HelloReply reply = client.SayHello(new HelloRequest { Name = user });
            output.text = reply.Message;
        }
        catch (Exception e)
        {
            output.text = e.Message;
        }
    }
}

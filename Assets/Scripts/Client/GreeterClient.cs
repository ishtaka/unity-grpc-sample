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

    void Start()
    {
        button.OnClickAsObservable().Subscribe(_ => Send()).AddTo(this);
    }

    void Send()
    {
        Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);

        var client = new Greeter.GreeterClient(channel);
        String user = "you";

        HelloReply reply = client.SayHello(new HelloRequest { Name = user });
        Debug.Log("Greeting: " + reply.Message);

        channel.ShutdownAsync();
    }
}

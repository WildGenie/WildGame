using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

using Random = UnityEngine.Random;

public class SignalR : MonoBehaviour
{
    // Start is called before the first frame update
    private int x;
    private static HubConnection connection;

    public ChatController ChatController;


    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate {return true;};

        Debug.Log("Hello World!");
        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:44325/chatHub")
            .Build();

        connection.Closed += async (error) =>
        {
            await Task.Delay(Random.Range(0, 5) * 1000);
            await connection.StartAsync();
        };

        Connect();
    }

    private async void Connect()
    {
        connection.On<string, string>("ReceiveMessage", (name, message) =>
        {
            Debug.Log($"{name}: {message}");
            this.ChatController.ReciveChatMessage(name, message);
        });

        //try
        //{
            await connection.StartAsync();

        //    Debug.Log("Connection started");
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.Log(ex.Message);
        //}
    }

    public async void Send(string msg)
    {
        try
        {
            await connection.InvokeAsync("SendMessage", connection.GetHashCode().ToString(), msg);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void SetPosition(int x)
    {
        this.x = x;
    }

    private void Update()
    {
        transform.position = new Vector2(x, 0);
    }
}

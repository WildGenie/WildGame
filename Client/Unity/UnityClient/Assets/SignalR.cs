using Microsoft.AspNetCore.SignalR.Client;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SignalR : MonoBehaviour
{
    // Start is called before the first frame update
    private int x;
    private static HubConnection connection;

    public ChatController ChatController;

    static InvokePump invoke = new InvokePump();

    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


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
        connection.On("ReceiveMessage", (System.Action<string, string>)((name, message) =>
        {
            Debug.Log($"{name}: {message}");
            //this.ChatController.ReciveChatMessage(name, message);
            invoke.BeginInvoke(() => this.ChatController.ReciveChatMessage(name, message));
            //StartCoroutine(this.ChatController.ReciveChatMessageSafe(name, message));
        }));

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
        invoke.Update();
    }
}

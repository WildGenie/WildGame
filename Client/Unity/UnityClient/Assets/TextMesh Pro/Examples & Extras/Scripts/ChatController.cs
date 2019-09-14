using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChatController : MonoBehaviour {


    public TMP_InputField TMP_ChatInput;

    public TMP_Text TMP_ChatOutput;

    public Scrollbar ChatScrollbar;

    public SignalR SignalR;

    void OnEnable()
    {
        TMP_ChatInput.onSubmit.AddListener(AddToChatOutput);

    }

    void OnDisable()
    {
        TMP_ChatInput.onSubmit.RemoveListener(AddToChatOutput);

    }

    public void  ReciveChatMessage(string name, string message)
    {
        // Clear Input Field

        var timeNow = System.DateTime.Now;

        TMP_ChatOutput.text +=
            $"[<#FFFF80>{timeNow:T}</color>] [<#FF8080>{name}</color>] {message}\n";

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;

    }

    void AddToChatOutput(string newText)
    {
        // Clear Input Field
        TMP_ChatInput.text = string.Empty;

        var timeNow = System.DateTime.Now;

        //TMP_ChatOutput.text += "[<#FFFF80>" + timeNow.Hour.ToString("d2") + ":" + timeNow.Minute.ToString("d2") + ":" + timeNow.Second.ToString("d2") + "</color>] " + newText + "\n";

        this.SignalR.Send(newText);

        TMP_ChatInput.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;

    }

}

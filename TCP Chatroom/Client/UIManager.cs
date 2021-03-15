using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<Message> messageList = new List<Message>();

    public ClientManager clientManager;

    public GameObject connectedPanel;
    public GameObject unConnectedPanel;
    public GameObject chatPanel;
    public GameObject textObject;

    private string message;
    private string username;

    
    public Text userIDText;
    public Text usernameText;
    public InputField chatField;

    public int maxMessages = 100;

    private void Start()
    {
        clientManager.client.onRecievedUserID += RecievedUserID;
        clientManager.client.onRecievedMessage += RecievedMessage;
        message = "";
    }

    void RecievedMessage(string message)
    {
        Message newMessage = new Message();
        newMessage.text = message;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    public void SetMessage(string message)
    {
        this.message = username + ": " + message;
    }

    void RecievedUserID(string message)
    {
        userIDText.text = "User ID - " + username + " (" + message + ")";
    }


    public void SetUsername(string username)
    {
        this.username = username;
    }

    public void Send()
    {
        clientManager.client.SendToServer(10, message);
    }

    private void Update()
    {
        if(clientManager.client.IsConnected())
        {
            connectedPanel.gameObject.SetActive(true);
            unConnectedPanel.gameObject.SetActive(false);
        }

        else
        {
            connectedPanel.gameObject.SetActive(false);
            unConnectedPanel.gameObject.SetActive(true);
        }

        ClampMessages();

        ChatboxCheck();
    }

    public void ClampMessages()
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
    }

    public void ChatboxCheck()
    {
        if (chatField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Send();
                chatField.text = "";
            }
        }

        else
        {
            if (!chatField.isFocused && Input.GetKeyDown(KeyCode.Return))
                chatField.ActivateInputField();
        }
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}

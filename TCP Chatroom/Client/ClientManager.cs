using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    public Client client;
    public UIManager manager;
    public InputField port;
    public InputField username;
    public string ipAddress;

    private string ipAddressName;
    private int portNumber;

    public void ConnectToServer()
    {
        ipAddressName = ipAddress;
        portNumber = int.Parse(port.text);
        manager.SetUsername(username.text);
        client.Connect(ipAddressName, portNumber);
    }

    public void HandleIPDropdownData(int val) 
    {
        switch (val)
        {
            case 0: ipAddress = ""; break;
            case 1: ipAddress = ""; break;
            case 2: ipAddress = ""; break;
        }

        Debug.Log(ipAddress);
            
    }
}

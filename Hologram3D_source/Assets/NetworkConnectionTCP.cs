using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Linq;

public class NetworkConnectionTCP : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 8888;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 pos = Vector3.zero;
    public ChangeExpression expressionChanger;
    static int previousCount ;
    static List<float[]> paramzs;
    private int frameCountSkip = 0; 

    private void Start()
    {
        paramzs = new List<float[]>();
        previousCount = 0; 
        ThreadStart ts = new ThreadStart(TryToConnect);
        mThread = new Thread(ts);

        mThread.Start();
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    private void Update()
    {
        frameCountSkip += 1; 
        if (previousCount < paramzs.Count && frameCountSkip > 100)
        { 
            Debug.Log("Change expression at " + Time.time); 
            expressionChanger.SendMessage("changeExpression", paramzs[previousCount]);
            previousCount += 1;
            frameCountSkip = 0; 
        }
    }
    void TryToConnect()
    {
        Connect(connectionIP, "aaaa"); 
    }
    static void Connect(string server, string message)
    {
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.
            int port = 8888;
            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Debug.Log("Sent: "+ message);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new byte[13];

            // String to store the response ASCII representation.
            string responseData = string.Empty;

            // Read the first batch of the TcpServer response bytes.
            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Log("Received:  "+ responseData);
            int count = int.Parse(responseData); 
            for (var i =0; i < count; ++i)
            {
                data = new byte[13];

                // String to store the response ASCII representation.
                responseData = string.Empty;
            

                // Read the first batch of the TcpServer response bytes.
                if (responseData != "None")
                {
                    bytes = stream.Read(data, 0, data.Length);
                    if (responseData != "None")
                    {
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Debug.Log(i + ": " + "Received:  " + responseData);

                        float expressionID = int.Parse(responseData);
                        bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        if (responseData != "None")
                        {
                            Debug.Log("   " + "Received:  " + responseData);
                            float value = float.Parse(responseData);
                            float [] temp = new float[2];
                            temp[0] = expressionID;
                            temp[1] = value;
                            paramzs.Add(temp); 
                        }
                    }
                }
            }
            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (System.ArgumentNullException e)
        {
            Debug.LogError("ArgumentNullException: " +  e.ToString());
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: "+   e.ToString());
        }

    }

  

   
}

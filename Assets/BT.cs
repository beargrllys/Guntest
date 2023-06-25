using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class BT : MonoBehaviour
{
    public string deviceName = "HC-06";
    public int baudRate = 9600;
    private SerialPort serialPort;

    void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            Debug.Log("Found port: " + port);
            if (port.Contains("COM3") )
            {
                serialPort = new SerialPort(port, baudRate);
                serialPort.ReadTimeout = 1000;
                serialPort.Open();
                Debug.Log("Connected to Bluetooth device: " + port);
                break;
            }
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string data = serialPort.ReadLine();
            Debug.Log("Received data from Bluetooth device: " + data);
        }
    }

    public void SendData(string data)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Write(data);
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
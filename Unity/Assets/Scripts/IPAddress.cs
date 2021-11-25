using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using UnityEngine;

public class IPAddress : MonoBehaviour
{
	public string output = "0.0.0.0";
	public Text myText;

    // Start is called before the first frame update
    void Start()
    {
    	IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
    	IPAddress ipAddr = ipHost.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
    	output = ipAddr.MapToIPv4().ToString();
    	myText.text = output;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

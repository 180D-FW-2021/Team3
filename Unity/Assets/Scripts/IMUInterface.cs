using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class IMUInterface : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    static Socket listener;
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Renderer objectRenderer;
    private Color matColor;

    public static readonly int PORT = 8081;
    public static readonly int WAITTIME = 1;


    IMUInterface()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    async void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        await Task.Run(() => ListenEvents(source.Token));   
    }

    // Update is called once per frame
    void Update()
    {
        objectRenderer.material.color = matColor;
    }

    private void ListenEvents(CancellationToken token)
    {

        
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

         
        listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

         
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

             
            while (!token.IsCancellationRequested)
            {
                allDone.Reset();

                print("Waiting for a connection... host :" + ipAddress.MapToIPv4().ToString() + " port : " + PORT);
                listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

                while(!token.IsCancellationRequested)
                {
                    if (allDone.WaitOne(WAITTIME))
                    {
                        break;
                    }
                }
      
            }

        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }

    void AcceptCallback(IAsyncResult ar)
    {  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
 
        allDone.Set();
  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    void ReadCallback(IAsyncResult ar)
    {
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        int read = handler.EndReceive(ar);
  
        if (read > 0)
        {
            Debug.Log(Encoding.ASCII.GetString(state.buffer, 0, read));
            //state.colorCode.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
            //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }
        else
        {
            if (state.colorCode.Length > 1)
            { 
                //string content = state.colorCode.ToString();
                //Debug.Log(content);
                //print($"Read {content.Length} bytes from socket.\n Data : {content}");
                //SetColors(content);
            }
            handler.Close();
        }
    }

    //Set color to the Material
    private void SetColors (string data) 
    {
        string[] colors = data.Split(',');
        matColor = new Color()
        {
            r = float.Parse(colors[0]) / 255.0f,
            g = float.Parse(colors[1]) / 255.0f,
            b = float.Parse(colors[2]) / 255.0f,
            a = float.Parse(colors[3]) / 255.0f
        };

    }

    private void OnDestroy()
    {
        source.Cancel();
    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder colorCode = new StringBuilder();
    }
    */
    
    async void Start() {
        await Task.Run(() => ExecuteServer());   
        //ExecuteServer();
    }

    public static void ExecuteServer()
    {
        // Establish the local endpoint
        // for the socket. Dns.GetHostName
        // returns the name of the host
        // running the application.
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 8081);

        //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        //IPAddress ipAddr = ipHost.AddressList[0];
        //IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 8081);
    
        // Creation TCP/IP Socket using
        // Socket Class Constructor
        Socket listener = new Socket(ipAddr.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
    
        try {
            
            // Using Bind() method we associate a
            // network address to the Server Socket
            // All client that will connect to this
            // Server Socket must know this network
            // Address
            listener.Bind(localEndPoint);
    
            // Using Listen() method we create
            // the Client list that will want
            // to connect to Server
            listener.Listen(10);
    
            while (true) {
                
                Debug.Log("Waiting connection ... ");
    
                // Suspend while waiting for
                // incoming connection Using
                // Accept() method the server
                // will accept connection of client
                Socket clientSocket = listener.Accept();
    
                // Data buffer
                byte[] bytes = new Byte[1024];
                string data = null;
    
                while (true) {
    
                    int numByte = clientSocket.Receive(bytes);
                    Debug.Log(Encoding.ASCII.GetString(bytes, 0, numByte));
                    data += Encoding.ASCII.GetString(bytes,
                                            0, numByte);
                                                
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
    
                Debug.Log("Text received -> " + data);
    
                // Close client Socket using the
                // Close() method. After closing,
                // we can use the closed Socket
                // for a new Client Connection
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
        
        catch (Exception e) {
            Debug.Log(e.ToString());
        }
    }
}

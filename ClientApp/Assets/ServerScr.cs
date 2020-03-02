using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientApp.Network.ServerSide;
using System.Threading.Tasks;

public class ServerScr : MonoBehaviour
{
    
    Server server = new Server();
    Task execTask;
    void Awake()
    {
        execTask = new Task(Serve);
        execTask.Start();
    }

    void Serve()
    {
        server.StartServer(10, "127.0.0.1", 8888);
    }
}

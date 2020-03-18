using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
using System.Net.Sockets;
using System.Text;
using System.Net;
using UnityEngine;
using ClientApp.Network.ClientSide;

public class NewBehaviourScript : MonoBehaviour
{
    Client client = new Client("127.0.0.1", 8888, 8192);
    private void Start()
    {
        System.Threading.Tasks.Task.Run(() => client.ConnectToServer());
    }



    private void FixedUpdate()
    {
    }
}

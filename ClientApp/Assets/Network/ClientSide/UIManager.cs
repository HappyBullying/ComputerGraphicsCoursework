using UnityEngine;

using System;
using System.Collections.Concurrent;

namespace ClientApp.Network.ClientSide
{
    public class UIManager : MonoBehaviour
    {
        public static ConcurrentQueue<string> ToApply = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> ToSend = new ConcurrentQueue<string>();

        public GameObject startMenue;

    }
}
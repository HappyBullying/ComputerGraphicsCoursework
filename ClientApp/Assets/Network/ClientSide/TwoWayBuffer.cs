using System.Collections.Generic;

namespace ClientApp.Network.ClientSide
{
    public class TwoWayBuffer
    {
        private static Queue<Package> ToSend = new Queue<Package>();
        private static Queue<int> ToApply = new Queue<int>();

        public static void AddToSendSequence(Package pckg)
        {
            ToSend.Enqueue(pckg);
        }
        public static Package[] GetAllFromSendSequence()
        {
            int currentCount = ToSend.Count;
            Package[] packages = new Package[currentCount];
            for (int i = 0; i < currentCount; i++)
            {
                packages[i] = ToSend.Dequeue();
            }
            return packages;
        }
    }
}
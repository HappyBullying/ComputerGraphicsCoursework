namespace ClientApp.Network
{
    public class EventPackage
    {
        public int PlayerId { get; set; }
        public string[] Events;

        // public static EventPackage CreateFromBytes(byte[] bytesReceived, int indexFrom, int bytesCount)
        // {
        //     string allCommands = System.Text.Encoding.UTF8.GetString(bytesReceived, indexFrom, bytesCount);
        //     string[] commands = allCommands.Split(';');
            
        // }
    }
}
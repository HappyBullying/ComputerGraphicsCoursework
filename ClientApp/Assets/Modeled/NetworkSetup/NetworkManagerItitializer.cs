using UnityEngine;
using MenuAssets;
using Mirror;

public class NetworkManagerItitializer : MonoBehaviour
{
    void Start()
    {
        TelepathyTransport transportLayer = GetComponent<TelepathyTransport>();
        NetworkManager mg = GetComponent<NetworkManager>();
        mg.maxConnections = SceneDataTransfer.MaxPlayers;
        mg.networkAddress = SceneDataTransfer.IPAddress;
        transportLayer.port = SceneDataTransfer.Port;
        
        switch(SceneDataTransfer.GameType)
        {
            case "Host":
            {
                mg.StartHost();
                break;
            }
            case "Client":
            {
                mg.StartClient();
                break;
            }
            default:
            {
                mg.StartHost();
                break;
            }
        }
    }
}

using UnityEngine;
using MenuAssets;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI IPText;
    public TextMeshProUGUI PortText;
    public TextMeshProUGUI MaxPlayersText;
    public void CreateServer()
    {
        string tmpIp = "";
        foreach(char c in IPText.text)
        {
            if ((c >= '0' && c <= '9') || c == '.')
                tmpIp += c;
        }
        string tmpPort = "";
        foreach(char c in PortText.text)
        {
            if (c >= '0' && c <= '9')
                tmpPort += c;
        }
        string tmpMaxP = "";
        foreach(char c in PortText.text)
        {
            if (c >= '0' && c <= '9')
                tmpMaxP += c;
        }
        SceneDataTransfer.IPAddress = tmpIp;
        SceneDataTransfer.Port = (ushort)(System.Convert.ToInt32(tmpPort));
        SceneDataTransfer.MaxPlayers = System.Convert.ToInt32(tmpMaxP);
        SceneDataTransfer.GameType = "Host";
        SceneManager.LoadScene("MainScene");
    }

    public void ConnectToServer()
    {
        string tmpIp = "";
        foreach(char c in IPText.text)
        {
            if ((c >= '0' && c <= '9') || c == '.')
                tmpIp += c;
        }
        string tmpPort = "";
        foreach(char c in PortText.text)
        {
            if (c >= '0' && c <= '9')
                tmpPort += c;
        }
        string tmpMaxP = "";
        foreach(char c in PortText.text)
        {
            if (c >= '0' && c <= '9')
                tmpMaxP += c;
        }
        SceneDataTransfer.IPAddress = tmpIp;
        SceneDataTransfer.Port = (ushort)(System.Convert.ToInt32(tmpPort));
        SceneDataTransfer.MaxPlayers = System.Convert.ToInt32(tmpMaxP);
        SceneDataTransfer.GameType = "Client";
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

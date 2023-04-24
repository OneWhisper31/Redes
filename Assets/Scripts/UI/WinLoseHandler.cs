using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    public bool onReplay { get; private set; }

    public void OnReplay()
    {
        onReplay = true;

        //si los dos tienen true reinicia contadores
    }

    public void OnQuit()
    {
        NetworkPlayer.Local.OnDisconnected();
    }
}

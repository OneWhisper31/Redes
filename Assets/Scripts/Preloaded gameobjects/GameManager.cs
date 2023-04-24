using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class GameManager : NetworkBehaviour
{
    public static GameManager GM { get; private set; }

    [SerializeField] TextMeshProUGUI _textRestarting;
/*
    [Networked]
    public string player1ID { get; set; }
    [Networked]
    public int player1Points { get; set; }

    [Networked]
    public string player2ID { get; set; }
    [Networked]
    public int player2Points { get; set; }*/

    public Transform StateAuthorityInitialPos;
    public Transform PlayerInitialPos;

    //float pointsToWin=3;

    //[Networked]
    //float _timeAcum2 { get; set; }


    private void Start()
    {
        GM = GetComponent<GameManager>();
    }

    public override void Spawned()
    {
        Debug.Log("Authority: " + Object.HasStateAuthority + " - Proxy: " + Object.IsProxy);
    }

    /*public void AddNegativePoint(NetworkId playerID)
    {
        if(playerID.ToNamePrefixString()== player1ID)
        {
            player1Points++;
        }
        else if (playerID.ToNamePrefixString() == player2ID)
        {
            player2Points++;
        }
        Debug.Log(player1ID+": "+player1Points);
        Debug.Log(player2ID + ": " + player2Points);
        //for (int i = 0; i < PlayerNegativePoints.Length; i++)
        //{
        //    if (PlayerNegativePoints[i].ID == playerID)
        //        PlayerNegativePoints[i].AddPoint();
        //    return;
        //}



        //if (PlayerNegativePoints[playerID] >= pointsToWin) { /*win}
        //
        //foreach (var item in PlayerNegativePoints)
        //{
        //    Debug.Log(item.Key + ": " + item.Value);
        //}
        //RPC_UpdateList(PlayerNegativePoints);
    }
    //[Rpc(RpcSources.All, RpcTargets.All)]
    //public void RPC_UpdateList(Dictionary<int, int> _new)
    //{
    //    PlayerNegativePoints = _new;
    //
    //    foreach (var item in PlayerNegativePoints)
    //    {
    //        Debug.Log(item.Key + ": " + item.Value);
    //    }
    //}
    */
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_OnResetLevel() => StartCoroutine(ResetLevel());

    IEnumerator ResetLevel()
    {
        _textRestarting.gameObject.SetActive(true);
        NetworkPlayer.Local.player.ResetLife();
        NetworkPlayer.Local.transform.position = !Object.IsProxy ? 
            StateAuthorityInitialPos.position: PlayerInitialPos.position;

        yield return new WaitForSecondsRealtime(2f);
        _textRestarting.gameObject.SetActive(false);
    }
}

/*
public struct PlayerPoints
{
    public NetworkId ID;
    public int Points { get; private set; }

    public void AddPoint() { Points++; }
}*/

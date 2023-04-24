using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager GM { get; private set; }

    [SerializeField] Text _textRestarting;

    public Transform StateAuthorityInitialPos;
    public Transform PlayerInitialPos;
    //[Networked(OnChanged = nameof(OnTimeChanged))]
    //float _timeAcum { get; set; }

    [Networked]
    float _timeAcum2 { get; set; }


    private void Start()
    {
        GM = GetComponent<GameManager>();
    }

    public override void Spawned()
    {
        Debug.Log("Authority: " + Object.HasStateAuthority + " - Proxy: " + Object.IsProxy);
    }

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

    public override void FixedUpdateNetwork()
    {
        //if (!Object.HasStateAuthority) return;

        //_timeAcum += Time.deltaTime;

        //if (Object.HasStateAuthority)
        //{
        //    _timeAcum2 += Time.deltaTime;
        //}
        //
        //_textTimer.text = "Time: " + _timeAcum2;
    }

    //void Update()
    //{
    //    if (Object && Object.HasStateAuthority)
    //    {
    //        _timeAcum += Time.deltaTime;
    //    }
    //}

    //static void OnTimeChanged(Changed<GameManager> changed)
    //{
    //    changed.Behaviour._textTimer.text = "Time: " + changed.Behaviour._timeAcum;
    //}
}

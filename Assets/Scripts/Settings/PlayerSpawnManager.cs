using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager instance;

    public GameObject[] spawnPoints;
    public Color[] spawnColors;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnPlayerJoined()
    {
        Debug.Log("PlayerInput Joined");
    }

    public void PlayerId(PlayerInput input)
    {
        Debug.Log("Player ID " + input.playerIndex);

        if(input.playerIndex == 0)
        {
            input.gameObject.transform.position = spawnPoints[input.playerIndex].transform.position;
            UIManager.instance.Player1Joined();
            input.gameObject.GetComponent<PlayerController>().playerSR.color = spawnColors[input.playerIndex];
        }
        else
        {
            input.gameObject.transform.position = spawnPoints[input.playerIndex].transform.position;
            UIManager.instance.Player2Joined();
            input.gameObject.GetComponent<PlayerController>().playerSR.color = spawnColors[input.playerIndex];
        }

    }
}

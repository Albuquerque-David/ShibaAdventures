using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject playerPrefab;
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.C)))
        {
            SceneManager.LoadScene(1);
            GameObject player = Instantiate(playerPrefab);
            player.transform.position = new Vector2(-43f, -0.1f);
            print(player);
        }
    }
}

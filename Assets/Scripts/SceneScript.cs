using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    Player player;
    [SerializeField]
    int scene;
    [SerializeField]
    bool impulseDirection;
    [SerializeField]
    bool isBossScene = false;
    [SerializeField]
    bool isCheemsScene = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isBossScene)
        {
            player = FindObjectOfType<Player>();
            if (!player.getBossKey())
            {
                print("Need key!");
                return;
            }
        }

        if (isCheemsScene)
        {
            player = FindObjectOfType<Player>();
            if (!player.getCheemsKey())
            {
                print("NeedKey!");
                return;
            }
        }

        if (collision.CompareTag("Player"))
            loadScene();
    }

    public void loadScene()
    {
        SceneManager.LoadScene(scene);

        player = FindObjectOfType<Player>();

        if(impulseDirection)
            player.transform.position += Vector3.up * 50 * Time.deltaTime;

        Camera camera = player.GetComponentInChildren<Camera>();
        AudioListener audioListener = player.GetComponentInChildren<AudioListener>();

        if (scene == 3)
        {
            camera.enabled = false;
            audioListener.enabled = false;
        }   
        else
        {
            camera.enabled = true;
            audioListener.enabled = true;
        }
    }

    public void loadScene(int scene)
    {
        this.scene = scene;
        loadScene();
    }
}

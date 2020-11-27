using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player Instance;
    private bool hasBossKey = false;
    private bool hasCheemsKey = false;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BossKey"))
        {
            hasBossKey = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("KingdomKey"))
        {
            hasCheemsKey = true;
            Destroy(collision.gameObject);
        }
    }

    public void setCheemsKey(bool hasCheemsKey)
    {
        this.hasCheemsKey = hasCheemsKey;
    }

    public bool getBossKey()
    {
        return hasBossKey;
    }

    public bool getCheemsKey()
    {
        return hasBossKey;
    }
}

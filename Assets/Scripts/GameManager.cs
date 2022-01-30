using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region  Singleton _instance

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    #endregion

    [Header("References")]
    [SerializeField] public GameObject p1;
    [SerializeField] public GameObject p2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(p1.transform.position.x - p2.transform.position.x) < 0.5f)
        {
            GameOver();
        }
        
    }

    public void GameOver()
    {
        p1.SetActive(false);
        p2.SetActive(false);
    }


}

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

    private Vector3 p1InitialPos;
    private Vector3 p2InitialPos;


    // Start is called before the first frame update
    void Start()
    {
        p1InitialPos = p1.transform.position;
        p2InitialPos = p2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameOverCheck(false);
    }


    public void GameOverCheck(bool check)
    {
        if (check || (Mathf.Abs(p1.transform.position.x - p2.transform.position.x) < 0.5f))
        {
            StartCoroutine(GameOverSequence());
        }
    }


    public IEnumerator GameOverSequence()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        p1.transform.position = p1InitialPos;
        p2.transform.position = p2InitialPos;
        p1.SetActive(true);
        p2.SetActive(true);
    }


}

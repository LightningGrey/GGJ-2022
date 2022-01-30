using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject linkedPortal;

    private bool enableWarp = true;
    [SerializeField] private GameObject endPlayer = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")){

            if (linkedPortal != null && enableWarp)
            {
                col.gameObject.transform.position = linkedPortal.transform.position;
                col.gameObject.GetComponent<PlayerControls>().moveVec.y = 0.0f;
                linkedPortal.GetComponent<Portal>().enableWarp = false;
            } else if (col.gameObject == endPlayer)
            {
                col.gameObject.SetActive(false);
                GameManager.Instance.WinCheck();
            }

        } 
    }

    void OnTriggerExit2D(Collider2D col)
    {
        enableWarp = true;
    }

}

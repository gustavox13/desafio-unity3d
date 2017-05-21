using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BoxScript : MonoBehaviour {



    public string objectTag;

    //COLIDIR COM ESPINHOS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == objectTag)
        {
            StartCoroutine("Respawn");
        }

    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.8f);


        SceneManager.LoadScene("Game");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
    
    // references
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject graymanPrefab;
    public GameObject simpleMistPrefab;
    public GameObject mapCanvas;

    // constants
    public int numberOfEnemies = 48;

    // other
    bool mapOnScreen = false;
    float mistTimer;

    void Start() {
        for (int i = 0; i < numberOfEnemies; i++) {
            GameObject inst = Instantiate(enemyPrefab); // right now it figures out a random position on its own
            inst.GetComponent<Enemy>().player = player;
        }

        if (SceneManager.GetActiveScene().name == "MistTest") {
            /*for (int i = 0; i < 3; i++) {
                GameObject inst = Instantiate(graymanPrefab); // right now it figures out a random position on its own
                inst.GetComponent<Grayman>().player = player;
            }
            
            for (int i = 0; i < 40; i++) {
                GameObject inst = Instantiate(simpleMistPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-20f, 20f),
                                                          player.transform.position.y + Random.Range(-20f, 20f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity);
                //GameObject inst = Instantiate(simpleMistPrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 100), Quaternion.identity);
                inst.GetComponent<Mist>().player = player;                
                inst.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.36f);
            }*/
            mistTimer = 0.02f;
        }
    }

    void Update() {
        if (Input.GetKeyDown("m")) {
            mapOnScreen = !mapOnScreen;
            mapCanvas.SetActive(mapOnScreen);
        }
        
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void FixedUpdate() {

         if (SceneManager.GetActiveScene().name == "MistTest") {

            if (Random.Range(0, 3) == 0) {
                int n = Random.Range(1, 3);
                for (int i = 0; i < n; i++) {
                    GameObject inst = Instantiate(simpleMistPrefab,
                                                  new Vector3(player.transform.position.x + Random.Range(-36f, 36f),
                                                              player.transform.position.y + Random.Range(-36f, 36f),
                                                              player.transform.position.z + 100),
                                                  Quaternion.identity);
                    inst.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);                    
                }
            }
 
            if (Random.Range(0, 100) >= 86) {
                GameObject inst = Instantiate(graymanPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-32f, 32f),
                                                          player.transform.position.y + Random.Range(-32f, 32f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity); // right now it figures out a random position on its own
                inst.GetComponent<Grayman>().player = player;
            }
            /*mistTimer -= Time.deltaTime;
            if (mistTimer <= 0) {

                GameObject inst = Instantiate(simpleMistPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-36f, 36f),
                                                          player.transform.position.y + Random.Range(-36f, 36f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity);
                inst.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);

                mistTimer = Random.Range(0.02f, 0.06f);

            }*/
        }
    }
}

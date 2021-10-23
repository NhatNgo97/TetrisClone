using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] list;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
     void Update()
    {
        
    }


    public void spawn(){
        Instantiate(list[Random.Range(0, list.Length)], transform.position, transform.rotation);
    }   
}

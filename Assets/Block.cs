using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    static int width = 10; 
    static int length = 20;
    private float previousTime;
    private float fallTime = 1f;
    [SerializeField]
    private Vector3 root;

    private static Transform[,] grid = new Transform[width, length];
    // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            rotate(true);
            if(!isVadlidMove()){
                rotate(false);
            }
        }
        if( Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position += new Vector3(-1, 0 ,0);
            if (!isVadlidMove()){
                transform.position += new Vector3(1, 0 ,0);
            }
        }
        if( Input.GetKeyDown(KeyCode.RightArrow) && isVadlidMove()){
            transform.position += new Vector3(1, 0 ,0);
            if (!isVadlidMove()){
                transform.position += new Vector3(-1, 0 ,0);
            }
        }
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow)? fallTime/10: fallTime)){
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!isVadlidMove()){
                transform.position += new Vector3(0,1,0);
                Addtogrid();
                Checkforlines();

                this.enabled = false;
                FindObjectOfType<Spawner>().spawn();
            }
        }
    }

    void Checkforlines(){
        for(int i = length - 1; i>= 0; i--){
            if(Hasline(i)){
                Deleteline(i);
                Rowdown(i);
            }
        }
    }

    bool Hasline(int i ){
        for( int j = 0; j < width; j++){
            if(grid[j,i]  ==null)
                return false;
        }
        return true;
    }

    void Deleteline(int i){
        for (int j = 0 ; j< width; j ++){
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;

        }
    }

    void Rowdown(int i){
        for(int y = i; y <length; y++){
            for (int j =  0; j < width; j++){
                if (grid[j,y] != null){
                    grid[j, y -1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j,y-1].transform.position -= new Vector3(0,1,0);
                    Debug.Log("i" + i + " j" + j + " y" +y);
                } 
            }
        }
    }
    bool isVadlidMove(){
        foreach (Transform child in transform){
            if (child.transform.position.x < 0|| child.transform.position.x  > width || 
            child.transform.position.y > length || child.transform.position.y < 0){
                return false;
            }
            int positionX = Mathf.FloorToInt(child.transform.position.x);
            int positionY = Mathf.FloorToInt(child.transform.position.y);
            if( grid[positionX, positionY] != null){
                return false;
            }
        }
        return true;
    }


    void rotate(bool forward){
        transform.RotateAround(transform.TransformPoint(root), new Vector3(0,0,1), -90 * (forward ? 1: -1));
    }

    void Addtogrid(){
        foreach( Transform child in transform){
            int positionX = Mathf.FloorToInt(child.transform.position.x);
            int positionY = Mathf.FloorToInt(child.transform.position.y);

            grid[positionX, positionY] = child;
        }
    }

}

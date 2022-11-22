using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw_Extended : Trap
{
   private Animator anim;
  
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;


    private bool goingForward = true;
    private int movePointIndex;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
         anim.SetBool("isWorking", true);
         Flip();
         transform.position = movePoint[0].position;
    }

    // Update is called once per frame
    void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movePoint[movePointIndex].position) < 0.15f)
       {    

        if(movePointIndex == 0)
          {goingForward = true;
            Flip();
          }
        if(goingForward)
              movePointIndex ++;
        else
            movePointIndex --;  
        

        if(movePointIndex >= movePoint.Length)
              {
               movePointIndex = movePoint.Length -1; 
               goingForward = false; 
               Flip();
              }
        
        }

    }
     private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}

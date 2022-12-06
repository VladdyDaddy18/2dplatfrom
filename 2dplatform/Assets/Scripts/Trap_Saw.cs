using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : Trap
{

    private Animator anim;
    private bool isWorking;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    [SerializeField] private float coolDown;
    

    private float coolDownTimer;

    private int movePointIndex;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = movePoint[0].position;

    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Time.deltaTime;

        bool isWorking = coolDownTimer < 0;

        anim.SetBool("isWorking", isWorking);

        if(isWorking)
        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movePoint[movePointIndex].position) < 0.15f)
       {    
              Flip();
              coolDownTimer = coolDown;
              movePointIndex ++;

        if(movePointIndex >= movePoint.Length)
               movePointIndex = 0; 
        
        
        }

    }
    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
   
}

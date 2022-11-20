using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : Trap
{

    private Animator anim;
    [SerializeField] private bool isWorking;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWorking", isWorking);

        transform.position = Vector3.MoveTowards(transform.position, movePoint[0].position, speed * Time.deltaTime);

    }
}

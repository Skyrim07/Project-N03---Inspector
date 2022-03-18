using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;

public sealed class Character : MonoSingleton<Character>
{
    [Header("Movement")]
    public float speed = 1f;


    [SerializeField] Animator visualAnim;
    [SerializeField] AudioSource walkAudio;

    private float h, v;
    private Vector3 oScale;
    private void Start()
    {
        oScale = transform.localScale;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (ConvManager.instance.inConv)
            return;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            h = -1;
            transform.localScale = new Vector3(-oScale.x, oScale.y, oScale.z);
            visualAnim.Play("ChWalk");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            h = 1;
            transform.localScale = new Vector3(oScale.x, oScale.y, oScale.z);
            visualAnim.Play("ChWalk");
        }
        else
        {
            h = 0;
            visualAnim.Play("ChIdle");
        }

        transform.Translate(speed * Time.deltaTime * new Vector3(h, 0, 0));
        walkAudio.volume = Mathf.Abs(h);
    }
}

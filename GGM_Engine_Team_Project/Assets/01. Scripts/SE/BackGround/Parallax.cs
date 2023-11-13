using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private Vector2 startPos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;      // 이 스프라이트의 x 크기를 가져옴.
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));         // 현 카메라 포지션에서 1 - parallaxEffect 를 곲해서 현재 위치를 가져와줌. 그래서 뒤에서 startPos + Length 해주는거.
        //Debug.Log(temp);
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * 0.9f);

        transform.position = new Vector3(startPos.x + dist, startPos.y + distY, transform.position.z);
        //transform.position = new Vector3(startPos +dist,  transform.position.y, transform.position.z);

        // startPos -+ lenght = 카메라 밖. 오른쪽, 왼쪽구별해서.
        if (temp > startPos.x + length) startPos.x += length;
        else if (temp < startPos.x - length) startPos.x -= length;
    }
}

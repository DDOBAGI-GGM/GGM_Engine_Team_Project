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
        length = GetComponent<SpriteRenderer>().bounds.size.x;      // �� ��������Ʈ�� x ũ�⸦ ������.
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));         // �� ī�޶� �����ǿ��� 1 - parallaxEffect �� ���ؼ� ���� ��ġ�� ��������. �׷��� �ڿ��� startPos + Length ���ִ°�.
        //Debug.Log(temp);
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * 0.9f);

        transform.position = new Vector3(startPos.x + dist, startPos.y + distY, transform.position.z);
        //transform.position = new Vector3(startPos +dist,  transform.position.y, transform.position.z);

        // startPos -+ lenght = ī�޶� ��. ������, ���ʱ����ؼ�.
        if (temp > startPos.x + length) startPos.x += length;
        else if (temp < startPos.x - length) startPos.x -= length;
    }
}

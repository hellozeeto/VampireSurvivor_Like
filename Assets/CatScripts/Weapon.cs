using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float rate; // ���ݼӵ�
    public TrailRenderer trailEffect;

    public Transform bulletPos; // �������� ������ ��ġ 
    public GameObject bullet; // �������� ������ ����

    
    private float nextShotTime = 0f;




    void Update()
    {
        // ���콺 ���� ��ư�� ������ ��, ���� �ӵ��� ���� �Ѿ� �߻�
        //if (Input.GetMouseButtonDown(0) && Time.time >= nextShotTime)
        //{
        //    nextShotTime = Time.time + rate; // ���� �ӵ��� ���缭 �߻� �ð� ����
        //    Use(); // �Ѿ� �߻� �� �ִϸ��̼� Ʈ����
        //}
    }

    public void Use()
    {
        // �Ѿ� �߻�
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        // �Ѿ� �������� �����ϰ� �߻�
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        // Trail Effect �߰�
        if (trailEffect != null)
        {
            TrailRenderer trail = Instantiate(trailEffect, bulletPos.position, Quaternion.identity);
            trail.transform.parent = instantBullet.transform;
        }

        yield return null;
    }
}

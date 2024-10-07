using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float rate; // 공격속도
    public TrailRenderer trailEffect;

    public Transform bulletPos; // 프리펩을 생성할 위치 
    public GameObject bullet; // 프리펩을 저장할 변수

    
    private float nextShotTime = 0f;




    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸을 때, 공격 속도에 맞춰 총알 발사
        //if (Input.GetMouseButtonDown(0) && Time.time >= nextShotTime)
        //{
        //    nextShotTime = Time.time + rate; // 공격 속도에 맞춰서 발사 시간 설정
        //    Use(); // 총알 발사 및 애니메이션 트리거
        //}
    }

    public void Use()
    {
        // 총알 발사
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        // 총알 프리팹을 생성하고 발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        // Trail Effect 추가
        if (trailEffect != null)
        {
            TrailRenderer trail = Instantiate(trailEffect, bulletPos.position, Quaternion.identity);
            trail.transform.parent = instantBullet.transform;
        }

        yield return null;
    }
}

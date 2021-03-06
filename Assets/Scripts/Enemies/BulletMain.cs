﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : HostileParent {

    [Header("References")]
    #region References
    [SerializeField]
    private Transform BulletTransfom;
    #endregion

    [Header("Properties")]
    #region Properties
    [SerializeField]
    public float Speed = 20.0f;
    [SerializeField]
    private float LifeSpan = 5.0f;
    #endregion

    private float ExpireTime;
    public Vector3 Velocity;

    void Start()
    {
        ExpireTime = Time.time + LifeSpan;
        StartCoroutine(CheckForExpiry());
    }

    void Update()
    {
        if (!canMove())
            return;
        MoveBullet();
    }

    void MoveBullet()
    {
        BulletTransfom.position = BulletTransfom.position + Velocity * Time.deltaTime * Speed;
    }

    IEnumerator CheckForExpiry()
    {
        while (true)
        {
            if (Time.time > ExpireTime)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}

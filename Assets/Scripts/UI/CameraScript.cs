﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour {

    public Vector2 offset;
    private Vector3 velocity = Vector3.zero;
    public float dampTime = .6f;
    [SerializeField]
    Transform player;

    Transform player2;

    Camera cam;

    bool multiplayer;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        multiplayer = GameController.multiplayer;
        if(multiplayer)
            player2 = player = GameObject.FindGameObjectWithTag("Player2").transform;
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (!GameController.hostileCanMove)
            return;
        Vector2 player_sp = cam.WorldToScreenPoint(player.position);
        if (multiplayer)
            player_sp = (cam.WorldToScreenPoint(player.position) + cam.WorldToScreenPoint(player2.position)) / 2;

        Vector2 cam_sp = cam.WorldToScreenPoint(transform.position);
        Vector2 move = new Vector2();
        if (Mathf.Abs(cam_sp.x - player_sp.x) > offset.x)
            move.x = (player_sp - cam_sp).x;
        if (Mathf.Abs(cam_sp.y - player_sp.y) > offset.y)
            move.y = (player_sp - cam_sp).y;


        transform.position = Vector3.SmoothDamp(transform.position, cam.ScreenToWorldPoint((Vector3)(move + cam_sp)), ref velocity, dampTime / (move.magnitude / 100 ));

    }
}

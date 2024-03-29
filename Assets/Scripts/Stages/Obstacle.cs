﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int type = 0;//     0 = red;      1 = blue;      2 = green;      3 = violet;     4 = orange;     5 = yellow

    enum EnemyType { BasicRed, BasicBlue, BasicGreen, BasicViolet, BasicOrange, BasicYellow };

    [SerializeField]
    EnemyType enemyType = 0;

    bool dead = false;

    public bool isFirst = false;

    public int life = 1;

    [SerializeField] Player player;

    
    
    [SerializeField] Animator circle;

    public GameObject destroy;

    ShakeCamera shakeCamera;

    Collider[] hitColliders;

    Instanciator inst;

    void Start()
    {
        inst = FindObjectOfType<Instanciator>();

        shakeCamera = FindObjectOfType<ShakeCamera>();

        player = FindObjectOfType<Player>();

    }


    void Update()
    {
        CheckCollision();



    }

    private void CheckCollision()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        Collider firstBulletCol;

        Collider bestBullet = null;

        foreach (Collider col in hitColliders)
        {

            if (col.name == "Wall")
            {

            }

            else if (col.tag == "RedBullet" || col.tag == "BlueBullet" || col.tag == "GreenBullet"
                || col.tag == "OrangeBullet" || col.tag == "VioletBullet" || col.tag == "YellowBullet")
            {

                if (bestBullet == null)
                {
                    bestBullet = col;
                }
                if (col.GetComponent<Bullet>().index < bestBullet.GetComponent<Bullet>().index)
                {
                    bestBullet = col;
                }

            }

        }
        if (bestBullet != null && dead == false)
        {
            if (bestBullet.tag == "RedBullet")   // si la bala es roja y el tipo de enemigo tambien destruye al enemigo y a la bala. Si la bala es roja pero el enemigo no, destruye la bala
            {
                if (type == 0)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 1) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }

            }

            else if (bestBullet.tag == "BlueBullet")
            {
                if (type == 1)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 2) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }
            }
            else if (bestBullet.tag == "GreenBullet")
            {
                if (type == 2)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 0) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }


            }
            else if (bestBullet.tag == "VioletBullet")
            {
                if (type == 3)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 4) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }


            }
            else if (bestBullet.tag == "OrangeBullet")
            {
                if (type == 4)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 5) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }


            }
            else if (bestBullet.tag == "YellowBullet")
            {
                if (type == 5)
                {
                    if (life > 1) { transform.localScale -= new Vector3(.1f, .1f, 0); }
                    life -= bestBullet.gameObject.GetComponent<Bullet>().Damage;
                    Destroy(bestBullet.gameObject);
                    //life--;
                }
                else if (type == 3) { life++; Destroy(bestBullet.gameObject); transform.localScale += new Vector3(.1f, .1f, 0); }
                else { Destroy(bestBullet.gameObject); }


            }
        }

        if (life <= 0 && dead != true)
        {
            dead = true; player.Experience = 10f;
            player.combo++; player.comboTimer = Time.time + player.comboDuration * Time.deltaTime;
        }



        if (dead == true)
        {


            
            
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
            StartCoroutine(Destroy());
        }

    }

    public void IsFirst()
    {
        if (circle != null) { circle.SetBool("isFirst", true); }
        isFirst = true;
    }

    IEnumerator Destroy()
    {

        if (player != null)
        {

            gameObject.tag = "Untagged";

            player.GetEnemyList();
            //player.GetClosestEnemy(player.enemiesList);
        }
        yield return new WaitForSeconds(0.6f);
        GameManager.score += 10;
        Destroy(gameObject);
        inst.enemiesAlive--;
    }
}

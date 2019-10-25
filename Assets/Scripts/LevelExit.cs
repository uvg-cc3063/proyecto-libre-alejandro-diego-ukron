using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    //private Vector3 offset;
    public bool animationStarted = false;
    public GameObject effect;
    public float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //offset = transform.position - target.position;
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        //if (Mathf.Abs(offset.x) < 7.5 && animationStarted == false)
        if (distanceToPlayer < 7.5 && animationStarted == false)
        {
            anim.SetTrigger("Hit");
            effect.gameObject.SetActive(true);
            animationStarted = true;
            AudioManager.instance.PlaySfx(18);
        }
        //Debug.Log(offset.x + " / " + offset.y + " / " + offset.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(GameManager.instance.LevelEndCo());
        }
    }
}

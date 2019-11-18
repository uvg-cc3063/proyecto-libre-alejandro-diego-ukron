using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public GameObject victoryZone,shoulder,sword,canion,coat,sunLight;
    
    public float waitToShowExit;
    public Animator anim;

    public Transform firePoint;
    public Rigidbody bomb;
    public float shotSpeed = 10f;

    public ParticleSystem muzzleFlash;

    public enum BossPhase {
        intro,
        phase1,
        changeToSword,
        phase2,
        goingPoint,
        waitForHit,
        isBeingHitted,
        death
    }

    public bool phase1, phase2;
    public int bossMusic, bossDeathShout, bossHit, Unexpected, YouGonnaDie,ChangeSword,laguhingWait,laserSword,bossDeathPhrase;
    public BossPhase currentPhase = BossPhase.intro;
    

    public int BossHealth = 6;
    public int BossMaxHealth = 6;

    public float Timer;
    public float elapsedTime;

    public GameObject[] spotlights;
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public NavMeshAgent agent;

    public bool start = false;

    private Rigidbody rb;

    public bool isDeath = false;
    private bool Hitted = false;
    private bool waitOnce = false;
    private bool dashSound = false;

    private bool addPoint = false;

    public Transform Playertarget;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController_s.instance.transform.position);
        if (GameManager.instance.isRespawning)
        {

            DeActivateSpotLights();
            currentPhase = BossPhase.intro;

            start = false;
            anim.SetBool("StandUp", false);
            anim.SetTrigger("StartOver");

            BossCamera.instance.IsdeathFalse();
            anim.SetBool("isDeath", false);
            anim.SetBool("Wait&Laughing", false);
            anim.SetBool("bombAttack", false);
            anim.SetBool("dashAttack", false);
            anim.SetBool("changeToSword", false);
            anim.SetBool("Jump", false);

            agent.SetDestination(patrolPoints[0].position);

            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
            //rb.transform.position = new Vector3(0, 0, 10);
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            transform.LookAt(PlayerController_s.instance.transform, Vector3.up);

            //gameObject.SetActive(false);
            shoulder.gameObject.SetActive(false);
            sword.gameObject.SetActive(false);
            canion.gameObject.SetActive(false);
            coat.gameObject.SetActive(true);
            BossHealth = BossMaxHealth;

            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(false);
            sunLight.gameObject.SetActive(false);

            GameManager.instance.isRespawning = false;
        }

        switch (currentPhase)
        {
            case BossPhase.intro:
                if (start == true)
                {
                    PlayerController_s.instance.stopMove = true;
                    transform.LookAt(PlayerController_s.instance.transform, Vector3.up);
                    ActivateCurrentSpotLight(0);
                    anim.SetBool("StandUp", true);
                    Timer = 15;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {
                        currentPhase = BossPhase.phase1;
                        AudioManager.instance.PlayMusic(bossMusic);
                        sunLight.gameObject.SetActive(true);
                        elapsedTime = 0;
                        shoulder.gameObject.SetActive(true);
                        canion.gameObject.SetActive(true);
                        anim.SetBool("StandUp", false);
                        BossActivator.instance.ReturnToMainCamera();
                    }
                }                
                break;
            case BossPhase.phase1:
                PlayerController_s.instance.stopMove = false;
                
                phase1 = true;
                phase2 = false;
                anim.SetBool("bombAttack",true);
                transform.LookAt(PlayerController_s.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                Timer = 15;
                elapsedTime += Time.deltaTime;
                               
                if (elapsedTime >= Timer)
                {
                    currentPhase = BossPhase.waitForHit;
                    anim.SetBool("bombAttack", false);
                    elapsedTime = 0;
                }
                break;
            case BossPhase.waitForHit:
                rb.velocity = Vector3.zero;
                anim.SetBool("Wait&Laughing", true);
                if(waitOnce == false)
                {
                    anim.SetTrigger("Wait");
                    waitOnce = true;
                }
                transform.LookAt(PlayerController_s.instance.transform, Vector3.up);
                if (phase1 == true)
                {
                    Timer = 3;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {
                        currentPhase = BossPhase.phase1;
                        anim.SetBool("Wait&Laughing", false);
                        elapsedTime = 0;
                        waitOnce = false;
                    }
                }
                else if (phase2 == true)
                {
                    rb.isKinematic = true;
                    addPoint = false;
                    Timer = 4;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {
                        anim.SetBool("Wait&Laughing", false);
                        elapsedTime = 0;
                        currentPhase = BossPhase.phase2;
                        waitOnce = false;
                    }
                }               
                break;
            case BossPhase.changeToSword:
                BossActivator.instance.ChangeToAnimatedCamera();
                phase1 = false;
                anim.SetBool("changeToSword", true);
                Timer = 9;
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= 7)
                {
                    sword.gameObject.SetActive(true);
                    coat.gameObject.SetActive(false);
                    canion.gameObject.SetActive(false);
                    BossActivator.instance.ReturnToMainCamera();
                }
                if (elapsedTime >= Timer)
                {
                    anim.SetBool("changeToSword", false);
                    currentPhase = BossPhase.phase2;
                    elapsedTime = 0;
                    
                }
                break;
            case BossPhase.phase2:                
                anim.SetBool("bombAttack", false);
                anim.SetBool("dashAttack", true);   
                if(dashSound == false)
                {
                    AudioManager.instance.PlaySfx(34);
                    dashSound = true;
                }
                phase2 = true;
                Debug.Log("phase2");
                agent.SetDestination(PlayerController_s.instance.transform.position);
                if(distanceToPlayer <= 2f)
                {                    
                    currentPhase = BossPhase.goingPoint;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    anim.SetBool("dashAttack", false);
                    anim.SetBool("Jump",true);
                    AudioManager.instance.PlaySfx(33);
                    rb.isKinematic = true; //false
                    //rb.AddForce(PlayerController_s.instance.transform.up * 50f, ForceMode.Impulse);
                    elapsedTime = 0;
                    dashSound = false;
                }
                Timer = 5;
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= Timer)
                {
                    currentPhase = BossPhase.goingPoint;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    anim.SetBool("dashAttack", false);
                    anim.SetBool("Jump", true);
                    AudioManager.instance.PlaySfx(33);
                    rb.isKinematic = true; //false
                    //rb.AddForce(PlayerController_s.instance.transform.up * 50f, ForceMode.Impulse);
                    elapsedTime = 0;
                    dashSound = false;
                }
                break;
            case BossPhase.goingPoint:                      
                Debug.Log("goingPoint");
                rb.velocity = Vector3.zero;
                if (agent.remainingDistance <= 1f)
                {
                    currentPhase = BossPhase.waitForHit;
                    if (addPoint == false)
                    {
                        currentPatrolPoint++;
                        ActivateCurrentSpotLight(currentPatrolPoint-1);
                        addPoint = true;
                    }                    
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                    anim.SetBool("Jump", false);
                    /*Timer = 2;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {*/
                    
                        /*elapsedTime = 0;
                    }*/
                }
                break;
            case BossPhase.isBeingHitted:
                agent.velocity = Vector3.zero;                

                if (phase1==true)
                {                    
                    Timer = 2;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {
                        currentPhase = BossPhase.phase1;
                        elapsedTime = 0;
                        rb.velocity = Vector3.zero;
                        Hitted = false;
                    }
                    if (BossHealth <= 3)
                    {
                        elapsedTime = 0;
                        currentPhase = BossPhase.changeToSword;
                        anim.SetTrigger("sword");
                        Debug.Log("chasword");
                    }
                    /*if (!Hitted)
                    {
                        rb.AddForce(PlayerController_s.instance.transform.forward * 200, ForceMode.Impulse);
                        Hitted = true;
                    }*/

                }
                else if(phase2 == true)
                {
                    Timer = 8;
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= Timer)
                    {
                        currentPhase = BossPhase.phase2;
                        elapsedTime = 0;
                        rb.velocity = Vector3.zero;
                        Hitted = false;
                    }
                    if (!Hitted)
                    {
                        rb.isKinematic = false;
                        rb.AddForce(PlayerController_s.instance.transform.forward * 7.5f, ForceMode.Impulse);
                        StartCoroutine(StopRbForce());
                        Hitted = true;
                    }
                    transform.LookAt(PlayerController_s.instance.transform, Vector3.up);
                }
                break;
            case BossPhase.death:
                if (isDeath == false)
                {
                    anim.SetBool("isDeath", true);
                    anim.SetTrigger("Death");
                    isDeath = true;
                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    rb.AddForce(PlayerController_s.instance.transform.forward * 5, ForceMode.Impulse);
                    StartCoroutine(StopRbForce());
                    //hurtBox.gameObject.SetActive(false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    agent.speed = 0;
                    agent.acceleration = 0;

                    StartCoroutine(EndBossCo());
                }                   
                break;
        }
    }

    public IEnumerator StopRbForce()
    {
        yield return new WaitForSeconds(2);
        rb.isKinematic = true;
    }

    public void ShotBoss()
    {
        muzzleFlash.Play();
        //Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);        
        Vector3 direction = Playertarget.position.normalized;
        Ray ray = new Ray(firePoint.position, direction);
        Debug.DrawRay(firePoint.position, direction * 100, Color.red, 2f);
        RaycastHit hitInfo;

        //Disparo
        Rigidbody shotPrefab;
        shotPrefab = Instantiate(bomb, firePoint.position, Quaternion.identity);

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            shotPrefab.transform.LookAt(hitInfo.point, Vector3.forward);
            shotPrefab.AddForce(((hitInfo.point - Playertarget.position)+hitInfo.point).normalized * 100 * shotSpeed);

            //Destroy(hitInfo.collider.gameObject);
            /*if (health != null)
                health.TakeDamage(damage);*/
        }
        else
        {
            shotPrefab.transform.LookAt(ray.direction, Vector3.forward);
            shotPrefab.AddForce(ray.direction * 100 * shotSpeed);
        }
    }

    public void DamageBoss()
    {
        anim.SetBool("Wait&Laughing", false);
        
        if (currentPhase != BossPhase.death)
        {
            AudioManager.instance.PlaySfx(bossHit);
            BossHealth--;
            currentPhase = BossPhase.isBeingHitted;
            
            if (phase1 == true)
            {
                anim.SetTrigger("Hurt1");                
            }
            else if (phase2 == true)
            {
                anim.SetTrigger("Hurt2");
            }
            if (BossHealth <= 0)
            {
                currentPhase = BossPhase.death;
            }
            
        }        
    }

    public void DamageBoss2()
    {
        if (currentPhase != BossPhase.death)
        {
            AudioManager.instance.PlaySfx(bossHit);
            BossHealth = BossHealth - 2;
            currentPhase = BossPhase.isBeingHitted;
            anim.SetBool("Wait&Laughing", false);

            if (phase1 == true)
            {
                anim.SetTrigger("Hurt1");
            }
            else if (phase2 == true)
            {
                anim.SetTrigger("Hurt2");
            }                     
            if (BossHealth <= 3)
            {
                currentPhase = BossPhase.phase2;
            }else if (BossHealth <= 0)
            {
                currentPhase = BossPhase.death;
            }           
        }        
    }

    IEnumerator EndBossCo()
    {
        //AudioManager.instance.PlaySfx(bossDeath);
        AudioManager.instance.PlaySfx(bossDeathShout);
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

        yield return new WaitForSeconds(waitToShowExit);
        victoryZone.SetActive(true);
    }

    public void ActivateCurrentSpotLight(int currentSpot)
    {
        for(int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].gameObject.SetActive(false);
        }
        spotlights[currentSpot].gameObject.SetActive(true);
    }
    public void DeActivateSpotLights()
    {
        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].gameObject.SetActive(false);
        }
    }

    public void DestroyBoss()
    {
        Destroy(gameObject);
    }
}

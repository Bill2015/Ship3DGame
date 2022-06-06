using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyStats
{
    protected TurretState currentState;

    public Transform Target { get; set; }
   
    [Header("Turret Elements")]
    [SerializeField]
    private Transform rotator;

    [SerializeField]
    private Transform ghostRotator;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Transform[] gunBarrels;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Animator animator;
    [SerializeField] private AudioSource fireSound;


    [Header("Parameters")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Vector3 aimOffset;

    public Quaternion DefaultRotation { get; set; }

    public Transform Rotator { get => rotator; set => rotator = value; }
    public Vector3 AimOffset { get => aimOffset; set => aimOffset = value; }
    public Transform GhostRotator { get => ghostRotator; set => ghostRotator = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public Transform[] GunBarrels { get => gunBarrels; set => gunBarrels = value; }
    public Animator Animator { get => animator; set => animator = value; }

    new private void Start()
    {
        base.Start();
        fireSound.playOnAwake = false;
        DefaultRotation = rotator.rotation;
        ChangeState(new IdleState());
    }

    new private void Update()
    {
        currentState.Update();
        base.Update();
    }

    public bool CanSeeTarget(Vector3 direction, Vector3 origin, string tag)
    {
        RaycastHit hit;
        // Debug.Log(tag);

        if (Physics.Raycast(origin,direction, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == tag)
            {
                return true;
            }
        }

        return false;
    }

    public void Shoot(int index)
    {
        Vector3 rand = new Vector3(1, 1, 1) * 0.05f;
        Vector3 v = GunBarrels[index].forward + MathUtilities.Random( -rand, rand );
        Quaternion headingDirection = Quaternion.FromToRotation(projectile.transform.forward, v);

        GameObject bullet = Instantiate(projectile, GunBarrels[index].position, headingDirection);
        bullet.GetComponent<Projectile>().Direction = v;
        bullet.transform.localScale = new Vector3( transform.localScale.x, Mathf.Min( transform.localScale.y, 6),  transform.localScale.z);
        fireSound.Play();
    }
    public void ChangeState(TurretState newState)
    {
        if (newState != null)
        {
            newState.Exit();
        }
        this.currentState = newState;

        newState.Enter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    public void OnDestory()
    {
        if(explosionPrefab){
            GameObject exlosionEffect = Instantiate(explosionPrefab, rotator.position, Quaternion.identity);
            Destroy(exlosionEffect, explosionLifeTime);
        }
        enemyLiftNumber -= 1;
        Destroy(gameObject);
    }


}

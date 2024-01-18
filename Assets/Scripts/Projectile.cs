using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    [SerializeField] float aliveTime;
    [SerializeField] float speed;
    [SerializeField] float damage;

    Rigidbody rbody;
    Collider projCollider;
    public Collider ownerCollider { get; set; }
    public GameObject impactVFX { get; set; }

    Collision collision;
    bool collided = false;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    void FixedUpdate()
    {
        if (collided) Impact(collision.contacts[0].point, collision.contacts[0].normal);
    }

    void OnCollisionEnter(Collision other)
    {
        collision = other;
        collided = true;
    }

    public void Direction(Vector3 shotDirection)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), ownerCollider);
        rbody.AddForce(shotDirection.normalized * speed, ForceMode.Impulse);
    }

    void Impact(Vector3 point, Vector3 normal)
    {
        Destroy(gameObject);
        impactVFX.transform.position = point;
        impactVFX.transform.forward = normal;
        impactVFX.GetComponentInChildren<ParticleSystem>().Play();
    }

    public float HitDamage()
    {
        return damage;
    }
}

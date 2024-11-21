using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(BoundsCheck))]
public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField]
    private eWeaponType _type;

    // for special handling
    [HideInInspector] public bool live = false;
    // for phaser
    [HideInInspector] public bool reflected;
    private float lifetime;

    // for missile
    [HideInInspector] public GameObject target;

    public eWeaponType type
    {
        get { return _type; }
        set { SetType(value); }
    }

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
        if (live)
        {
            switch (type)
            {
                case eWeaponType.phaser:
                    lifetime += 10 * Time.deltaTime;
                    float x = transform.position.x + (reflected ? 0.05f : -0.05f) + (Mathf.Sin(lifetime) * (reflected ? -1 : 1)) / 4;
                    transform.position = new Vector3(x, transform.position.y, 0);
                    break;

                case eWeaponType.missile:
                    try { transform.position = Vector3.Lerp(transform.position, target.transform.position, .1f); }
                    catch (MissingReferenceException) { Retarget(); }
                    break;

            }
        }
    }

    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        rend.material.color = def.projectileColor;
    }

    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }

    public void PhaserWake(bool reflect)
    {
        reflected = reflect; lifetime = 0.5f; live = true;
    }

    public void MissileWake(GameObject target)
    {
        if (target == null) return;
        this.target = target; live = true;
    }

    private void Retarget()
    {
        Enemy e = GameObject.FindObjectOfType<Enemy>();
        if (e != null) { target = e.gameObject; } else { Debug.LogWarning("No valid target found for missile projectile"); Destroy(gameObject); }
    }
}

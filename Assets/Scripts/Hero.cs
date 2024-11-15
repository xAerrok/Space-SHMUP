using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S {  get; private set; }

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;
    public Joystick joystick;
    private Touch touch;

    [Header("Dynamic")] [Range(0, 4)]
    private float _shieldLevel = 1;
    [Tooltip("Holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;
    public delegate void WeaponFireDelegate();
    public event WeaponFireDelegate fireEvent;
    public int weaponsNum;

    private void Awake()
    {
        if (S == null) S = this;
        else
        {
            Debug.LogError("HeroAwake() - Attempted to assign second Hero.S");
        }
        ClearWeapons();
        weapons[0].SetType(eWeaponType.phaser);
        weaponsNum++;
    }

    // Update is called once per frame
    void Update()
    {
        // Pull in information from the Input class
        //float hAxis = Input.GetAxis("Horizontal");
        float hAxis = joystick.Horizontal;
        //float vAxis = Input.GetAxis("Vertical");
        float vAxis = joystick.Vertical;


        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);

        // Use the fireEvent to fire Weapons when the Spacebar is pressed.
        //if (Input.GetAxis("Jump") == 1 && fireEvent != null) { fireEvent(); }
        if (Input.touchCount > 1 && fireEvent != null) { fireEvent(); }
    }


    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //Debug.Log("Shield trigger hit by: " + go.gameObject.name);

        // Prevent repeat triggers
        if (go == lastTriggerGo) return;
        lastTriggerGo = go;

        Enemy enemy = go.GetComponent<Enemy>();
        PowerUp pUp = go.GetComponent<PowerUp>();
        if (enemy != null)
        {
            shieldLevel--;
            Destroy(go);
        }
        else if (pUp != null)
        {
            AbsorbPowerUp(pUp);
        }
        else
        {
            Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
        }
    }

    public void AbsorbPowerUp(PowerUp pUp)
    {
        Debug.Log("Absorbed PowerUp: " + pUp.type);
        switch(pUp.type)
        {
            case eWeaponType.shield:
                shieldLevel++;
                break;
            default: 
                if (pUp.type == weapons[0].type)
                {
                    Weapon weap = GetEmptyWeaponSlot();
                    if (weap != null)
                    {
                        weap.SetType(pUp.type);
                        weaponsNum++;
                    }
                    else if (weaponsNum != 5)
                    {
                        Debug.LogWarning("Ran out of empty weapon slots before limit 5.");
                        ClearWeapons();
                        weapons[0].SetType(pUp.type);
                        weaponsNum++;
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].SetType(pUp.type);
                    weaponsNum++;
                }
                break;
        }
        pUp.AbsorbedBy(gameObject);
    }

    public float shieldLevel
    {
        get { return _shieldLevel; }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // If the shield is going to be less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);   // Destroy the Hero
                Main.HERO_DIED();
            }
        }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0;i < weapons.Length;i++)
        {
            if (weapons[i].type == eWeaponType.none)
            {
                return weapons[i];
            }
        }
        return null;
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(eWeaponType.none);
        }
        weaponsNum = 0;
    }
}

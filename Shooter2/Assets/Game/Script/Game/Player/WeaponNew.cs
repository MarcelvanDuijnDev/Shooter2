using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponNew : MonoBehaviour 
{

    [SerializeField]private int gunId;
    [SerializeField]private Text[] textUI;
    [SerializeField]private GameObject flash;
    [SerializeField]private Animator flashAnim;

    [Header("Weapons")]
    public Weapons[] weaponsClass;

    [HideInInspector]public int kills;

    void Start () 
    {
        flashAnim = flash.GetComponent<Animator>();
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            weaponsClass[gunId].objectPoolScript = (ObjectPool_Script)weaponsClass[gunId].objectPool.GetComponent(typeof(ObjectPool_Script));
            weaponsClass[gunId].anim = weaponsClass[gunId].weapon.GetComponent<Animator>();
            weaponsClass[gunId].reloadTimeReset = weaponsClass[gunId].reloadTime;
            weaponsClass[gunId].shootSpeedReset = weaponsClass[gunId].shootSpeed;
        }
    }

    void Update()
    {
        //Single Shot
        if (Input.GetMouseButtonDown(0) && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 0)
        {
            Fire(gunId);
            weaponsClass[gunId].anim.SetTrigger("M4");
            flashAnim.SetTrigger("Flash");
            flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
            weaponsClass[gunId].currentAmmo -= 1;
            if (weaponsClass[gunId].reloading)
            {
                weaponsClass[gunId].reloading = false;
                weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;
            }
        }
        //Burst
        if (Input.GetMouseButtonDown(0) && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 1)
        {
            BurstFire();
            if (weaponsClass[gunId].reloading)
            {
                weaponsClass[gunId].reloading = false;
                weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;

            }
        }

        //Auto
        if (Input.GetMouseButton(0) && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 2)
        {
            weaponsClass[gunId].shootSpeed -= 1 * Time.deltaTime;
            if (weaponsClass[gunId].shootSpeed <= 0)
            {
                Fire(gunId);
                weaponsClass[gunId].anim.SetTrigger("M4");
                flashAnim.SetTrigger("Flash");
                flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
                weaponsClass[gunId].currentAmmo -= 1;
                if (weaponsClass[gunId].reloading)
                {
                    weaponsClass[gunId].reloading = false;
                    weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;
                }
                weaponsClass[gunId].shootSpeed = weaponsClass[gunId].shootSpeedReset;
            }
        }
        //Aim
        if (Input.GetMouseButton(1) && gunId <= weaponsClass.Length)
        {
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].aimPos.x, weaponsClass[gunId].aimPos.y, weaponsClass[gunId].aimPos.z);
        }
        if (Input.GetMouseButtonUp(1) && gunId <= weaponsClass.Length)
        {
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].normalPos.x, weaponsClass[gunId].normalPos.y, weaponsClass[gunId].normalPos.z);
        }
        //Get WeaponInput
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunId = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunId = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunId = 2;
        }
        //Reload
        if(Input.GetKeyDown(KeyCode.R) && weaponsClass[gunId].currentAmmo != weaponsClass[gunId].magazineSize)
        {
            weaponsClass[gunId].reloading = true;
        }
        if(weaponsClass[gunId].reloading)
        {
            weaponsClass[gunId].reloadTime -= 1 * Time.deltaTime;
            if(weaponsClass[gunId].reloadTime <= 0)
            {
                Reload();
                weaponsClass[gunId].reloading = false;
            }
        }

        //Set weapon type
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(weaponsClass[gunId].shootType == 0 && weaponsClass[gunId].burst)
            {weaponsClass[gunId].shootType = 1;}
            else if(weaponsClass[gunId].shootType == 0 && !weaponsClass[gunId].burst && weaponsClass[gunId].automatic)
            {weaponsClass[gunId].shootType = 2;}
            else if(weaponsClass[gunId].shootType == 1 && !weaponsClass[gunId].automatic)
            {weaponsClass[gunId].shootType = 0;}
            else if(weaponsClass[gunId].shootType == 1 && weaponsClass[gunId].automatic)
            {weaponsClass[gunId].shootType = 2;}
            else if(weaponsClass[gunId].shootType == 2)
            { weaponsClass[gunId].shootType = 0;}
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            if (weaponsClass[gunId].weapon.activeSelf && i != gunId)
            {
                weaponsClass[gunId].weapon.SetActive(false);
            }
            if (!weaponsClass[gunId].weapon.activeSelf && i == gunId)
            {
                weaponsClass[gunId].weapon.SetActive(true);
            }
        }
        SetUI();
    }

    void Reload()
    {
        weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;
        if (weaponsClass[gunId].currentAmmo <= 0 && weaponsClass[gunId].ammo >= weaponsClass[gunId].magazineSize)
        {
            weaponsClass[gunId].currentAmmo += weaponsClass[gunId].magazineSize;
            weaponsClass[gunId].ammo -= weaponsClass[gunId].magazineSize;
        }
        if (weaponsClass[gunId].currentAmmo <= 0 && weaponsClass[gunId].ammo < weaponsClass[gunId].magazineSize)
        {
            weaponsClass[gunId].currentAmmo += weaponsClass[gunId].ammo;
            weaponsClass[gunId].ammo = 0;
        }
        if (weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].magazineSize > weaponsClass[gunId].currentAmmo && weaponsClass[gunId].ammo >= weaponsClass[gunId].magazineSize - weaponsClass[gunId].currentAmmo)
        {
            weaponsClass[gunId].ammo -= weaponsClass[gunId].magazineSize - weaponsClass[gunId].currentAmmo;
            weaponsClass[gunId].currentAmmo = weaponsClass[gunId].magazineSize;
        }
    }

    void BurstFire()
    {
        int burstAmountCalculate = weaponsClass[gunId].burstAmount;
        weaponsClass[gunId].shootSpeed -= 1 * Time.deltaTime;
        if (burstAmountCalculate <= weaponsClass[gunId].burstAmount)
        {
            Debug.Log("o");
            if (weaponsClass[gunId].shootSpeed <= 0 && weaponsClass[gunId].currentAmmo >= 0)
            {
                Fire(gunId);
                weaponsClass[gunId].anim.SetTrigger("M4");
                flashAnim.SetTrigger("Flash");
                flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
                weaponsClass[gunId].currentAmmo -= 1;
                burstAmountCalculate++;
                weaponsClass[gunId].shootSpeed = weaponsClass[gunId].shootSpeedReset;
            }
        }
    }

    void Fire(int gunID)
    {
        for (int i = 0; i < weaponsClass[gunId].objectPoolScript.objects.Count; i++)
        {
            if (!weaponsClass[gunId].objectPoolScript.objects[i].activeInHierarchy)
            {
                weaponsClass[gunId].objectPoolScript.objects[i].transform.position = weaponsClass[gunId].shootPoint.transform.position;
                weaponsClass[gunId].objectPoolScript.objects[i].transform.rotation = weaponsClass[gunId].shootPoint.transform.rotation;
                weaponsClass[gunId].objectPoolScript.objects[i].SetActive(true);
                weaponsClass[gunId].objectPoolScript.objects[i].GetComponent<Rigidbody>().velocity = weaponsClass[gunId].objectPoolScript.objects[i].transform.forward * weaponsClass[gunId].bulletSpeed;
                break;
            }
        }
    }

    void SetUI()
    {
        textUI[0].text = weaponsClass[gunId].currentAmmo.ToString();
        textUI[1].text = weaponsClass[gunId].ammo.ToString();
        string shootTypetText = "";
        if (weaponsClass[gunId].shootType == 0) { shootTypetText = "Single";}
        if (weaponsClass[gunId].shootType == 1) { shootTypetText = "Burst"; }
        if (weaponsClass[gunId].shootType == 2) { shootTypetText = "Auto"; }
        textUI[2].text = shootTypetText;
        textUI[3].text = "Kills: " + kills;
    }

    public void GetAmmo(int amount)
    {
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            weaponsClass[i].ammo += amount;
        }
    }
}

[System.Serializable]
public struct Weapons
{
    public GameObject weapon, objectPool;
    public Transform shootPoint;
    public float bulletSpeed, reloadTime, shootSpeed, burstShootSpeed;
    public Vector3 normalPos, aimPos;
    public int ammo, magazineSize, burstAmount;
    public bool automatic, burst;
    public int shootType;

    [HideInInspector]public Animator anim;
    [HideInInspector]public ObjectPool_Script objectPoolScript;
    [HideInInspector]public float reloadTimeReset,shootSpeedReset;
    [HideInInspector]public int currentAmmo;
    [HideInInspector]public bool reloading;
}
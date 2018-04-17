using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponNew : MonoBehaviour 
{
    [SerializeField]private GameObject flashLight;
    [SerializeField]private GameObject flash;
    [SerializeField]private Animator flashAnim;
    [SerializeField]private Text[] textUI;
    [Header("Weapons")]public Weapons[] weaponsClass;

    [HideInInspector]public int kills;
    private int gunId;

    void Start () 
    {
        flashAnim = flash.GetComponent<Animator>();
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            weaponsClass[i].objectPoolScript = (ObjectPool_Script)weaponsClass[i].objectPool.GetComponent(typeof(ObjectPool_Script));
            weaponsClass[i].anim = weaponsClass[i].weapon.GetComponent<Animator>();
            weaponsClass[i].reloadTimeReset = weaponsClass[i].reloadTime;
            weaponsClass[i].shootSpeedReset = weaponsClass[i].fireRate;
        }
    }

    void Update()
    {
        #region Shoot_Type :  Single Shot
        //Single Shot
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("R1Button") && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 0)
        {
            Fire();
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
        #endregion
        #region Shoot_Type :  Burst
        //Burst
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("R1Button") && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 1)
        {
            BurstFire();
            if (weaponsClass[gunId].reloading)
            {
                weaponsClass[gunId].reloading = false;
                weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;
            }
        }
        #endregion
        #region Shoot_Type :  Automatic
        //Automatic
        if (Input.GetMouseButton(0) || Input.GetButton("R1Button") && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0 && weaponsClass[gunId].shootType == 2)
        {
            weaponsClass[gunId].fireRate -= 1 * Time.deltaTime;
            if (weaponsClass[gunId].fireRate <= 0)
            {
                Fire();
                weaponsClass[gunId].anim.SetTrigger("M4");
                flashAnim.SetTrigger("Flash");
                flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
                weaponsClass[gunId].currentAmmo -= 1;
                if (weaponsClass[gunId].reloading)
                {
                    weaponsClass[gunId].reloading = false;
                    weaponsClass[gunId].reloadTime = weaponsClass[gunId].reloadTimeReset;
                }
                weaponsClass[gunId].fireRate = weaponsClass[gunId].shootSpeedReset;
            }
        }
        #endregion
        #region Aim / WeaponInput / FlashLight
        //Aim
        if (Input.GetMouseButton(1) || Input.GetButton("L1Button") && gunId <= weaponsClass.Length)
        {
            Debug.Log("L1");
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].aimPos.x, weaponsClass[gunId].aimPos.y, weaponsClass[gunId].aimPos.z);
        }
        if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("L1Button") && gunId <= weaponsClass.Length)
        {
            Debug.Log("L1UP");
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].normalPos.x, weaponsClass[gunId].normalPos.y, weaponsClass[gunId].normalPos.z);
        }
        //Get WeaponInput
        if (Input.GetKeyDown(KeyCode.Alpha1)) { gunId = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { gunId = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { gunId = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { gunId = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { gunId = 4; }

        //FlashLight
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashLight.activeSelf)
            {
                flashLight.SetActive(false);
            }
            else
            {
                flashLight.SetActive(true);
            }
        }
        #endregion
        #region Reloading / Set Weapon Type
        //Reloading
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("XButton") && weaponsClass[gunId].currentAmmo != weaponsClass[gunId].magazineSize)
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
        #endregion
        #region Set weapon active
        //Set weapon true/false
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            if (weaponsClass[i].weapon.activeSelf && i != gunId)
            {
                weaponsClass[i].weapon.SetActive(false);
            }
            if (!weaponsClass[i].weapon.activeSelf && i == gunId)
            {
                weaponsClass[i].weapon.SetActive(true);
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
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
        weaponsClass[gunId].fireRate -= 1 * Time.deltaTime;
        if (burstAmountCalculate <= weaponsClass[gunId].burstAmount)
        {
            Debug.Log("o");
            if (weaponsClass[gunId].fireRate <= 0 && weaponsClass[gunId].currentAmmo >= 0)
            {
                Fire();
                weaponsClass[gunId].anim.SetTrigger("M4");
                flashAnim.SetTrigger("Flash");
                flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
                weaponsClass[gunId].currentAmmo -= 1;
                burstAmountCalculate++;
                weaponsClass[gunId].fireRate = weaponsClass[gunId].shootSpeedReset;
            }
        }
    }

    void Fire()
    {
        for (int i = 0; i < weaponsClass[gunId].objectPoolScript.objects.Count ; i++)
        {
            if (!weaponsClass[gunId].objectPoolScript.objects[i].activeInHierarchy)
            {
                ParticleShoot();
                weaponsClass[gunId].objectPoolScript.objects[i].transform.position = weaponsClass[gunId].shootPoint.transform.position;
                weaponsClass[gunId].objectPoolScript.objects[i].transform.rotation = weaponsClass[gunId].shootPoint.transform.rotation;
                weaponsClass[gunId].objectPoolScript.objects[i].SetActive(true);
                weaponsClass[gunId].objectPoolScript.objects[i].GetComponent<Rigidbody>().velocity = weaponsClass[gunId].objectPoolScript.objects[i].transform.forward * weaponsClass[gunId].bulletSpeed;
                break;
            }
        }
    }

    void ParticleShoot()
    {
        if(weaponsClass[gunId].shootEffects.Length >= 1)
        {
            int randomEffect = Random.Range(0,weaponsClass[gunId].shootEffects.Length);
            GameObject obj = (GameObject)Instantiate(weaponsClass[gunId].shootEffects[randomEffect]);
            obj.transform.position = weaponsClass[gunId].shootPoint.transform.position;
            obj.transform.rotation = weaponsClass[gunId].shootPoint.transform.rotation;
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

    public void Manage_Upgrades(int gunID,bool add,int attachment)
    {
        #region Weapon :  Pistol
        if (gunID == 0)
        {
            #region Attachments Add
            if (add)
            {
                if (attachment == 1)
                {
                    weaponsClass[gunID].magazineSize += 5;
                }
            }
            #endregion
            #region Attachments Remove
            else
            {
                if(attachment == 1)
                {
                    weaponsClass[gunID].magazineSize -= 5;
                }
            }
            #endregion
        }
        #endregion
    }
}

[System.Serializable]
public struct Weapons
{
    [Header("Weapon")]
    public string stuctName;
    public GameObject weapon, objectPool;
    public Transform shootPoint;
    public float bulletSpeed, reloadTime, fireRate, burstShootSpeed;
    public Vector3 normalPos, aimPos;
    public int ammo, magazineSize, burstAmount;
    public bool automatic, burst;
    public int shootType;
    public GameObject[] shootEffects;

    [HideInInspector]public Animator anim;
    [HideInInspector]public ObjectPool_Script objectPoolScript;
    [HideInInspector]public float reloadTimeReset,shootSpeedReset;
    [HideInInspector]public int currentAmmo;
    [HideInInspector]public bool reloading;
    [Header("Attachments")]
    public bool attchment_1;
    public bool att_2, att_3, att_4;
}
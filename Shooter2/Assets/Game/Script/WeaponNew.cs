using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponNew : MonoBehaviour {

    [SerializeField]private int gunId;
    [SerializeField]private Text[] textUI;
    [SerializeField]private GameObject flash;
    [SerializeField]private Animator flashAnim;

    public Weapons[] weaponsClass;


    void Start () 
    {
        flashAnim = flash.GetComponent<Animator>();
        for (int i = 0; i < weaponsClass.Length; i++)
        {
            weaponsClass[gunId].objectPoolScript = (ObjectPool_Script)weaponsClass[gunId].objectPool.GetComponent(typeof(ObjectPool_Script));
            weaponsClass[gunId].anim = weaponsClass[gunId].weapon.GetComponent<Animator>();
            weaponsClass[gunId].reloadTimeReset = weaponsClass[gunId].reloadTime;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunId <= weaponsClass.Length && weaponsClass[gunId].currentAmmo > 0)
        {
            Fire(gunId);
            weaponsClass[gunId].anim.SetTrigger("M4");
            flashAnim.SetTrigger("Flash");
            flash.transform.localPosition = weaponsClass[gunId].shootPoint.transform.localPosition + weaponsClass[gunId].weapon.transform.localPosition;
            weaponsClass[gunId].currentAmmo -= 1;
        }
        if (Input.GetMouseButton(1) && gunId <= weaponsClass.Length)
        {
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].aimPos.x, weaponsClass[gunId].aimPos.y, weaponsClass[gunId].aimPos.z);
        }
        if (Input.GetMouseButtonUp(1) && gunId <= weaponsClass.Length)
        {
            weaponsClass[gunId].weapon.transform.localPosition = new Vector3(weaponsClass[gunId].normalPos.x, weaponsClass[gunId].normalPos.y, weaponsClass[gunId].normalPos.z);
        }

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
    }
}

[System.Serializable]
public class Weapons
{
    public GameObject weapon, objectPool;
    public Transform shootPoint;
    public float bulletSpeed, reloadTime;
    public Vector3 normalPos, aimPos;
    public int ammo, magazineSize;

    [HideInInspector]public Animator anim;
    [HideInInspector]public ObjectPool_Script objectPoolScript;
    [HideInInspector]public float reloadTimeReset;
    [HideInInspector]public int currentAmmo;
    [HideInInspector]public bool reloading;
}

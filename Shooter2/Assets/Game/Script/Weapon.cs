using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour {

    ObjectPool_Script[] objectPoolScript;
    [SerializeField]private int gunId;
    [SerializeField]private GameObject flash;
    [SerializeField]private Animator flashAnim;
    [SerializeField]private GameObject[] weapons, objectPool;
    [SerializeField]private Transform[] shootPoint;
    [SerializeField]private float[] bulletSpeed, reloadTime;
    [SerializeField]private Vector3[] normalPos, aimPos;
    [SerializeField]private Animator[] anim;
    [SerializeField]private int[] ammo, magazineSize;
    [SerializeField]private Text[] textUI;

    private float[] reloadTimeReset;
    private int[] currentAmmo;
    private bool reloading;

    void Start () 
    {
        objectPoolScript = new ObjectPool_Script[weapons.Length];
        anim = new Animator[weapons.Length];
        currentAmmo = new int[weapons.Length];
        reloadTimeReset = new float[weapons.Length];
        flashAnim = flash.GetComponent<Animator>();
        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPoolScript[i] = (ObjectPool_Script)objectPool[i].GetComponent(typeof(ObjectPool_Script));
            anim[i] = weapons[i].GetComponent<Animator>();
            reloadTimeReset[i] = reloadTime[i];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunId <= weapons.Length && currentAmmo[gunId] > 0)
        {
            Fire(gunId);
            anim[gunId].SetTrigger("M4");
            flashAnim.SetTrigger("Flash");
            flash.transform.localPosition = shootPoint[gunId].transform.localPosition + weapons[gunId].transform.localPosition;
            currentAmmo[gunId] -= 1;
        }
        if (Input.GetMouseButton(1) && gunId <= weapons.Length)
        {
            weapons[gunId].transform.localPosition = new Vector3(aimPos[gunId].x,aimPos[gunId].y,aimPos[gunId].z);
        }
        if (Input.GetMouseButtonUp(1) && gunId <= weapons.Length)
        {
            weapons[gunId].transform.localPosition = new Vector3(normalPos[gunId].x,normalPos[gunId].y,normalPos[gunId].z);
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
        if(Input.GetKeyDown(KeyCode.R) && currentAmmo[gunId] != magazineSize[gunId])
        {
            reloading = true;
        }
        if(reloading)
        {
            reloadTime[gunId] -= 1 * Time.deltaTime;
            if(reloadTime[gunId] <= 0)
            {
                Reload();
                reloading = false;
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].activeSelf && i != gunId)
            {
                weapons[i].SetActive(false);
            }
            if (!weapons[i].activeSelf && i == gunId)
            {
                weapons[i].SetActive(true);
            }
        }
        SetUI();
    }

    void Reload()
    {
        reloadTime[gunId] = reloadTimeReset[gunId];
        if (currentAmmo[gunId] <= 0 && ammo[gunId] >= magazineSize[gunId])
        {
            currentAmmo[gunId] += magazineSize[gunId];
            ammo[gunId] -= magazineSize[gunId];
        }
        if (currentAmmo[gunId] <= 0 && ammo[gunId] < magazineSize[gunId])
        {
            currentAmmo[gunId] += ammo[gunId];
            ammo[gunId] = 0;
        }
    }

    void Fire(int gunID)
    {
        for (int i = 0; i < objectPoolScript[gunID].objects.Count; i++)
        {
            if (!objectPoolScript[gunID].objects[i].activeInHierarchy)
            {
                objectPoolScript[gunID].objects[i].transform.position = shootPoint[gunID].transform.position;
                objectPoolScript[gunID].objects[i].transform.rotation = shootPoint[gunID].transform.rotation;
                objectPoolScript[gunID].objects[i].SetActive(true);
                objectPoolScript[gunID].objects[i].GetComponent<Rigidbody>().velocity = objectPoolScript[gunID].objects[i].transform.forward * bulletSpeed[gunID];
                break;
            }
        }
    }

    void SetUI()
    {
        textUI[0].text = currentAmmo[gunId].ToString();
        textUI[1].text = ammo[gunId].ToString();
    }
}
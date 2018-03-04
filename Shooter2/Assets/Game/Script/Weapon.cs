using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int gunId;
    public GameObject flash;
    public Animator flashAnim;
    ObjectPool_Script[] objectPoolScript;
    public GameObject[] weapons;
    public GameObject[] objectPool;
    public Transform[] shootPoint;
    public float[] bulletSpeed;
    public Vector3[] normalPos;
    public Vector3[] aimPos;
    public Animator[] anim;
    public int[] ammo, magazineSize;
    private int[] currentAmmo;

    void Start () 
    {
        objectPoolScript = new ObjectPool_Script[weapons.Length];
        anim = new Animator[weapons.Length];
        flashAnim = flash.GetComponent<Animator>();
        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPoolScript[i] = (ObjectPool_Script)objectPool[i].GetComponent(typeof(ObjectPool_Script));
            anim[i] = weapons[i].GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunId <= weapons.Length)
        {
            Fire(gunId);
            anim[gunId].SetTrigger("M4");
            flashAnim.SetTrigger("Flash");
            flash.transform.localPosition = shootPoint[gunId].transform.localPosition + weapons[gunId].transform.localPosition;
        }
        if (Input.GetMouseButton(1) && gunId <= weapons.Length)
        {
            weapons[gunId].transform.localPosition = new Vector3(aimPos[gunId].x,aimPos[gunId].y,aimPos[gunId].z);
            Debug.Log("aim");
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
}

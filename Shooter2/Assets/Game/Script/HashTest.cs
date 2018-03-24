using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashTest : MonoBehaviour 
{
    [Header("Input")]
    public string input;
    public string inputHash,hash,decryptHash;
    public bool boolInput, boolInputHash, boolHash, boolDecryptHash;

    [Header("Output")]
    public int hashCount;

	void Start () 
    {
        if (boolInput){Input();}
        if (boolInput){InputHash();}
        if (boolInput){Hash();}
        if (boolInput){DecryptHash();}
	}
    void Input () 
    {

	}
    void InputHash()
    {
        hashCount = inputHash.Length;
    }
    void Hash()
    {

    }
    void DecryptHash()
    {

    }
}

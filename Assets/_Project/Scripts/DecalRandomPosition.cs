using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalRandomPosition : MonoBehaviour
{

    [SerializeField] private float _difference = .1f;
    
    private void Awake()
    {
        transform.position += transform.right * Random.Range(_difference, -_difference) + transform.up * Random.Range(_difference, -_difference);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform followed;
    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;

    private void Update()
    {
        float x = this.x ? followed.position.x : transform.position.x;
        float y = this.y ? followed.position.y : transform.position.y;
        float z = this.z ? followed.position.z : transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}

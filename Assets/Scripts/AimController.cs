using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    private Camera _camera;
    private Transform _aimTransform;
    
    void Awake()
    {
        _camera = Camera.main;
        _aimTransform = transform.Find("Aim");
    }

    void Update()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPositionWithZ(_camera);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}

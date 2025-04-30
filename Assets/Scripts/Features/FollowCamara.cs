using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class FollowCamara : MonoBehaviour
{
    public GameObject target;
    public float rayLength;

    public LayerMask ignoreLayers;
    private CinemachineVirtualCamera followCamera;


    private void Start()
    {
        followCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void LateUpdate()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, target.transform.position, rayLength, ignoreLayers.value);
        foreach(var ignoreObj in hits)
        {
            //ignoreObj.collider.gameObject.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }

    private void SetFollowPlayerMove(PlayerMove player)
    {
        followCamera.Follow = player.transform;
    }
}

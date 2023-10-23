using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private PolygonCollider2D pc2D;
    [SerializeField] private Color gizmosColor;
    



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() != null)
        {
            cam.SetActive(true);
             CameraFollow();

        }

    }

    private void  OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Player>() != null)
        {
            cam.SetActive(false);
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(pc2D.bounds.center, pc2D.bounds.size);
    }


    private void CameraFollow()
    {
        cam.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.currentPlayer.transform;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    Transform player;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }

    public void SetupMiniMap(GameObject currentPlayer)
    {
        player = currentPlayer.GetComponent<Transform>();
    }

}

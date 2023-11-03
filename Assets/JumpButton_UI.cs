
using UnityEngine;
using UnityEngine.EventSystems;


public class JumpButton_UI : MonoBehaviour , IPointerDownHandler
{
    private Player player;  

    public void OnPointerDown(PointerEventData eventData)
    {
        if(PlayerManager.instance.currentPlayer != null )
        {
            player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
            player.JumpButton();
        }
    }


}

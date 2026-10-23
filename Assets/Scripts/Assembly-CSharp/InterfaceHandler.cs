using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaceHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	public GameObject MeineCamera;

	public void OnPointerClick(PointerEventData eventData)
	{
		MeineCamera.GetComponent<CameraHandler>().OnInterface = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		MeineCamera.GetComponent<CameraHandler>().OnInterface = true;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		MeineCamera.GetComponent<CameraHandler>().OnInterface = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		MeineCamera.GetComponent<CameraHandler>().OnInterface = false;
	}
}

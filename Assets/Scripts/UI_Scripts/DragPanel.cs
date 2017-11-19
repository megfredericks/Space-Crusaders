using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

	private Vector2 pointerOffset;
	private RectTransform canvasTransform;
	private RectTransform panelTransform;

	void Awake(){
		Canvas canvas = GetComponentInParent<Canvas> ();
		if (canvas != null) {
			canvasTransform = canvas.transform as RectTransform;
			panelTransform = transform as RectTransform;
		}
	} 

	public void OnPointerDown (PointerEventData data){
		panelTransform.SetAsLastSibling ();
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelTransform, data.position, data.pressEventCamera, out pointerOffset);
	}

	public void OnDrag(PointerEventData data){
		if (panelTransform == null) {
			return;
		}

		Vector2 localPointerPosition;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, data.position, data.pressEventCamera, out localPointerPosition)){
			panelTransform.localPosition = localPointerPosition - pointerOffset;
		}
	}
}

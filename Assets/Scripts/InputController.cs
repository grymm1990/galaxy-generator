using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] SelectionManager selectionManager;
    [SerializeField] TMP_Text infoTag;
    [SerializeField] Vector3 tagOffset;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            StarSystem selectedSystem = hitInfo.collider.transform.parent.GetComponent<StarSystem>();

            if (selectedSystem != null)
            {
                infoTag.transform.position = Input.mousePosition + tagOffset;
                infoTag.text = selectedSystem.SystemName;
                infoTag.gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedSystem != null)
                {
                    selectionManager.SetSelectedSystem(selectedSystem);
                }
            }
        }
        else
        {
            infoTag.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                selectionManager.SetSelectedSystem(null);
            }
        }
    }
}

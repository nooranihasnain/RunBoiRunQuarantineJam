using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetIndicator : MonoBehaviour
{
    private Camera mainCamera;
    private RectTransform m_icon;
    private Image m_iconImage;
    private Canvas mainCanvas;
    private Vector3 m_cameraOffsetUp;
    private Vector3 m_cameraOffsetRight;
    private Vector3 m_cameraOffsetForward;
    private GameObject Player;
    private Text m_DistanceText;
    private Text m_ObjectiveText;
    public GameObject m_Marker { get; set; }
    //public GameObject m_TargetMarker;
    public Sprite m_targetIconOnScreen;
    [Space]
    [Range(0, 200)]
    public float m_edgeBufferHorizontal;
    [Range(0, 350)]
    public float m_edgeBufferVertical;
    [Space]
    public Vector3 m_targetIconScale;


    void Start()
    {
        mainCamera = Camera.main;
        mainCanvas = FindObjectOfType<Canvas>();
        Player = GameObject.FindWithTag("Player");
        Debug.Assert((mainCanvas != null), "There needs to be a Canvas object in the scene for the OTI to display");
        InstainateTargetIcon();
        m_DistanceText = m_Marker.transform.GetChild(1).GetComponent<Text>();
        m_ObjectiveText = m_Marker.transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        UpdateTargetIconPosition();
        UpdateDistance();
    }

    private void UpdateDistance()
    {
        Vector3 PlayerPos = Player.transform.position;
        Vector3 TargetPos = this.transform.position;
        int Distance = (int)Vector3.Distance(TargetPos, PlayerPos);
        m_DistanceText.text = Distance + "m";
    }

    private void InstainateTargetIcon()
    {
        m_icon = new GameObject().AddComponent<RectTransform>();
        m_icon.transform.SetParent(mainCanvas.transform);
        m_icon.localScale = m_targetIconScale;
        m_icon.name = name + ": OTI icon";
        m_iconImage = m_icon.gameObject.AddComponent<Image>();
        m_iconImage.sprite = m_targetIconOnScreen;
        m_iconImage.color = new Color(0f, 0f, 0f, 0.5f);
        /*m_Marker = Instantiate(m_TargetMarker);
        m_Marker.transform.SetParent(mainCanvas.transform, false);
        m_Marker.transform.localScale = m_targetIconScale;*/
    }


    private void UpdateTargetIconPosition()
    {
        Vector3 newPos = transform.position;
        newPos = mainCamera.WorldToViewportPoint(newPos);
        if (newPos.z < 0)
        {
            newPos.x = 1f - newPos.x;
            newPos.y = 1f - newPos.y;
            newPos.z = 0;
            newPos = Vector3Maxamize(newPos);
        }
        newPos = mainCamera.ViewportToScreenPoint(newPos);
        newPos.x = Mathf.Clamp(newPos.x, m_edgeBufferHorizontal, Screen.width - m_edgeBufferHorizontal);
        newPos.y = Mathf.Clamp(newPos.y, m_edgeBufferVertical, Screen.height - m_edgeBufferVertical);
        //m_icon.transform.position = newPos;
        m_Marker.transform.position = newPos;
    }

    public Vector3 Vector3Maxamize(Vector3 vector)
    {
        Vector3 returnVector = vector;
        float max = 0;
        max = vector.x > max ? vector.x : max;
        max = vector.y > max ? vector.y : max;
        max = vector.z > max ? vector.z : max;
        returnVector /= max;
        return returnVector;
    }
}
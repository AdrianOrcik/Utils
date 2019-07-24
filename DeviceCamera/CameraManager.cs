using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [Tooltip("rawImage for streaming a camera texture")]
    public RawImage Display;

    [Header("Camera Settings")] public Vector2 Resolution = new Vector2(400, 300);
    public int FPS = 12;

    [Header("PhotoStack Settings")] public Transform ContentTransform;
    public GameObject PhotoPrefab;

    [Header("Buttons Settings")] public Button CameraResetBtn;
    public Button SnapshotBtn;
    public Button SwitchCameraBtn;

    private string deviceName;
    private WebCamTexture camera;
    private int captureCounter = 0;
    private int camIndex = 0;

    private void Awake()
    {
        CameraResetBtn.onClick.AddListener(InitCamera);
        SnapshotBtn.onClick.AddListener(TakeSnapshot);
        SwitchCameraBtn.onClick.AddListener(SwitchCamera);
    }

    void Start()
    {
        InitCamera();
    }

    private void SwitchCamera()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            camIndex += 1;
            camIndex %= WebCamTexture.devices.Length;
        }
    }

    private void InitCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        deviceName = devices[0].name;
        camera = new WebCamTexture(deviceName, (int) Resolution.x, (int) Resolution.y, FPS);
        Display.texture = camera;
        camera.Play();
    }

    private void TakeSnapshot()
    {
        Texture2D snap = new Texture2D(camera.width, camera.height, TextureFormat.ARGB32, false);
        snap.SetPixels(camera.GetPixels());
        snap.Apply();

        Instantiate(PhotoPrefab, ContentTransform).GetComponent<RawImage>().texture = snap;

        System.IO.File.WriteAllBytes(Application.dataPath + "/" + "Resources/" + captureCounter.ToString() + ".png",
            snap.EncodeToPNG());
        ++captureCounter;
    }
}
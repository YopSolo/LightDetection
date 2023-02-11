using UnityEngine;

public class Light_detection : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The camera who scans for light intensity")]
    public Camera m_camLightScan;//The camera who scans for light intensity
    [Tooltip("Float value between 0 and 1 related to the light intensity")]
    public float Light_Intensity;//Float value between 0 and 1
    private RenderTexture rt;//The render texture created based on "m_camLightScan"
    private Texture2D texture2D;//The texture 2D created based on "rt"
    
    private Rect rect;//Rect determine the size where you read the texture
    private Color pixel_color;//Variable that keep the color of a specific pixel


    // Start is called before the first frame update
    void Start()
    {
        texture2D = new Texture2D(16, 16, TextureFormat.RGB24, false);//set the size of the texture 2D (only on start because all textures 2D will have the same size)
        rect = new Rect(0f, 0f, 16f, 16f);//set the size of the rect (only on start because all rects will have the same size)
    }

    // Update is called once per frame
    void Update()
    {
        rt = new RenderTexture(16, 16, 16, RenderTextureFormat.ARGB32);//set the render texture ID
        m_camLightScan.targetTexture = rt;//Which render texture "rt" is using
        m_camLightScan.Render();//Render the camera manually
        RenderTexture.active = rt;// set the render texture active 
        texture2D.ReadPixels(rect, 0, 0);//Reads the pixels from the current render texture, and writes it to the texture
        pixel_color = texture2D.GetPixel(8, 8);//Scan a pixel at the specific location and return his color
        Light_Intensity = (float)(0.2126 * pixel_color.r + 0.7152 * pixel_color.g + 0.0722 * pixel_color.b);//light intensity calculation
    }
}
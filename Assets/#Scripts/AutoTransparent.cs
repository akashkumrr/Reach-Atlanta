using UnityEngine;
using System.Collections;


public class AutoTransparent : MonoBehaviour
{
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;
    private float m_Transparency = 0.3f;
    private const float m_TargetTransparancy = 0.3f;
    private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec

    public Renderer myRenderer;
    private float t;

    void Start()
    {
        t = 0;
        myRenderer = this.gameObject.GetComponent<Renderer>();
        myRenderer.material.SetFloat("_Mode", 2);
        Color c = myRenderer.material.color;
        c.a = 0.5f;
        myRenderer.material.color= c;
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t < 2)
        {
            //myRenderer.material.SetFloat("_Mode", 2);
            Color c = myRenderer.material.color;
            c.a = 0.5f;
            myRenderer.material.color = c;
            t = 0;
            //STandard material OPAQUE
            myRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            myRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            myRenderer.material.SetInt("_ZWrite", 1);
            myRenderer.material.DisableKeyword("_ALPHATEST_ON");
            myRenderer.material.DisableKeyword("_ALPHABLEND_ON");
            myRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            myRenderer.material.renderQueue = -1;

        }

        if(t==0)
        {
            //myRenderer.material.SetFloat("_Mode", 2);
            Color c = myRenderer.material.color;
            c.a = 0.5f;
            myRenderer.material.color = c;

            //Standard material transparent

            myRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            myRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            myRenderer.material.SetInt("_ZWrite", 0);
            myRenderer.material.DisableKeyword("_ALPHATEST_ON");
            myRenderer.material.DisableKeyword("_ALPHABLEND_ON");
            myRenderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            myRenderer.material.renderQueue = 3000;
            
        }
    }
    /*
    public void BeTransparent()
    {
        // reset the transparency;
        m_Transparency = m_TargetTransparancy;
        if (m_OldShader == null)
        {
            // Save the current shader
            m_OldShader = myRenderer.material.shader;
            m_OldColor = myRenderer.material.color;
            myRenderer.material.shader = Shader.Find("Transparent/Diffuse");
        }
    }
    void Update()
    {
        if (m_Transparency < 1.0f)
        {
            Color C = myRenderer.material.color;
            C.a = m_Transparency;
            myRenderer.material.color = C;
        }
     else
     {
            // Reset the shader
            myRenderer.material.shader = m_OldShader;
            myRenderer.material.color = m_OldColor;
            // And remove this script
            Destroy(this);
        }
        m_Transparency += ((1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
    }
    */

}
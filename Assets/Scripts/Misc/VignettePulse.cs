using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignettePulse : MonoBehaviour
{
    [SerializeField] private PostProcessProfile _profile;
    [SerializeField, Range(0f, 0.5f)] private float amplitude;
    [SerializeField, Range(0f, 1.0f)] private float offset;

    void Update()
    {
        foreach(PostProcessEffectSettings s in _profile.settings)
        {
            if (s.GetType() == typeof(Vignette))
            {
                var asdf = s as Vignette;
                asdf.intensity.value = amplitude * Mathf.Sin(Time.realtimeSinceStartup) + offset;
            }
        }
    }
}

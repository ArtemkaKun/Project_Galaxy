using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/**
    \brief Klasa, która odpowiada za obracanie satelitów dookoła swej osi i dookoła swej planety.    
 */
public class satelit : MonoBehaviour
{
    private float _mass; ///< masa satelita
    private Vector3 _starRotation; ///< Wektor normalizujący dla obracanie planety
    private Vector3 _rotator; ///< Wektor obracania planety
    private string _parPlanet; ///< planeta rodzinna
    private GameObject _parentPlanet; ///< planeta rodzinna
    // Start is called before the first frame update
    void Start()
    {
        _starRotation = new Vector3(0.0f, Random.Range(0f, 1f), 0.0f);
        gameObject.GetComponent<Renderer>().material.enableInstancing = true;
        var position = transform.position;
        _rotator = new Vector3(0f, 1 - position.y, position.y);
        if (transform.position.z < 0)
        {
            _rotator.z *= -1;
        } 
        
    }
    /**
        Funkcja, która ustawia masę satelita.
        \param[in] x Masa satelita.
     */
    public void SetMass(float x)
    {
        _mass = x;
    }
    
    private void OnBecameVisible()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                MainThreadDispatcher.StartUpdateMicroCoroutine(aroundR());
                MainThreadDispatcher.StartUpdateMicroCoroutine(Rot());
        });
    }
    
    IEnumerator aroundR()
    {
        transform.RotateAround(_parentPlanet.transform.position, _rotator, 20 * Time.deltaTime / _mass);
        yield return null;
    }

    IEnumerator Rot()
    {
        transform.Rotate(_starRotation);
        yield return null;
    }
    /**
        Funkcja, która ustawia planetę rodzinną.
        \param[in] x Obiekt planety rodzinnej.
     */
    public void SetParPlanet(GameObject x)
    {
        _parentPlanet = x;
    }
}

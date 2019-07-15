using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
/**
    \brief Klasa, która generuje satelity i odpowiada za obracanie planet.

    Klasa generuje satelity (pozycję i ich charakterystykę) oraz odpowiada za obracanie planet dookoła swej osi i dookoła swej gwiazdy.
    
 */
public class Planet : MonoBehaviour
{
    private float _mass; ///< masa planety
    private char _type; ///< typ planety
    private Vector3 _starRotation; ///< Wektor normalizujący dla obracanie planety
    private Vector3 _rotator; ///< Wektor obracania planety
    private GameObject _parentStar; ///< gwiazda rodzinna
    private string _planetName; ///< imię planety
    private GameObject _blackHoll; ///< centrum galaktyki
    private bool isEarth; ///< czy jest planeta planetą pozasłoneczną
    private static readonly int Color1 = Shader.PropertyToID("Color1"); ///< pierwszy kolor planety
    private static readonly int Color2 = Shader.PropertyToID("Color2"); ///< drugi kolor planety
    /**
        Funkcja odpowiada za generowanie satelitów.
     */
    // Start is called before the first frame update
    void Start()
    {
        name = _planetName;
        var planetMaterial = gameObject.GetComponent<Renderer>().material;
        _starRotation = new Vector3(0.0f, Random.Range(0f, 1f), 0.0f);
        planetMaterial.shader = Shader.Find("Shader Graphs/planet");
        planetMaterial.SetVector("Color_2C734209", new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        planetMaterial.SetVector("Color_1EBC618C", new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
//        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/planet") as Material;
//        gameObject.GetComponent<Renderer>().material.shader.Color1 = new Vector3(Random.Range(1f, 0f), Random.Range(1f, 0f), Random.Range(1f, 0f));
//        planetMaterial.enableInstancing = true;
        _blackHoll = GameObject.Find("WorldGenerator");
        if (_mass > 15f)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                if (_mass > 7f)
                {
                    var magicRandom = Random.Range(1, 15);
                    while (magicRandom != 0)
                    {
                        var satelit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        satelit.AddComponent<satelit>();
                        var satelitComp = satelit.GetComponent<satelit>();
                        Vector3 position;
                        position = Random.insideUnitSphere * 100 + transform.position;
                        satelit.transform.position = position;
                        var satelitRandom = Random.Range(0.1f, 5.5f);
                        satelit.transform.localScale = new Vector3(satelitRandom, satelitRandom, satelitRandom);
                        satelitComp.SetMass(Random.Range(0.1f, 10f));
                        satelit.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f),
                            Random.Range(0f, 360f));
                        satelitComp.SetParPlanet(gameObject);
                        _blackHoll.GetComponent<StarsGeneration>().AddSat();
                        magicRandom--;
                    }
                }
            }
        }

        var position1 = transform.position;
        _rotator = new Vector3(0f, 1 - position1.y, position1.y);
        if (position1.z < 0)
        {
            _rotator.z *= -1;
        }
        
    }
    
    private void OnBecameVisible()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                MainThreadDispatcher.StartUpdateMicroCoroutine(AroundR());
                MainThreadDispatcher.StartUpdateMicroCoroutine(Rot());
            });
    }
    /**
        Funkcja ustawia, czy planeta jest planetą pozasłoneczną.
        \param[in] x Czy planeta jest planetą pozasłoneczną (true\false).
     */
    public void SetEarth(bool x)
    {
        isEarth = x;
    }
    private IEnumerator AroundR()
    {
        transform.RotateAround(_parentStar.transform.position, _rotator, 20 * (Time.deltaTime / _mass));
        yield return null;
    }

    private IEnumerator Rot()
    {
        transform.Rotate(_starRotation);
        yield return null;
    }
    /**
        Funkcja, która ustawia masę planety.
        \param[in] x Masa planety.
     */
    public void SetMass(float x)
    {
        _mass = x;
    }
    /**
        Funkcja, która zwraca masę planety.
        \return _mass Mada planety.
     */
    public float GetMass()
    {
        return _mass;
    }
    /**
        Funkcja, która ustawia typ planety.
        \param[in] x Typ planety.
     */
    public void SetType(char x)
    {
        _type = x;
    }
    
    /**
        Funkcja, która zwraca typ planety.
        \return _type Typ planety.
     */
    public char GetPType()
    {
        return _type;
    }

    /**
        Funkcja, która ustawia gwiazdę rodzinną dla planety
        \param[in] x Obiekt gwiazdy rodzinnej.
     */
    public void SetParStar(GameObject x)
    {
        _parentStar = x;
    }
    /**
        Funkcja, która ustawia imię planety
        \param[in] x Imię planety.
     */
    public void SetPlanetName(string x)
    {
        _planetName = x;
    }
}

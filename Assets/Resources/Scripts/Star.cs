using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;
/**
    \brief Klasa, która generuje planety i odpowiada za obracanie gwiazd.

    Klasa generuje planety (pozycję i ich charakterystykę) oraz odpowiada za obracanie gwiazd dookoła swej osi i dookoła centrum galaktyki.
    
 */
public class Star : MonoBehaviour
{
    private GameObject _lightStar; ///< component światła gwiazdy
    private float _intence; ///< ilość światlość gwiazdy
    private char _type; ///< typ gwiazdy
    private string _starName; ///< imię gwiazdy
    private int _temp; ///< temperatura gwiazdy
    private Color _starColor; ///< color gwiazdy
    private bool _havePlanet; ///< możliwość mieć planety
    private GameObject _starParticle; ///< component wizualizacyjny gwiazdy
    private float _mass; ///< masa gwiazdy
    private Vector3 _starRotation; ///< Vector, na który obraca gwiazda
    private GameObject _blackHoll; ///< centrum galaktyki
    private Vector3 _blackHollPos; ///< pozycja centruma galaktyki 
    /**
        Funkcja odpowiada za generowanie planet.
     */
    // Start is called before the first frame update
    void Start()
    {
        name = _starName;
        var transformPuls = transform;
        _starRotation = new Vector3(0.0f, 0.005f, 0.0f);
        var localScale = transformPuls.localScale;            
        
        _blackHoll = GameObject.Find("WorldGenerator");
        _blackHollPos = _blackHoll.transform.position;
        
        if (gameObject.GetComponent<Renderer>() != null)
        {
            gameObject.GetComponent<Renderer>().material.enableInstancing = true;
            gameObject.GetComponent<Renderer>().material.color = _starColor;
        }
        else
        {
            _starParticle = Resources.Load("Prefabs/Star") as GameObject;
 //           _lightStar = Resources.Load("Prefabs/starLight") as GameObject;
 //          _lightStar.GetComponent<Light>().intensity = _intence;
            var pulsing = Instantiate(_starParticle, transformPuls, true);
            pulsing.transform.position = transformPuls.position;
            pulsing.transform.localScale = transformPuls.localScale;
            pulsing.transform.GetChild(0).GetComponent<ParticleSystem>().transform.localScale = localScale;
//            pulsing.transform.GetChild(1).GetComponent<ParticleSystem>().transform.localScale = localScale;
//            var color0 = pulsing.transform.GetChild(0).GetComponent<ParticleSystem>().textureSheetAnimation;
//            color0.startFrame = 0;
            pulsing.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = _starColor;
//            pulsing.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = _starColor;
        }

        if (_havePlanet)
        {
            var havePlanetChance = Random.Range(0f, 1f);
            if (havePlanetChance >= 0.75f)
            {
                    var magicCalc = (int) (_intence * 8);
                    while (magicCalc != 0)
                    {
                        var planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        planet.AddComponent<Planet>();
                        if (Random.Range(0f, 1f) >= 0.5)
                        {
                            planet.GetComponent<Planet>().SetType('E');
                        }
                        else
                        {
                            planet.GetComponent<Planet>().SetType('G');
                        }
                        var planetComp = planet.GetComponent<Planet>();
                        planetComp.SetPlanetName(planetNameGenerator(planetComp.GetPType()));
                        Vector3 position;
                        position = Random.insideUnitCircle * 1000;
                        position = new Vector3(position.x, 0f, position.y);
                        position += new Vector3(transformPuls.position.x, transformPuls.position.y, transformPuls.position.z);
//                        position = new Vector3(position.x, position.z, position.y);
                        planet.transform.position = position;
                        if (planetComp.GetPType() == 'E')
                        {
                            var magicRandom = Random.Range(0.5f, 10.5f);
                            planet.transform.localScale = new Vector3(magicRandom, magicRandom, magicRandom);
                            planetComp.SetMass(Random.Range(1f, 18f));
                            if (Mathf.Abs(transform.position.z - planet.transform.position.z) > 35f &&
                                Mathf.Abs(transform.position.z - planet.transform.position.z) < 350f)
                            {
                                if (planetComp.GetMass() > 3f)
                                {
                                    planetComp.SetEarth(true);
                                    _blackHoll.GetComponent<StarsGeneration>().AddExo();
                                }
                            }
                        }
                        else
                        {
                            var magicRandom = Random.Range(5f, 22.5f);
                            planet.transform.localScale = new Vector3(magicRandom, magicRandom, magicRandom);
                            planetComp.SetMass(Random.Range(15f, 150f));
                        }                       
                        planet.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f),
                            Random.Range(0f, 360f));
                        planetComp.SetParStar(gameObject);
                        _blackHoll.GetComponent<StarsGeneration>().AddPlanet();
                        magicCalc--;
                    }
            }
        }

    }

    /**
        Funkcja, która generuje imię planety.
        \param[in] typeX Typ planety.
        \return starN Imię planety.
     */
    private string planetNameGenerator(char typeX)
    {
        var starN = "" + typeX;
        var charCount = Random.Range(1, 16);
        while (charCount != 0)
        {
            starN += Random.Range(0, 9);
            charCount--;
        }
        return starN;
    }
    /**
        Funkcja, która ustawia masę gwiazdy.
        \param[in] x Masa gwiazdy.
     */
    public void SetMass(float x)
    {
        _mass = x;
    }
    /**
        Funkcja, która ustawia typ gwiazdy.
        \param[in] x Typ gwiazdy.
     */
    public void SetType(char x)
    {
        _type = x;
    }

    /**
        Funkcja, która ustawia imię gwiazdy.
        \param[in] x Imię gwiazdy.
     */
    public void SetStarName(string x)
    {
        _starName = x;
    }

    /**
        Funkcja, która ustawia temperaturę gwiazdy.
        \param[in] x Temperatura gwiazdy.
     */
    public void SetTemp(int x)
    {
        _temp = x;
    }

    /**
        Funkcja, która ustawia możliwość gwiazdy mieć planety.
        \param[in] x Czy może mieć planety (true/false).
     */
    public void SetPlanet(bool x)
    {
        _havePlanet = x;
    }

    /**
        Funkcja, która ustawia kolor gwiazdy.
        \param[in] x Kolor gwiazdy.
     */
    public void SetStarColor(Color x)
    {
        _starColor = x;
    }
    /**
        Funkcja, która zwraca typ gwiazdy
        \return _type Typ gwiazdy
     */
    public char GetPType()
    {
        return _type;
    }

    /**
        Funkcja, która zwraca temperaturę gwiazdy
        \return _temp Temperatura gwiazdy
     */
    public int GetTemp()
    {
        return _temp;
    }
    /**
        Funkcja, która ustawia światłość gwiazdy
        \param[in] x światłość gwiazdy
     */
    public void SetIntence(float x)
    {
        _intence = x;
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

    private IEnumerator AroundR()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * (Time.deltaTime / _mass));
        yield return null;
    }

    private IEnumerator Rot()
    {
        transform.Rotate(_starRotation);
        yield return null;
    }
    
}

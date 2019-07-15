using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*! \mainpage Strona główna
 *
 * \section intro_sec Wstęp
 *
 * Projekt Galaxy - to jest projekt, który pozwala randomowo generować galaktykę i wizualizować to za pomocą narzędzia Unity3D
 *
 */

/**
    \brief Klasa, która generuje gwiazdy.

    Z tej klasy zaczyna się działanie programu. Ona jest wykorzystywana do generowania gwiazd (ich pozycji oraz charakterystyk). Również ta klasa mieści w sobie funkcją, która odpowiada za interfejs użytkownika.
    
 */

public class StarsGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform UICanv;///< objekt UI
    public int mainStar; ///< ilość gwiazd, które mogą mieć planety
    public int otherStar; ///< ilość gwiazd, które nie mogą mieć planety
    private int classO; ///< ilość gwiazd klasy O
    private int classB; ///< ilość gwiazd klasy B
    private int classA; ///< ilość gwiazd klasy A
    private int classF; ///< ilość gwiazd klasy F
    private int classG; ///< ilość gwiazd klasy G
    private int classK; ///< ilość gwiazd klasy K
    private int classM; ///< ilość gwiazd klasy M
    private int classT; ///< ilość gwiazd klasy T
    private int classW; ///< ilość gwiazd klasy W
    private int classR; ///< ilość gwiazd klasy R
    private int classN; ///< ilość gwiazd klasy N
    private int planets; ///< ilość planet
    private int satellite; ///< ilość satelitów
    private int exo; ///< ilość planet pozasłonecznych
    public void Awake()
    {
        for (var i = 0; i < 5000; i++)
        {
            StarSpawn();
        }

        StartCoroutine(UISet());
    }
    /**
        Funkcja odpowiada za generowanie obiektów gwiazd i ich charakterystyk.
     */
    void StarSpawn()
    {
        var neutronRand = Random.Range(0f, 1f);
        if (neutronRand > 0.999)
        {
            var star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            star.AddComponent<Star>();
            star.transform.localScale = new Vector3(15f, 15f, 15f);
            var _star = star.GetComponent<Star>();
            Vector3 position;
            do
            {
                position = Random.insideUnitSphere * 100000;
                position = new Vector3(position.x, Random.Range(-1000f, 1000f), position.z);
                star.transform.position = position;
            }while (Physics.OverlapSphere(star.transform.position, 1000f).Length != 0);
            star.AddComponent<SphereCollider>();
            star.GetComponent<SphereCollider>().radius = 1;
            star.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            _star.SetType('N');
            _star.SetTemp(Random.Range(0, 500));
            _star.SetMass(100000f);
            _star.SetStarName(starNameGenerator(_star.GetPType()));
            _star.SetPlanet(false);
            _star.SetStarColor(starColorGenerator(_star.GetPType()));
            _star.SetIntence(0.001f);
            otherStar++;
            classN++;
        }
        else
        {
            var starRand = Random.Range(0f, 1f);
            if (starRand > 0.5f)
            {
                var mainStarRand = Random.Range(0, 7);
                var star = new GameObject();
                star.AddComponent<Star>();
                star.AddComponent<SphereCollider>();
                var magicRandom = Random.Range(17.5f, 150f);
                star.transform.localScale = new Vector3(magicRandom, magicRandom, magicRandom);
                star.GetComponent<SphereCollider>().radius = 1;
                var _star = star.GetComponent<Star>();
                Vector3 position;
                do
                {
                    position = Random.insideUnitSphere * 100000;
                    position = new Vector3(position.x, Random.Range(-1000f, 1000f), position.z);
                    star.transform.position = position;
                } while (Physics.OverlapSphere(star.transform.position, 2000f).Length != 0);
                star.AddComponent<SphereCollider>();
                star.GetComponent<SphereCollider>().radius = 1;
                star.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f),Random.Range(0f, 360f));
                switch (mainStarRand)
                {
                    case 0:
                        _star.SetType('O');
                        _star.SetTemp(Random.Range(30000, 60000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classO++;
                        break;
                    case 1:
                        _star.SetType('B');
                        _star.SetTemp(Random.Range(10000, 30000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classB++;
                        break;
                    case 2:
                        _star.SetType('A');
                        _star.SetTemp(Random.Range(7500, 10000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classA++;
                        break;
                    case 3:
                        _star.SetType('F');
                        _star.SetTemp(Random.Range(6000, 7500));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classF++;
                        break;
                    case 4:
                        _star.SetType('G');
                        _star.SetTemp(Random.Range(5000, 6000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classG++;
                        break;
                    case 5:
                        _star.SetType('K');
                        _star.SetTemp(Random.Range(3500, 5000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classK++;
                        break;
                    case 6:
                        _star.SetType('M');
                        _star.SetTemp(Random.Range(2000, 3500));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classM++;
                        break;
                }

                mainStar++;
            }
            else
            {
                var otherStarRand = Random.Range(0, 3);
                var star = new GameObject();
                star.AddComponent<Star>();
                var magicRandom = Random.Range(17.5f, 150f);
                star.transform.localScale = new Vector3(magicRandom, magicRandom, magicRandom);
                var _star = star.GetComponent<Star>();
                Vector3 position;
                do
                {
                    position = Random.insideUnitSphere * 100000;
                    position = new Vector3(position.x, Random.Range(-1000f, 1000f), position.z);
                    star.transform.position = position;
                } while (Physics.OverlapSphere(star.transform.position, 1000f).Length != 0);
                star.AddComponent<SphereCollider>();
                star.GetComponent<SphereCollider>().radius = 1;
                star.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f),Random.Range(0f, 360f));
                switch (otherStarRand)
                {
                    case 0:
                        _star.SetType('T');
                        _star.SetTemp(Random.Range(1000, 2000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(false);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classT++;
                        break;
                    case 1:
                        _star.SetType('W');
                        _star.SetTemp(Random.Range(500, 1000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(false);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classW++;
                        break;
                    case 2:
                        _star.SetType('R');
                        _star.SetTemp(Random.Range(3000, 5000));
                        _star.SetMass(Random.Range(10000f, 100000f));
                        _star.SetStarName(starNameGenerator(_star.GetPType()));
                        _star.SetPlanet(true);
                        _star.SetStarColor(starColorGenerator(_star.GetPType()));
                        _star.SetIntence(IntenceCalc(_star.GetTemp()));
                        classR++;
                        break;
                }

                otherStar++;
            }
        }
    }

    /**
        Funkcja odpowiada za wyświetlanie interfejsu użytkownika.
     */
    private IEnumerator UISet()
    {
        yield return new WaitForSeconds(1);
        UICanv.GetChild(0).GetComponent<Text>().text = "Main stars: " + mainStar;
        UICanv.GetChild(1).GetComponent<Text>().text = "Other stars: " + otherStar;
        UICanv.GetChild(2).GetComponent<Text>().text = "classO stars: " + classO;
        UICanv.GetChild(3).GetComponent<Text>().text = "classB stars: " + classB;
        UICanv.GetChild(4).GetComponent<Text>().text = "classA stars: " + classA;
        UICanv.GetChild(5).GetComponent<Text>().text = "classF stars: " + classF;
        UICanv.GetChild(6).GetComponent<Text>().text = "classG stars: " + classG;
        UICanv.GetChild(7).GetComponent<Text>().text = "classK stars: " + classK;
        UICanv.GetChild(8).GetComponent<Text>().text = "classM stars: " + classM;
        UICanv.GetChild(9).GetComponent<Text>().text = "classT stars: " + classT;
        UICanv.GetChild(10).GetComponent<Text>().text = "classW stars: " + classW;
        UICanv.GetChild(11).GetComponent<Text>().text = "classR stars: " + classR;
        UICanv.GetChild(12).GetComponent<Text>().text = "classN stars: " + classN;
        UICanv.GetChild(13).GetComponent<Text>().text = "Planets: " + planets;
        UICanv.GetChild(14).GetComponent<Text>().text = "Satellites: " + satellite;
        UICanv.GetChild(15).GetComponent<Text>().text = "Exoplanets: " + exo;
    }
    /**
        Funkcja dodaje jedną planetę pozasłoneczną do liczby planet pozasłonecznych.
     */
    public void AddExo()
    {
        exo++;
    }
    /**
        Funkcja dodaje jedną satelitę do liczby satelitów.
     */
    public void AddSat()
    {
        satellite++;
    }
    /**
        Funkcja dodaje jedną planetę do liczby planet. 
     */
    public void AddPlanet()
    {
        planets++;
    }
    /**
        Funkcja oblicza światłość gwiazdy.
        \param[in] temp Temperatura gwiazdy.
        \return intence Liczba światłości gwiazdy.
     */
    private float IntenceCalc(int temp)
    {
        var intence = temp / 5700;
        return intence;
    }
    /**
        Funkcja generuje imię gwiazdy.
        \param[in] typeX Typ gwiazdy.
        \return starN Imię gwiazdy.
     */
    private string starNameGenerator(char typeX)
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
        Funkcja generuje kolor gwiazdy.
        \param[in] ColorX Typ gwiazdy.
        \return starColor Kolor gwiazdy.
     */
    private Color starColorGenerator(char colorX)
    {
        var starColor = Color.white;
        switch (colorX)
        {
            case 'O':
                starColor = new Color(0.1f, 0.7f, 0.7f);
                break;
            case 'B':
                starColor = new Color(0.7f, 1f,1f);
                break;
            case 'A':
                starColor = new Color(1f, 1f, 1f);
                break;
            case 'F':
                starColor = new Color(1f, 1f,0.6f);
                break;
            case 'G':
                starColor = new Color(1f, 0.92f, 0.0016f);
                break;
            case 'K':
                starColor = new Color(1f, 0.6f, 0f);
                break;
            case 'M':
                starColor = new Color(1f, 0f, 0f);
                break;
            case 'T':
                starColor = new Color(1f,0.4f, 0.2f);
                break;
            case 'W':
                starColor = new Color(1f, 1f, 1f);
                break;
            case 'R':
                starColor = new Color(1f, 0.6f, 0f);
                break;
            case 'N':
                starColor = new Color(0f, 0f, 0f);
                break;
        }
        return starColor;
    }
}

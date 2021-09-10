using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] GameObject starHighlighter;
    [SerializeField] TMP_Text selectedSystemTitle;
    [SerializeField] TMP_Text selectedSystemType;
    [SerializeField] Transform starHolder;
    [SerializeField] Transform orbitalHolder;


    private void Awake()
    {
        starHighlighter.SetActive(false);
        selectedSystemType.text = "";
        ClearSystem();
    }

    public void SetSelectedSystem (StarSystem starSystem)
    {
        if (starSystem == null)
        {
            starHighlighter.SetActive(false);
            BuildSystemInterface(null);
        }
        else
        {
            starHighlighter.transform.position = starSystem.transform.position;
            starHighlighter.SetActive(true);

            BuildSystemInterface(starSystem);
        }
    }

    void BuildSystemInterface(StarSystem starSystem)
    {
        if (starSystem == null)
        {
            selectedSystemTitle.text = "None";
            selectedSystemType.text = "";
            selectedSystemTitle.fontStyle = FontStyles.Italic;
            selectedSystemTitle.color = Color.gray;
            SetStar(null);
            ClearSystem();
        }
        else
        {
            selectedSystemTitle.fontStyle = FontStyles.Normal;
            selectedSystemTitle.color = Color.white;
            selectedSystemTitle.text = starSystem.SystemName;
            selectedSystemType.text = "Type: " + starSystem.TypeString;

            SetStar(starSystem);
            ClearSystem();
            BuildSystem(starSystem);
        }
    }

    void SetStar(StarSystem starSystem)
    {
        if (starSystem == null)
        {
            starHolder.GetChild(0).gameObject.SetActive(true);
            for (int i = starHolder.childCount-1; i > 0; i--)
            {
                Destroy(starHolder.GetChild(i).gameObject);
            }
        }
        else
        {
            starHolder.GetChild(0).gameObject.SetActive(false);
            for (int i = starHolder.childCount-1; i > 0; i--)
            {
                Destroy(starHolder.GetChild(i).gameObject);
            }
            GameObject newStar = GameObject.Instantiate(starSystem.GetStarType().prefab, starHolder);
        }
    }

    void BuildSystem(StarSystem starSystem)
    {
        for (int i = 0; i < starSystem.planetDatas.Length; i++)
        {
            Transform orbital = orbitalHolder.GetChild(i);
            Transform body = orbital.GetChild(0);
            Transform belt = orbital.GetChild(1);
            Transform circle = orbital.GetChild(2);

            if (starSystem.planetDatas[i].type == BodyType.Belt)
            { 
                circle.gameObject.SetActive(true);
                belt.gameObject.SetActive(true);
                belt.GetComponent<ParticleSystem>().Pause();
            }
            else
            {
                body.GetComponent<MeshRenderer>().material = starSystem.planetDatas[i].material;
                body.localScale = Vector3.one * starSystem.planetDatas[i].size;
                body.gameObject.SetActive(true);
                circle.gameObject.SetActive(true);

                orbital.eulerAngles = new Vector3(0, 0, starSystem.planetDatas[i].orbitalPeriod);
            }
        }
    }

    void ClearSystem()
    {
        foreach (Transform orbital in orbitalHolder)
        {
            orbital.GetChild(0).gameObject.SetActive(false);
            orbital.GetChild(1).GetComponent<ParticleSystem>().Play();
            orbital.GetChild(1).gameObject.SetActive(false);
            orbital.GetChild(2).gameObject.SetActive(false);
            
        }
    }
}

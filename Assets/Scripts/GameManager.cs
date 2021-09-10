using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public StarType[] starTypes;
    GalaxyGenerator galaxyGenerator;

    string seed;
    float rotation = 300f;
    int arms = 2;

    [SerializeField] TMP_InputField inputField;
    [SerializeField] Slider rotationSlider;
    [SerializeField] TMP_Dropdown armDropdown;
    [SerializeField] IntegerVariable state;

    private void Awake()
    {
        galaxyGenerator = GetComponent<GalaxyGenerator>();
        state.value = 0;
    }

    public void TakeSeedString(string seedString)
    {
        seed = inputField.text;
    }

    public void TakeRotationString(float unused)
    {
        rotation = rotationSlider.value;
    }

    public void TakeArmInput(int selection)
    {
        if (armDropdown.value == 0) arms = 2;
        else if (armDropdown.value == 1) arms = 4;
    }

    public void GenerateGalaxy()
    {
        int seedInt = 0;
        int.TryParse(seed, out seedInt);

        GenerationParameters parameters = new GenerationParameters(seedInt, rotation, arms, starTypes);

        if (state.value == 1) galaxyGenerator.ClearThenGenerate(parameters);
        else if (state.value == 0) galaxyGenerator.Generate(parameters);
    }
}

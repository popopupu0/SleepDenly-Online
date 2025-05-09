using TMPro;
using UnityEngine;

public class CharacterStatsTooltipDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    //public TextMeshProUGUI cardTypesText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI age;
    public TextMeshProUGUI totalHour;
    public TextMeshProUGUI currentHour;

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    [SerializeField] private float lerpFactor = 0.1f;
    [SerializeField] private float xOffset = 200f;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    // void Update()
    // {
    //     if(canvasGroup.alpha != 0)
    //     {
    //         RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 pos);
    //         rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(pos.x + xOffset + 700, pos.y + 540), lerpFactor);
    //     }
    // }

    public void SetStatsText(CharacterStats stats)
    {
        nameText.text = $"{stats.cardName}";
        //cardTypesText.text = string.Join(", ", stats.cardType);
        description.text = stats.description.ToString();
        age.text = stats.ageText.ToString();
        totalHour.text = stats.requiredSleepHours.ToString();
        currentHour.text = stats.currentSleepHours.ToString();
    }
}

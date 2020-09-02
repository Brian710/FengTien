using UnityEngine;

public class HandtemperController : MonoBehaviour
{
    [SerializeField]    Transform parent;
    [SerializeField]    Renderer render;
    [SerializeField]    Collider colli;
    private void Start()
    {
        transform.SetParent(parent, false);
        transform.localPosition = new Vector3(0.02f, -0.01f, -0.07f);
        transform.localRotation = Quaternion.Euler(-90, 0, -90);
        render.enabled = false;
        colli.enabled = false;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.GetTemper).OnGoalStateChange += OnGoalStateChange;
    }
    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.GetTemper).OnGoalStateChange -= OnGoalStateChange;
    }

    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {

        switch (state)
        {
            case Goal.State.WAITING:
                render.enabled = false;
                colli.enabled = false;
                break;
            case Goal.State.CURRENT:
                render.enabled = true;
                colli.enabled = true;
                break;
            case Goal.State.DONE:
                render.enabled = false;
                colli.enabled = false;
                break;
        }
    }
}

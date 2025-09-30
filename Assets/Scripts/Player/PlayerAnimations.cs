using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void ChangeStateVariable(string variable, float value)
    {
        Debug.Log($"����� ����� <color=green>{variable}");
        animator.SetFloat(variable,value);
    }
}

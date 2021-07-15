using UnityEngine;
using System.Collections;

public class NejikoController : MonoBehaviour
{
	const int MinLane = -2;
	const int MaxLane = 2;
	const float LaneWidth = 1.0f;
	const int DefaultLife = 3;
	const float StunDuration = 0.5f;

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float recoverTime = 0.0f;

	public float gravity;
	public float speedZ;
	public float speedX;
	public float speedJump;
	public float accelerationZ;

	public int Life()
	{
		return life;
	}

	public bool IsStan()
	{
		return recoverTime > 0.0f || life <= 0;
	}

	void Start()
	{
		// �ʿ��� ������Ʈ�� �ڵ� ���
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		// ����� ��
		if (Input.GetKeyDown("left")) MoveToLeft();
		if (Input.GetKeyDown("right")) MoveToRight();
		if (Input.GetKeyDown("space")) Jump();

		if (IsStan())
		{
			// �������� ���� ���¿��� ���� ī��Ʈ�� �����Ѵ�
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		}
		else
		{
			// ������ �����Ͽ� Z �������� ��� ������Ų��
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
			moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

			// X������ ��ǥ�� �����Ǳ����� ���� ������ �ӵ��� ���
			float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
			moveDirection.x = ratioX * speedX;
		}

		// �߷¸�ŭ�� ���� �� ������ �߰�
		moveDirection.y -= gravity * Time.deltaTime;

		// �̵� ����
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);

		// �̵� �� �����ϰ� ������ Y ������ �ӵ��� �����Ѵ�
		if (controller.isGrounded) moveDirection.y = 0;

		// �ӵ��� 0 �̻��̸� �޸��� �ִ� �÷��׸� true�� �Ѵ�
		animator.SetBool("run", moveDirection.z > 0.0f);
	}

	// ���� �������� �̵��� ����
	public void MoveToLeft()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane) targetLane--;
	}

	// ������ �������� �̵��� ����
	public void MoveToRight()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane) targetLane++;
	}

	public void Jump()
	{
		if (IsStan()) return;
		if (controller.isGrounded)
		{
			moveDirection.y = speedJump;

			// ���� Ʈ���Ÿ� ����
			animator.SetTrigger("jump");
		}
	}

	// CharacterController�� �浹�� �߻����� ���� ó��
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan()) return;

		if (hit.gameObject.tag == "Robo")
		{
			// �������� ���̰� ���� ���·� ��ȯ
			life--;
			recoverTime = StunDuration;

			// ������ Ʈ���Ÿ� ����
			animator.SetTrigger("damage");

			// ��Ʈ�� ������Ʈ�� ����
			Destroy(hit.gameObject);
		}
	}
}
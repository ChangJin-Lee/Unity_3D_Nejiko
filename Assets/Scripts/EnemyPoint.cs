using UnityEngine;
using System.Collections;

public class EnemyPoint : MonoBehaviour 
{
	public GameObject prefab;


	// Start()는 스크립트가 활성화되어야 실행되고 Awake()는 오브젝트가 활성화되자마자 실행됨.
	// StageGenerator에서 Start()로 스테이지 프리팹을 미리 생성했기 때문에 여기서 Start()보다 먼저 실행되어야함.
	// Start()로 실행하면 StageGenerator랑 같이 실행되므로 Vector3.zero 위치에 적 하나가 만들어져 버림.
	void Awake()
	{
		// 프리팹을 동일 위치에 생성
		GameObject go = (GameObject)Instantiate(
			prefab,
			Vector3.zero,
			Quaternion.identity
			);

		// 함께 삭제되도록 생성한 적 오브젝트를 자식으로 설정
		go.transform.SetParent(transform, false);
	}

	// one으로 하면 Nejiko의 Position을 (1,1,1)로 옮기면 같은 현상이 일어남.
	//void Start ()
	//{
	//	// 프리팹을 동일 위치에 생성
	//	GameObject go = (GameObject)Instantiate(
	//		prefab,
	//		Vector3.one,
	//		Quaternion.identity
	//		);

	//	// 함께 삭제되도록 생성한 적 오브젝트를 자식으로 설정
	//	go.transform.SetParent(transform, false);
	//}
	
	// 스테이지 편집 중이기 때문에 씬에 기즈모를 표시
	void OnDrawGizmos ()
	{
		// 기즈모의 아래 부분이 지면과 같은 높이가 되도록 오프셋을 설정
		Vector3 offset = new Vector3(0, 0.5f, 0);
		
		// 공을 표시
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawSphere(transform.position + offset, 0.5f);
		
		// 프리팹명의 아이콘을 표시
		if (prefab != null)
			Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
	}
}

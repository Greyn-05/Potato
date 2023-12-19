using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake()
    {
        // HealthSystem 컴포넌트를 가져옵니다.
        _healthSystem = GetComponent<HealthSystem>();
        if (_healthSystem != null)
        {
            // OnDeath 이벤트에 HandleDeath 메소드를 구독합니다.
            _healthSystem.OnDeath += HandleDeath;
        }
    }

    private void OnDestroy()
    {
        if (_healthSystem != null)
        {
            // 메모리 누수를 방지하기 위해 이벤트 구독을 해제합니다.
            _healthSystem.OnDeath -= HandleDeath;
        }
    }

    private void HandleDeath()
    {
        // 적 사망시 처리할 로직을 여기에 작성합니다.
        // 예: 사망 애니메이션 재생, 파티클 효과 등
        Debug.Log("적이 사망했습니다.");

        // 선택적: 적 게임 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }
}

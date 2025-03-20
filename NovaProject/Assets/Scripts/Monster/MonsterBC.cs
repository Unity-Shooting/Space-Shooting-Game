using System.Collections;
using UnityEngine;

public class MonsterBC : Monster
{
    protected override void OnEnable()  // Start()랑 같은 역할 한다고 보시면 됩니다
    {
        base.OnEnable();    // 상속받은 Monster 클래스의 OnEnable() 실행. 공통적인 변수들 초기화는 저기서 했음
        StartCoroutine(Death()); // 파괴 애니매이션 테스트용
    }
    void Update()
    {
        Move();
    }

    public override void Shoot()
    {

    }

    IEnumerator Death()  // 파괴 애니메이션 테스트용 코루틴
    {
        yield return new WaitForSeconds(0.1f);
        Die();
        Debug.Log("Death");
    }

    private void OnDisable() // 파괴될 때 정리해야할 것 있으면 여기에서 하기. 지금은 안썼지만 Invoke를 쓰게되면 여기서 캔슬해줄것
    {
    }





}

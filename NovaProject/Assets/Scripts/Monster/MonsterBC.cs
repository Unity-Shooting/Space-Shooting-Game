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


    private void OnBecameInvisible() // 카메라 밖으로 나가면 오브젝트 풀로 반환
    {                               // 다른 함수에서 반환해도 해당 메서드가 실행되기 때문에(비활성화 되면서 카메라에서 사라지는것으로 인식하는듯함)
                                    // Release()가 호출되면 isReleased를 True로 해서 중복실행을 방지함
                                    // 이거 안하니까 같은 오브젝트를 두번씩 반환해서 오브젝트 풀 10칸을 다 쓰고 처음으로 돌아오면
                                    // 같은 오브젝트가 두번씩 호출돼서 한바퀴 돌때마다 두배씩 느려지는 현상이 발생
                                    // 느려지는것뿐만이 아니라 한 오브젝트를 두번 씩 부르니 이것저것 문제가 커서 꼭 신경써야할듯!!
        Debug.Log("인비저블");
        if (!isReleased)
        {
            Release();
        }
    }


}

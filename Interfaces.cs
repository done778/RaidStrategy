
namespace RaidStrategy
{
    // 특수 능력이 있는 캐릭터는 이 인터페이스를 상속 받는다.
    interface ISpecialAbility
    {
        string[] Description { get; set; } // 특수 능력 설명
        void CastingSpecialAbility(); // 특수 능력 발동
        void CheckTriggerCondition(); // 발동 조건
    }
}

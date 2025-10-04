
namespace RaidStrategy
{
    // 특수 능력이 있는 캐릭터는 이 인터페이스를 상속 받는다.
    interface ISpecialAbility
    {
        string[] Description { get; set; } // 특수 능력 설명
        TimingCondition Timing { get; set; }
        void CastingSpecialAbility(CurrentBattleStatus battleStatus); // 특수 능력 발동
    }
}

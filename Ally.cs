using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidStrategy
{
    // 아군 클래스가 공통적으로 가질 요소는 편성 여부
    abstract class Ally : Character
    {
        public bool IsDecking { get; set; }
        public Ally(string name, int att, int hp) : base(name, att, hp)
        {
            IsDecking = false;
        }
        // 슬롯에 그려질 아스키 아트를 캐릭터마다 가지고 있어야 함
        public abstract void DrawAsciiArt(int startX, int startY, bool Info = true);
        public abstract Ally GetClone();
        protected void DrawingImage(int startX, int startY, string[] drawAscii, bool Info)
        {
            for (int i = 0; i < drawAscii.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(drawAscii[i]);
            }
            if (Info) { DrawInfo(startX, startY); }
        }

        // 각 캐릭터 이름, 스탯, 특능, 편성 정보 등을 출력
        protected void DrawInfo(int startX, int startY)
        {
            int cursorX;
            int temp = GameManager.BUFFER_SIZE_WIDTH / 8; //30
            temp += (temp / 2); // 45
            cursorX = temp - (Name.Length + 2); // 41
            int cursorY;
            cursorY = startY + 4;
            Console.SetCursorPosition(startX + cursorX, cursorY);
            Console.Write($"> {Name} <");

            cursorX = temp - 5;
            string att = $"공격력 : {StatusAttack}";
            string hp = $" 체력  : {StatusHealth}";
            cursorY += 2;
            Console.SetCursorPosition(startX + cursorX, cursorY);
            Console.Write(att);
            Console.SetCursorPosition(startX + cursorX, cursorY + 1);
            Console.Write(hp);

            string skill = "- 특수  능력 -";
            cursorX = temp - 7;
            cursorY += 3;
            Console.SetCursorPosition(startX + cursorX, cursorY);
            Console.Write(skill);
            string[] des;
            if (this is ISpecialAbility)
            {
                des = (this as ISpecialAbility).Description;
            }
            else
            {
                des = new string[] { "특수 능력 없음" };
            }
            cursorY += 2;
            for (int i = 0; i < des.Length; i++)
            {
                cursorX = temp - des[i].Length;
                Console.SetCursorPosition(startX + cursorX, cursorY + i);
                Console.Write(des[i]);
            }

            if (IsDecking == true)
            {
                cursorY += 5;
                cursorX = temp - 9;
                Console.SetCursorPosition(startX + cursorX, cursorY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("- 덱에  편성됨 -");
                Console.ResetColor();
            }
        }
    }





    // 8종류의 각 캐릭터들은 처음 생성시 고정된 이름과 스탯을 가진다.
    class SwordMan : Ally
    {
        // 기본 스탯이 높은 탱커
        public SwordMan() : base("검사", 5, 13) { }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii = {
                "                                ",
                "                     +*.        ",
                "         .-==:       %@:        ",
                "       -#+-:-+%=     @@+        ",
                "      :#-.   .:#+.  +@%+        ",
                "      =#:     .++. :@-%+        ",
                "      .*#.. ..+*. :%- %+        ",
                "       ..+%@@#.. .#*. %+        ",
                "          @@:%*  *#.. @:        ",
                "         :@:@*@=+#:  %@         ",
                "         %*.:@%*-%@:.@:         ",
                "         %+ .-@@@=.#@:          ",
                "         %*  .=%=.              ",
                "         =@#.                   ",
                "         +%#@-                  ",
                "         *%.-%=                 ",
                "      .:+%= .-#                 ",
                "     .*#=.  .-*                 ",
                "                                "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new SwordMan(); }
    }





    class Archer : Ally, ISpecialAbility
    {
        public TimingCondition Timing { get; set; }
        public string[] Description { get; set; }
        public Archer() : base("궁사", 6, 5)
        {
            Description = new string[] {
                "턴 종료 전 현재 위치가",
                "   맨 앞이 아닐 경우  ",
                "  지원 사격을 합니다. "
            };
            Timing = TimingCondition.TurnPreEnd;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            if (!battleStatus.Allies[0].Equals(this)) // 맨 앞이 본인이 아니라면
            {
                Attack(battleStatus.CurEnemy); // 적을 공격
                GameManager.AddLogInQueue(new string[] {
                    "특수　능력　발동!",
                    $"궁사가　적에게　{StatusAttack}의　피해를　가합니다."
                }
                );
            }
        }

        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii = {
                "             ..::..             ",
                "           .=#*==*#=.           ",
                "           +%.    .#*           ",
                "           @=      =@           ",
                "           -@-    :%-           ",
                "           .:+%%%%+:.           ",
                "           .:%%@@#:.            ",
                "         -%@*.:#+*+. -%%#:      ",
                "         .*%  -%--%-.  .-%*.    ",
                "           @= -%- **.    -#-    ",
                "    :+:    .. -@= .-.    -#-    ",
                "    =#:       .=+. .-%@@@%+.    ",
                "    -#-     ...::..%*.          ",
                "    .+%=.. .-@*--#@@:           ",
                "      :+%##%#.    .:.           ",
                "           .:     **.           ",
                "           +%     :%:           ",
                "          :@-      #*.          ",
                "         .*+       -*.          "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Archer(); }
    }





    class Scholar : Ally, ISpecialAbility
    {
        int HealPower;
        public TimingCondition Timing { get; set; }
        public string[] Description { get; set; }
        public Scholar() : base("학자", 4, 6)
        {
            Description = new string[] {
                "  매 턴 시작마다   ",
                "맨 앞의 아군 체력을",
                "  2 증가시킵니다.  "
            };
            Timing = TimingCondition.TurnStart;
            HealPower = 2;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            battleStatus.Allies[0].TakeHeal(HealPower);
            GameManager.AddLogInQueue(new string[] {
                    "특수　능력　발동!",
                    "학자가　맨　앞　아군의　체력을　2　증가시킵니다."
                }
            );

        }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii =
            {
                "         .:+**+-.               ",
                "        :#+: .:*#-.             ",
                "       .#-.     +*:             ",
                "       .#=.     +*.             ",
                "        :%*: .:+*:.             ",
                "         .:-++-:.        .:--.  ",
                "        .=..:%@@@%%#-..:%@@@@#: ",
                "      .*@-..+#:::=%@@@%@+:::#=. ",
                "    .*@=.  :@:       *@.   =*:  ",
                "   .+*.    *%        @:   :#=.  ",
                "    .+@=. .@        #%    =*:   ",
                "      ... +%        %:   :#=    ",
                "         .@.......:%@#@@@@#:    ",
                "         +%:::=+#@@@@=.         ",
                "         .:  ...                ",
                "        :%#  .-%*..             ",
                "      .=%=.    .+%=..           ",
                "   .:+%#:.       .#=.           ",
                " .=%*-.           =@:           "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Scholar(); }
    }





    class Berserker : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public TimingCondition Timing { get; set; }
        public Berserker() : base("광전사", 2, 15)
        {
            Description = new string[] {
                "피해를 받으면 ",
                "현재 공격력이 ",
                "2 배가 됩니다."
            };
            Timing = TimingCondition.TakenDamage;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            if (battleStatus.Target.Equals(this))
            {
                StatusAttack *= 2;
                GameManager.AddLogInQueue(new string[] {
                    "특수　능력　발동!",
                    "광전사의　공격력이　2배로　증가합니다."
                }
                );

            }
        }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii = {
                "                     .%%:       ",
                "                     %@:        ",
                "            ..:::.. #@-         ",
                "           .##---*#::- ..-+#+.  ",
                "          .@:     :%: .++*@*:.  ",
                "          :%       #- -#%+:.    ",
                "          .%#.   .+#. ::.       ",
                "          ..:#%%%%-...%.        ",
                "         .@* .-*-   :@:         ",
                "        .%* .%@%-  :@.          ",
                "       .#+.:@@@*. :%:           ",
                "      .#=.   .-* -%-.           ",
                "     :#+.   .=@+-#-.            ",
                "    :*+.   .##:-#=.             ",
                "   :*+.   -@+.:#+:+:.           ",
                " .:#+.   -@-.:#+  +%:           ",
                " .:#:   :@=.:#+    ##.          ",
                "  :*+   .--*#+      %+          ",
                "  .+#:=*#*=.        +@.         "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Berserker(); }


    }





    class Boxer : Ally, ISpecialAbility
    {
        public TimingCondition Timing { get; set; }
        public string[] Description { get; set; }
        public Boxer() : base("격투가", 7, 7)
        {
            Description = new string[] {
                "    자신이 쓰러지면    ",
                "      모든 아군의      ",
                " 공격력이 3배가 됩니다 "
            };
            Timing = TimingCondition.AllyDown;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            if (battleStatus.Target.Equals(this))
            {
                for (int i = 0; i < battleStatus.Allies.Count; i++)
                {
                    battleStatus.Allies[i].TakeBuff(battleStatus.Allies[i].StatusAttack * 2);
                }
                GameManager.AddLogInQueue(new string[] {
                        "특수　능력　발동!",
                        "격투가가　모든　아군의　공격력을　3배　증가시킵니다."
                    }
                );
            }
        }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii = {
                "                                ",
                "                      .-**+:.   ",
                "           .=#***#+.. *@@@@#-   ",
                "          .%*.   .=%: #@@@@%-   ",
                "          :%       #- .-*%@%-   ",
                "          .@+     -%:    -%%-   ",
                "           .+%#+*%+:. -%::*%-   ",
                "           ....:....  #% :**:   ",
                "       .-%@%.-@+..*@@.%+:*%-.   ",
                "      =@@:  .*#      =@+        ",
                "        .#@::%-                 ",
                "           .:#                  ",
                "           .:%=                 ",
                "           ..=%-.               ",
                "         .:#%#+%%=..            ",
                "       :#%#:    .+%*-.          ",
                "       :#%:        :*%#-.       ",
                "        .*#           .+*:      ",
                "                                "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Boxer(); }
    }





    class Fighter : Ally, ISpecialAbility
    {

        public string[] Description { get; set; }
        public TimingCondition Timing { get; set; }
        public Fighter() : base("싸움꾼", 1, 8) 
        {
            Description = new string[] {
                "    공격 후 적의     ",
                "현재 체력의 50% 만큼 ",
                "   피해를 줍니다.    "
            };
            Timing = TimingCondition.AfterAttack;
        }

        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            battleStatus.CurEnemy.TakeDamage(battleStatus.CurEnemy.StatusHealth / 2);
            GameManager.AddLogInQueue(new string[] {
                    "특수　능력　발동!",
                    $"싸움꾼이　적에게　{battleStatus.CurEnemy.StatusHealth / 2}의　피해를　가합니다."
                }
            );
        }

        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii = {
                "                                ",
                "            ..-=-:.             ",
                "           .#*-:-=#+.           ",
                "          .@=     .#=           ",
                "          .@.      #*           ",
                "           -%:   .+@:           ",
                "         .:%@@@%%#-..           ",
                "      .#@%:.#%#+.               ",
                "   -%*#=.  =% .**.              ",
                "  .=@%%-  :@:   .=#@@@@.        ",
                "   :#*:#@@+..                   ",
                "    .=%*:.:#@%=:...             ",
                "       :#@+: ..-*%%#=::.        ",
                "        .:-%%+:.    :-%%%*-..   ",
                "       .*%. .-#%#=:..    .++.   ",
                "      .##.    .=+-*#%*-:.:*+.   ",
                "    .-#*.     .+#:   .-*##+.    ",
                "  .=#*-.      .-=.              ",
                "                                "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Fighter(); }
    }




    class Oracle : Ally, ISpecialAbility
    {
        public TimingCondition Timing { get; set; }
        public string[] Description { get; set; }
        public Oracle() : base("점술사", 2, 8)
        {
            Description = new string[] {
                "   아군이 쓰러지면  ",
                "모든 아군의 공격력이",
                "   2 배가 됩니다.   "
            };
            Timing = TimingCondition.AllyDown;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            for (int i = 0; i < battleStatus.Allies.Count; i++)
            {
                battleStatus.Allies[i].TakeBuff(battleStatus.Allies[i].StatusAttack);
                GameManager.AddLogInQueue(new string[] {
                        "특수　능력　발동!",
                        "점술사가　모든　아군의　공격력을　2배로　증가시켰습니다."
                    }
                );

            }
        }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii =
            {
                "                      =:  :==:. ",
                "       .:==:.        :@%. .-+%: ",
                "     .=#=--=*#:   -%@*-+%@*.=%: ",
                "    .+*:    .-#: ::.#%++%- =*:. ",
                "    .*+.    .-%-:#=.%#:=%*      ",
                "     :#=.  .:%* =@-.            ",
                "      .:#%%%+:   ..+%%#.        ",
                "        --..+#:  +@+.           ",
                "      .+%:+%.-@++*..            ",
                "     .*%: +%  .=*.              ",
                "     +%-  @=                    ",
                "     .+%-.@#                    ",
                "       ...:@=                   ",
                "          .@@=                  ",
                "          :@+#                  ",
                "         .@*:#                  ",
                "        .#%.:#                  ",
                "        +%..:#                  ",
                "        =-..:=                  "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }
        public override Ally GetClone() { return new Oracle(); }
    }





    class Magician : Ally, ISpecialAbility
    {
        public TimingCondition Timing { get; set;}
        public string[] Description { get; set; }
        public Magician() : base("마법사", 12, 2)
        {
            Description = new string[] {
                "    적이 쓰러지면   ",
                "자신의 현재 공격력이",
                "   3 배가 됩니다.   "
            };
            Timing = TimingCondition.EnemyDown;
        }
        public void CastingSpecialAbility(CurrentBattleStatus battleStatus)
        {
            StatusAttack *= 3;
            GameManager.AddLogInQueue(new string[] {
                    "특수　능력　발동!",
                    "마법사의　공격력이　3배로　증가합니다."
                }
            );
            
        }
        public override void DrawAsciiArt(int startX, int startY, bool Info = true)
        {
            string[] drawAscii =
            {
                "                                ",
                "           ....       -***-.    ",
                "         :##+=*#+.   -@..-#-    ",
                "        =%:    .**:  .##+#*.    ",
                "        #+.     -%-   .-%:.     ",
                "        -%:    .++.   .=#.      ",
                "         :#%%%%#=.    .+#       ",
                "      ..#%.:@-=%%%%%%+:**       ",
                "   .:#@*:..@*..........#+       ",
                "    .*@-   @=         .#+       ",
                "      -%-  @=         .#:       ",
                "      ...  *%         .#:       ",
                "           -@+.       .%.       ",
                "          .@=+#.      .%.       ",
                "          #% :%*      =%.       ",
                "         -@: .-%-.    =%        ",
                "       :#%-   .+*.    **        ",
                "     .+#=.     =%-    *=        ",
                "     .:.       .:.              "
            };
            DrawingImage(startX, startY, drawAscii, Info);
        }

        public override Ally GetClone() { return new Magician(); }
    }

}

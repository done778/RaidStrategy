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
        public void DrawingDeath(int startX, int startY)
        {
            string[] drawAscii =
            {
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                   :%%**%#-     ",
                "                  *%:    :*+    ",
                "                  #-      =*:   ",
                "             .-%= *%     :*+    ",
                "          .#@#.    :%@+*%#-     ",
                "        .*%-          .         ",
                "       .#*.          %+         ",
                "       .*..#####:.   %*         ",
                "     .=#: ....:+%-   +@         ",
                "    .:*=.   ..+%-.   =@.        ",
                "  -=+*%-    .##:.    .@-.       ",
                " .:-:       .-:.      --.       ",
                "                                "
            };
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < drawAscii.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(drawAscii[i]);
            }
            Console.ResetColor();
            for (int i = 0; i < drawAscii.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                for (int j = 0; j < drawAscii[0].Length; j++)
                {
                    Console.Write(" ");
                }
                Thread.Sleep(50);
            }
        }
    }

    // 8종류의 각 캐릭터들은 처음 생성시 고정된 이름과 스탯을 가진다.
    class SwordMan : Ally
    {
        // 기본 스탯이 높은 탱커
        public SwordMan() : base("검사", 5, 12) { }
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
    class Fighter : Ally
    {
        // 기본 스탯이 높은 딜러
        public Fighter() : base("싸움꾼", 10, 8) { }
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
    class Berserker : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Berserker() : base("광전사", 2, 15)
        {
            Description = new string[] {
                "피해를 받으면",
                "현재 공격력이",
                "2 배가 됩니다."
            };
        }
        public void UniqueAbility()
        {
            // 피해를 받으면 공격력이 현재 수치의 2배가 됨.
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
    class Archer : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Archer() : base("궁사", 6, 5)
        {
            Description = new string[] {
                "현재 위치가",
                "맨 앞이 아닐 경우",
                "지원 사격을 합니다."
            };
        }
        public void UniqueAbility()
        {
            // 맨 앞에 있지 않으면 지원 공격을 함.
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
    class Boxer : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Boxer() : base("격투가", 7, 7)
        {
            Description = new string[] {
                "공격 후",
                "자신 뒤의 아군에게",
                "피해를 1 줍니다."
            };
        }
        public void UniqueAbility()
        {
            // 공격 후 자신 뒤에 있는 아군에게 1의 피해를 줌.
            // 특수 능력 트리거로서 활용
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
    class Magician : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Magician() : base("마법사", 12, 2)
        {
            Description = new string[] {
                "적이 쓰러지면",
                "현재 공격력이",
                "3 배가 됩니다."
            };
        }
        public void UniqueAbility()
        {
            // 적을 쓰러뜨리면 현재 공격력이 3배가 됨.
            // 엄청난 공격력 뻥튀기, 낮은 체력.
            // 후반에 뻥튀기된 공격력으로 적에게 원기옥 모아서 원킬을 노리는 역할
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
    class Scholar : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Scholar() : base("학자", 4, 8)
        {
            Description = new string[] {
                "매 턴마다",
                "맨 앞의 아군을",
                "2 만큼 회복시킵니다."
            };
        }
        public void UniqueAbility()
        {
            // 매 턴 맨 앞에 위치한 아군의 체력 +2
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
    class Oracle : Ally, ISpecialAbility
    {
        public string[] Description { get; set; }
        public Oracle() : base("점술사", 2, 8)
        {
            Description = new string[] {
                "아군이 쓰러지면",
                "모든 아군의 공격력이",
                "2 배가 됩니다."
            };
        }
        public void UniqueAbility()
        {
            // 아군이 쓰러지면
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
}

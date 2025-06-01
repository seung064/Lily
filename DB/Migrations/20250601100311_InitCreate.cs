using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryNum = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryProperty = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryNum);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductNum = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description_P = table.Column<string>(type: "TEXT", nullable: false),
                    Receipt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductNum);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientNum = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description_I = table.Column<string>(type: "TEXT", nullable: false),
                    CountryNum = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientNum);
                    table.ForeignKey(
                        name: "FK_Ingredient_Countries_CountryNum",
                        column: x => x.CountryNum,
                        principalTable: "Countries",
                        principalColumn: "CountryNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryNum", "CountryName", "CountryProperty" },
                values: new object[,]
                {
                    { 1, "테라노스(흙의 나라)", "거대한 산맥과 광활한 평야 지형\n자원이 풍부하지만 환경은 거칠고 건조함\n지진 및 지열 활동이 활발함" },
                    { 2, "아쿠아리스(물의 나라)", "전체 표면의 95%가 바다로 덮여 있음\n기후가 온화하나 폭풍이 자주 발생\n 수중 생명체와 해양 자원 풍부" },
                    { 3, "실피(바람의 나라)", "대기 밀도가 낮고, 바람이 극도로 강함\n구름이 낮게 깔려 있고, 수증기 성분이 높음\n부유식 생물과 공중 섬이 존재" },
                    { 4, "인페르나(불의 나라)", "활화산과 용암 지대가 곳곳에 분포\n낮은 대기 안정성, 높은 복사열\n강한 자기장과 고온 생물체 서식" },
                    { 5, "몯도샤인", "제5원소를 만들어낸 고대 기술 문명 행성\n 제4원소의 조합을 통해 만들어진 재료와 제5원소를 활성화할 수 있는 에너지 전송 탑이 존재.\n자원들은 이 탑을 통해 실체화 시킬 수 있다." },
                    { 6, "릴리", "제5원소의 결정체이자 생명과 우주의 균형을 지키는 존재\n제4원소의 재료를 통해 필요한 물질을 조합할 수 있는 공간.\n릴리는 물리적 행성처럼 보이지만, 4가지 원소들을 조합할 수 있는 물건에 지나지 않음" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "IngredientNum", "CountryNum", "Description_I", "IngredientName" },
                values: new object[,]
                {
                    { 1, 0, "무기 제작에 사용되는 고강도 금속 원석", "암철석" },
                    { 2, 0, "고열을 견디는 귀금속, 전도성이 뛰어남", "진토금" },
                    { 3, 0, "고대 생물의 화석에서 채굴되는 결정성 자원", "석기정" },
                    { 4, 0, "깊은 해저에서 추출되는 희귀 금속", "심해석" },
                    { 5, 0, "정제된 고순도 수자원, 생명 유지나 에너지 전환에 사용", "청류정수" },
                    { 6, 0, "수중 생명체 뼈대에서 채취한 반투명 결정체", "바다수정" },
                    { 7, 0, "대기 중에서 응결되는 가벼운 결정, 부유 장치 제작에 활용", "풍정석" },
                    { 8, 0, "고속 회전에너지로 저장된 압축 에너지 컨테이너", "회오리 플라스크" },
                    { 9, 0, "대기 진동과 공명하는 음파 기반의 마력 결정", "공명결정" },
                    { 10, 0, "용암 지대에서 채굴되는 고열 금속 결정", "화염정광" },
                    { 11, 0, "천연 용암석으로 이루어진 고열 연료 자원", "용석탄" },
                    { 12, 0, "마그마 생물의 분비물로 형성된 진귀한 에너지 진주", "발화진주" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductNum", "Description_P", "ProductName", "Receipt" },
                values: new object[,]
                {
                    { 1, "고온, 고압의 핵융합 에너지 결정. 무기 코어나 동력 장치에 사용됨", "플라즈마 하트(Plasma Heart)", "화염정광 + 심해석 + 풍정석 + 암철석" },
                    { 2, "공중 부양 능력이 있는 에너지 광물. 공중 장비나 비행체에 쓰임", "천공핵(Skycore Crystal)", "용석탄 + 청류정수 + 공명결정 + 석기정" },
                    { 3, "대지와 불의 융합체로, 강력한 지진 및 폭발 효과를 가진 전술 병기 코어", "지화탄(Geomagma Capsule)", "발화진주 + 바다수정 + 회오리 플라스크 + 진토금" },
                    { 4, "원소의 핵심 에너지 덩어리. 고급 마법 무기나 장비에 필수", "에테르 코어(Aether Core)", "용석탄 + 청류정수 + 풍정석 + 암철석" },
                    { 5, "강한 자기력을 가진 금속 합성체. 방어구 또는 에너지 실드용", "마그네실(Magnesyl Alloy)", "화염정광 + 심해석 + 공명결정 + 진토금" },
                    { 6, "냉각 및 연료 안정화에 사용되는 특수 결정. 에너지 장치 필수 자원", "플루오라이트(Fluorite X)", "발화진주 + 청류정수 + 회오리 플라스크 + 석기정" },
                    { 7, "생명 에너지와 기술이 융합된 생체 동력 코어", "제네시움(Genesium)", "화염정광 + 바다수정 + 공명결정 + 석기정" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_CountryNum",
                table: "Ingredient",
                column: "CountryNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

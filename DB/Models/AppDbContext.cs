using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.EntityFrameworkCore;
using DB.Models; // 네임스페이스와 일치 (끌어오기)


namespace DB.Models
{
    //EF Core가 DB를 관리할 수 있게 해주는 클래스
    public class AppDbContext : DbContext
    {
        // Countries라는 테이블과 연결됨 (DbSet<모델 클래스> 이름)
        public DbSet<Country> Countries { get; set; }


        // DB 연결 문자열 설정
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite 사용, db파일이 없으면 자동 생성
            optionsBuilder.UseSqlite("Data Source=app.db");
        }


        // 모델 추가가 필요한 경우 여기서 처리
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 기본 모델 생성 호출

            // 나라 데이터
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    CountryNum = 1,
                    CountryName = "테라노스(흙의 나라)",
                    CountryProperty = "거대한 산맥과 광활한 평야 지형\n" +
                                                  "자원이 풍부하지만 환경은 거칠고 건조함\n" +
                                                  "지진 및 지열 활동이 활발함"
                },

                new Country
                {
                    CountryNum = 2,
                    CountryName = "아쿠아리스(물의 나라)",
                    CountryProperty = "전체 표면의 95%가 바다로 덮여 있음\n" +
                                                  "기후가 온화하나 폭풍이 자주 발생\n" +
                                                  " 수중 생명체와 해양 자원 풍부"
                },

                new Country
                {
                    CountryNum = 3,
                    CountryName = "실피(바람의 나라)",
                    CountryProperty = "대기 밀도가 낮고, 바람이 극도로 강함\n" +
                                                  "구름이 낮게 깔려 있고, 수증기 성분이 높음\n" +
                                                  "부유식 생물과 공중 섬이 존재"
                },

                new Country
                {
                    CountryNum = 4,
                    CountryName = "인페르나(불의 나라)",
                    CountryProperty = "활화산과 용암 지대가 곳곳에 분포\n" +
                                                  "낮은 대기 안정성, 높은 복사열\n" +
                                                  "강한 자기장과 고온 생물체 서식"
                },

                new Country
                {
                    CountryNum = 5,
                    CountryName = "몯도샤인",
                    CountryProperty = "제5원소를 만들어낸 고대 기술 문명 행성\n " +
                                                  "제4원소의 조합을 통해 만들어진 재료와 제5원소를 활성화할 수 있는 에너지 전송 탑이 존재.\n" +
                                                  "자원들은 이 탑을 통해 실체화 시킬 수 있다."
                },


                new Country
                {
                    CountryNum = 6,
                    CountryName = "릴리",
                    CountryProperty = "제5원소의 결정체이자 생명과 우주의 균형을 지키는 존재\n" +
                                                  "제4원소의 재료를 통해 필요한 물질을 조합할 수 있는 공간.\n" +
                                                  "릴리는 물리적 행성처럼 보이지만, 4가지 원소들을 조합할 수 있는 물건에 지나지 않음",
                }
            );




            // 재료 데이터
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientNum = 1, IngredientName = "암철석", Description_I = "무기 제작에 사용되는 고강도 금속 원석", },
                new Ingredient { IngredientNum = 2, IngredientName = "진토금", Description_I = "고열을 견디는 귀금속, 전도성이 뛰어남", },
                new Ingredient { IngredientNum = 3, IngredientName = "석기정", Description_I = "고대 생물의 화석에서 채굴되는 결정성 자원",  },
                new Ingredient { IngredientNum = 4, IngredientName = "심해석", Description_I = "깊은 해저에서 추출되는 희귀 금속",  },
                new Ingredient { IngredientNum = 5, IngredientName = "청류정수", Description_I = "정제된 고순도 수자원, 생명 유지나 에너지 전환에 사용",  },
                new Ingredient { IngredientNum = 6, IngredientName = "바다수정", Description_I = "수중 생명체 뼈대에서 채취한 반투명 결정체",  },
                new Ingredient { IngredientNum = 7, IngredientName = "풍정석", Description_I = "대기 중에서 응결되는 가벼운 결정, 부유 장치 제작에 활용",  },
                new Ingredient { IngredientNum = 8, IngredientName = "회오리 플라스크", Description_I = "고속 회전에너지로 저장된 압축 에너지 컨테이너",  },
                new Ingredient { IngredientNum = 9, IngredientName = "공명결정", Description_I = "대기 진동과 공명하는 음파 기반의 마력 결정",  },
                new Ingredient { IngredientNum = 10, IngredientName = "화염정광", Description_I = "용암 지대에서 채굴되는 고열 금속 결정", },
                new Ingredient { IngredientNum = 11, IngredientName = "용석탄", Description_I = "천연 용암석으로 이루어진 고열 연료 자원",  },
                new Ingredient { IngredientNum = 12, IngredientName = "발화진주", Description_I = "마그마 생물의 분비물로 형성된 진귀한 에너지 진주", }
            );


            // 조합품 데이터
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductNum = 1,
                    ProductName = "플라즈마 하트(Plasma Heart)",
                    Description_P = "고온, 고압의 핵융합 에너지 결정. 무기 코어나 동력 장치에 사용됨",
                    Receipt = "화염정광 + 심해석 + 풍정석 + 암철석",


                },

                new Product
                {
                    ProductNum = 2,
                    ProductName = "천공핵(Skycore Crystal)",
                    Description_P = "공중 부양 능력이 있는 에너지 광물. 공중 장비나 비행체에 쓰임",
                    Receipt = "용석탄 + 청류정수 + 공명결정 + 석기정"

                },

                new Product
                {
                    ProductNum = 3,
                    ProductName = "지화탄(Geomagma Capsule)",
                    Description_P = "대지와 불의 융합체로, 강력한 지진 및 폭발 효과를 가진 전술 병기 코어",
                    Receipt = "발화진주 + 바다수정 + 회오리 플라스크 + 진토금"
                },

                new Product
                {
                    ProductNum = 4,
                    ProductName = "에테르 코어(Aether Core)",
                    Description_P = "원소의 핵심 에너지 덩어리. 고급 마법 무기나 장비에 필수",
                    Receipt = "용석탄 + 청류정수 + 풍정석 + 암철석"
                },

                new Product
                {
                    ProductNum = 5,
                    ProductName = "마그네실(Magnesyl Alloy)",
                    Description_P = "강한 자기력을 가진 금속 합성체. 방어구 또는 에너지 실드용",
                    Receipt = "화염정광 + 심해석 + 공명결정 + 진토금"

                },

                new Product
                {
                    ProductNum = 6,
                    ProductName = "플루오라이트(Fluorite X)",
                    Description_P = "냉각 및 연료 안정화에 사용되는 특수 결정. 에너지 장치 필수 자원",
                    Receipt = "발화진주 + 청류정수 + 회오리 플라스크 + 석기정"
                },

                new Product
                {
                    ProductNum = 7,
                    ProductName = "제네시움(Genesium)",
                    Description_P = "생명 에너지와 기술이 융합된 생체 동력 코어",
                    Receipt = "화염정광 + 바다수정 + 공명결정 + 석기정",
                }

            );

        }
    }
}

// .csproj 파일이 있는 폴더에서 실행
// dotnet ef database update - DB 업데이트
// dotnet ef migrations list - 생성된 마이그레이션 목록 확인
// dotnet ef migrations add InitCreate - 마이그레이션 생성



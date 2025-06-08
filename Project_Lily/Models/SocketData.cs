
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Lily.Models
{
    public enum SocketDataType
    {
        None = 0, // 초기값
                  // 서버 데이터 처리전
        ServerData,
        // 서버 데이터 처리후
        ServerDataProcessed,
        // 데터 초기화 필요
        ServerDataReset,
        // 클라이언트 데이터 처리전
        ClientData,
        // 클라이언트 데이터 처리후
        ClientDataProcessed,
        // 클라이언트 데이터 초기화 필요
        ClientDataReset,

    }


    public class SocketData
    {
        public static SocketData? Parse(string json) => JsonConvert.DeserializeObject<SocketData>(json);
        // 역 직렬화

        public ulong Id { get; set; } = 0;
        public SocketDataType Status = SocketDataType.None;
        public bool Start { get; set; } = false;

        ///<summary>
        /// 조합 시작 여부
        /// </summary>
        public bool CombinationStart { get; set; } = false;
        /// <summary>
        ///  조합 성공 여부
        /// </summary>
        public bool CombinationSuccess { get; set; } = false;

        /// <summary>
        /// 커맨드 문자열
        /// </summary>
        public string Commend { get; set; } = string.Empty;
        /// <summary>
        /// 업적 달성(퀘스트 완료) 여부
        /// </summary>
        /// <returns></returns>
        public int Achievement { get; set; } = 999;

        /// <summary>
        /// 광석 생성
        /// </summary>
        /// 
        public int OreCount { get; set; } = 999;
        public string ToJsonString() => JsonConvert.SerializeObject(this);
        // JSON 문자열을 객체로 변환하는 메서드 직열화
    }
}

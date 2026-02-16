using System;

namespace IronJade.Util.Core
{
    public class TimeConvenienceModel
    {
        public System.Func<DateTime> ServerTimeUTC { get; private set; }
        public System.Func<DateTime> ServerTimeKST { get; private set; }

        public void SetTime(System.Func<DateTime> serverTimeUTC, System.Func<DateTime> serverTimeKST)
        {
            ServerTimeUTC = serverTimeUTC;
            ServerTimeKST = serverTimeKST;
        }

        /// <summary>
        /// string을 DateTime으로 리턴
        /// </summary>
        public DateTime GetStringToDateTime(string timeString)
        {
            if (DateTimeOffset.TryParse(timeString, out DateTimeOffset dt))
            {
                return dt.DateTime;
            }
            else
            {
                // 여기에 걸리는거 자체가 문제!!
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 남은 시간 리턴
        /// </summary>
        public TimeSpan GetRemainTime(DateTime currentTime, DateTime targetTime)
        {
            return targetTime - currentTime;
        }

        /// <summary>
        /// 남은 시간 리턴
        /// </summary>
        public TimeSpan GetRemainTimeUTC(DateTime targetTime)
        {
            return targetTime - ServerTimeUTC();
        }

        /// <summary>
        /// 남은 시간 리턴
        /// </summary>
        public TimeSpan GetRemainTimeKST(DateTime targetTime)
        {
            return targetTime - ServerTimeKST();
        }

        /// <summary>
        /// 경과 시간 리턴
        /// </summary>
        public TimeSpan GetElapsedTimeUTC(DateTime targetTime)
        {
            return ServerTimeUTC() - targetTime;
        }

        /// <summary>
        /// 현재 시간을 Integer로 리턴해준다.
        /// </summary>
        public int GetTimeToInteger(DateTime targetTime, TimeToIntegerType timeToIntegerType)
        {
            switch (timeToIntegerType)
            {
                case TimeToIntegerType.YYYY_MM_DD:
                    {
                        // Year : 20240000
                        // Month : 1200
                        // Day : 31
                        // Result : 20241231
                        return (targetTime.Year * 10000) + (targetTime.Month * 100) + targetTime.Day;
                    }
            }

            return 0;
        }

        /// <summary>
        /// 아직 시간이 남았는지 체크
        /// </summary>
        public bool CheckRemainTime(DateTime currentTime, DateTime targetTime)
        {
            TimeSpan remainTime = GetRemainTime(currentTime, targetTime);

            return remainTime.TotalMilliseconds > 0;
        }

        /// <summary>
        /// 아직 시간이 남았는지 체크
        /// </summary>
        public bool CheckRemainTimeUTC(DateTime targetTime)
        {
            TimeSpan remainTime = GetRemainTime(ServerTimeUTC(), targetTime);

            return remainTime.TotalMilliseconds > 0;
        }

        /// <summary>
        /// 아직 시간이 남았는지 체크
        /// </summary>
        public bool CheckRemainTimeKST(DateTime targetTime)
        {
            TimeSpan remainTime = GetRemainTime(ServerTimeKST(), targetTime);

            return remainTime.TotalMilliseconds > 0;
        }

        /// <summary>
        /// 오픈 시간 체크
        /// </summary>
        public bool CheckOpenTimeUTC(DateTime startTime, DateTime endTime)
        {
            bool isStart = !ConvenienceModel.Time.CheckRemainTimeUTC(startTime);
            bool isEnd = !ConvenienceModel.Time.CheckRemainTimeUTC(endTime);

            // 이벤트 시간이 지났으면 넘어간다.
            if (isEnd)
                return false;

            // 아직 이벤트 시작 시간이 아니면 넘어간다.
            if (!isStart)
                return false;

            return true;
        }

        /// <summary>
        /// 오늘인지 체크
        /// </summary>
        public bool CheckToday(DateTime targetTime)
        {
            var elapsedTime = GetElapsedTimeUTC(targetTime);

            return elapsedTime.Days == 0;
        }
    }
}


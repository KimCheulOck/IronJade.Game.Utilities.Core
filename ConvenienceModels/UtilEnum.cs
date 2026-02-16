public enum CompareType
{
    Equal = 0,
    Greater = 1,
    GreaterEqual = 2,
    Less = 3,
    LessEqual = 4
}

public enum RemainTimeStringType
{
    RemainDaysHours = 0,        //  $"{days}일 {hours}시간 남음";
    RemainHoursMinutes,			//  $"{hours}시간 {minutes}분 남음";
    RemainMinutesSeconds,		//  $"{minutes}분 {seconds}초 남음";

    RemainMonths,               //  $"{months}달 남음";
    RemainDays,                 //  $"{days}일 남음";
    RemainHours,                //  $"{hours}시간 남음";
    RemainMinutes,              //  $"{minutes}분 남음";
    RemainSeconds,              //  $"{seconds}초 남음";

    Hours_Minutes_Seconde,      // 09 : 30 : 58 pm
}

public enum ElapsedTimeStringType
{
    ElapsedMonths,         //{0}달 전
    ElapsedDays,             //{0}일 전
    ElapsedHours,            //{0}시간 전
    ElapsedMinutes,         //{0}분 전
    ElapsedSeconds,         //방금 전
}


public enum DateTimeStringType
{
    Today,                  // 오늘
}

public enum TimeStringTextType
{
    One,        // {0}일 남음
    Two,        // (0}일 {1}시간 남음
    Three,      // {0}일 {1}시간 {2}분 남음
}

public enum TimeStringFormType
{
    // H 24시간 기준
    // h 12시간 기준
    // M 월
    // m 분

    hh_mm_ss = 0,               // hh:mm:ss
    yyyy_MM_dd_HH_mm_ss,        // yyyy-MM-dd HH:mm:ss
    yyyy_MM_dd,                 // yyyy-MM-dd
    HH_mm,                      // HH:mm
    hhH_mmM,		            // hhH mmM

    hh_mm_ss_tt,                // hh:mm:ss tt (09 : 10 : 59 AM)
    HHH_mm_ss,                  // HHH:mm:ss (100: 59 : 59)

    // '__'가 '/' 인걸로
    yyyy__MM__dd,               // yyyy/mm/dd

}

public enum TimeToIntegerType
{
    YYYY_MM_DD, // 20241231
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

namespace IronJade.Util.Core
{
    public class StringConvenienceModel
    {
        // 국가 코드와 언어 코드를 매핑하는 딕셔너리
        // 사용하는 코드만 제외하고 주석할 것
        private Dictionary<string, string> countryToLocaleMap = new Dictionary<string, string>()
        {
            //{"ZA", "af_ZA"}, // 남아프리카 공화국 (아프리칸스어)
            //{"SA", "ar_SA"}, // 사우디아라비아 (아랍어)
            //{"IN", "as_IN"}, // 인도 (아삼어)
            //{"AZ", "az_AZ"}, // 아제르바이잔 (아제르바이잔어)
            //{"BG", "bg_BG"}, // 불가리아 (불가리아어)
            //{"IN", "bn_IN"}, // 인도 (벵골어)
            //{"ES", "ca_ES"}, // 스페인 (카탈로니아어)
            //{"CZ", "cs_CZ"}, // 체코 (체코어)
            //{"DK", "da_DK"}, // 덴마크 (덴마크어)
            //{"DE", "de_DE"}, // 독일 (독일어)
            //{"GR", "el_GR"}, // 그리스 (그리스어)
            //{"AU", "en_AU"}, // 호주 (영어)
            //{"GB", "en_GB"}, // 영국 (영어)
            //{"US", "en_US"}, // 미국 (영어)
            //{"ES", "es_ES"}, // 스페인 (스페인어)
            //{"US", "es_US"}, // 미국 라틴계 (스페인어)
            //{"EE", "et_EE"}, // 에스토니아 (에스토니아어)
            //{"ES", "eu_ES"}, // 스페인 (바스크어)
            //{"IR", "fa_IR"}, // 이란 (페르시아어)
            //{"FI", "fi_FI"}, // 핀란드 (핀란드어)
            //{"CA", "fr_CA"}, // 캐나다 (프랑스어)
            //{"FR", "fr_FR"}, // 프랑스 (프랑스어)
            //{"IE", "ga_IE"}, // 아일랜드 (아일랜드어)
            //{"ES", "gl_ES"}, // 스페인 (갈라시아어)
            //{"IN", "gu_IN"}, // 인도 (구자라티어)
            //{"IL", "he_IL"}, // 이스라엘 (히브리어)
            //{"IN", "hi_IN"}, // 인도 (힌디어)
            //{"HR", "hr_HR"}, // 크로아티아 (크로아티아어)
            //{"HU", "hu_HU"}, // 헝가리 (헝가리어)
            //{"AM", "hy_AM"}, // 아르메니아 (아르메니아어)
            //{"ID", "id_ID"}, // 인도네시아 (인도네시아어)
            //{"IS", "is_IS"}, // 아이슬란드 (아이슬란드어)
            //{"IT", "it_IT"}, // 이탈리아 (이탈리아어)
            //{"JP", "ja_JP"}, // 일본 (일본어)
            //{"GE", "ka_GE"}, // 조지아 (조지아어)
            //{"KZ", "kk_KZ"}, // 카자흐스탄 (카자흐어)
            //{"KH", "km_KH"}, // 캄보디아 (크메르어)
            //{"IN", "kn_IN"}, // 인도 (칸나다어)
            //{"KR", "ko_KR"}, // 한국 (한국어)
            //{"KG", "ky_KG"}, // 키르기스스탄 (키르기스어)
            //{"LA", "lo_LA"}, // 라오스 (라오어)
            //{"LT", "lt_LT"}, // 리투아니아 (리투아니아어)
            //{"LV", "lv_LV"}, // 라트비아 (라트비아어)
            //{"MK", "mk_MK"}, // 마케도니아 (마케도니아어)
            //{"IN", "ml_IN"}, // 인도 (말라얄람어)
            //{"MN", "mn_MN"}, // 몽골 (몽골어)
            //{"IN", "mr_IN"}, // 인도 (마라티어)
            //{"MY", "ms_MY"}, // 말레이시아 (말레이어)
            //{"MM", "my_MM"}, // 미얀마 (미얀마어)
            //{"NO", "nb_NO"}, // 노르웨이 (노르웨이어)
            //{"IN", "ne_IN"}, // 네팔 (네팔어)
            //{"NL", "nl_NL"}, // 네덜란드 (네덜란드어)
            //{"IN", "or_IN"}, // 인도 (오리야어)
            //{"IN", "pa_IN"}, // 인도 (펀자브어)
            //{"PL", "pl_PL"}, // 폴란드 (폴란드어)
            //{"BR", "pt_BR"}, // 브라질 (포르투갈어)
            //{"PT", "pt_PT"}, // 포르투갈 (포르투갈어)
            //{"RO", "ro_RO"}, // 루마니아 (루마니아어)
            //{"RU", "ru_RU"}, // 러시아 (러시아어)
            //{"LK", "si_LK"}, // 스리랑카 (싱할라어)
            //{"SK", "sk_SK"}, // 슬로바키아 (슬로바키아어)
            //{"SI", "sl_SI"}, // 슬로베니아 (슬로베니아어)
            //{"AL", "sq_AL"}, // 알바니아 (알바니아어)
            //{"RS", "sr_RS"}, // 세르비아 (세르비아어)
            //{"SE", "sv_SE"}, // 스웨덴 (스웨덴어)
            //{"TH", "th_TH"}, // 태국 (태국어)
            //{"TR", "tr_TR"}, // 터키 (터키어)
            //{"UA", "uk_UA"}, // 우크라이나 (우크라이나어)
            //{"PK", "ur_PK"}, // 파키스탄 (우르두어)
            //{"UZ", "uz_UZ"}, // 우즈베키스탄 (우즈베크어)
            //{"VN", "vi_VN"}, // 베트남 (베트남어)
            //{"CN", "zh_CN"}, // 중국 (중국어)
            //{"HK", "zh_HK"}, // 홍콩 (홍콩어)
            //{"TW", "zh_TW"}, // 대만 (대만어)
            //{"ZA", "zu_ZA"}  // 남아프리카 공화국 (줄루어)
        };

        private string[] remainTimeStringFormats = null;
        private string[] elapsedTimeStringFormats = null;
        private string[] dateTimeStringFormats = null;

        /// <summary>
        /// 남은시간과 관련한 로컬값
        /// </summary>
        public void SetRemainTimeStringFormats(string[] formats)
        {
            remainTimeStringFormats = formats;
        }

        /// <summary>
        /// 지나간 시간과 관련한 로컬값
        /// </summary>
        public void SetElapsedTimeStringFormats(string[] formats)
        {
            elapsedTimeStringFormats = formats;
        }

        /// <summary>
        /// 현재 시간과 관련한 로컬값
        /// </summary>
        public void SetDateTimeStringFormats(string[] formats)
        {
            dateTimeStringFormats = formats;
        }

        public string GetColorText(string text, Color color, bool isLineReplace = false)
        {
            int PADDING = 2;
            int LENGTH_FIXED_TEXT = 24 + PADDING;
            StringBuilder colorCode = new StringBuilder(text.Length + LENGTH_FIXED_TEXT);

            colorCode.Append("<color=#");
            colorCode.Append(ColorUtility.ToHtmlStringRGBA(color));
            colorCode.Append(">");
            colorCode.Append(text);
            colorCode.Append("</color>");

            if (isLineReplace)
                return colorCode.Replace("\n", "").ToString();

            return colorCode.ToString();
        }

        public string GetHtmlCodeText(string text, string htmlString)
        {
            int PADDING = 2;
            int LENGTH_FIXED_TEXT = 24 + PADDING;
            StringBuilder colorCode = new StringBuilder(text.Length + LENGTH_FIXED_TEXT);

            colorCode.Append("<color=#");
            colorCode.Append(htmlString);
            colorCode.Append(">");
            colorCode.Append(text);
            colorCode.Append("</color>");

            return colorCode.ToString();
        }

        public string GetNumberOfDigitsText(int number)
        {
            return string.Format("{0:#,0}", number);
        }

        public string GetNumberOfDigitsText(float number)
        {
            return string.Format("{0:#,0}", number);
        }

        public string GetNumberOfDigitsText(decimal number)
        {
            return string.Format("{0:#,0}", number);
        }

        public string GetNumberOfDigitsText(long number)
        {
            return string.Format("{0:#,0}", number);
        }

        /// <summary>
        /// 1000 단위가 넘는 숫자를 1K, 1M 등으로 표기시킵니다.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="targetNumber">표기단위 최소치입니다. targertNumber 이상인 경우만 단위를 붙여 표기합니다.</param>
        /// <param name="isRealNumber">소수점 표시 여부입니다.</param>
        /// <returns></returns>
        public string GetLargeNumberText(long number, int targetNumber = 100000, bool isRealNumber = false)
        {
            if (number < targetNumber)
                return number.ToString();

            StringBuilder sb = new StringBuilder();
            string[] suffix = new string[] { "", "K", "M", "B" };

            int suffixIndex = (int)Math.Min(suffix.Length - 1, Math.Floor(Math.Log10(number) / 3));

            if (isRealNumber)
                sb.Append(Math.Round(number / Math.Pow(1000, suffixIndex), 2));
            else
                sb.Append((int)Math.Round(number / Math.Pow(1000, suffixIndex), 2));

            sb.Append(suffix[suffixIndex]);

            return sb.ToString();
        }

        /// <summary>
        /// 두 날짜간 '달' 차이 구하기
        /// time1이 뒷 시간
        /// tiem2이 앞 시간
        /// </summary>
        public int GetMonthDifference(DateTime time1, DateTime time2)
        {
            return (12 * (time1.Year - time2.Year)) + (time1.Month - time2.Month);
        }

        /// <summary>
        /// TimeStringTextType.Two가 많이 쓰이니까 기본은 Two로..
        /// </summary>
        public string GetRemainTimeLocalizationText(DateTime nowTime, DateTime endTime, TimeStringTextType timeStringTextType = TimeStringTextType.Two)
        {
            TimeSpan remainTime = endTime - nowTime;

            switch (timeStringTextType)
            {
                case TimeStringTextType.One:
                    {
                        int month = GetMonthDifference(endTime, nowTime);

                        if (month > 0 && remainTime.Days >= 30)
                            return GetRemainTimeString(RemainTimeStringType.RemainMonths, month);
                        else if (remainTime.Days > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainDays, remainTime.Days);
                        else if (remainTime.Hours > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainHours, remainTime.Hours);
                        else if (remainTime.Minutes > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainMinutes, remainTime.Minutes);
                        else
                            return GetRemainTimeString(RemainTimeStringType.RemainSeconds, remainTime.Seconds);
                    }

                case TimeStringTextType.Two:
                    {
                        if (remainTime.Days > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainDaysHours, remainTime.Days, remainTime.Hours);
                        else if (remainTime.Hours > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainHoursMinutes, remainTime.Hours, remainTime.Minutes);
                        else if (remainTime.Minutes > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainMinutesSeconds, remainTime.Minutes, remainTime.Seconds);
                        else
                            return GetRemainTimeString(RemainTimeStringType.RemainSeconds, remainTime.Seconds);
                    }

                case TimeStringTextType.Three:
                    {
                        break;
                    }
            }

            return string.Empty;
        }

        public string GetRemainTimeLocalizationText(TimeSpan remainTime, TimeStringTextType timeStringTextType = TimeStringTextType.Two)
        {
            switch (timeStringTextType)
            {
                case TimeStringTextType.One:
                    {
                        if (remainTime.Days > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainDays, remainTime.Days);
                        else if (remainTime.Hours > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainHours, remainTime.Hours);
                        else if (remainTime.Minutes > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainMinutes, remainTime.Minutes);
                        else
                            return GetRemainTimeString(RemainTimeStringType.RemainSeconds, remainTime.Seconds);
                    }

                case TimeStringTextType.Two:
                    {
                        if (remainTime.Days > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainDaysHours, remainTime.Days, remainTime.Hours);
                        else if (remainTime.Hours > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainHoursMinutes, remainTime.Hours, remainTime.Minutes);
                        else if (remainTime.Minutes > 0)
                            return GetRemainTimeString(RemainTimeStringType.RemainMinutesSeconds, remainTime.Minutes, remainTime.Seconds);
                        else
                            return GetRemainTimeString(RemainTimeStringType.RemainSeconds, remainTime.Seconds);
                    }

                case TimeStringTextType.Three:
                    {
                        break;
                    }
            }

            return string.Empty;
        }

        public string GetRemainTimeLocalizationTextForcedType(DateTime nowTime, DateTime endTime, RemainTimeStringType forcedType)
        {
            TimeSpan remainTime = endTime - nowTime;

            switch (forcedType)
            {
                case RemainTimeStringType.RemainDaysHours:
                    {
                        int days = (int)Math.Floor(remainTime.TotalDays);
                        int hours = (int)Math.Floor(remainTime.Subtract(TimeSpan.FromDays(days)).TotalHours);

                        return GetRemainTimeString(RemainTimeStringType.RemainDaysHours, days, hours);
                    }
                case RemainTimeStringType.RemainHoursMinutes:
                    {
                        int hours = (int)Math.Floor(remainTime.TotalHours);
                        int minutes = (int)Math.Floor(remainTime.Subtract(TimeSpan.FromHours(hours)).TotalMinutes);

                        return GetRemainTimeString(RemainTimeStringType.RemainHoursMinutes, hours, minutes);
                    }
                case RemainTimeStringType.RemainMinutesSeconds:
                    {
                        int minutes = (int)Math.Floor(remainTime.TotalMinutes);
                        int seconds = (int)Math.Floor(remainTime.Subtract(TimeSpan.FromMinutes(minutes)).TotalSeconds);

                        return GetRemainTimeString(RemainTimeStringType.RemainMinutesSeconds, minutes, seconds);
                    }
                default:
                    {
                        int days = (int)Math.Floor(remainTime.TotalDays);
                        int hours = (int)Math.Floor(remainTime.Subtract(TimeSpan.FromDays(days)).TotalHours);

                        return GetRemainTimeString(RemainTimeStringType.RemainDaysHours, days, hours);
                    }
            }
        }

        public string GetRemainTimeToString(DateTime nowTime, DateTime endTime, TimeStringFormType timeStringFormType)
        {
            return GetRemainTimeToString(endTime - nowTime, timeStringFormType);
        }

        public string GetRemainTimeToString(TimeSpan remainTime, TimeStringFormType timeStringFormType)
        {
            ShowLogErrorByRemainTimeFormType(timeStringFormType);

            switch (timeStringFormType)
            {
                case TimeStringFormType.hh_mm_ss:
                    {
                        return remainTime.ToString("hh\\:mm\\:ss");
                    }

                case TimeStringFormType.HH_mm:
                    {
                        return remainTime.ToString("hh\\:mm");
                    }

                case TimeStringFormType.hhH_mmM:
                    {
                        return string.Format("{0}H {1}M", (int)remainTime.TotalHours, remainTime.Minutes);
                    }

                case TimeStringFormType.HHH_mm_ss:
                    {
                        return string.Format("{0:D2}H:{1:D2}M:{2:D2}S", (int)remainTime.TotalHours, remainTime.Minutes, remainTime.Seconds);
                    }

                default:
                    {
                        return remainTime.ToString("hh\\:mm\\:ss");
                    }
            }
        }

        public string GetElapsedTimeToString(DateTime nowTime, DateTime startTime)
        {
            TimeSpan timeSpan = nowTime - startTime;
            int month = GetMonthDifference(nowTime, startTime);
            if (month > 0 && timeSpan.Days > 30)
                return GetElapsedTimeString(ElapsedTimeStringType.ElapsedMonths, month);
            else
                return GetElapsedTimeToString(nowTime - startTime);
        }

        public string GetElapsedTimeToString(TimeSpan elapsedTime)
        {
            if (elapsedTime.Days > 0)
                return GetElapsedTimeString(ElapsedTimeStringType.ElapsedDays, elapsedTime.Days);
            else if (elapsedTime.Hours > 0)
                return GetElapsedTimeString(ElapsedTimeStringType.ElapsedHours, elapsedTime.Hours);
            else if (elapsedTime.Minutes > 0)
                return GetElapsedTimeString(ElapsedTimeStringType.ElapsedMinutes, elapsedTime.Minutes);
            else
                return GetElapsedTimeString(ElapsedTimeStringType.ElapsedSeconds);
        }

        private void ShowLogErrorByRemainTimeFormType(TimeStringFormType timeStringFormType)
        {
            switch (timeStringFormType)
            {
                case TimeStringFormType.yyyy_MM_dd_HH_mm_ss:
                case TimeStringFormType.yyyy_MM_dd:
                    {
                        Debug.LogError("지원하지 않는 형식입니다.");
                        break;
                    }
            }
        }

        /// <summary>
        /// 포멧 형식에 맞게 string으로 변환해준다.
        /// isTodayTime이 true면 타겟 날짜가 오늘 일 경우 Today로 변환한다.
        /// </summary>
        public string GetTimeToString(DateTime time, TimeStringFormType timeStringFormType, bool isTodayTime = false)
        {
            if (isTodayTime)
            {
                if (ConvenienceModel.Time.CheckToday(time))
                    return GetDateTimeString(DateTimeStringType.Today);
            }

            switch (timeStringFormType)
            {
                case TimeStringFormType.hh_mm_ss:
                    {
                        return time.ToString("hh:mm:ss");
                    }

                case TimeStringFormType.hh_mm_ss_tt:
                    {
                        // 나중에 띄어쓰기나 소대문자 등이 명확해지면 통일시키자.
                        return time.ToString("hh : mm : ss tt").ToLower();
                    }

                case TimeStringFormType.yyyy_MM_dd_HH_mm_ss:
                    {
                        return time.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                case TimeStringFormType.yyyy_MM_dd:
                    {
                        return time.ToString("yyyy-MM-dd");
                    }

                case TimeStringFormType.HH_mm:
                    {
                        return time.ToString("HH:mm");
                    }

                case TimeStringFormType.yyyy__MM__dd:
                    {
                        return time.ToString("yyyy/MM/dd");
                    }

                default:
                    {
                        return time.ToString("hh:mm:ss");
                    }
            }
        }

        public string GetMillisecondRoundTimeText(int time, int round = 1)
        {
            return $"{System.Math.Round(time / 1000.0f, round)}s";
        }

        /// <summary> 로마숫자로 표기, (40 이하만) </summary>
        public string ToRoman(int number)
        {
            if (number >= 40)
                return string.Empty;

            if (number >= 10)
                return string.Format("X{0}", ToRoman(number - 10));

            if (number >= 9)
                return string.Format("IX{0}", ToRoman(number - 9));

            if (number >= 5)
                return string.Format("V{0}", ToRoman(number - 5));

            if (number >= 4)
                return string.Format("IV{0}", ToRoman(number - 4));

            if (number >= 1)
                return string.Format("I{0}", ToRoman(number - 1));

            return string.Empty;
        }

        /// <summary>
        /// 파일용량 표기
        /// </summary>
        public string GetBytesReadable(long size)
        {
            long absolute = size < 0 ? -size : size;

            string suffix;
            double readable;

            if (absolute >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = size >> 50;
            }
            else if (absolute >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = size >> 40;
            }
            else if (absolute >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = size >> 30;
            }
            else if (absolute >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = size >> 20;
            }
            else if (absolute >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = size >> 10;
            }
            else if (absolute >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = size;
            }
            else
            {
                return size.ToString("0 B"); // Byte
            }

            readable = readable / 1024;
            return readable.ToString("0.### ") + suffix;
        }

        public string CompressString(string uncompressed)
        {
            var bytes = Encoding.UTF8.GetBytes(uncompressed);
            using (var compressedStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    gZipStream.Write(bytes, 0, bytes.Length);
                }
                bytes = compressedStream.ToArray();
            }
            return Convert.ToBase64String(bytes);
        }

        public string UncompressString(string compressed)
        {
            var bytes = Convert.FromBase64String(compressed);
            var buffer = new byte[4096];
            var uncompressedStream = new MemoryStream();
            using (var compressedStream = new MemoryStream(bytes))
            {
                using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    var length = 0;
                    do
                    {
                        length = gZipStream.Read(buffer, 0, 4096);
                        if (length > 0)
                            uncompressedStream.Write(buffer, 0, length);
                    }
                    while (length > 0);
                }
            }

            return Encoding.UTF8.GetString(uncompressedStream.ToArray());
        }

        /// <summary>
        /// 국가 코드를 Locale 형식으로 변환하는 함수
        /// </summary>
        public string GetLocaleFromCountryCode(string countryCode)
        {
            // CultureInfo를 통해 국가 코드에 대한 언어와 지역 정보를 추정
            try
            {
                return countryToLocaleMap[countryCode];
            }
            catch (CultureNotFoundException)
            {
                // 매핑되지 않은 경우 기본값 반환 (예: en_US)
                return "en_US";
            }
        }

        /// <summary>
        /// 현재 문화권의 이름에서 Locale 형식으로 변환하는 함수
        /// </summary>
        public string GetLocaleFromCurrentCulture()
        {
            return System.Globalization.CultureInfo.CurrentCulture.Name.Replace('-', '_');
        }


        public void Copy(string text)
        {
#if UNITY_ANDROID || UNITY_IOS
            TextEditor textEditor = new TextEditor
            {
                text = text
            };
            textEditor.SelectAll();
            textEditor.Copy();
#else
            GUIUtility.systemCopyBuffer = text;
#endif
        }

        public string Paste()
        {
#if UNITY_ANDROID || UNITY_IOS
            TextEditor textEditor = new TextEditor();
            textEditor.Paste();

            return textEditor.text;
#else
            return GUIUtility.systemCopyBuffer;
#endif
        }

        private string GetRemainTimeString(RemainTimeStringType timeStringType, params object[] values)
        {
            try
            {
                return string.Format(remainTimeStringFormats[(int)timeStringType], values);
            }
            catch (Exception e)
            {
                IronJade.Debug.LogError($"[GetTimeString] Error!! Time String Parse : {timeStringType}, {values}, {e.Message}, {e.StackTrace}");
                return string.Empty;
            }
        }

        private string GetElapsedTimeString(ElapsedTimeStringType timeStringType, params object[] values)
        {
            try
            {
                return string.Format(elapsedTimeStringFormats[(int)timeStringType], values);
            }
            catch (Exception e)
            {
                IronJade.Debug.LogError($"[GetTimeString] Error!! Time String Parse : {timeStringType}, {values}, {e.Message}, {e.StackTrace}");
                return string.Empty;
            }
        }

        private string GetDateTimeString(DateTimeStringType timeStringType, params object[] values)
        {
            try
            {
                return string.Format(dateTimeStringFormats[(int)timeStringType], values);
            }
            catch (Exception e)
            {
                IronJade.Debug.LogError($"[GetTimeString] Error!! Time String Parse : {timeStringType}, {values}, {e.Message}, {e.StackTrace}");
                return string.Empty;
            }
        }
    }
}


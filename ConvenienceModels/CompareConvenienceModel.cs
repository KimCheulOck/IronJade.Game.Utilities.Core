using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace IronJade.Util.Core
{
    public class CompareConvenienceModel
    {
        public bool CompareByInt(CompareType compareType, long target1, long target2)
        {
            switch (compareType)
            {
                case CompareType.Equal:
                    {
                        return target1 == target2;
                    }

                case CompareType.Greater:
                    {
                        return target1 > target2;
                    }

                case CompareType.GreaterEqual:
                    {
                        return target1 >= target2;
                    }

                case CompareType.Less:
                    {
                        return target1 < target2;
                    }

                case CompareType.LessEqual:
                    {
                        return target1 <= target2;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        public int SortInteger(int value1, int value2, int prevResult)
        {
            if (prevResult == 0)
                return value1 - value2;

            return prevResult;
        }

        public int SortIntegerWithZeroLowest(int value1, int value2, int prevResult)
        {
            if (prevResult == 0)
            {
                if (value1 == 0)
                    return 1;

                if (value2 == 0)
                    return -1;
            }

            return SortInteger(value1, value2, prevResult);
        }

        public int SortLong(long value1, long value2, int prevResult)
        {
            if (prevResult == 0)
                return (int)(value1 - value2);

            return prevResult;
        }

        public int SortString(string value1, string value2, int prevResult)
        {
            if (prevResult == 0)
                return value1.CompareTo(value2);

            return prevResult;
        }

        public int SortDateTime(System.DateTime value1, System.DateTime value2, int prevResult)
        {
            if (prevResult == 0)
                return value1.CompareTo(value2);

            return prevResult;
        }

        public void Sort<T, TKey>(List<T> list, Func<T, TKey> keySelector, SortDirection direction = SortDirection.Ascending) where TKey : IComparable<TKey>
        {
            if (list.Count < 100)
                SortBy(list, keySelector, direction);
            else
                QuickSort(list, keySelector, direction);
        }

        // 특정 속성 기준 정렬
        private void SortBy<T, TKey>(List<T> list, Func<T, TKey> keySelector, SortDirection direction = SortDirection.Ascending) where TKey : IComparable<TKey>
        {
            int n = list.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    int comparison = keySelector(list[j]).CompareTo(keySelector(list[j + 1]));
                    bool shouldSwap = direction == SortDirection.Ascending ? comparison > 0 : comparison < 0;

                    if (shouldSwap)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        // 퀵소트 방식 (더 효율적)
        private void QuickSort<T, TKey>(List<T> list, Func<T, TKey> keySelector, SortDirection direction = SortDirection.Ascending) where TKey : IComparable<TKey>
        {
            QuickSortInternal(list, 0, list.Count - 1, keySelector, direction);
        }

        private void QuickSortInternal<T, TKey>(List<T> list, int left, int right, Func<T, TKey> keySelector, SortDirection direction) where TKey : IComparable<TKey>
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, left, right, keySelector, direction);
                QuickSortInternal(list, left, pivotIndex - 1, keySelector, direction);
                QuickSortInternal(list, pivotIndex + 1, right, keySelector, direction);
            }
        }

        private int Partition<T, TKey>(List<T> list, int left, int right, Func<T, TKey> keySelector, SortDirection direction) where TKey : IComparable<TKey>
        {
            T pivot = list[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                int comparison = keySelector(list[j]).CompareTo(keySelector(pivot));
                bool shouldSwap = direction == SortDirection.Ascending ? comparison < 0 : comparison > 0;

                if (shouldSwap)
                {
                    i++;
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            T temp2 = list[i + 1];
            list[i + 1] = list[right];
            list[right] = temp2;

            return i + 1;
        }
    }
}
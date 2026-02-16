using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronJade.Util.Core
{
    public class RandomConvenienceModel
    {
        public int[] GenerateRandomNumbers(int count, int minValue, int maxValue)
        {
            if (count > (maxValue - minValue + 1))
            {
                Debug.LogWarning("Count should be less than or equal to the range of numbers.");
                return null;
            }

            // 배열을 생성하여 중복이 없는 무작위 숫자 저장
            int[] randomNumbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                int randomNumber;
                bool duplicateFound;

                do
                {
                    randomNumber = Random.Range(minValue, maxValue + 1);
                    duplicateFound = false;

                    for (int j = 0; j < i; j++)
                    {
                        if (randomNumbers[j] == randomNumber)
                        {
                            duplicateFound = true;
                            break;
                        }
                    }
                } while (duplicateFound);

                randomNumbers[i] = randomNumber;
                Debug.Log("Random Number " + (i + 1) + ": " + randomNumber);
            }

            return randomNumbers;
        }
    }
}

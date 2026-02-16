using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronJade.Util.Core
{
    public class MathConvenienceModel
    {
        /// <summary>
        /// 두 벡터 사이의 각도
        /// -180 ~ 180
        /// </summary>
        public float GetAngle(Vector3 from, Vector3 to)
        {
            Vector3 v = to - from;
            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// 두 벡터 사이의 각도
        /// 0 ~ 360
        /// </summary>
        public float CalculateAngle(Vector3 from, Vector3 to, Vector3 direction)
        {
            return Quaternion.FromToRotation(direction, to - from).eulerAngles.z;
        }

        /// <summary>
        /// 두 벡터 사이의 각도
        /// -180 ~ 180
        /// </summary>
        public float SignedAngle(Vector3 direction, Vector3 from, Vector3 to, Vector3 axis)
        {
            return Vector3.SignedAngle(direction, to - from, axis);
        }

        public float Truncate(double value, int precision)
        {
            var step = Math.Pow(10, precision);
            var tmp = Math.Truncate(step * value);
            return (float)(tmp / step);
        }

        /// <summary>
        /// 거리가 가장 가까운 Vector를 찾는다.
        /// </summary>
        public Vector3 FindClosestWaypoint(Vector3 currentPosition, Vector3[] waypoints)
        {
            float distance = float.MaxValue;
            int index = 0;

            for (int i = 0; i < waypoints.Length; ++i)
            {
                float checkDistance = Vector3.Distance(currentPosition, waypoints[i]);

                if (checkDistance < distance)
                {
                    index = i;
                    distance = checkDistance;
                }
            }

            return waypoints[index];
        }

        /// <summary>
        /// 거리가 가장 가까운 Vector n개를 찾는다.
        /// </summary>
        public Vector3[] FindClosestWaypoints(Vector3 currentPosition, Vector3[] waypoints, int count)
        {
            // 현재 내 위치도 포함
            count++;

            // 가장 가까운 3개의 웨이포인트를 저장할 배열
            Vector3[] closestWaypoints = new Vector3[count];
            float[] closestDistances = new float[count];

            closestWaypoints[0] = currentPosition;
            closestDistances[0] = 0f;

            // 초기화: 매우 큰 값으로 초기화
            for (int i = 0; i < count; i++)
                closestDistances[i] = float.MaxValue;

            // 모든 웨이포인트를 순회하며 가장 가까운 3개를 찾음
            foreach (Vector3 waypoint in waypoints)
            {
                if (Mathf.Abs(currentPosition.y - waypoint.y) > 5)
                    continue;

                float distance = Vector3.Distance(currentPosition, waypoint);

                for (int i = 1; i < count; i++)
                {
                    if (distance < closestDistances[i])
                    {
                        // 기존 값들을 한 칸씩 뒤로 밀기
                        for (int j = count - 1; j > i; j--)
                        {
                            closestDistances[j] = closestDistances[j - 1];
                            closestWaypoints[j] = closestWaypoints[j - 1];
                        }

                        // 새로 찾은 가장 가까운 웨이포인트를 삽입
                        closestDistances[i] = distance;
                        closestWaypoints[i] = waypoint;
                        break;
                    }
                }
            }

            return closestWaypoints;
        }

        /// <summary>
        /// 목적지와 가장 가까운 Vector들을 찾는다.
        /// </summary>
        public List<Vector3> FindBestWaypoints(Vector3[] closestWaypoints, Vector3[] allWaypoints, Vector3 finalDestination)
        {
            List<Vector3> bestRoute = new List<Vector3>();
            float bestDistance = float.MaxValue;

            // Y값 차이가 5 이상이면 건너뜀
            if (Mathf.Abs(closestWaypoints[0].y - finalDestination.y) <= 5)
            {
                // 경유지를 거치지 않고 갔을 때가 더 빠른지 확인
                float directDistance = Vector3.Distance(closestWaypoints[0], finalDestination);
                if (directDistance < bestDistance)
                {
                    bestRoute = new List<Vector3> { closestWaypoints[0], finalDestination };
                    bestDistance = directDistance;
                }
            }

            // closestWaypoints 중 하나에서 시작하여 목적지까지의 최적 경로를 찾음
            foreach (var startPoint in closestWaypoints)
            {
                // 우선 시작 지점을 추가
                List<Vector3> currentRoute = new List<Vector3> { startPoint };
                float currentDistance = 0f;
                Vector3 currentPosition = startPoint;

                // allWaypoints 중에서 최적의 경유지를 순차적으로 찾음
                while (true)
                {
                    Vector3 nextWaypoint = Vector3.zero;
                    float shortestStepDistance = float.MaxValue;

                    foreach (var waypoint in allWaypoints)
                    {
                        // 이미 방문한 웨이포인트는 건너뜀
                        if (currentRoute.Contains(waypoint))
                            continue;

                        // Y값 차이가 5 이상이면 건너뜀
                        if (Mathf.Abs(currentPosition.y - waypoint.y) > 5)
                            continue;

                        float stepDistance = Vector3.Distance(currentPosition, waypoint);
                        if (stepDistance < shortestStepDistance)
                        {
                            shortestStepDistance = stepDistance;
                            nextWaypoint = waypoint;
                        }
                    }

                    // Y값 차이가 5 이상이면 건너뜀
                    if (Mathf.Abs(currentPosition.y - finalDestination.y) <= 5)
                    {
                        // 다음 웨이포인트가 최종 목적지보다 가까운지 확인
                        float distanceToFinal = Vector3.Distance(currentPosition, finalDestination);
                        if (distanceToFinal < shortestStepDistance)
                        {
                            currentDistance += distanceToFinal;
                            currentRoute.Add(finalDestination);
                            break; // 목적지에 도달하면 루프 종료
                        }
                    }

                    // 다음 웨이포인트로 이동
                    currentDistance += shortestStepDistance;
                    currentRoute.Add(nextWaypoint);
                    currentPosition = nextWaypoint;
                }

                // 현재 경로가 최적 경로인지 확인
                if (currentDistance < bestDistance)
                {
                    bestDistance = currentDistance;
                    bestRoute = new List<Vector3>(currentRoute);
                }
            }

            return bestRoute;
        }

        /// <summary>
        /// 순열 생성 메서드
        /// </summary>
        public bool NextPermutation(int[] nums)
        {
            int n = nums.Length;
            int i = n - 2;

            // 첫 번째로 nums[i] < nums[i + 1]인 i를 찾는다.
            // 이는 배열의 뒤에서부터 감소하지 않는 첫 번째 위치를 찾는 과정.
            while (i >= 0 && nums[i] >= nums[i + 1])
            {
                i--;
            }

            // i가 0 이상일 때만 다음 단계로 진행
            // i가 0 미만이면 더 이상 다음 순열이 존재하지 않으므로 false를 반환.
            if (i >= 0)
            {
                int j = n - 1;

                // nums[i]보다 큰 첫 번째 요소 nums[j]를 뒤에서부터 찾는다.
                while (nums[j] <= nums[i])
                {
                    j--;
                }

                // nums[i]와 nums[j]의 위치를 서로 바꾼다.
                Swap(ref nums[i], ref nums[j]);
            }

            // i+1 위치부터 배열의 끝까지를 반전시켜 사전 순으로 가장 작은 순열을 만든다.
            Reverse(nums, i + 1, n - 1);

            // i가 0 이상이면 다음 순열이 존재하므로 true를 반환
            // 그렇지 않으면 false를 반환
            return i >= 0;
        }

        /// <summary>
        /// 두 값을 교체한다.
        /// </summary>
        public void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 값을 반전시킨다.
        /// </summary>
        public void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                Swap(ref nums[start], ref nums[end]);
                start++;
                end--;
            }
        }

        /// <summary>
        /// 맨해튼 거리 (타일 칸 수 기준)
        /// 4방향 이동에 적합
        /// </summary>
        public int Manhattan(Vector2Int a, Vector2Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}
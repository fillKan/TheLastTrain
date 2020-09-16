using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace InGame.UI.Resource.Control
{

    /*
     * 축약어 정의표 :
     * P : POPULATION (인구)
     * BASE VALUE : BV (기준치)
     * DIFF : DIFFERNCE (차이)
     * 
     * F : FOOD (식량)
     * 0_TO_25_P : 0 ~ 25 PERCENT
     * 
     * EX : EXCEED (초과)
     */
    enum FoodBalanceType
    {
        NORMAL,
        P_HIGHER_THAN_BV,
        P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_0_TO_25_P,        // 0~ 25%
        P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_26_TO_50_P,       // 26 ~ 50%
        P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_51_TO_75_P,       // 51 ~ 75%
        P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_76_TO_100_P,      // 76 ~ 100%
    }

    public class FoodControl : MonoBehaviour
    {
        private Resource res;

        private FoodBalanceType foodBalanceType = FoodBalanceType.NORMAL;
        const short EXCEPTION_KEY = -1;

        private uint population_Val;        // 인구 수
        private uint population_baseVal;        // 인구 기준치
        private uint foodCnt; // 식량 개수
        private uint MaxFoodCnt;  // 최대 음식 개수
        private float foodDiff;  // 식량과의 차이
        public FoodControl(Resource res)
        {
            this.res = res;
        }
        
        // 인구가 기준치보다 높은지 체크
        bool IsOverPopulationBaseValue()
        {
            return population_Val > population_baseVal;
        }
        bool IsNormal()
        {
            return !IsOverPopulationBaseValue();
        }
        // 식량과의 차이 (인구수 - 식량) / (현재 인구수 + 현재 식량)
        float FoodDiffToPercent()
        {
            if (population_Val < foodCnt)
                return -1;
            Debug.Log(res.ConvertPercent((population_Val - foodCnt), (population_Val + foodCnt)));
            return res.ConvertPercent((population_Val - foodCnt), (population_Val + foodCnt));
        }

        bool CheckFoodDiffToPercent(short percent)
        {
            return (FoodDiffToPercent() != EXCEPTION_KEY && FoodDiffToPercent() <= percent);
        }
        public void FoodBalance()
        {
            population_Val = res.GetResourceTable.populationTable.Now;      // 인구 수
            population_baseVal = res.GetResourceTable.populationTable.Max;      // 인구 수
            foodCnt = res.GetResourceTable.foodTable.Now; // 식량 개수
            MaxFoodCnt = res.GetResourceTable.foodTable.Max;  // 최대 식량 개수


            if (IsNormal())
                foodBalanceType = FoodBalanceType.NORMAL;
            else if (IsOverPopulationBaseValue())
            {
                foodBalanceType = FoodBalanceType.P_HIGHER_THAN_BV;
                if (CheckFoodDiffToPercent(25))
                    foodBalanceType
                        = FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_0_TO_25_P;
                else if (CheckFoodDiffToPercent(50))
                    foodBalanceType
                        = FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_26_TO_50_P;
                else if (CheckFoodDiffToPercent(75))
                    foodBalanceType
                        = FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_51_TO_75_P;
                else if (CheckFoodDiffToPercent(100))
                    foodBalanceType
                        = FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_76_TO_100_P;
            }

            switch (foodBalanceType)
            {
                case FoodBalanceType.NORMAL:
                    break;
                case FoodBalanceType.P_HIGHER_THAN_BV:
                    break;
                case FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_0_TO_25_P:
                    break;
                case FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_26_TO_50_P:
                    break;
                case FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_51_TO_75_P:
                    break;
                case FoodBalanceType.P_HIGHER_THAN_BV__AND__WHEN_DIFF_F_IS_76_TO_100_P:
                    break;
                default:
                    break;
            }
        }
    }
}
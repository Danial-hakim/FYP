using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{
    public float playerHealth;
    public float enemyHealth;

    double pDying;
    double pHurt;
    double pHealthy;

    double eDying;
    double eHurt;
    double eHealthy;

    double total;

    // Add these variables to represent the degree of desire to flee or attack
    double desireToFlee;
    double desireToAttack;
    // Start is called before the first frame update
    void Start()
    {
        pDying = FuzzyTrapeZoid(playerHealth, 0, 10, 35, 45);
        eDying = FuzzyTrapeZoid(enemyHealth, 0, 10, 35, 45);

        pHurt = FuzzyTrapeZoid(playerHealth, 30, 42.5, 67.5, 80);
        eHurt = FuzzyTrapeZoid(enemyHealth, 30, 42.5, 67.5, 80);

        pHealthy = FuzzyTrapeZoid(playerHealth, 55, 70, 95, 100);
        eHealthy = FuzzyTrapeZoid(enemyHealth, 55, 70, 95, 100);

        total = pDying + pHurt + pHealthy;

        pDying = normalized(pDying, total);
        pHurt = normalized(pHurt, total);
        pHealthy = normalized(pHealthy, total);

        Debug.Log("Player is " + pDying + " : " + pHurt + " : " + pHealthy);
        Debug.Log("Enemy is " + eDying + " : " + eHurt + " : " + eHealthy);

        Fuzzification();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    double FuzzyGrade(double value, double x0, double x1)
    {
        double result = 0;
        double x = value;

        if(x <= x0)
        {
            result = 0;
        }
        else if(x > x1)
        {
            result = 1;
        }
        else
        {
            result  = (( x - x0) / (x1 - x0));
        }
        return result;
    }
    double FuzzyTrapeZoid(double value, double x0, double x1, double x2, double x3)
    {
        double result = 0;
        double x = value;

        if((x <= x0) || (x >= x3))
        {
            result = 0;
        }
        else if((x >= x1) && (x <= x2))
        {
            result = 1;
        }
        else if((x > x0) && (x < x1))
        {
            result = (( x- x0) / (x1 - x0));
        }
        else
        {
            result = (( x3 - x) / (x3 - x2));
        }
        return result;
    }

    double normalized(double value, double total)
    {
        double result = 0;
        
        if(value != 0)
        {
            result = value / total;
        }
        return result;
    }
    double FuzzyAnd(double A , double B)
    {
        return Math.Min(A, B);
    }

    double FuzzyOr(double A, double B)
    {
        return Math.Max(A, B);
    }

    double FuzzyNot(double A)
    {
        return 1 - A;
    }

    double Fuzzification()
    {
        double mlow = FuzzyOr(FuzzyAnd(FuzzyNot(eDying), pDying), FuzzyAnd(eHealthy, pHurt));
        double mMid = FuzzyOr(FuzzyOr(FuzzyAnd(eDying, pDying), FuzzyAnd(eHurt, pHurt)),FuzzyAnd(eHealthy, pHealthy));
        double mHigh = FuzzyOr(FuzzyAnd(eDying,pHurt),FuzzyAnd(FuzzyNot(eHealthy),pHealthy));

        // Compute speed using mlow, mMid, and mHigh
        double lowSpeed = 10;
        double midSpeed = 1;
        double highSpeed = -10;

        double speed = ((mlow * lowSpeed) + (mMid * midSpeed) + (mHigh * highSpeed)) / (mlow + mMid + mHigh);
        Debug.Log(mlow);
        Debug.Log(mMid);
        Debug.Log(mHigh);
        Debug.Log(speed);
        return 0;
    }
}

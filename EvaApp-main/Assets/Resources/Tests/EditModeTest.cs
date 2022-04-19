using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class EditModeTest : MonoBehaviour
{
    //[Test]
    //public void Test1()
    //{
    //    float a = 2f;
    //    float b = 3f;

    //    float result = Mathf.Pow(a, b);

    //    Assert.That(result, Is.EqualTo(8f));
    //}

    [Test] //全ての配列が正の値であるかの判定
    public void Test1()
    {
        int[] Datas = new int[8];
        Datas[0] = 10;
        Datas[1] = 10;
        Datas[2] = 2;
        Datas[3] = 3;
        Datas[4] = 10;
        Datas[5] = 1;
        Datas[6] = 0;
        Datas[7] = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            Debug.Assert(Datas[i] >= 0);
            Debug.Log("正しい値です");
        }
    }
    [Test] //配列に負の値が含まれていないかの判定
    public void Test2()
    {
        int[] Datas = new int[8];
        Datas[0] = -10;
        Datas[1] = 10;
        Datas[2] = -2;
        Datas[3] = 3;
        Datas[4] = -10;
        Datas[5] = 1;
        Datas[6] = 0;
        Datas[7] = 0;

        int count = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            if (Datas[i] < 0)
            {
                count++;
            }
        }
        Debug.Assert(count >= 0);
        Debug.Log(count + "個、値にマイナスを含んでいます");
    }
    [Test] //配列ごとに上限の値の判定ができる
    public void Test3()
    {
        int[] Datas = new int[8];
        Datas[0] = 100;
        Datas[1] = 10;
        Datas[2] = 20;
        Datas[3] = 3;
        Datas[4] = 10;
        Datas[5] = 1;
        Datas[6] = 0;
        Datas[7] = 0;

        int count = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            if (Datas[i] > 10)
            {
                count++;
            }
        }
        Debug.Assert(count >= 0);
        Debug.Log(count + "個、値が上限を超えています");
    }    
    [Test] //配列ごとに下限の値の判定ができる
    public void Test4()
    {
        int[] Datas = new int[8];
        Datas[0] = -100;
        Datas[1] = 10;
        Datas[2] = -20;
        Datas[3] = 3;
        Datas[4] = -10;
        Datas[5] = 1;
        Datas[6] = -0;
        Datas[7] = 0;

        int count = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            if (Datas[i] < 0)
            {
                count++;
            }
        }
        Debug.Assert(count >= 0);
        Debug.Log(count + "個、値が下限を超えています");
    }
    [Test] //配列ごとに上限と下限の値が確認できる
    public void Test5()
    {
        int[] Datas = new int[8];
        Datas[0] = -100;
        Datas[1] = 10;
        Datas[2] = -20;
        Datas[3] = 3;
        Datas[4] = -10;
        Datas[5] = 1;
        Datas[6] = -0;
        Datas[7] = 0;

        int count = 0;

        for (int i = 0; i < Datas.Length; i++)
        {
            if (Datas[i] < 0)
            {
                count++;
            }
            else if (Datas[i] > 10)
            {
                count++;
            }
        }
        Debug.Assert(count >= 0);
        Debug.Log(count + "個、値が限界値を超えています");
    }
    [Test] //範囲内の乱数
    public void Test6()
    {       
        int rand1 = UnityEngine.Random.Range(0, 10);
        int count = 0;

        if(rand1 <= 10)
        {
            if(rand1 >= 0)
            {
                count++;
            }
        }

        Debug.Log("乱数の値：" +rand1);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(count + "範囲内の値です");
    }
    [Test] //乱数が機能している
    public void Test7()
    {       
        int rand2 = UnityEngine.Random.Range(0, 1);
        int count = 0;

        if(rand2 <= 0)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand2);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand2 + "は範囲内の値です");
    }
    [Test] //マイナスの乱数
    public void Test8()
    {       
        int rand3 = UnityEngine.Random.Range(-1, 0);
        int count = 0;

        if(rand3 < 0)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand3);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand3 + "は範囲外の値です");
    }
    [Test] //乱数の上限値
    public void Test9()
    {       
        int rand4 = UnityEngine.Random.Range(11, 100);
        int count = 0;

        if(rand4 > 10)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand4);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand4 + "は範囲外の値です");
    }
    [Test] //乱数の上限値
    public void Test10()
    {       
        int rand4 = UnityEngine.Random.Range(11, 100);
        int count = 0;

        if(rand4 > 10)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand4);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand4 + "は範囲外の値です");
    }
    [Test] //乱数がマイナス
    public void Test11()
    {       
        int rand5 = UnityEngine.Random.Range(-1, -100);
        int count = 0;

        if(rand5 < 0)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand5);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand5 + "は範囲外の値です");
    }
    [Test] //乱数が大きなマイナス
    public void Test12()
    {       
        int rand6 = UnityEngine.Random.Range(-1, -1000);
        int count = 0;

        if(rand6 < 0)
        {
            count++;
        }

        Debug.Log("乱数の値：" +rand6);
        Assert.That(count, Is.GreaterThan(0));
        Debug.Log(rand6 + "は範囲外の値です");
    }
    [Test] //文字列から数値
    public void Test13()
    {   
        int start1 = int.Parse("10");

        Debug.Log("変換後の値：" +start1);
        Assert.That(start1,Is.EqualTo(10));
        
    }
    [Test] //数値から文字列変更
    public void Test14()
    {   
        string start2;
        int s2 = 10;

        start2 = s2.ToString("00");//数値を桁数指定で文字列に変換
        Debug.Log("変換後の値：" + s2);//コンソールに表示
        Assert.That(start2,Is.EqualTo("10"));   
    }
    [Test] //乱数を配列に保存して負の値がないかを確認
    public void Test15()
    {   
        int randnum1 = UnityEngine.Random.Range(0, 10);
        int randnum2 = UnityEngine.Random.Range(0, 10);
        int randnum3 = UnityEngine.Random.Range(0, 10);

        Debug.Log(randnum1);
        Debug.Log(randnum2);
        Debug.Log(randnum3);
        Assert.That(new[] {randnum1,randnum2,randnum3},Has.None.Negative); 
    }
    [Test] //乱数を配列に保存して値の範囲を確認
    public void Test16()
    {   
        int randnum1 = UnityEngine.Random.Range(0, 10);
        int randnum2 = UnityEngine.Random.Range(0, 10);
        int randnum3 = UnityEngine.Random.Range(0, 10);

        Debug.Log(randnum1);
        Debug.Log(randnum2);
        Debug.Log(randnum3);
        Assert.That(new[] {randnum1,randnum2,randnum3},Has.All.GreaterThan(0)); 
    }
    
}

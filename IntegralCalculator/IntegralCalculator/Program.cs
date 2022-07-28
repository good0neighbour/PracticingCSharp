﻿using System;

namespace IntegralCalculator
{
    internal class Program
    {
        private static string input;
        private static short len = 31;
        static void Main(string[] args)
        {
            int[] sign = new int[2];
            int[,] index = new int[sign.Length,len];
            string copy;
            for (short i = 0; i < sign.Length; i++)
                sign[i] = -1;

            //입력
            Console.WriteLine("더하기 +\t빼기 -\t곱하기 *\t나누기 /");
            Console.WriteLine("괄호 ( )");
            input = Console.ReadLine();
            input = input.Trim();
            copy = input;
            for (int i = 0; i < len; i++)
                index[0,i] = input.Length;

            //인식
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '(':
                        sign[0]++;
                        index[0, sign[0]] = i;
                        break;
                    case ')':
                        sign[1]++;
                        index[1, sign[1]] = i;
                        break;
                    default:
                        break;
                }
            }

            //괄호 계산
            if (sign[1] > -1)
                OpenClose(index);

            //전체 계산
            //Console.WriteLine("전체 계산 시작");
            //Calculate(0, input.Length-1);
            Console.WriteLine(input);
        }
        static private void OpenClose(int[,] arr)
        {
            int[,] index = arr;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (index[0, j] > index[1, i])
                    {
                        Calculate(index[0, j - 1], index[1, i]);
                        index[0, j - 1] = -1;
                        break;
                    }
                }
            }
        }
        static private void Calculate(int a,int b)
        {
            double[] num = new double[len];
            char[] sign = new char[len];
            int index = 0;
            double[] temp = new double[2];
            for (int i = 0; i < sign.Length; i++)
                sign[i] = ' ';
            Console.WriteLine("계산 함수 진입");

            //인식
            for (int i = a; i < b; i++)
            {
                switch (input[i])
                {
                    case '(':
                        index--;
                        break;
                    case ' ':
                        index--;
                        break;
                    case ',':
                        index--;
                        break;
                    case '\n':
                        index--;
                        break;
                    case '+':
                        sign[index] = '+';
                        break;
                    case '-':
                        sign[index] = '-';
                        break;
                    case '*':
                        sign[index] = '*';
                        break;
                    case '/':
                        sign[index] = '/';
                        break;
                    default:
                        temp = NumIdentfy(i);
                        num[index] = temp[0];
                        i = (int)temp[1];
                        break;
                }
                index++;
            }
            //테스트 출력
            for (int i = 0; i < sign.Length; i++)
                Console.Write("{0} ", sign[i]);
            Console.Write("\n");
            for (int i = 0; i < num.Length; i++)
                Console.Write("{0} ", num[i]);
            Console.Write("\n\n");

            //곱하기, 나누기
            for (int i = 1; i < sign.Length; i=i+2)
            {
                if (sign[i] == '*')
                {
                    num[i - 1] = num[i - 1] * num[i + 1];
                    for (int j = i + 2; j < sign.Length; j = j + 2)
                    {
                        sign[j - 2] = sign[j];
                        sign[j] = ' ';
                        num[j - 1] = num[j + 1];
                    }
                    i -= 2;
                }
                else if (sign[i] == '/')
                {
                    num[i - 1] = num[i - 1] / num[i + 1];
                    for (int j = i + 2; j < sign.Length; j = j + 2)
                    {
                        sign[j - 2] = sign[j];
                        sign[j] = ' ';
                        num[j - 1] = num[j + 1];
                    }
                    i -= 2;
                }
                else if (sign[i] == ' ')
                    break;
                //테스트 출력
                for (int j = 0; j < sign.Length; j++)
                    Console.Write("{0} ", sign[j]);
                Console.Write("\n");
                for (int j = 0; j < num.Length; j++)
                    Console.Write("{0} ", num[j]);
                Console.Write("\n\n");
            }

            //더하기, 빼기
            while (true)
            {
                if (sign[1] == '+')
                {
                    num[0] = num[0] + num[2];
                    for (int j = 3; j < sign.Length; j = j + 2)
                    {
                        sign[j - 2] = sign[j];
                        sign[j] = ' ';
                        num[j - 1] = num[j + 1];
                    }
                }
                else if (sign[1] == '-')
                {
                    num[0] = num[0] - num[2];
                    for (int j = 3; j < sign.Length; j = j + 2)
                    {
                        sign[j - 2] = sign[j];
                        sign[j] = ' ';
                        num[j - 1] = num[j + 1];
                    }
                }
                else
                    break;
                //테스트 출력
                for (int j = 0; j < sign.Length; j++)
                    Console.Write("{0} ", sign[j]);
                Console.Write("\n");
                for (int j = 0; j < num.Length; j++)
                    Console.Write("{0} ", num[j]);
                Console.Write("\n\n");
            }

            //결과
            input.Remove(a,b);
            Console.WriteLine(input);
            input.Insert(a,num[0].ToString());
            Console.WriteLine(num[0]);
            Console.WriteLine("계산 끝");
            Console.WriteLine(input);
        }
        static private double[] NumIdentfy(int n)
        {
            bool c = true;
            bool o = true;
            short d = 0;
            double num = 0;
            double numD = 0;
            double[] result = new double[2];
            while (c)
            {
                if (o)
                {
                    switch (input[n])
                    {
                        case '0':
                            num = num * 10;
                            break;
                        case '1':
                            num = num * 10 + 1;
                            break;
                        case '2':
                            num = num * 10 + 2;
                            break;
                        case '3':
                            num = num * 10 + 3;
                            break;
                        case '4':
                            num = num * 10 + 4;
                            break;
                        case '5':
                            num = num * 10 + 5;
                            break;
                        case '6':
                            num = num * 10 + 6;
                            break;
                        case '7':
                            num = num * 10 + 7;
                            break;
                        case '8':
                            num = num * 10 + 8;
                            break;
                        case '9':
                            num = num * 10 + 9;
                            break;
                        case '.':
                            o = false;
                            break;
                        default:
                            c = false;
                            break;
                    }
                }
                else
                {
                    switch (input[n])
                    {
                        case '0':
                            numD = numD * 10;
                            break;
                        case '1':
                            numD = numD * 10 + 1;
                            break;
                        case '2':
                            numD = numD * 10 + 2;
                            break;
                        case '3':
                            numD = numD * 10 + 3;
                            break;
                        case '4':
                            numD = numD * 10 + 4;
                            break;
                        case '5':
                            numD = numD * 10 + 5;
                            break;
                        case '6':
                            numD = numD * 10 + 6;
                            break;
                        case '7':
                            numD = numD * 10 + 7;
                            break;
                        case '8':
                            numD = numD * 10 + 8;
                            break;
                        case '9':
                            numD = numD * 10 + 9;
                            break;
                        case '.':
                            break;
                        default:
                            c = false;
                            break;
                    }
                    d++;
                }
                n++;
            }
            if (d > 0)
                num += numD / Math.Pow(10,d);
            result[0] = num;
            result[1] = n-2;
            return result;
        }
    }
}
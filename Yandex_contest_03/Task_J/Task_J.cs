using System;
using System.Collections.Generic;
using System.IO;

partial class Program
{
    private static bool ValidateQuery(string query, out string[] queryParameters)
    {
        queryParameters = new string[50];


        string[] querryArray = query.Split(new char[] {' '});
        // querryArray[0] = название столбца.
        // querryArray[1] = оператор сравнения.
        // querryArray[2] = значение с которым происходит сравнение.


        bool flag = true;
        double rubish = 0;

        if (querryArray.Length != 3)
        {
            flag = false;
        }
        else
        {
            // Первый парматр не название столбца
            if (querryArray[0].ToLower() == "First_name".ToLower() ||
                querryArray[0].ToLower() == "Last_name".ToLower() ||
                querryArray[0].ToLower() == "Group".ToLower())
            {
                if (querryArray[1] != "==" && querryArray[1] != "<>")
                {
                    flag = false;
                }

            }
            else
            {
                if (querryArray[0].ToLower() == "Rating".ToLower() ||
                    querryArray[0].ToLower() == "GPA".ToLower())
                {
                    if (querryArray[1] != ">=" && querryArray[1] != "<=")
                    {
                        flag = false;
                    }
                }
            }

            // Все данные корректны.
            queryParameters[0] = querryArray[0];
            queryParameters[1] = querryArray[1];
            queryParameters[2] = querryArray[2];

        }
        return flag;


    }

    private static List<string> ProcessQuery(string[] queryParameters, string pathToDatabase)
    {
        List<string> Result = new List<string>();

        string[] DataBase = File.ReadAllLines(pathToDatabase);
        string[] CleanBase = new string[1];
        double temperaryNumber;
        double rubish;
        int i = 0;

        foreach (var str in DataBase)
        {
            temperaryNumber = 0;
            rubish = 0;

            // Массив содержащий все элементы строки базы данных.
            string[] strArray = str.Split(new char[] {';'});

            // Проверяем First_name.
            if (queryParameters[0] == "First_name".ToLower())
            {
                if (queryParameters[1] == "==")
                {
                    if (strArray[0].ToLower() == queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }
                    
                }
                else // не равно. 
                {
                    if (strArray[0].ToLower() != queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }
                }
                
            }

            // Проверяем Last_name.
            if (queryParameters[0] == "Last_name".ToLower())
            {
                if (queryParameters[1] == "==")
                {
                    if (strArray[1].ToLower() == queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }

                }
                else // не равно.
                {
                    if (strArray[1].ToLower() != queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }
                }
            }

            // Проверяем Group.
            if (queryParameters[0] == "Group".ToLower())
            {
                if (queryParameters[1] == "==")
                {
                    if (strArray[2].ToLower() == queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }

                }
                else // не равно.
                {
                    if (strArray[2].ToLower() != queryParameters[3].ToLower())
                    {
                        CleanBase[i++] = str;
                        Array.Resize(ref CleanBase, i + 1);
                    }
                }
            }

            // Проверяем Rating.
            if (queryParameters[0] == "Rating".ToLower())
            {
                if (double.TryParse(queryParameters[2], out temperaryNumber) &&
                    double.TryParse(strArray[3], out rubish))
                {
                    if (queryParameters[1] == ">=")
                    {
                        if (rubish >= temperaryNumber)
                        {
                            CleanBase[i++] = str;
                            Array.Resize(ref CleanBase, i + 1);
                        }
                        
                    }
                    else // меньше либо равно.
                    {
                        if (rubish <= temperaryNumber)
                        {
                            CleanBase[i++] = str;
                            Array.Resize(ref CleanBase, i + 1);
                        }
                    }
                }

            }

            // Проверяем GPA.
            if (queryParameters[0] == "GPA".ToLower())
            {
                if (double.TryParse(queryParameters[2], out temperaryNumber) &&
                    double.TryParse(strArray[4], out rubish))
                {
                    if (queryParameters[1] == ">=")
                    {
                        if (rubish >= temperaryNumber)
                        {
                            CleanBase[i++] = str;
                            Array.Resize(ref CleanBase, i + 1);
                        }

                    }
                    else // меньше либо равно.
                    {
                        if (rubish <= temperaryNumber)
                        {
                            CleanBase[i++] = str;
                            Array.Resize(ref CleanBase, i + 1);
                        }
                    }
                }
            }

        }
        for (int k = 0; k < CleanBase.Length; k++);
        {
            Result.Add(CleanBase[i]);
        }

        return Result;

    }
}

partial class Program
{
    public static void Main(string[] args)
    {
        if (!ValidateQuery(File.ReadAllLines("query.txt")[0], out string[] queryParameters))
        {
            File.WriteAllLines("queryResult.txt", new List<string> { "Incorrect input" });
            return;
        }

        File.WriteAllLines("queryResult.txt", ProcessQuery(queryParameters, "db.txt"));
    }
}

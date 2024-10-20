using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Debug = ccm.Debug;

public class CSVReader
{
    private static List<int> isValidHeader(string[] headers)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < headers.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(headers[i]) && !headers[i].StartsWith("_"))
            {
                result.Add(i);
            }
        }

        return result;  
    }

    private static void Parse<T>(string[] headers, List<int> validHeaderIndex, string[] lines, Dictionary<int, T> dataDic) where T : new()
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Trim().Split(',');
            if (values.Length < validHeaderIndex.Count || string.IsNullOrEmpty(values[0]))
                continue;

            T item = new T();
            foreach (int index in validHeaderIndex)
            {
                if (index < values.Length)
                {
                    SetField(item, headers[index].Trim(), values[index].Trim());
                }
            }

            dataDic.Add(int.Parse(values[0]), item);
        }
    }

    private static void SetField(object obj, string fieldName, string value)
    {
        FieldInfo field = obj.GetType().GetField(fieldName);
        if (field != null)
        {
            if (field.FieldType.IsEnum)
            {
                field.SetValue(obj, Enum.Parse(field.FieldType, value));
            }
            else if (field.FieldType == typeof(bool))
            {
                if (bool.TryParse(value, out bool boolValue))
                {
                    field.SetValue(obj, boolValue);
                }
                else
                {
                    field.SetValue(obj, value.ToUpper() == true.ToString().ToUpper());
                }
            }
            else
            {
                object convertedValue = Convert.ChangeType(value, field.FieldType);
                field.SetValue(obj, convertedValue);
            }
        }
        else
        {
            Debug.Log($"필드를 찾을 수 없습니다: {fieldName}");
        }
    }

    public static Dictionary<int, T> ReadCSV<T>(TextAsset csv) where T : new()
    {
        Dictionary<int, T> dataDic = new Dictionary<int, T>();

        try
        {
            string[] lines = csv.text.Split('\n');
            string[] headers = lines[0].Trim().Split(',');

            List<int> validHeaderIndex = isValidHeader(headers);
            Parse<T>(headers, validHeaderIndex, lines, dataDic);           
        }
        catch (Exception e)
        {
            Debug.Log($"CSV 파일 읽기 오류: {e.Message}");
        }

        return dataDic;
    }
}

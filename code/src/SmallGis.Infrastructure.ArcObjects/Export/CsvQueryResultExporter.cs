using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Export
{
    /// <summary>
    /// Exports query records to CSV using only domain query results. / 仅基于领域层查询结果导出 CSV。
    /// </summary>
    public class CsvQueryResultExporter : IQueryResultExportPort
    {
        public void ExportToCsv(QueryResult result, string outputPath)
        {
            if (result == null) throw new ArgumentNullException("result");
            if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentException("Output path is required.", "outputPath");

            List<string> headers = CollectHeaders(result);
            using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                writer.WriteLine(ToCsvLine(headers));
                for (int i = 0; i < result.Records.Count; i++)
                {
                    writer.WriteLine(ToCsvLine(ToValues(result.Records[i], headers)));
                }
            }
        }

        private static List<string> CollectHeaders(QueryResult result)
        {
            List<string> headers = new List<string>();
            headers.Add("ObjectId");
            headers.Add("LayerName");
            headers.Add("GeometryType");

            if (result.Records == null)
            {
                return headers;
            }

            for (int i = 0; i < result.Records.Count; i++)
            {
                foreach (string key in result.Records[i].Attributes.Keys)
                {
                    if (!headers.Contains(key))
                    {
                        headers.Add(key);
                    }
                }
            }

            return headers;
        }

        private static List<string> ToValues(FeatureRecord record, IList<string> headers)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < headers.Count; i++)
            {
                string header = headers[i];
                if (header == "ObjectId")
                {
                    values.Add(record.ObjectId.ToString());
                }
                else if (header == "LayerName")
                {
                    values.Add(record.LayerName);
                }
                else if (header == "GeometryType")
                {
                    values.Add(record.GeometryType.ToString());
                }
                else if (record.Attributes.ContainsKey(header) && record.Attributes[header] != null)
                {
                    values.Add(record.Attributes[header].ToString());
                }
                else
                {
                    values.Add(string.Empty);
                }
            }

            return values;
        }

        private static string ToCsvLine(IList<string> values)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(",");
                }

                builder.Append(Escape(values[i]));
            }

            return builder.ToString();
        }

        private static string Escape(string value)
        {
            string text = value ?? string.Empty;
            if (text.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }

            return text;
        }
    }
}

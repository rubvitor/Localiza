using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DivisorPrimo.Infra.CrossCutting.Util
{
    public class NumeroUtils
    {
        /// <summary>
        /// This method read Numero file without the header and convert to XML
        /// </summary>
        /// <param name="path">The path of file</param>
        /// <returns>XML File</returns>
        public static string ReadNumerotoXMLFromPath(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new Exception("O arquivo informado não existe no diretório.");

                //Read all text from file
                var lines = File.ReadAllLines(path);

                return ProcessLinesNumero(lines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method read Numero file without the header and convert to XML
        /// </summary>
        /// <param name="path">The path of file</param>
        /// <returns>XML File</returns>
        public static string ReadNumerotoXMLFromFile(IFormFile file)
        {
            try
            {
                if (file == null)
                    throw new Exception("O arquivo informado não existe.");

                //Read all text from file
                var lines = ReadAsArray(file);

                return ProcessLinesNumero(lines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReadNumerotoXMLFromArray(string[] lines)
        {
            try
            {
                if (lines == null || lines.Length == 0)
                    throw new Exception("O arquivo informado não existe.");

                return ProcessLinesNumero(lines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ProcessLinesNumero(string[] lines)
        {
            StringBuilder fileLinesXML = new StringBuilder();
            foreach (var line in lines)
            {
                if (line.StartsWith("<"))
                {
                    string lineAdd = line.Trim().ToUpper();

                    if (!lineAdd.Contains("</"))
                    {
                        var idxFirstNumero = lineAdd.IndexOf(">") + 1;
                        var tag = lineAdd.Substring(0, idxFirstNumero).Replace("<", "</");

                        if (!lines.Any(l => l.Trim().ToUpper().Contains(tag)))
                            lineAdd += tag;
                    }

                    fileLinesXML.Append(lineAdd);
                }
            }

            return fileLinesXML.ToString();
        }

        public static string[] ReadAsArray(IFormFile file)
        {
            List<string> result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine());
            }

            return result.ToArray();
        }

        public static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}

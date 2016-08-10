﻿using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace PostoSeguro.PDFParser
{
    public class Program
    {
        static Uri urlToDownloadDataBombaMedidora = new Uri(ConfigurationManager.AppSettings["URLDadosBombaMedidora"].ToString());
        static Uri mainUrlDataBombaMedidora = new Uri(ConfigurationManager.AppSettings["SubIndexURLDadosBombaMedidora"]);

        static string fileName = ConfigurationManager.AppSettings["PDFDadosBombaMedidoraFileName"].ToString();
        static string fileExtention = ConfigurationManager.AppSettings["PDFDadosBombaMedidoraFileExtention"].ToString();
        static string pathPDFDadosBombaMedidora = ConfigurationManager.AppSettings["PathPDFDadosBombaMedidora"].ToString();

        public static void Main(string[] args)
        {
            VerificarUltimaAtualizacaoSiteANPBombaMedidora();

            BaixarPDFBombaMedidoraAtualizado();
        }

        private static void BaixarPDFBombaMedidoraAtualizado()
        {
            Console.WriteLine("Preparando para realizar o download de: POSTOS REVENDEDORES AUTUADOS E/OU INTERDITADOS POR IRREGULARIDADES NO VOLUME DE COMBUSTÍVEL DISPENSADO POR BOMBA MEDIDORA");
            Console.WriteLine("Em: " + urlToDownloadDataBombaMedidora.AbsolutePath);

            string fileName = ConfigureFileName();

            WebClientHelper.WebClientHelper.DownloadPDFFile(urlToDownloadDataBombaMedidora, pathPDFDadosBombaMedidora, fileName);

            Console.WriteLine("Download realizado com sucesso, arquivo: " + fileName + " salvo em: " + pathPDFDadosBombaMedidora);
        }

        private static void VerificarUltimaAtualizacaoSiteANPBombaMedidora()
        {
            Console.WriteLine("Checando última data de atualização em: " + mainUrlDataBombaMedidora.AbsoluteUri);

            var request = WebClientHelper.WebClientHelper.WebRequestFactory(mainUrlDataBombaMedidora.AbsoluteUri);
            var response = request.GetResponse();

            DateTime ultimaDataAtualizacao = ObterUltimaDataAtualizacao(response);
        }

        private static DateTime ObterUltimaDataAtualizacao(WebResponse response)
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(response.GetResponseStream());
                string responseText = reader.ReadToEnd();

                string pattern = @"<\/span><p class='rodape'>Atualizado em (?<group>[0-9/ :]+)";

                Regex regex = new Regex(pattern);
                Match match = regex.Match(responseText);

                return Convert.ToDateTime(match.Groups["group"].Value);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                response.Close();
                reader.Close();
            }
        }

        private static string ConfigureFileName()
        {
            return fileName += DateTime.Now.ToString().Replace("/", string.Empty).Replace(":", string.Empty).Trim() + fileExtention;
        }
    }
}
using PostoSeguro.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using PostoSeguro.Model;

namespace PostoSeguro.PDFParser
{
    public class Program
    {
        #region Private Members

        static Uri urlToDownloadDataBombaMedidora = new Uri(ConfigurationManager.AppSettings["URLDadosBombaMedidora"].ToString());
        static Uri mainUrlDataBombaMedidora = new Uri(ConfigurationManager.AppSettings["SubIndexURLDadosBombaMedidora"]);
        static Uri mainUrlDataQualidade = new Uri(ConfigurationManager.AppSettings["SubIndexURLDadosQualidade"]);

        static string fileName = ConfigurationManager.AppSettings["PDFDadosBombaMedidoraFileName"].ToString();
        static string fileExtention = ConfigurationManager.AppSettings["PDFDadosBombaMedidoraFileExtention"].ToString();
        static string fileExtentionQualidade = ConfigurationManager.AppSettings["PDFDadosQualidadeFileExtention"].ToString();
        static string pathPDFDadosBombaMedidora = ConfigurationManager.AppSettings["PathPDFDadosBombaMedidora"].ToString();
        static string pathPDFDadosQualidade = ConfigurationManager.AppSettings["PathPDFDadosQualidade"].ToString();

        static string[] allKeys = ConfigurationManager.AppSettings.AllKeys;
        static List<string> urlsToDownloadDataQualidade;

        static ConfigurationDao configDao = new ConfigurationDao();

        static Model.Configuration config = null;

        #endregion

        #region Main

        public static void Main(string[] args)
        {
            ConfigurarParametrosDownloadEstados();

            config = configDao.ObterEntidadeUltimaAtualizacao();

            ProcessarDadosBombaMedidora();
            ProcessarDadosQualidade();

            Console.ReadKey();
        }

        #endregion

        #region Helper Methods

        private static void ProcessarDadosQualidade()
        {
            DateTime ultimaDataAtualizacaoQualidade = VerificarUltimaAtualizacaoSiteANPQualidade();

            if (ultimaDataAtualizacaoQualidade > config.UltimaAtualizacaoDadosQualidade)
                BaixarPDFsQualidade(config);
            else
                Console.WriteLine("Não houve atualização no site da ANP para os dados de Qualidade.");
        }

        private static void ProcessarDadosBombaMedidora()
        {
            DateTime ultimaDataAtualizacaoBombaMedidora = VerificarUltimaAtualizacaoSiteANPBombaMedidora();

            if (ultimaDataAtualizacaoBombaMedidora > config.UltimaAtualizacaoDadosBombaMedidora)
                BaixarPDFBombaMedidoraAtualizado(config);
            else
                Console.WriteLine("Não houve atualização no site da ANP para os dados de Bomba Medidora.");
        }

        private static void ConfigurarParametrosDownloadEstados()
        {
            urlsToDownloadDataQualidade = new List<string>();

            for (int i = 0; i < allKeys.Length; i++)
            {
                if (allKeys[i].StartsWith("URLDadosQualidade"))
                    urlsToDownloadDataQualidade.Add(allKeys[i]);
            }
        }

        private static void BaixarPDFBombaMedidoraAtualizado(Model.Configuration config)
        {
            Console.WriteLine("Preparando para realizar o download de: POSTOS REVENDEDORES AUTUADOS E/OU INTERDITADOS POR IRREGULARIDADES NO VOLUME DE COMBUSTÍVEL DISPENSADO POR BOMBA MEDIDORA");
            Console.WriteLine("Em: " + urlToDownloadDataBombaMedidora.AbsoluteUri);

            string fileName = ConfigureFileName();

            WebClientHelper.WebClientHelper.DownloadPDFFile(urlToDownloadDataBombaMedidora, pathPDFDadosBombaMedidora, fileName);

            Console.WriteLine("Download realizado com sucesso, arquivo: " + fileName + " salvo em: " + pathPDFDadosBombaMedidora);

            AtualizarUltimaDataAtualizacaoBombaMedidora(config);
        }

        private static void AtualizarUltimaDataAtualizacaoBombaMedidora(Model.Configuration config)
        {
            config.UltimaAtualizacaoDadosBombaMedidora = DateTime.Now;
            configDao.AtualizarUltimaDataAtualizacao(config);
        }

        private static void BaixarPDFsQualidade(Model.Configuration config)
        {
            Console.WriteLine("Preparando para realizar o download de: POSTOS REVENDEDORES AUTUADOS E/OU INTERDITADOS POR PROBLEMAS DE QUALIDADE DOS COMBUSTÍVEIS");

            foreach (var item in urlsToDownloadDataQualidade)
            {
                Uri urlToDownload = new Uri(ConfigurationManager.AppSettings[item]);

                Console.WriteLine("Em: " + urlToDownload.AbsoluteUri);

                string fileName = ConfigureFileName(item.Substring(item.Length - 2, 2));

                WebClientHelper.WebClientHelper.DownloadPDFFile(urlToDownload, pathPDFDadosQualidade, fileName);

                Console.WriteLine("Download realizado com sucesso, arquivo: " + fileName + " salvo em: " + pathPDFDadosQualidade);
            }

            AtualizarUltimaDataAtualizacaoQualidade(config);
        }

        private static void AtualizarUltimaDataAtualizacaoQualidade(Model.Configuration config)
        {
            config.UltimaAtualizacaoDadosQualidade = DateTime.Now;
            configDao.AtualizarUltimaDataAtualizacao(config);
        }

        private static DateTime VerificarUltimaAtualizacaoSiteANPBombaMedidora()
        {
            Console.WriteLine("Checando última data de atualização em: " + mainUrlDataBombaMedidora.AbsoluteUri);

            var request = WebClientHelper.WebClientHelper.WebRequestFactory(mainUrlDataBombaMedidora.AbsoluteUri);
            var response = request.GetResponse();

            return ObterUltimaDataAtualizacao(response);
        }

        private static DateTime VerificarUltimaAtualizacaoSiteANPQualidade()
        {
            Console.WriteLine("Checando última data de atualização em: " + mainUrlDataQualidade.AbsoluteUri);

            var request = WebClientHelper.WebClientHelper.WebRequestFactory(mainUrlDataQualidade.AbsoluteUri);
            var response = request.GetResponse();

            return ObterUltimaDataAtualizacao(response);
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

        private static string ConfigureFileName(string siglaEstado)
        {
            return siglaEstado.Trim() + fileExtentionQualidade;

        }

        #endregion
    }
}
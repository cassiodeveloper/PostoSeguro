using System;
using System.Net;
using System.Net.Cache;

namespace PostoSeguro.WebClientHelper
{
    public static class WebClientHelper
    {
        static WebClient webClient = new WebClient();

        public static void DownloadPDFFile(Uri urlToDownload, string pathToSaveFile, string fileName)
        {
            try
            {
                webClient.DownloadFile(urlToDownload, pathToSaveFile + fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar realizar o download do PDF. " + ex.Message);
            }
        }

        public static WebRequest WebRequestFactory(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Credentials = CredentialCache.DefaultCredentials;

            return request;
        }
    }
}
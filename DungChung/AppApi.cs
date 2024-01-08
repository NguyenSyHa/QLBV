using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DungChung
{
    public class AppApi
    {
        public static string Post<T, Y>(string url, Y para, string token, bool isHeader = true)
        {
            string result = "";

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(DungChung.Bien.xmlFilePath_LIS[19]);

                // Add an Accept header for JSON format.
                httpClient.DefaultRequestHeaders.Accept.Clear();
                if (isHeader)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                StringContent content = null;
                if (para != null)
                {
                    string jsonData = JsonConvert.SerializeObject(para);
                    content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        try
                        {
                            result = jsonResponse;
                        }
                        catch (Exception ex)
                        {
                            result = "Lỗi tạo hóa đơn: " + ex.Message;
                        }
                    }
                }
                else
                {
                    result = "Lỗi tạo hóa đơn: " + response.ToString();
                }

                httpClient.Dispose();
            }
            catch (Exception ex)
            {
                result = "Lỗi tạo hóa đơn: " + ex.Message;
            }

            return result;
        }

        public static ResultApi<T> PostAsync<T, Y>(string baseUrl, string url, Y para, string token)
        {
            var result = new ResultApi<T>();

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseUrl);

                // Add an Accept header for JSON format.
                httpClient.DefaultRequestHeaders.Accept.Clear();

                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                StringContent content = null;
                if (para != null)
                {
                    string jsonData = JsonConvert.SerializeObject(para);
                    content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(jsonResponse))
                    {
                        try
                        {
                            result.Result = JsonConvert.DeserializeObject<T>(jsonResponse);
                        }
                        catch (Exception ex)
                        {
                            result.Message = ex.Message;
                        }
                    }
                }
                else
                {
                    result.Message = "Not found";
                }

                httpClient.Dispose();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }

    public class ResultApi<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }

    public class InvoiceResult
    {
        public string errorCode { get; set; }
        public string description { get; set; }
        public InvoiceResultDetail result { get; set; }
    }

    public class InvoiceResultDetail
    {
        public string supplierTaxCode { get; set; }
        public string invoiceNo { get; set; }
        public string transactionID { get; set; }
        public string reservationCode { get; set; }
    }
}

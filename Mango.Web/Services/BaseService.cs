using System.Net;
using System.Text;
using Mango.Common.Dtos;
using Mango.Common.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Mango.Web.Services;

internal class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        var httpClient = _httpClientFactory.CreateClient("CouponAPI");
        HttpRequestMessage request = new HttpRequestMessage();
        request.Headers.Add("Accept","application/json");
        request.RequestUri = new Uri(requestDto.Url);
        switch (requestDto.ApiType)
        {
            case ApiType.DELETE:
                request.Method = HttpMethod.Delete;
                break;
            case ApiType.POST:
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                break;
            case ApiType.PUT:
                request.Method = HttpMethod.Put;
                request.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                break;
            default:
                request.Method = HttpMethod.Get;
                break;
        }

        var httpResponseMessage = await httpClient.SendAsync(request);

        ResponseDto? response = null;
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Forbidden:
                response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Forbidden."
                };
                break;
            case HttpStatusCode.InternalServerError:
                response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Internal Server Error."
                };
                break;
            case HttpStatusCode.Unauthorized:
                response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Unauthorized."
                };
                break;
            case HttpStatusCode.NotFound:
                response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Not Found."
                };
                break;
            case HttpStatusCode.OK:
                response = JsonConvert.DeserializeObject<ResponseDto>(await httpResponseMessage.Content.ReadAsStringAsync());
                break;
        }
        return response;
    }
}
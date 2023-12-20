using Mango.Common.Dtos;
using Newtonsoft.Json;

namespace Mango.Web.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using 禁漫天堂.Models;
namespace 禁漫天堂.Controllers
{
    public class ProxyController : ApiController
    {
        // GET: api/getRandomImage
        [HttpGet]
        [Route("api/proxy/getImage")]
        public async Task<IHttpActionResult> GetRandomImage()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 获取图像的二进制数据
                    var response = await client.GetAsync("https://www.dmoe.cc/random.php");
                    if (!response.IsSuccessStatusCode)
                        return BadRequest("目标服务器返回错误");

                    // 读取返回的二进制数据
                    var imageData = await response.Content.ReadAsByteArrayAsync();

                    // 将二进制数据转换为 Base64 字符串
                    var base64Image = Convert.ToBase64String(imageData);

                    // 返回 Base64 编码的图像数据
                    return Ok(new { imageUrl = "data:image/jpeg;base64," + base64Image });
                }
                catch (HttpRequestException ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

    }
}

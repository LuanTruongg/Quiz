﻿using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Quiz.UI.ServicesClient
{
    public class BaseApiClient
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //protected BaseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _configuration = configuration;
        //    _httpContextAccessor = httpContextAccessor;
        //}
        //protected async Task<TResponse> GetAsync<TResponse>(string url)
        //{
        //    var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSetting.Token);
        //    var client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri(_configuration[SystemConstant.AppSetting.BaseAddress]);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        //    var response = await client.GetAsync(url);
        //    var body = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
        //        return myDeserializedObjList;
        //    }
        //    return JsonConvert.DeserializeObject<TResponse>(body);
        //}
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Hubs;

public class ChatHub(IClientConnectionService client, IRedisService redisService, ILogger<ChatHub> logger) : Hub
{
    private readonly IClientConnectionService _client = client;
    private readonly ILogger<ChatHub> _logger = logger;
    private readonly IRedisService _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));

    public async Task SendMessageToUser(ChatDataDTO request, string email)
    {
        var connectionId = await _client.GetConnectionId(email);
        string json = System.Text.Json.JsonSerializer.Serialize(request);

        await _redisService.SetValueAsync(email, json);
        await this.Clients.Client(connectionId).SendAsync("ReceiveMessage", request);
    }

    
    public override async Task OnConnectedAsync()
    {
        //параметр запроса у клиента
        var email = Context.GetHttpContext().Request.Query["email"].ToString();
        //параметр запроса психотерапевта
        var clientEmail = Context.GetHttpContext().Request.Query["client-email"].ToString();

        var connection = Context.ConnectionId;
        if(!string.IsNullOrEmpty(email)) 
        {
            var result = await _client.GetConnectionId(email);

            if (result != null)
            {
                _client.UpdateConnection(email, connection);
            }
            else
            {
                var model = new ClientConnectionModel
                {
                    ConnectionId = connection,
                    ClientEmail = email
                };

                await _client.CreateAsync(model);
            }
        }
        

        var json = await _redisService.GetValueAsync(clientEmail ?? email);
        ChatDataDTO responseObject = new ChatDataDTO();

        if (!string.IsNullOrEmpty(json))
        {
            responseObject = System.Text.Json.JsonSerializer.Deserialize<ChatDataDTO>(json);
        }
        
        await Clients.Caller.SendAsync("ReceiveMessage", responseObject);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var email = await _client.GetEmailAsync(Context.ConnectionId);
        await _redisService.DeleteAsync(email);
        await _client.DeleteByConnectionId(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

}

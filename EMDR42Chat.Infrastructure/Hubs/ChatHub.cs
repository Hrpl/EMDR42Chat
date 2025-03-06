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

namespace Infrastructure.Hubs;

public class ChatHub(IClientConnectionService client) : Hub
{
    private readonly IClientConnectionService _client = client;

    public async Task SendMessageToUser(ChatRequest request, string email)
    {
        var connectionId = await _client.GetConnectionId(email);
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", request);
    }

    public override async Task OnConnectedAsync()
    {
        var email = Context.GetHttpContext().Request.Query["email"].ToString();

        var connection = Context.ConnectionId;

        var model = new ClientConnectionModel
        {
            ConnectionId = connection,
            ClientEmail = email
        };

        await _client.CreateAsync(model);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var result = await _client.DeleteByConnectionId(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Hubs;

public class ChatHub(IClientConnectionService client, IRedisService redisService, ITherapeftClientsService therapeftClientsService, ILogger<ChatHub> logger) : Hub
{
    private readonly IClientConnectionService _client = client;
    private readonly ITherapeftClientsService _therapeftClientsService = therapeftClientsService ?? throw new ArgumentNullException(nameof(therapeftClientsService));
    private readonly ILogger<ChatHub> _logger = logger;
    private readonly IRedisService _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));

    public async Task SendMessageToUser(ChatDataDTO request, string email)
    {
        try
        {

            var connectionId = await _client.GetConnectionId(email);
            string json = System.Text.Json.JsonSerializer.Serialize(request);

            _logger.LogInformation($"Id соединения: {connectionId}, сообщение {json}");

            await _redisService.SetValueAsync(email, json);
            await this.Clients.Client(connectionId).SendAsync("ReceiveMessage", request);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Произошла ошибка: {ex.Message}");
        }
    }

    public async Task SendEmotion(MorphCastDTO? request)
    {
        try
        {
            var connection = Context.ConnectionId;

            var email = await _client.GetEmailAsync(connection);

            var therapeftEmail = await _therapeftClientsService.Get(email);

            var connectiondTherapeft = await _client.GetConnectionId(therapeftEmail);
            _logger.LogError($"Номер соединения терапевта: {connectiondTherapeft}");
            await this.Clients.Client(connectiondTherapeft).SendAsync("ReceiveEmotion", request);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Произошла ошибка: {ex.Message}");
        }
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            //параметр запроса у клиента
            var email = Context.GetHttpContext().Request.Query["email"].ToString();
            //параметр запроса психотерапевта
            var clientEmail = Context.GetHttpContext().Request.Query["client-email"].ToString();
            var specialistEmail = Context.GetHttpContext().Request.Query["specialist-email"].ToString();

            var connection = Context.ConnectionId;
            string savedEmail = "";

            if (string.IsNullOrEmpty(email)) savedEmail = specialistEmail;
            else savedEmail = email;

            var result = await _client.GetConnectionId(savedEmail);

            if (result != null)
            {
                _client.UpdateConnection(savedEmail, connection);
            }
            else
            {
                if (!string.IsNullOrEmpty(savedEmail))
                {
                    var model = new ClientConnectionModel
                    {
                        ConnectionId = connection,
                        ClientEmail = savedEmail
                    };

                    await _client.CreateAsync(model);
                }
            }

            if (!string.IsNullOrEmpty(specialistEmail) && !string.IsNullOrEmpty(clientEmail))
            {
                var model = new TherapeftClientsModel
                {
                    ClientEmail = clientEmail,
                    TherapeftEmail = specialistEmail
                };

                await _therapeftClientsService.Create(model);
            }

            var json = await _redisService.GetValueAsync(clientEmail ?? email);
            ChatDataDTO responseObject = new ChatDataDTO();

            if (!string.IsNullOrEmpty(json))
            {
                responseObject = System.Text.Json.JsonSerializer.Deserialize<ChatDataDTO>(json);
            }

            await Clients.Caller.SendAsync("InitialData", responseObject);

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Произошла ошибка: {ex.Message}");
        }
        
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {

            var email = await _client.GetEmailAsync(Context.ConnectionId);
            if (email != null)
            {
                await _client.DeleteByConnectionId(Context.ConnectionId);
                await _redisService.DeleteAsync(email);

                _therapeftClientsService.Delete(email);
            }

            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Произошла ошибка: {ex.Message}");
        }
    }

}

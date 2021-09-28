﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace FossiumBot.Commands
{
    public class Owner : BaseCommandModule
    {
        [Command("shutdown"), Aliases("shutdownbot"), RequireOwner]
        public async Task ShutdownCommand(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = $"The bot is now shut down",
                Color = new DiscordColor(0x2ECC70)
            };
            await ctx.RespondAsync(embed);
            await ctx.Client.DisconnectAsync();
            Environment.Exit(1);
        }
        [Command("servers"), RequireOwner]
        public async Task ServersCommand(CommandContext ctx)
        {
            List<string> guildslist = new List<string>();

            foreach (DiscordGuild guild in ctx.Client.Guilds.Values)
            {
                guildslist.Add(guild.ToString());
            }
            string[] guildsarray = guildslist.ToArray();
            string guildsstring = String.Join(" ", guildsarray);
            string connectedguilds = guildsstring.Replace(" Guild ", "\nID: ").Replace("Guild ", "ID: ").Replace("; ", " Name: ");

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Connected on {ctx.Client.Guilds.Count} servers",
                Description = connectedguilds,
                Color = new DiscordColor(0x0080FF)
            };
            await ctx.RespondAsync(embed);
        }

        [Command("leaveserver"), RequireOwner]
        public async Task LeaveserverCommand(CommandContext ctx, ulong server)
        {
            DiscordGuild guild = await ctx.Client.GetGuildAsync(server);

            var embed = new DiscordEmbedBuilder
            {
                Title = $"Left {guild.Name}",
                Description = $"ID: {guild.Id}",
                Color = new DiscordColor(0x2ECC70)
            };
            await guild.LeaveAsync();
            await ctx.RespondAsync(embed);
        }
    }
}
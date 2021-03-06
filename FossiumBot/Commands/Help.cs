// Copyright (c) 2021 Fossium-Team
// See LICENSE in the project root for license information.

﻿//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DSharpPlus;
//using DSharpPlus.CommandsNext;
//using DSharpPlus.SlashCommands;
//using DSharpPlus.CommandsNext.Attributes;
//using DSharpPlus.Entities;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using VideoLibrary;
//using System.Text.RegularExpressions;

//namespace FossiumBot.Commands
//{
//    public class Help : BaseCommandModule
//    {
//        [Group("help")]
//        public class SettingsGroup : BaseCommandModule
//        {
//            [GroupCommand]
//            public async Task HelpCommand(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Help",
//                    Description = $"Use `{ctx.Prefix}help <command>` for extended information on a command\nClick on one of the buttons to see help for that group",
//                    Color = new DiscordColor(0x0080FF)
//                };

//                var builder = new DiscordMessageBuilder()
//                    .AddEmbed(embed)
//                    .AddComponents(new DiscordComponent[]
//                    {
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_moderation", "Moderation", false),
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_utils", "Utils", false),
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_fun", "Fun", false),
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_music", "Music", false),
//                    })
//                    .AddComponents(new DiscordComponent[]
//                    {
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_owner", "Owner", false),
//                        new DiscordButtonComponent(ButtonStyle.Primary, "help_settings", "Settings", false)
//                    });
//                await ctx.RespondAsync(builder);
//            }

//            // Moderation
//            [Command("autodelete")]
//            public async Task HelpAutodelete(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Autodelete",
//                    Description = $"{ctx.Prefix}autodelete <mention channel> <time>\nAutomatically delete messages in a channel",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("ban")]
//            public async Task HelpBan(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Ban",
//                    Description = $"{ctx.Prefix}ban <user>\nBan a user",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("delwarn"), Aliases("dewarn", "rmwarn", "removewarn")]
//            public async Task HelpDelwarn(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Delwarn",
//                    Description = $"{ctx.Prefix}delwarn <user> <caseid or all>\nRemove warnings from a user\nAliases: dewarn, rmwarn, removewarn",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("kick")]
//            public async Task HelpKick(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Delwarn",
//                    Description = $"{ctx.Prefix}kick <user>\nKick a user",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("mute")]
//            public async Task HelpMute(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Mute",
//                    Description = $"{ctx.Prefix}mute <user> [time in minutes]\nMute a user\n`[time in minutes]` is optional, default value is 15",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("purge")]
//            public async Task HelpPurge(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Purge",
//                    Description = $"{ctx.Prefix}purge <amount of messages>\nPurge a certain amount of messages",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("softban")]
//            public async Task HelpSoftban(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Softban",
//                    Description = $"{ctx.Prefix}softban <user>\nBan and unban a user to delete all their messages",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("unban")]
//            public async Task HelpUnban(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Unban",
//                    Description = $"{ctx.Prefix}unban <user>\nUnban a user",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("unmute")]
//            public async Task HelpUnmute(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Unmute",
//                    Description = $"{ctx.Prefix}unmute <user>\nUnmute a user",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("warn")]
//            public async Task HelpWarn(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Warn",
//                    Description = $"{ctx.Prefix}warn <user> [reason]\nWarn a user\n`[reason]` is optional",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }

//            [Command("warns"), Aliases("warnings")]
//            public async Task HelpWarns(CommandContext ctx)
//            {
//                var embed = new DiscordEmbedBuilder
//                {
//                    Title = $"Moderation: Warns",
//                    Description = $"{ctx.Prefix}warns <user>\nSee all the warnings of a user\nAliases: warnings",
//                    Color = new DiscordColor(0x0080FF)
//                };
//                await ctx.RespondAsync(embed);
//            }
//        }
//    }
//}

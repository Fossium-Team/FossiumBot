// Copyright (c) 2021 Fossium-Team
// See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FossiumBot.Commands
{
    public class Fun : ApplicationCommandModule
    {
        [SlashCommand("rate", "Rate something out of 10")]
        public async Task RateCommand(InteractionContext ctx, [Option("thing", "Thing to rate")] string thing)
        {
            Random r = new Random();
            int randomnum = r.Next(0, 10);
            var embed = new DiscordEmbedBuilder
            {
                Title = $"I rate `{thing}` a {randomnum}/10",
                Color = new DiscordColor(0x0080FF)
            };
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
        }

        [SlashCommand("cat", "Shows a random picture of a cat")]
        public async Task CatCommand(InteractionContext ctx)
        {
            HttpResponseMessage response;
            string content = String.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FossiumBot", Program.localversion));
                response = await client.GetAsync("https://api.thecatapi.com/v1/images/search");
                content = await response.Content.ReadAsStringAsync();
            }
            if (response.IsSuccessStatusCode)
            {
                JArray jsonData = JArray.Parse(content);
                var caturl = jsonData[0]["url"];
                string catpic = (string)caturl;
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Cat Picture",
                    ImageUrl = catpic,
                    Color = new DiscordColor(0x0080FF)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = "Cannot contact The Cat Api",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
        }

        [SlashCommand("dog", "Shows a random picture of a dog")]
        public async Task DogCommand(InteractionContext ctx)
        {
            HttpResponseMessage response;
            string content = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FossiumBot", Program.localversion));
                response = await client.GetAsync("https://api.thedogapi.com/v1/images/search");
                content = await response.Content.ReadAsStringAsync();
            }
            if (response.IsSuccessStatusCode)
            {
                JArray jsonData = JArray.Parse(content);
                var dogurl = jsonData[0]["url"];
                string dogpic = (string)dogurl;
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Dog Picture",
                    ImageUrl = dogpic,
                    Color = new DiscordColor(0x0080FF)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = "Cannot contact The Dog Api",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
        }

        [SlashCommand("wikipedia", "Get information about something from Wikipedia")]
        public async Task WikiCommand(InteractionContext ctx, [Option("query", "What you want to get information of")] string query)
        {
            HttpResponseMessage response;
            string content = String.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FossiumBot", Program.localversion));
                response = await client.GetAsync($"https://en.wikipedia.org/w/api.php?action=query&prop=extracts&exintro&explaintext&origin=*&format=json&generator=search&gsrnamespace=0&gsrlimit=1&gsrsearch={query}");
                content = await response.Content.ReadAsStringAsync();
            }
            JObject jsonData = JObject.Parse(content);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string pageID = ((JProperty)jsonData["query"]["pages"].First()).Name;
                    if (pageID == "-1")
                    {
                        var errEmbed = new DiscordEmbedBuilder
                        {
                            Title = "Oops...",
                            Description = "The page you've requested might not exist",
                            Color = new DiscordColor(0xFF0000)
                        };
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(errEmbed));
                        return;
                    }
                    else
                    {
                        string pageTitle = (string)jsonData["query"]["pages"][pageID]["title"];
                        string extract = (string)jsonData["query"]["pages"][pageID]["extract"];
                        if (extract.Length >= 260)
                        {
                            string brief = extract.Substring(0, 260);
                            var embed = new DiscordEmbedBuilder
                            {
                                Title = $"{pageTitle}",
                                Description = $"{brief}...",
                                Color = new DiscordColor(0x0080FF)
                            };
                            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                        }
                        else
                        {
                            var embed = new DiscordEmbedBuilder
                            {
                                Title = $"{pageTitle}",
                                Description = $"{extract}",
                                Color = new DiscordColor(0x0080FF)
                            };
                            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    var errEmbed = new DiscordEmbedBuilder
                    {
                        Title = "Oops...",
                        Description = $"```{ex}```",
                        Color = new DiscordColor(0xFF0000)
                    };
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(errEmbed));
                    return;
                }
            }
            else if (((int)response.StatusCode) == 404)
            {
                var notFound = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = "Page not found",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(notFound));
            }
            else
            {
                var errEmbed = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = "Something went wrong...",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(errEmbed));
            }
        }

        [SlashCommand("github", "Get information about a GitHub repository")]
        public async Task GithubCommand(InteractionContext ctx, [Option("repository", "Which repo do you want to get information of `owner/repo`?")] string repository)
        {
            HttpResponseMessage response;
            string responseBody;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("FossiumBot", Program.localversion));
                response = await client.GetAsync($"https://api.github.com/repos/{repository}/commits");
                responseBody = await response.Content.ReadAsStringAsync();
            }
            if (response.IsSuccessStatusCode)
            {
                JArray responseData = JArray.Parse(responseBody);
                string lasCommitSHA = (string)responseData[0]["sha"];
                string lasCommitURL = (string)responseData[0]["html_url"];
                string committer = (string)responseData[0]["commit"]["committer"]["name"];
                string commitMessage = (string)responseData[0]["commit"]["message"];
                string commitAuthor = (string)responseData[0]["commit"]["author"]["name"];
                string committerAvatar = (string)responseData[0]["committer"]["avatar_url"];
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"**{repository}**",
                    Description = $"\n\n**Last commit:**\n{lasCommitSHA}\n\n**Commit Author:**\n`{commitAuthor}`\n**Committer:**\n`{committer}`\n\n**Link:**\n{lasCommitURL}\n\n**Commit Message:** ```{commitMessage}```\n\nCommit Time: `{responseData[0]["commit"]["committer"]["date"]}`",
                    //Description = $"Testing result: {commitDate}",
                    Color = new DiscordColor(0x0080FF)
                };
                embed.WithThumbnail(committerAvatar);
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
            }
            else if (((int)response.StatusCode) == 404)
            {
                var notFound = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = "Page not found",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(notFound));
            }
            else
            {
                JObject responseData = JObject.Parse(responseBody);
                var message = responseData["message"];
                var error = new DiscordEmbedBuilder
                {
                    Title = "Oops...",
                    Description = $"{message}",
                    Color = new DiscordColor(0xFF0000)
                };
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(error));
            }
        }
    }
}

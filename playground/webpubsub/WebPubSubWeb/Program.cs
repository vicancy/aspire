using Azure.Messaging.WebPubSub;

using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.AddAzureWebPubSubHub("wps1", "chatHub");
builder.AddAzureWebPubSubHub("wps1", "notificationHub");

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// return the Client Access URL with negotiate endpoint
app.MapGet("/negotiate/chat", (IAzureClientFactory<WebPubSubServiceClient> clientFactory) =>
{
    var service = clientFactory.CreateClient("chatHub");
    return
        new
        {
            url = service.GetClientAccessUri(roles: ["webpubsub.sendToGroup.group1", "webpubsub.joinLeaveGroup.group1"]).AbsoluteUri
        };
});
app.MapGet("/negotiate/notification", (IAzureClientFactory<WebPubSubServiceClient> clientFactory) =>
{
    var service = clientFactory.CreateClient("notificationHub");
    return
        new
        {
            url = service.GetClientAccessUri(roles: ["webpubsub.sendToGroup.group1", "webpubsub.joinLeaveGroup.group1"]).AbsoluteUri
        };
});
app.Run();

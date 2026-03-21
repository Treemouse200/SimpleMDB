namespace Smdb.Api.ActorMovies;

using Smdb.Core.ActorMovies;
using System.Net;
using Shared.Http;
using System.Collections;
using System.Collections.Specialized;
using System.Text.Json;

public class ActorMoviesController
{
    private IActorMovieService service;

    public ActorMoviesController(IActorMovieService service)
    {
        this.service = service;
    }

    public async Task ReadActorMovies(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 5;

        var result = await service.ReadActorMovies(page, size);
        await JsonUtils.SendPagedResultResponse(req, res, props, result, page, size);
        await next();
    }

    public async Task CreateActorMovie(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var text = (string)props["req.text"]!;
        var data = JsonSerializer.Deserialize<ActorMovie>(text, JsonSerializerOptions.Web);

        if (data == null)
        {
            await JsonUtils.SendResultResponse(req, res, props,
                new Result<ActorMovie>(new Exception("Invalid JSON"), 400));
            return;
        }

        var result = await service.CreateActorMovie(data);

        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task ReadActorMovie(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;

        if (!int.TryParse(p["id"], out int id))
        {
            await JsonUtils.SendResultResponse(req, res, props,
                new Result<ActorMovie>(new Exception("Invalid id"), 400));
            return;
        }

        var result = await service.ReadActorMovie(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task UpdateActorMovie(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;

        if (!int.TryParse(p["id"], out int id))
        {
            await JsonUtils.SendResultResponse(req, res, props,
                new Result<ActorMovie>(new Exception("Invalid id"), 400));
            return;
        }

        var text = (string)props["req.text"]!;
        var data = JsonSerializer.Deserialize<ActorMovie>(text, JsonSerializerOptions.Web);

        if (data == null)
        {
            await JsonUtils.SendResultResponse(req, res, props,
                new Result<ActorMovie>(new Exception("Invalid JSON"), 400));
            return;
        }

        var result = await service.UpdateActorMovie(id, data);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }

    public async Task DeleteActorMovie(HttpListenerRequest req, HttpListenerResponse res,
        Hashtable props, Func<Task> next)
    {
        var p = (NameValueCollection)props["req.params"]!;

        if (!int.TryParse(p["id"], out int id))
        {
            await JsonUtils.SendResultResponse(req, res, props,
                new Result<ActorMovie>(new Exception("Invalid id"), 400));
            return;
        }

        var result = await service.DeleteActorMovie(id);
        await JsonUtils.SendResultResponse(req, res, props, result);
        await next();
    }
}
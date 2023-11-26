using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Labels.Commands;
using Notes.Application.Labels.Queries;

namespace Notes.Presentation.Endpoints;

public static class LabelEndpoints
{
    public static void MapLabelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/labels");

        group.MapPost("", async (CreateLabelCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        group.MapGet("{labelId:Guid}", async (Guid labelId, ISender sender) =>
        {
            var query = new GetLabelQuery(labelId);
            LabelResponse label = await sender.Send(query);

            return Results.Ok(label);
        });

        group.MapGet("", async (ISender sender) =>
        {
            return await sender.Send(new GetLabelsQuery());
        });
    }
}

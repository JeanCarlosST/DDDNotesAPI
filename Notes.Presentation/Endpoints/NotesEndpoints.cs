using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Notes.Commands;
using Notes.Application.Notes.Queries;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Notes.Presentation.Endpoints;

public static class NotesEndpoints
{
    public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/notes");

        group.MapPost("", async (CreateNoteCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        group.MapGet("/{noteId:Guid}", async (Guid noteId, ISender sender) =>
        {
            var query = new GetNoteQuery(noteId);
            NoteReponse? note = await sender.Send(query);

            if (note != null)
            {
                return Results.Ok(note);
            }

            return Results.NotFound();
        });
    }
}

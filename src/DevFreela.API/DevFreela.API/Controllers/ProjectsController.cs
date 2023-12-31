﻿using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.ChangeStatus;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.CreateProject;
using DevFreela.Application.UseCases.Project.DeleteProject;
using DevFreela.Application.UseCases.Project.GetProject;
using DevFreela.Application.UseCases.Project.UpdateProject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
        => _mediator = mediator;


    [HttpPost]
    [Authorize(Roles = "client")]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProjectInput inputModel)
    {
        var output = await _mediator.Send(inputModel);

        return CreatedAtAction(nameof(Create), new { id = output }, output);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<ProjectModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var project = await _mediator.Send(new GetProjectInput(id));
        var response = new ApiResponse<ProjectModelOutput>(project);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteProjectInput(id));
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectInput inputModel)
    {
        await _mediator.Send(inputModel);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeStatusInputModel inputModel)
    {
        await _mediator.Send(inputModel);
        return NoContent();
    }


}

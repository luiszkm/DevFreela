﻿
using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.GetProject;
public class GetProject : IGetProject
{
    private readonly IProjectRepository _projectRepository;

    public GetProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectModelOutput> Handle(GetProjectInput request, CancellationToken cancellationToken)
    {
        var project =
             await _projectRepository.GetById(request.Id, cancellationToken);
        if (project == null) throw new NotFoundException();


        return ProjectModelOutput.FromProject(project);
    }
}

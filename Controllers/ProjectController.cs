using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using be_company.Data;
using be_company.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be_company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        // PUT: api/projects/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateProject(int id, Project project)
{
    if (id != project.ProjectId)
    {
        return BadRequest();
    }

    // Find the existing project (without including ProjectDetails)
    var existingProject = await _context.Projects
        .FirstOrDefaultAsync(p => p.ProjectId == id);

    if (existingProject == null)
    {
        return NotFound();
    }

    // Update the project fields
    existingProject.ProjectName = project.ProjectName;
    existingProject.StartDate = project.StartDate;
    existingProject.EndDate = project.EndDate;
    existingProject.Status = project.Status;
    existingProject.ManagerID = project.ManagerID;

    // Manually update the ProjectDetails if project.ProjectDetails is not null
    if (project.ProjectDetails != null)
    {
        var existingProjectDetail = await _context.ProjectDetails
            .Where(pd => pd.ProjectId == id) // Connect using ProjectId
            .FirstOrDefaultAsync();

        if (existingProjectDetail != null)
        {
            // Update the existing ProjectDetail
            existingProjectDetail.DetailedDescription = project.ProjectDetails.DetailedDescription;
            existingProjectDetail.EstimatedBudget = project.ProjectDetails.EstimatedBudget;
            existingProjectDetail.ActualBudget = project.ProjectDetails.ActualBudget;
            existingProjectDetail.SRS = project.ProjectDetails.SRS;
            existingProjectDetail.ClientID = project.ProjectDetails.ClientID;

            // Mark the ProjectDetail entity as modified
            _context.Entry(existingProjectDetail).State = EntityState.Modified;
        }
        else
        {
            // If ProjectDetails doesn't exist, create a new one
            var newProjectDetail = new ProjectDetail
            {
                DetailedDescription = project.ProjectDetails.DetailedDescription,
                EstimatedBudget = project.ProjectDetails.EstimatedBudget,
                ActualBudget = project.ProjectDetails.ActualBudget,
                SRS = project.ProjectDetails.SRS,
                ClientID = project.ProjectDetails.ClientID,
                ProjectId = id // Associate with the correct project
            };

            // Add the new ProjectDetail
            _context.ProjectDetails.Add(newProjectDetail);
        }
    }

    // Mark the project entity as modified
    _context.Entry(existingProject).State = EntityState.Modified;

    try
    {
        // Save changes to the database
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ProjectExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}

        // DELETE: api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}

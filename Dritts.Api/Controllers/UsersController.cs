using Dritts.Api.Contracts.Requests;
using Dritts.Api.Repositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dritts.Api.Controllers
{
    /// <summary>
    /// Controller for handling User related actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <response code="200">Returns the list of users.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>The user with the given ID.</returns>
        /// <response code="200">Returns the user with the given ID.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid Id)
        {
            var user = await _userRepository.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A newly created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="409">If the user already exists.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser user)
        {
            if (await _userRepository.GetUserByFullName(user.FirstName, user.MiddleName, user.LastName))
            {
                return Conflict("User already exists");
            }

            var userId = await _userRepository.CreateUser(user);

            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = userId }, userId);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="Id">The ID of the user to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the user is successfully deleted.</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid Id)
        {
            await _userRepository.DeleteUser(Id);
            return NoContent();
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            // Return the Index view with the user list
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            // Return the Details view with the user data
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Return the Create view
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (user == null)
            {
                return BadRequest(); // Return a 400 Bad Request if the user is null
            }

            try
            {
                // Add the user to the userlist
                userlist.Add(user);

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Return the Create view with the current user data in case of an error
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            return View(user); // Return the Edit view with the user data
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            try
            {
                // Update the user's information
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                // Redirect to the Index action after successful update
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Return the Edit view with the current user data in case of an error
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return a 404 if the user is not found
            }

            // Return the Delete view with the user data
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Find the user by ID
                var user = userlist.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(); // Return a 404 if the user is not found
                }

                // Remove the user from the list
                userlist.Remove(user);

                // Redirect to the Index action after successful deletion
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Return the Delete view in case of an error
                return View();
            }
        }

        // GET: User/Search
        public ActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View("Index", userlist); // Si no se proporciona un nombre, retorna la lista completa
            }

            // Filtra los usuarios cuyo nombre contiene el texto proporcionado (ignorando mayúsculas/minúsculas)
            var filteredUsers = userlist.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            // Retorna la vista Index con los usuarios filtrados
            return View("Index", filteredUsers);
        }
}
